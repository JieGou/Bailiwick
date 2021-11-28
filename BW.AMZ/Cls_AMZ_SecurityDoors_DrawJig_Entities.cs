using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.AMZ
{
    sealed class Cls_AMZ_SecurityDoors_DrawJig_Entities
    {
        public static string CreateEntities_1320(
            Transaction tr,
            BlockTableRecord btr,
            double bulge,
            double startWidth,
            double endWidth,
            string t,
            Cls_AMZ_SecurityDoors_DrawJig jigger,
            int xOffset = 0,
            int yOffset = 0
            )
        {
            if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType == "ACD")
            {
                t = "\\pxqc;" + Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType + " " +
                    Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorNumber + Environment.NewLine + "CR-IN/CR-OUT";
                AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, 0, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, -50, 50, 12, 0, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, 100, 200, 12, 0, jigger, xOffset, yOffset);
                AddLine(tr, btr, 50, 100, 0, jigger, xOffset, yOffset);
                AddLine(tr, btr, -50, -200, 0, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;LNL-1320", -250, 6, 12, jigger, xOffset, yOffset);

                AddMText(tr, btr, t, 0, 9, 6, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;" + "LOCAL POWER" + Environment.NewLine + "SUPPLY", 150, 9, 6, jigger, xOffset, yOffset);
            }
            else if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType == "TS")
            {
                t = "\\pxqc;" + "TURNSTILE" + Environment.NewLine +
                    Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType + " " + Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorNumber;
                AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, -24, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, 24, jigger, xOffset, yOffset);

                AddRec(tr, btr, bulge, startWidth, endWidth, -50, 50, 12, 0, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, 100, 200, 12, 0, jigger, xOffset, yOffset);

                AddLine(tr, btr, 50, 100, 0, jigger, xOffset, yOffset);

                Point3d p1 = new Point3d(-100, 24, 0);
                Point3d p2 = new Point3d(-91.4629, 20.3824, 0);
                Point3d p3 = new Point3d(-87.5, 12, 0);
                AddArc(tr, btr, p1, p2, p3, jigger, xOffset, yOffset);

                p1 = new Point3d(-87.5, 12, 0);
                p2 = new Point3d(-83.5371, 3.6176, 0);
                p3 = new Point3d(-75, 0, 0);
                AddArc(tr, btr, p1, p2, p3, jigger, xOffset, yOffset);

                //
                p1 = new Point3d(-87.5, -12, 0);
                p2 = new Point3d(-83.5371, -3.6176, 0);
                p3 = new Point3d(-75, 0, 0);
                AddArc(tr, btr, p1, p2, p3, jigger, xOffset, yOffset);

                p1 = new Point3d(-87.5, -12, 0);
                p2 = new Point3d(-91.4629, -20.3824, 0);
                p3 = new Point3d(-100, -24, 0);
                AddArc(tr, btr, p1, p2, p3, jigger, xOffset, yOffset);
                ////

                // lines with arcs
                AddLine(tr, btr, -100, -200, 24, jigger, xOffset, yOffset);
                AddLine(tr, btr, -100, -200, -24, jigger, xOffset, yOffset);

                AddLine(tr, btr, -50, -75, 0, jigger, xOffset, yOffset);

                AddMText(tr, btr, "\\pxqc;LNL-1320", -250, 30, 12, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;LNL-1320", -250, -18, 12, jigger, xOffset, yOffset);

                AddMText(tr, btr, t, 0, 9, 6, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;" + "LOCAL POWER" + Environment.NewLine + "SUPPLY", 150, 9, 6, jigger, xOffset, yOffset);
            }
            else if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType == "OHD")
            {
                t = "\\pxqc;" + Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType + " " +
                    Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorNumber + Environment.NewLine + "CR-IN/CR-OUT";
                AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, 0, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, -50, 50, 12, 0, jigger, xOffset, yOffset);
                AddRec(tr, btr, bulge, startWidth, endWidth, 100, 200, 12, 0, jigger, xOffset, yOffset);
                AddLine(tr, btr, 50, 100, 0, jigger, xOffset, yOffset);
                AddLine(tr, btr, -50, -200, 0, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;LNL-1320", -250, 6, 12, jigger, xOffset, yOffset);

                AddMText(tr, btr, t, 0, 9, 6, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;" + "LOCAL POWER" + Environment.NewLine + "SUPPLY", 150, 9, 6, jigger, xOffset, yOffset);
            }
            else if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType == "N/A" | Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType == "NA")
            {
                t = "\\pxqc;" + "N/A" + Environment.NewLine +
                    Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorType + " " + Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DoorNumber;
                AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, 0, jigger, xOffset, yOffset); // module
                AddRec(tr, btr, bulge, startWidth, endWidth, -50, 50, 12, 0, jigger, xOffset, yOffset); // device

                AddLine(tr, btr, -50, -200, 0, jigger, xOffset, yOffset);
                AddMText(tr, btr, "\\pxqc;LNL-1320", -250, 6, 12, jigger, xOffset, yOffset); // module

                if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DeviceDesc.Contains("ENROLLMENT"))
                    AddMText(tr, btr, "\\pxqc;ENROLLMENT CR", 0, 4, 6, jigger, xOffset, yOffset); // device

                if (Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts.DeviceDesc.Contains("SCREENING"))
                    AddMText(tr, btr, "\\pxqc;SCREENING CR", 0, 4, 6, jigger, xOffset, yOffset); // device


            }


            return t;
        }

        private static void AddArc(
            Transaction tr,
            BlockTableRecord btr,
            Point3d strtPt,
            Point3d PtOnArc,
            Point3d endPt,
            Cls_AMZ_SecurityDoors_DrawJig jigger,
            int xOffset = 0,
            int yOffset = 0
            )
        {
            //Arc l1 = new Arc(
            //    new Point3d(cX, cY, 0),
            //                 12, sA, eA);

            CircularArc3d carc = new CircularArc3d(strtPt, PtOnArc, endPt);
            Point3d cpt = carc.Center;
            Vector3d normal = carc.Normal;
            Vector3d refVec = carc.ReferenceVector;
            Plane plan = new Plane(cpt, normal);
            double ang = refVec.AngleOnPlane(plan);
            Arc arc = new Arc(cpt, normal, carc.Radius, carc.StartAngle + ang, carc.EndAngle + ang);

            arc.SetDatabaseDefaults();

            arc.Layer = "ty-detl";
            btr.AppendEntity(arc);
            tr.AddNewlyCreatedDBObject(arc, true);
            jigger.AddEntity(arc);

            Matrix3d mat1 = Matrix3d.Displacement(new Vector3d(xOffset, yOffset, 0));
            arc.TransformBy(mat1);

            carc.Dispose();
        }


        public static void AddMText(
            Transaction tr,
            BlockTableRecord btr,
            string txt,
            double x,
            double y,
            int h,
            Cls_AMZ_SecurityDoors_DrawJig jigger,
            int xOffset = 0,
            int yOffset = 0
            )
        {
            MText txtMod = new MText();
            txtMod.TextHeight = h;
            txtMod.Contents = txt;
            Point3d acPtIns = new Point3d(x, y, 0);
            txtMod.Location = acPtIns;
            txtMod.Layer = "t-tlcm-anno";
            btr.AppendEntity(txtMod);
            tr.AddNewlyCreatedDBObject(txtMod, true);
            jigger.AddEntity(txtMod);

            Matrix3d mat1 = Matrix3d.Displacement(new Vector3d(xOffset, yOffset, 0));
            txtMod.TransformBy(mat1);
        }

        public static void AddLine(
            Transaction tr,
            BlockTableRecord btr,
            double x1,
            double x2,
            double y,
            Cls_AMZ_SecurityDoors_DrawJig jigger,
            int xOffset = 0,
            int yOffset = 0
            )
        {
            Line l1 = new Line();
            l1.StartPoint = new Point3d(x1, y, 0);
            l1.EndPoint = new Point3d(x2, y, 0);
            l1.Layer = "ty-detl";
            btr.AppendEntity(l1);
            tr.AddNewlyCreatedDBObject(l1, true);
            jigger.AddEntity(l1);

            Matrix3d mat1 = Matrix3d.Displacement(new Vector3d(xOffset, yOffset, 0));
            l1.TransformBy(mat1);
        }

        public static void AddRec(
            Transaction tr,
            BlockTableRecord btr,
            double bulge,
            double startWidth,
            double endWidth,
            double x1,
            double x2,
            double y,
            double yc,
            Cls_AMZ_SecurityDoors_DrawJig jigger,
            int xOffset = 0,
            int yOffset = 0
            )
        {
            Autodesk.AutoCAD.DatabaseServices.Polyline acPolyMod = new Autodesk.AutoCAD.DatabaseServices.Polyline();
            acPolyMod.AddVertexAt(0, new Point2d(x1, yc), bulge, startWidth, endWidth);
            acPolyMod.AddVertexAt(1, new Point2d(x1, y + yc), bulge, startWidth, endWidth);
            acPolyMod.AddVertexAt(2, new Point2d(x2, y + yc), bulge, startWidth, endWidth);
            acPolyMod.AddVertexAt(3, new Point2d(x2, -y + yc), bulge, startWidth, endWidth);
            acPolyMod.AddVertexAt(4, new Point2d(x1, -y + yc), bulge, startWidth, endWidth);
            acPolyMod.AddVertexAt(5, new Point2d(x1, yc), bulge, startWidth, endWidth);
            acPolyMod.Layer = "ty-detl";
            btr.AppendEntity(acPolyMod);
            tr.AddNewlyCreatedDBObject(acPolyMod, true);
            jigger.AddEntity(acPolyMod);

            Matrix3d mat1 = Matrix3d.Displacement(new Vector3d(xOffset, yOffset, 0));
            acPolyMod.TransformBy(mat1);
        }

    }
}
