using System.Diagnostics;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;


namespace MyFirstProject.BW
{
    public class Cls_BW_HandleAutocadObjTypes
    {
        //public static Autodesk.AutoCAD.Colors.Color _ColorAcad_ByLayer =
        //   Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci,  256);
        //public static Autodesk.AutoCAD.Colors.Color _ColorAcad_8 =
        //    Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 8);
        //public static Autodesk.AutoCAD.Colors.Color _ColorAcad_9 =
        //    Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, 9);


        //private static int _ColorNumber_8 = 8;
        //private static int _ColorNumber_9 = 9;

        public static bool EraseCircles { get; set; }
        public static bool EraseLines { get; set; }
        public static bool EraseText { get; set; }

        public static double CirDia { get; set; }
        public static double LinLen { get; set; }
        public static double TxtHt { get; set; }

        private Leader _Leader;
        private Dimension _Dimension;
        private Hatch _Hatch;
     

        public void Hot_LEADER(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            _Leader = (Leader)entity;                       
            _Leader.Dimclrd  = clr;
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_HATCH(Entity entity, Autodesk.AutoCAD.Colors.Color clr, Autodesk.AutoCAD.Colors.Color clrG1, Autodesk.AutoCAD.Colors.Color clrG2)
        {
            _Hatch = (Hatch)entity;

            if (_Hatch.IsGradient)
            {
                GradientColor[] gcs = new GradientColor[2];

                gcs[0] = new GradientColor(clrG1, 0);
                gcs[1] = new GradientColor(clrG2, 1);

                _Hatch.SetGradientColors(gcs);
                entity.ColorIndex = clr.ColorIndex;
            }
            else
            {
                entity.ColorIndex = clr.ColorIndex;
            }
        }
        
        public void Hot_MTEXT(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            //string Pattern = "\\{?(?:\\[fHLC].*?;|\\}).*?(.*?)";
            //string rep = Regex.Replace(_MText.Contents, Pattern, string.Empty);

            MText t = (MText)entity;

            if (EraseText && t.TextHeight <= TxtHt)
            {
                if (t.TextHeight == 0)
                {
                    Debug.WriteLine("0 mtext " + entity.ObjectId);
                }
                else
                {
                    t.Erase();
                    Debug.WriteLine("erased mtext " + entity.ObjectId);
                    return;
                }               
            }

            // loses justifications >> ??
            t.Contents = t.Text;
            
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_DIMENSION(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            try
            {
                _Dimension = (Dimension)entity;
                _Dimension.Dimclrd = clr;
                _Dimension.Dimclre = clr;
                _Dimension.Dimclrt = clr;
                entity.ColorIndex = clr.ColorIndex;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Application.ShowAlertDialog("Autodesk.AutoCAD.Runtime.Exception; " + ex.ToString());
            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog("System.Exception; " + ex.ToString());
            }
            

        }

        public void Hot_INSERT(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_LWPOLYLINE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {          
            Polyline l = (Polyline)entity;

            if (EraseLines && l.Length <= LinLen)
            {
                entity.Erase();
                //Debug.WriteLine("erased lwpolyline " + entity.ObjectId);
                return;
            }

            //if (_Polyline.st.ConstantWidth > 0)
            //    _Polyline.ConstantWidth = 0.025;

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_MULTILEADER(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_LINE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Line l = (Line)entity;

            if (EraseLines && l.Length <= LinLen)
            {
                entity.Erase();
                //Debug.WriteLine("erased line " + entity.ObjectId);
                return;
            }

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_CIRCLE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Circle c = (Circle)entity;

            if (EraseCircles && c.Diameter <= CirDia)
            {
                entity.Erase();
                //Debug.WriteLine("erased circle " + entity.ObjectId);
                return;
            }

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_ARC(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Arc a = (Arc)entity;

            if (EraseCircles && a.Radius <= CirDia / 2.0)
            {
                entity.Erase();
                //Debug.WriteLine("erased arc " + entity.ObjectId);
                return;
            }

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_SPLINE(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_WIPEOUT(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_ATTDEF(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }

        public void Hot_TEXT(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            DBText t = (DBText)entity;

            if (EraseText && t.Height <= TxtHt)
            {
                if (t.Height == 0)
                {
                    Debug.WriteLine("0 text " + entity.ObjectId);
                }
                else
                {
                    t.Erase();
                    Debug.WriteLine("erased text " + entity.ObjectId);
                    return;
                }              
            }

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_3DSOLID(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_SOLID(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_3DFACE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_POINT(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }

        public void Hot_ELLIPSE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Ellipse e = (Ellipse)entity;

            if (EraseCircles && (e.MinorRadius <= CirDia / 2.0 | e.MajorRadius <= CirDia / 2.0))
            {
                entity.Erase();
                //Debug.WriteLine("erased ellipse " + entity.ObjectId);
                return;
            }

            entity.ColorIndex = clr.ColorIndex;
        }
        
        public void Hot_OLE2FRAME(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_MESH(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_REGION(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }
        public void Hot_XLINE(Entity entity, Autodesk.AutoCAD.Colors.Color clr) { entity.ColorIndex = clr.ColorIndex; }

        public void Hot_POLYLINE(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Polyline2d l = (Polyline2d)entity;
            
            if (EraseLines && l.Length <= LinLen)
            {
                entity.Erase();
                //Debug.WriteLine("erased polyline2d " + entity.ObjectId);
                return;
            }

            //if (l.ConstantWidth > 0)
            //    l.ConstantWidth = 0.025;

            entity.ColorIndex = clr.ColorIndex;
        }


        public void Hot_PolyFaceMesh(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            //  PolyFaceMesh _PolyFaceMesh = (PolyFaceMesh)entity;

            //if (_PolyFaceMesh..ConstantWidth > 0)
            //    _PolyFaceMesh.ConstantWidth = 0.025;

            entity.ColorIndex = clr.ColorIndex;
        }

        public void Hot_Polyline3d(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            Polyline3d l = (Polyline3d)entity;
                  
            if (EraseLines && l.Length <= LinLen)
            {
                entity.Erase();
                //Debug.WriteLine("erased polyline3d " + entity.ObjectId);
                return;
            }

            //if (_Polyline2d.ConstantWidth > 0)
            //    _Polyline2d.ConstantWidth = 0.025;

            entity.ColorIndex = clr.ColorIndex;
        }



        public void Hot_PolygonMesh(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            PolygonMesh _PolygonMesh = (PolygonMesh)entity;

            //if (_PolygonMesh.ConstantWidth > 0)
            //    _PolygonMesh.ConstantWidth = 0.025;

            entity.ColorIndex = clr.ColorIndex;
        }


        public void Hot_DEFAULT(Entity entity, Autodesk.AutoCAD.Colors.Color clr)
        {
            entity.ColorIndex = clr.ColorIndex;
        }

        
            public void ProcessEntityType(
            Entity entity,
            Autodesk.AutoCAD.Colors.Color clr,
            Autodesk.AutoCAD.Colors.Color clrG1,
            Autodesk.AutoCAD.Colors.Color clrG2)
        {
            var typ = entity.Id.ObjectClass.DxfName;

            switch (typ)
            {
                case "LEADER":
                    //Debug.Print(string.Format("\\LEADER: {0} {1}", entity.ToString(), typ));
                    Hot_LEADER(entity, clr);
                    break;
                case "HATCH":
                    //Debug.Print(string.Format("\\HATCH: {0} {1}", entity.ToString(), typ));
                    Hot_HATCH(entity, clr, clrG1, clrG2);
                    break;
                case "MTEXT":
                    //Debug.Print(string.Format("\\MTEXT: {0} {1}", entity.ToString(), typ));
                    Hot_MTEXT(entity, clr);
                    break;
                case "DIMENSION":
                    //Debug.Print(string.Format("\\DIMENSION: {0} {1}", entity.ToString(), typ));
                    Hot_DIMENSION(entity, clr);
                    break;
                case "INSERT":
                    //Debug.Print(string.Format("\\INSERT: {0} {1}", entity.ToString(), typ));
                    Hot_INSERT(entity, clr);
                    break;
                case "LWPOLYLINE":
                    //Debug.Print(string.Format("\\LWPOLYLINE: {0} {1}", entity.ToString(), typ));
                    Hot_LWPOLYLINE(entity, clr);
                    break;
                case "MULTILEADER":
                    //Debug.Print(string.Format("\\MULTILEADER: {0} {1}", entity.ToString(), typ));
                    Hot_MULTILEADER(entity, clr);
                    break;
                case "LINE":
                    //Debug.Print(string.Format("\\LINE: {0} {1}", entity.ToString(), typ));
                    Hot_LINE(entity, clr);
                    break;
                case "CIRCLE":
                    //Debug.Print(string.Format("\\CIRCLE: {0} {1}", entity.ToString(), typ));
                    Hot_CIRCLE(entity, clr);
                    break;
                case "ARC":
                    //Debug.Print(string.Format("\\ARC: {0} {1}", entity.ToString(), typ));
                    Hot_ARC(entity, clr);
                    break;
                case "SPLINE":
                    //Debug.Print(string.Format("\\SPLINE: {0} {1}", entity.ToString(), typ));
                    Hot_SPLINE(entity, clr);
                    break;
                case "WIPEOUT":
                    //Debug.Print(string.Format("\\WIPEOUT: {0} {1}", entity.ToString(), typ));
                    Hot_WIPEOUT(entity, clr);
                    break;
                case "ATTDEF":
                    //Debug.Print(string.Format("\\ATTDEF: {0} {1}", entity.ToString(), typ));
                    Hot_ATTDEF(entity, clr);
                    break;
                case "TEXT":
                    //Debug.Print(string.Format("\\TEXT: {0} {1}", entity.ToString(), typ));
                    Hot_TEXT(entity, clr);
                    break;
                case "3DSOLID":
                    //Debug.Print(string.Format("\\3DSOLID: {0} {1}", entity.ToString(), typ));
                    Hot_3DSOLID(entity, clr);
                    break;
                case "SOLID":
                    //Debug.Print(string.Format("\\SOLID: {0} {1}", entity.ToString(), typ));
                    Hot_SOLID(entity, clr);
                    break;
                case "3DFACE":
                    //Debug.Print(string.Format("\\3DFACE: {0} {1}", entity.ToString(), typ));
                    Hot_3DFACE(entity, clr);
                    break;
                case "POINT":
                    //Debug.Print(string.Format("\\POINT: {0} {1}", entity.ToString(), typ));
                    Hot_POINT(entity, clr);
                    break;
                case "ELLIPSE":
                    //Debug.Print(string.Format("\\ELLIPSE: {0} {1}", entity.ToString(), typ));
                    Hot_ELLIPSE(entity, clr);
                    break;               
                case "OLE2FRAME":
                    //Debug.Print(string.Format("\\OLE2FRAME: {0} {1}", entity.ToString(), typ));
                    Hot_OLE2FRAME(entity, clr);
                    break;
                case "MESH":
                    //Debug.Print(string.Format("\\MESH: {0} {1}", entity.ToString(), typ));
                    Hot_MESH(entity, clr);
                    break;
                case "REGION":
                    //Debug.Print(string.Format("\\REGION: {0} {1}", entity.ToString(), typ));
                    Hot_REGION(entity, clr);
                    break;
                case "XLINE":
                    //Debug.Print(string.Format("\\XLINE: {0} {1}", entity.ToString(), typ));
                    Hot_XLINE(entity, clr);
                    break;
                case "POLYLINE":
                    //Debug.Print(string.Format("\\POLYLINE: {0} {1}", entity.ToString(), typ));
                    if (entity is PolyFaceMesh)
                    {
                        Hot_PolyFaceMesh(entity, clr);
                    }
                    else if (entity is Polyline3d)
                    {
                        Hot_Polyline3d(entity, clr);
                    }
                    else if (entity is PolygonMesh)
                    {
                        Hot_PolygonMesh(entity, clr);
                    }
                    else //Polyline2d
                    {
                        Hot_POLYLINE(entity, clr);
                    }
                    break;


                default:
                    Debug.Print(string.Format("\\default: {0} {1}", entity.ToString(), typ));
                    Hot_DEFAULT(entity, clr);
                    break;
            }
        }






    }

}
