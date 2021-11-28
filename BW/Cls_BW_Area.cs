
using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using Autodesk.AutoCAD.Runtime;


namespace MyFirstProject.BW
{
    public class Cls_BW_AreaCmd
    {
        private Document _dwg;
        private Editor _editor;

        private double _area = 0.0;
        private double _perimeter = 0.0;

        private Autodesk.AutoCAD.DatabaseServices.Polyline _pline = null;

        private List<Point2d> _points;
        private bool _pickDone;

        private int _color = 1;

        public Cls_BW_AreaCmd(Document dwg)
        {
            _dwg = dwg;
            _editor = _dwg.Editor;
        }

        public double Area
        {
            get { return _area; }
        }

        public double Perimeter
        {
            get { return _perimeter; }
        }

        public bool GetArea()
        {
            _pline = null;

            //Pick first point
            Point3d pt1;
            if (!GetFirstPoint(out pt1)) return false;

            //Pick second point
            Point3d pt2;
            if (!GetSecondPoint(pt1, out pt2)) return false;

            _pickDone = false;

            _points = new List<Point2d>();
            _points.Add(new Point2d(pt1.X, pt1.Y));
            _points.Add(new Point2d(pt2.X, pt2.Y));

            try
            {
                //Enable custom Overrule
                MyPolylineOverrule.Instance.StartOverrule(_points, _color);

                //Handling mouse cursor moving during picking
                _editor.PointMonitor +=
                    new PointMonitorEventHandler(M_editor_PointMonitor);

                while (true)
                {
                    if (!PickNextPoint()) break;
                }

                if (_pline != null && _pickDone)
                {
                    Calculate();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                ClearTransientGraphics();

                //Remove PointMonitor handler
                _editor.PointMonitor -=
                    new PointMonitorEventHandler(M_editor_PointMonitor);

                //Disbale custom Overrule
                MyPolylineOverrule.Instance.EndOverrule();
            }

            return _pickDone;
        }

        #region private methods

        private void Calculate()
        {
            Autodesk.AutoCAD.DatabaseServices.Polyline p =
                new Autodesk.AutoCAD.DatabaseServices.Polyline(_points.Count);
            for (int i = 0; i < _points.Count; i++)
                p.AddVertexAt(i, _points[i], 0.0, 0.0, 0.0);

            p.Closed = true;

            _area = p.Area;
            _perimeter = p.Length;

            p.Dispose();
        }

        private bool GetFirstPoint(out Point3d pt)
        {
            pt = new Point3d();

            while (true)
            {
                PromptPointOptions opt =
                    new PromptPointOptions("\nPick first corner: ");

                opt.Keywords.Add("Background");
                opt.AppendKeywordsToMessage = true;

                PromptPointResult res = _editor.GetPoint(opt);

                if (res.Status == PromptStatus.OK)
                {
                    pt = res.Value;
                    return true;
                }
                else if (res.Status == PromptStatus.Keyword)
                {
                    PromptIntegerOptions intOpt = new PromptIntegerOptions("\nEnter color number (1 to 7): ");
                    intOpt.AllowNegative = false;
                    intOpt.AllowZero = false;
                    intOpt.AllowArbitraryInput = false;
                    intOpt.UseDefaultValue = true;
                    intOpt.DefaultValue = 1;

                    PromptIntegerResult intRes = _editor.GetInteger(intOpt);

                    if (intRes.Status == PromptStatus.OK)
                    {
                        _color = intRes.Value;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        private bool GetSecondPoint(Point3d basePt, out Point3d pt)
        {
            pt = new Point3d();

            PromptPointOptions opt =
                new PromptPointOptions("\nPick next corner: ");
            opt.UseBasePoint = true;
            opt.BasePoint = basePt;
            PromptPointResult res = _editor.GetPoint(opt);

            if (res.Status == PromptStatus.OK)
            {
                pt = res.Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PickNextPoint()
        {
            PromptPointOptions opt =
                new PromptPointOptions("\nPick next corner: ");
            if (_points.Count > 2)
            {
                opt.Keywords.Add("Undo");
                opt.Keywords.Add("Total");
                opt.Keywords.Default = "Total";
                opt.AppendKeywordsToMessage = true;
                opt.AllowArbitraryInput = false;
            }

            PromptPointResult res = _editor.GetPoint(opt);

            if (res.Status == PromptStatus.OK)
            {
                _points.Add(new Point2d(res.Value.X, res.Value.Y));
                return true;
            }
            else if (res.Status == PromptStatus.Keyword)
            {
                if (res.StringResult == "Undo")
                {
                    if (_points.Count > 2)
                    {
                        _points.RemoveAt(_points.Count - 1);
                    }
                    return true;
                }
                else
                {
                    _pickDone = true;
                    return false;
                }
            }
            else
            {
                _pickDone = false;
                return false;
            }
        }

        private void ClearTransientGraphics()
        {
            if (_pline != null)
            {
                TransientManager.CurrentTransientManager.EraseTransients(
                    TransientDrawingMode.DirectTopmost,
                    128, new IntegerCollection());

                _pline.Dispose();
                _pline = null;
            }
        }

        private void M_editor_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            ClearTransientGraphics();

            //Get mouse cursor location
            Point2d pt = new Point2d(e.Context.RawPoint.X, e.Context.RawPoint.Y);

            _pline = new Autodesk.AutoCAD.DatabaseServices.Polyline(_points.Count + 1);

            for (int i = 0; i < _points.Count; i++)
            {
                _pline.AddVertexAt(i, _points[i], 0.0, 0.0, 0.0);
            }

            _pline.AddVertexAt(_points.Count, pt, 0.0, 0.0, 0.0);
            _pline.Closed = true;

            TransientManager.CurrentTransientManager.AddTransient(
                _pline, TransientDrawingMode.DirectTopmost,
                128, new IntegerCollection());
        }
 
    #endregion








 
        public class MyPolylineOverrule : DrawableOverrule
        {
            private static MyPolylineOverrule _instance = null;
            private bool _existingOverrulling;
            private int _color = 1;

            private List<Point2d> _points = null;

            public static MyPolylineOverrule Instance
            {
                get
                {
                    if (_instance == null) _instance = new MyPolylineOverrule();
                    return _instance;
                }
            }

            public int Color
            {
                set { _color = value; }
                get { return _color; }
            }

            public void StartOverrule(List<Point2d> points)
            {
                _points = points;

                _existingOverrulling = Overruling;

                //Add the custom overrule
                AddOverrule(RXObject.GetClass(
                    typeof(Autodesk.AutoCAD.DatabaseServices.Polyline)), this, false);

                //Use custom filter, implemented in IsApplicable() method
                SetCustomFilter();

                //Make sure Overrule is enabled
                Overruling = true;
            }

            public void StartOverrule(List<Point2d> points, int color)
            {
                _color = color;

                _points = points;

                _existingOverrulling = Overruling;

                //Add the custom overrule
                AddOverrule(RXObject.GetClass(
                    typeof(Autodesk.AutoCAD.DatabaseServices.Polyline)), this, false);

                //Use custom filter, implemented in IsApplicable() method
                SetCustomFilter();

                //Make sure Overrule is enabled
                Overruling = true;
            }

            public void EndOverrule()
            {
                //Remove this custom Overrule
                RemoveOverrule(RXObject.GetClass(
                    typeof(Autodesk.AutoCAD.DatabaseServices.Polyline)), this);

                //restore to previous Overrule status (enabled or disabled)
                Overruling = _existingOverrulling;
            }

            public override bool WorldDraw(Drawable drawable, WorldDraw wd)
            {
                Point3dCollection pts = new Point3dCollection();
                for (int i = 0; i < _points.Count; i++)
                {
                    pts.Add(new Point3d(_points[i].X, _points[i].Y, 0.0));
                }

                wd.SubEntityTraits.FillType = FillType.FillAlways;
                wd.SubEntityTraits.Color = Convert.ToInt16(_color);

                wd.Geometry.Polygon(pts);

                return base.WorldDraw(drawable, wd);
            }

            public override bool IsApplicable(
                Autodesk.AutoCAD.Runtime.RXObject overruledSubject)
            {
                Autodesk.AutoCAD.DatabaseServices.Polyline pl = overruledSubject
                    as Autodesk.AutoCAD.DatabaseServices.Polyline;

                if (pl != null)
                {
                    //Only apply this overrule to the polyline
                    //that has not been added to working database
                    //e.g. created for the Transient Graphics
                    if (pl.Database == null)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
  

  
        public class MyCommands
        {   
            /// <summary>
            /// gets area in feet
            /// </summary>
            /// <returns></returns>
            public static Cls_BW_AreaCmd GetNewArea()
            {
                Document dwg = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = dwg.Editor;

                Cls_BW_AreaCmd cmd = new Cls_BW_AreaCmd(dwg);

                if (cmd.GetArea())
                {
                    ed.WriteMessage("\nArea = {0}", cmd.Area / 12 + " Ft");
                    ed.WriteMessage("\nPerimeter = {0}", cmd.Perimeter / 12 + " Ft");
                    return cmd;              
                }
                else
                {
                    ed.WriteMessage("\n*Cancelled*");
                    return cmd;                  
                }
            }
        }




    }



}

