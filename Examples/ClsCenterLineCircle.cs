using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;
 
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
 
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using MgdAcDocument = Autodesk.AutoCAD.ApplicationServices.Document;
using AcWindowsNS = Autodesk.AutoCAD.Windows;
  
using Autodesk.AutoCAD.GraphicsInterface;
 


namespace MyFirstProject.Examples 
{
    /// <summary>
    /// Features:
    ///     A derivative from the DrawableOverrule;
    ///     Being made a singleton to avoid duplicate registrations;
    ///     UCS being hornored;
    ///     Adding center lines to all cirlces;
    ///     Line.WorldDraw() being used to draw the lines;
    ///     Drawing the center lines with a particular color (Cyan with index 4);
    /// </summary>
    public class CenterlineCircle_DrawableOverrule3 : DrawableOverrule
    {
        protected CenterlineCircle_DrawableOverrule3()
        {
        }

        protected static CenterlineCircle_DrawableOverrule3 mInstance;
        public static CenterlineCircle_DrawableOverrule3 Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new CenterlineCircle_DrawableOverrule3();
                return mInstance;
            }
            set
            {
                mInstance = value;
            }
        }

        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {
            if (drawable is Circle)
            {
                Matrix3d ucs = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem;

                Circle cir = drawable as Circle;
                Point3d cen = cir.Center.TransformBy(ucs.Inverse()); ;
                double rad = cir.Radius;

                Point3d line1Pt1 = new Point3d(cen.X - rad * 1.2, cen.Y, cen.Z).TransformBy(ucs);
                Point3d line1Pt2 = new Point3d(cen.X + rad * 1.2, cen.Y, cen.Z).TransformBy(ucs);
                Point3d line2Pt1 = new Point3d(cen.X, cen.Y - rad * 1.2, cen.Z).TransformBy(ucs);
                Point3d line2Pt2 = new Point3d(cen.X, cen.Y + rad * 1.2, cen.Z).TransformBy(ucs);

                short backupColor = wd.SubEntityTraits.Color;
                wd.SubEntityTraits.Color = 4;
                using (Line line = new Line(line1Pt1, line1Pt2))
                {
                    line.WorldDraw(wd);
                }
                using (Line line = new Line(line2Pt1, line2Pt2))
                {
                    line.WorldDraw(wd);
                }
                wd.SubEntityTraits.Color = backupColor;
            }

            return base.WorldDraw(drawable, wd);
        }

        

        public class CircleTransformOverrule : TransformOverrule
        {
            static public CircleTransformOverrule theOverrule =
              new CircleTransformOverrule();       

            public override void Explode(Entity e, DBObjectCollection objs)
            {
                Matrix3d ucs = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem;
                              
                Circle circle;
                if (e is Circle)
                {
                    circle = e as Circle;
                    Point3d cen = circle.Center.TransformBy(ucs.Inverse()); ;
                    double rad = circle.Radius;

                    //   Vector3d normal =
                    //     (cen - circle.StartPoint.TransformBy(ucs.Inverse())).
                    //       CrossProduct(circle.Normal);
                                     
                    //   //Circle clr =
                    //   //  new Circle(
                    //   //    cen, circle.Normal, rad
                    //   //  );                  

                    Point3d line1Pt1 = new Point3d(cen.X - rad * 1.2, cen.Y, cen.Z).TransformBy(ucs);
                    Point3d line1Pt2 = new Point3d(cen.X + rad * 1.2, cen.Y, cen.Z).TransformBy(ucs);
                    Point3d line2Pt1 = new Point3d(cen.X, cen.Y - rad * 1.2, cen.Z).TransformBy(ucs);
                    Point3d line2Pt2 = new Point3d(cen.X, cen.Y + rad * 1.2, cen.Z).TransformBy(ucs);
                    
                    Line line1 = new Line(line1Pt1, line1Pt2);
                    objs.Add(line1);

                    Line line2 = new Line(line2Pt1, line2Pt2);
                    objs.Add(line2);

                    Point3d center = cen;
                    Vector3d normal = Vector3d.ZAxis;
                    Vector3d majorAxis = new Vector3d((line2.Length / 2 / 1.2), 0, 0);
                    double radiusRatio = 1.0;
                    double startAng = 0.0;
                    double endAng = 360 * Math.Atan(1.0) / 45.0;

                    Ellipse ellipse = new Ellipse(center,
                                                    normal,
                                                    majorAxis,
                                                    radiusRatio,
                                                    startAng,
                                                    endAng
                                                );

                    objs.Add(ellipse);

                    return;
                }
                base.Explode(e, objs);
            }

        }


    }
}
