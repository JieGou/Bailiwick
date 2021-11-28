using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public class Cls_BW_DynamicBlockAlignmentJig : EntityJig
    {
        #region Fields
        
        const string annoScalesDict = "ACDB_ANNOTATIONSCALES";

        private static ObjectId _attRefId = ObjectId.Null;
        private static Point3d _attPosition;

        private static string _blockName = "";
        private static string _note = "";
        private static string _noteAttr = "";
        private static string _moveAttr = "";

        private static string _deptAttr = "";
        private static string _deptNote = "";


        private static AttachmentPoint _justifyAttr = AttachmentPoint.BaseCenter;

        private static MyMessageFilter _filter;
        private static bool _FlipBlock = false; 

        private int _CurJigFactorNumber = 1;

        private bool _IsDynamicAlignment = false;
        private double _AlignmentAngle = 0.0;
        private ObjectId _ObjectIdToAlignWith = ObjectId.Null;

        private double _AngleOffset = 0.0;

        private Point3d _Position = new Point3d(0, 0, 0); // Factor #1
        private double _Rotation = 0.0;                 // Factor #2
        private double _ScaleFactor = 1.0;              // Factor #3

        

        #endregion

        #region Constructors

        public Cls_BW_DynamicBlockAlignmentJig(BlockReference ent)
            : base(ent)
        {
            _AngleOffset = ent.Rotation;

            if (ent.IsDynamicBlock)
                _IsDynamicAlignment = true;
        }

        #endregion

        #region Properties

        protected static Editor CurEditor
        {
            get
            {
                return MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;
            }
        }

        protected static Matrix3d UCS
        {
            get
            {
                return CurEditor.CurrentUserCoordinateSystem;
            }
        }

        protected new BlockReference Entity
        {
            get
            {
                return (BlockReference)base.Entity;
            }
        }

        #endregion
                
        private void UpdAtts(BlockReference Entity)
        {         
            foreach (DynamicBlockReferenceProperty prop in Entity.DynamicBlockReferencePropertyCollection)
            {
                if (prop.PropertyName == "Flip state1")
                {
                    if (_FlipBlock == true)
                    {
                        prop.Value = (short)1;
                    }
                    else
                    {
                        prop.Value = (short)0;
                    }
                }
            }

            using (Transaction tr = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTbl = tr.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord acBlkTblRec = tr.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                foreach (ObjectId a in Entity.AttributeCollection)
                {
                    AttributeReference ar = (AttributeReference)tr.GetObject(a, OpenMode.ForWrite);

                    ar.Position = _attPos[ar.Tag];
                    Point3d pt = ar.Position.TransformBy(Entity.BlockTransform);
                    ar.Position = pt;

                    System.Diagnostics.Debug.WriteLine(pt.ToString());

                    if (_note != "")
                    {
                        if (ar.Tag == _noteAttr)
                        {
                            ar.TextString = _note;
                        }
                    }

                    if (_deptNote != "")
                    {
                        if (ar.Tag == _deptAttr)
                        {
                            ar.TextString = _deptNote;
                        }
                    }

                    if (ar.Tag == _moveAttr)// | ar.Tag == "LABEL2")
                    {
                        _attPosition = ar.Position = Entity.Position;
                        _attRefId = ar.ObjectId;
                        ar.Justify = _justifyAttr;
                   
                        if (_justifyAttr == AttachmentPoint.BaseCenter)
                        {                           
                            ar.AlignmentPoint = ar.Position;
                            ar.AdjustAlignment(HostApplicationServices.WorkingDatabase);
                        }
                        if (_justifyAttr == AttachmentPoint.BaseRight)
                        {
                            ar.AlignmentPoint = ar.Position;
                            ar.AdjustAlignment(HostApplicationServices.WorkingDatabase);
                        }

                    }


                    //if (ar.Tag == "LABEL2")
                    //{
                    //    _attPosition = ar.Position;
                    //    _attRefId = ar.ObjectId;           
                    //}

                }
                tr.Commit();
            }
        }


        #region Overrides


        protected override bool Update()
        {
            switch (_CurJigFactorNumber)
            {
                case 1:
                    Entity.Position = _Position.TransformBy(UCS);

                    UpdAtts(Entity);

                    if (_IsDynamicAlignment && !_ObjectIdToAlignWith.IsNull)
                    {
                        if (_ObjectIdToAlignWith.ObjectClass.IsDerivedFrom(RXClass.GetClass(typeof(Curve))))
                        {
                            Curve curve = (Curve)Entity.Database.TransactionManager.TopTransaction.GetObject(_ObjectIdToAlignWith, OpenMode.ForRead);
                            Vector3d dir = curve.GetFirstDerivative(curve.GetClosestPointTo(Entity.Position, false)).TransformBy(UCS.Inverse());
                            _AlignmentAngle = Vector3d.XAxis.GetAngleTo(dir, Vector3d.ZAxis);
                            Entity.Rotation = _AlignmentAngle + _AngleOffset;
                        }
                        else
                            _ObjectIdToAlignWith = ObjectId.Null;
                    }
                    break;
                case 2:
                    Entity.Rotation = _Rotation + _AngleOffset;
                    break;
                case 3:
                    Entity.ScaleFactors = new Scale3d(_ScaleFactor);
                    break;
                default:
                    break;
            }

            return true;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            switch (_CurJigFactorNumber)
            {
                case 1:
                    JigPromptPointOptions prOptions1 = new JigPromptPointOptions("\nBlock insertion point:");
                    PromptPointResult prResult1 = prompts.AcquirePoint(prOptions1);
                    if (prResult1.Status == PromptStatus.Cancel)
                        return SamplerStatus.Cancel;
       

                    Point3d tempPt = prResult1.Value.TransformBy(UCS.Inverse());
                    if (tempPt.IsEqualTo(_Position))
                    {
                        return SamplerStatus.NoChange;
                    }
                    else
                    {
                        _Position = tempPt;
                        _ObjectIdToAlignWith = pickObjId;
                        return SamplerStatus.OK;
                    }
                case 2:
                    //JigPromptAngleOptions prOptions2 = new JigPromptAngleOptions("\nBlock rotation angle:");
                    //prOptions2.BasePoint = mPosition.TransformBy(UCS);
                    //prOptions2.UseBasePoint = true;
                    //PromptDoubleResult prResult2 = prompts.AcquireAngle(prOptions2);
                    //if (prResult2.Status == PromptStatus.Cancel)
                    //    return SamplerStatus.Cancel;

                    //if (prResult2.Value.Equals(mRotation))
                    //{
                    //    return SamplerStatus.NoChange;
                    //}
                    //else
                    //{
                    //    mRotation = prResult2.Value;
                    //    return SamplerStatus.OK;
                    //}
                    return SamplerStatus.NoChange;
                case 3:
                    //JigPromptDistanceOptions prOptions3 = new JigPromptDistanceOptions("\nBlock scale factor:");
                    //prOptions3.BasePoint = mPosition.TransformBy(UCS);
                    //prOptions3.UseBasePoint = true;
                    //PromptDoubleResult prResult3 = prompts.AcquireDistance(prOptions3);
                    //if (prResult3.Status == PromptStatus.Cancel)
                    //    return SamplerStatus.Cancel;

                    //if (prResult3.Value.Equals(mScaleFactor))
                    //{
                    //    return SamplerStatus.NoChange;
                    //}
                    //else
                    //{
                    //    mScaleFactor = prResult3.Value;
                    //    return SamplerStatus.OK;
                    //}
                    return SamplerStatus.NoChange;
                default:
                    break;
            }
                 
            return SamplerStatus.OK;
        }

        #endregion

        #region Point Monitor


        private static ObjectId pickObjId = ObjectId.Null;
        private static void Editor_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            Point3d computedPt = e.Context.ComputedPoint;
            FullSubentityPath[] pickedEnts = e.Context.GetPickedEntities();
            
            if (pickedEnts != null && pickedEnts.Length > 0)
            {
                pickObjId = pickedEnts[0].GetObjectIds()[0];
                
                Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;

                using (Transaction tr = db.TransactionManager.StartOpenCloseTransaction())
                {
                    var bt = tr.GetObject(pickObjId, OpenMode.ForRead);
                    if (bt is BlockReference)
                    {
                        pickObjId = pickedEnts[0].GetObjectIds()[1];                        
                    }
                }

            }
            else
                pickObjId = ObjectId.Null;
        }

        #endregion

        #region Method to Call

        public static bool Jig(BlockReference ent)
        {
            try
            {
                Cls_BW_DynamicBlockAlignmentJig jigger = new Cls_BW_DynamicBlockAlignmentJig(ent);
                PromptResult pr;
                do
                {
                    pr = CurEditor.Drag(jigger);
                    if (jigger._CurJigFactorNumber == 1 && jigger._IsDynamicAlignment && !jigger._ObjectIdToAlignWith.IsNull)
                        jigger._CurJigFactorNumber++;
                } while (pr.Status != PromptStatus.Cancel && pr.Status != PromptStatus.Error && jigger._CurJigFactorNumber++ <= 3);

                return pr.Status == PromptStatus.OK;
            }
            catch
            {
                return false;
            }
        }

        #endregion


        private class MyMessageFilter : IMessageFilter
        {
            public const int WM_KEYDOWN = 0x0100;
            public bool bCanceled = false;

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_KEYDOWN)
                {                  
                    Keys kc = (Keys)(int)m.WParam & Keys.KeyCode;
                                
                    switch (kc)
                    {
                        case Keys.Up:                      
                            _FlipBlock = !_FlipBlock;
                            break;
                    
                        case Keys.Left:
                            _justifyAttr = AttachmentPoint.BaseLeft;
                            break;
                        case Keys.Down:
                            _justifyAttr = AttachmentPoint.BaseCenter;
                            break;
                        case Keys.Right:
                            _justifyAttr = AttachmentPoint.BaseRight;
                            break;
                  
                        case Keys.Escape:
                            bCanceled = true;
                            break;

                        default:
                            break;
                    }


                    // Return true to filter all keypresses
                    return true;
                }

                // Return false to let other messages through
                return false;
            }
        }

  

        class attPnts
        {
            private string Tag { get; set; }
            private Point3d Pnt { get; set; }
        }




        #region Test Command

        // static ObjectContext anScale;

        private static Dictionary<string, Point3d> _attPos;


        public static void BlockAttributeJig_Method(
            string moveAttr,
            string noteAttr,
            string note,
            string deptAttr,
            string deptNote,
            string blockName)
        {
            _moveAttr = moveAttr;
            _noteAttr = noteAttr;
            _note = note;
            _deptAttr = deptAttr;
            _deptNote = deptNote;

            _blockName = blockName;

            _attPos = new Dictionary<string, Point3d>();

            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            try
            {
              //  PromptResult pr = CurEditor.GetString("\nName of the block to jig:");
             //   if (pr.Status == PromptStatus.OK)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                        if (!bt.Has(_blockName)) // pr.StringResult))
                        {
                            CurEditor.WriteMessage("\nThe block <{0}> does not exist.", _blockName);// pr.StringResult);
                            return;
                        }

                        BlockTableRecord btr = tr.GetObject(bt[_blockName], OpenMode.ForRead) as BlockTableRecord;//pr.StringResult], OpenMode.ForRead) as BlockTableRecord;
                        using (BlockReference ent = new BlockReference(new Point3d(0, 0, 0), btr.ObjectId))
                        {
                            _filter = new MyMessageFilter();
                            System.Windows.Forms.Application.AddMessageFilter(_filter);

                            ent.TransformBy(UCS);
                            BlockTableRecord modelspace = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                            modelspace.AppendEntity(ent);
                            tr.AddNewlyCreatedDBObject(ent, true);

                            if (btr.Annotative == AnnotativeStates.True)
                            {
                                ObjectContextManager ocm = db.ObjectContextManager;
                                ObjectContextCollection occ = ocm.GetContextCollection(annoScalesDict);
                                ent.AddContext(occ.CurrentContext);

                                //anScale = (AnnotationScale)occ.CurrentContext;
                            }
                            else
                            {
                                ent.ScaleFactors = new Scale3d(ent.UnitFactor);
                            }

                            var atts = new Dictionary<ObjectId, ObjectId>();
                            if (btr.HasAttributeDefinitions)
                            {
                                foreach (ObjectId id in btr)
                                {
                                    var obj = tr.GetObject(id, OpenMode.ForRead);

                                    var ad = obj as AttributeDefinition;

                                    if (ad != null && !ad.Constant)
                                    {
                                        var ar = new AttributeReference();

                                        ar.SetAttributeFromBlock(ad, ent.BlockTransform);
                                        ar.TextString = ad.TextString;
                                //        ar.Position = ad.Position.TransformBy(ent.BlockTransform);

                                        var arId = ent.AttributeCollection.AppendAttribute(ar);

                                        tr.AddNewlyCreatedDBObject(ar, true);

                                        _attPos.Add(ad.Tag, ad.Position);


                                        //System.Diagnostics.Debug.WriteLine(
                                        //    "BAJig_Method:  " +
                                        //    ar.Tag + ", " +
                                        //    ar.Position.X.ToString() + ", " +
                                        //    ar.Position.Y.ToString()
                                        //    );

                                        //attPnts pt = new attPnts();
                                        //pt.Tag = ar.Tag;
                                        //pt.Pnt = ar.Position;
                                        //attPntLst.Add(pt);

                                        //atts.Add(arId, ad.ObjectId);
                                    }

                                }
                            }
                      
                            CurEditor.TurnForcedPickOn();
                            CurEditor.PointMonitor += Editor_PointMonitor;

                            if (Cls_BW_DynamicBlockAlignmentJig.Jig(ent))
                            {
                                if (!_filter.bCanceled)
                                    tr.Commit();
                            }

                            CurEditor.PointMonitor -= Editor_PointMonitor;
                            System.Windows.Forms.Application.RemoveMessageFilter(_filter);
                            CurEditor.TurnForcedPickOff();
                        }
                    }

                    BW.Cls_BW_MovRotAtt_DrawJig.MoveRotAtt(false, _attRefId, _attPosition);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // CurEditor.WriteMessage(ex.Message);
            }
        }

        #endregion

    }
}
