using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;

namespace MyFirstProject.BW
{
    public class Cls_BW_MovRotAtt_DrawJig : DrawJig
    {
        private enum JigType
        {
            Move = 0,
            Rotate = 1,
        }

        private Document _dwg;
        private Autodesk.AutoCAD.DatabaseServices.Database _db;
        private Editor _ed;
        private ObjectId _attRefId = ObjectId.Null;
        private Point3d _attPosition;
        private JigType _jigType = JigType.Move;

        private AttributeReference _visualAtt;
        private Point3d _currentPoint;
        private Point3d _prevPoint;

        private double _currentAngle;
        private double _prevAngle;


        public Cls_BW_MovRotAtt_DrawJig(Document dwg, ObjectId attRefId, Point3d attPosition)
        {
            _dwg = dwg;
            _db = dwg.Database;
            _ed = dwg.Editor;

            _attRefId = attRefId;
            _attPosition = attPosition;
        }

        public Cls_BW_MovRotAtt_DrawJig(Document dwg)
        {
            _dwg = dwg;
            _db = dwg.Database;
            _ed = dwg.Editor;
        }


        #region public methods

        public static void MoveRotAtt(bool pickAttOnScreen = true, object attRefId = null, object attPosition = null)
        {
            Document dwg = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            try
            {
                if (pickAttOnScreen)
                {
                    BW.Cls_BW_MovRotAtt_DrawJig attJig = new BW.Cls_BW_MovRotAtt_DrawJig(dwg);

                    if (!attJig.MoveRotateAttribute())
                    {
                        ed.WriteMessage("\n*Cancel*");
                    }
                }
                else
                {
                    BW.Cls_BW_MovRotAtt_DrawJig attJig = new BW.Cls_BW_MovRotAtt_DrawJig(dwg, (ObjectId)attRefId, (Point3d)attPosition);
                    if (!attJig.MoveAttribute()) // origanally made for target
                    {
                        ed.WriteMessage("\n*Cancel*");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nError: {0}", ex.Message);
            }

            Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
        }
   
        #endregion


        #region DrawJig Overrides

        protected override bool WorldDraw(WorldDraw draw)
        {
            draw.Geometry.Draw(_visualAtt);
            return true;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            if (_jigType == JigType.Move)
            {
                JigPromptPointOptions opt = new JigPromptPointOptions("\nPick point to move attribute:");
                opt.UseBasePoint = true;
                opt.BasePoint = _attPosition;
                opt.Cursor = CursorType.RubberBand;
                PromptPointResult res = prompts.AcquirePoint(opt);

                if (res.Status == PromptStatus.OK)
                {
                    _currentPoint = res.Value;
                    if (_currentPoint == _prevPoint)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        Matrix3d mt = Matrix3d.Displacement(_prevPoint.GetVectorTo(_currentPoint));
                        _visualAtt.TransformBy(mt);

                        _prevPoint = _currentPoint;
                        return SamplerStatus.OK;
                    }
                }
                else
                {
                    return SamplerStatus.Cancel;
                }
            }

            if (_jigType == JigType.Rotate)
            {
                JigPromptAngleOptions opt = new JigPromptAngleOptions("\nEnter or pick rotation angle:");
                opt.UseBasePoint = true;
                opt.BasePoint = _attPosition;
                opt.Cursor = CursorType.RubberBand;
                PromptDoubleResult res = prompts.AcquireAngle(opt);

                if (res.Status == PromptStatus.OK)
                {
                    _currentAngle = res.Value;
                    if (_currentAngle == _prevAngle)
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        //Matrix3d mt = Matrix3d.Rotation(_currentAngle, Vector3d.ZAxis, _attPosition);
                        //_visualAtt.TransformBy(mt);

                        _visualAtt.Rotation = _currentAngle;

                        _prevAngle = _currentAngle;
                        return SamplerStatus.OK;
                    }
                }
                else
                {
                    return SamplerStatus.Cancel;
                }
            }

            return SamplerStatus.OK;
        }

        #endregion


        #region private methods


        private bool MoveAttribute()
        {                 
            try
            {
                bool go = true;
                while (go)
                {                       
                    HighlightAttribute(true);      

                    //Create Visual Attribute
                    CreateVisualAttribute();

                    _currentPoint = _attPosition;
                    _prevPoint = _attPosition;
                    _currentAngle = 0.0;
                    _prevAngle = 0.0;

                    //Drag visual attribute
                    PromptResult jigresult = _ed.Drag(this);

                    //Update the selected attribute
                    //if the drag status returns OK
                    if (jigresult.Status == PromptStatus.OK)
                    {
                        if (_jigType == JigType.Move)
                        {
                            MoveAttribute(_currentPoint);
                            _ed.WriteMessage("\nSelected attribute has been moved.");

                            //Update attribute position
                            _attPosition = GetAttributePosition(_attRefId);

                            go = false;
                        }           
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                HighlightAttribute(false);
                if (_visualAtt != null) _visualAtt.Dispose();
            }

            return true;
        }

        private bool MoveRotateAttribute()
        {
            if (!PickAttribute()) return false;
            HighlightAttribute(true);

            try
            {
                bool go = true;
                while (go)
                {
                    bool repicked = false;

                    PromptKeywordOptions kOpt = new PromptKeywordOptions(
                        "\nPick attribute transform option:");
                    kOpt.Keywords.Add("Move");
                    kOpt.Keywords.Add("Rotate");
                    kOpt.Keywords.Add("Pick");
                    kOpt.Keywords.Add("eXit");
                    kOpt.Keywords.Default = "Move";
                    kOpt.AppendKeywordsToMessage = true;

                    PromptResult res = _ed.GetKeywords(kOpt);

                    if (res.Status == PromptStatus.OK)
                    {
                        switch (res.StringResult.ToUpper())
                        {
                            case "MOVE":
                                _jigType = JigType.Move;
                                break;
                            case "ROTATE":
                                _jigType = JigType.Rotate;
                                break;
                            case "PICK":
                                HighlightAttribute(false);
                                if (!PickAttribute()) return false;
                                HighlightAttribute(true);
                                repicked = true;
                                break;
                            default:
                                go = false;
                                break;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    if (repicked || !go) continue;

                    //Create Visual Attribute
                    CreateVisualAttribute();

                    _currentPoint = _attPosition;
                    _prevPoint = _attPosition;
                    _currentAngle = 0.0;
                    _prevAngle = 0.0;

                    //Drag visual attribute
                    PromptResult jigresult = _ed.Drag(this);

                    //Update the selected attribute
                    //if the drag status returns OK
                    if (jigresult.Status == PromptStatus.OK)
                    {
                        if (_jigType == JigType.Move)
                        {
                            MoveAttribute(_currentPoint);
                            _ed.WriteMessage(
                                "\nSelected attribute has been moved.");

                            //Update attribute position
                            _attPosition = GetAttributePosition(_attRefId);
                        }

                        if (_jigType == JigType.Rotate)
                        {
                            RotateAttribute(_currentAngle);
                            _ed.WriteMessage(
                                "\nSelected attribute has been rotated");
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                HighlightAttribute(false);
                if (_visualAtt != null) _visualAtt.Dispose();
            }

            return true;
        }

        private bool PickAttribute()
        {
            while (true)
            {
                PromptNestedEntityOptions opt = new
                    PromptNestedEntityOptions("\nPick an attribute:");
                opt.AllowNone = false;

                PromptNestedEntityResult res = _ed.GetNestedEntity(opt);
                if (res.Status == PromptStatus.OK)
                {
                    if (res.ObjectId.ObjectClass.DxfName.ToUpper() == "ATTRIB")
                    {
                        _attRefId = res.ObjectId;
                        _attPosition = GetAttributePosition(_attRefId);
                        return true;
                    }
                    else
                    {
                        _ed.WriteMessage("\nInvalid pick: not an attribute");
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        private Point3d GetAttributePosition(ObjectId id)
        {
            Point3d p = new Point3d();

            using (_dwg.LockDocument())
            using (Transaction tran =
                _db.TransactionManager.StartOpenCloseTransaction())
            {
                AttributeReference att = tran.GetObject(id, OpenMode.ForRead) as AttributeReference;
                p = att.Position;
                tran.Commit();
            }

            return p;
        }

        private void HighlightAttribute(bool highlight)
        {
            using (_dwg.LockDocument())
            using (Transaction tran =
                _db.TransactionManager.StartOpenCloseTransaction())
            {
                Entity ent = tran.GetObject(_attRefId, OpenMode.ForWrite) as Entity;
                if (highlight)
                    ent.Highlight();
                else
                    ent.Unhighlight();

                tran.Commit();
            }
        }

        private void CreateVisualAttribute()
        {
            if (_visualAtt != null) _visualAtt.Dispose();
            _visualAtt = null;

            using (_dwg.LockDocument())
            using (Transaction tran =
                _db.TransactionManager.StartOpenCloseTransaction())
            {
                AttributeReference att =
                    (AttributeReference)tran.GetObject(
                    _attRefId, OpenMode.ForRead);
                _visualAtt = att.Clone() as AttributeReference;
                _visualAtt.SetDatabaseDefaults(_db);

                tran.Commit();
            }
        }

        private void MoveAttribute(Point3d toPoint)
        {
            using (_dwg.LockDocument())
            using (Transaction tran =
                _db.TransactionManager.StartOpenCloseTransaction())
            {
                Entity ent = (Entity)tran.GetObject(_attRefId, OpenMode.ForWrite);

                Matrix3d mt = Matrix3d.Displacement(_attPosition.GetVectorTo(toPoint));
                ent.TransformBy(mt);

                tran.Commit();
            }
        }

        private void RotateAttribute(double angle)
        {
            using (_dwg.LockDocument())
            using (Transaction tran =
                _db.TransactionManager.StartOpenCloseTransaction())
            {
          //      Entity ent = (Entity)tran.GetObject(_attRefId, OpenMode.ForWrite);

                AttributeReference ent = (AttributeReference)tran.GetObject(_attRefId, OpenMode.ForWrite);

                //Matrix3d mt = Matrix3d.Rotation(angle, Vector3d.ZAxis, _attPosition);
                //ent.TransformBy(mt);

                ent.Rotation = angle;

                tran.Commit();
            }
        }

        #endregion
    }
}
