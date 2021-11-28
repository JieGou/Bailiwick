#region Namespaces

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
using System.Drawing;
using System.IO;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;

using Autodesk.AutoCAD.GraphicsSystem;
using Autodesk.AutoCAD.GraphicsInterface;

using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.ComponentModel;
using Autodesk.AutoCAD.Customization;
using Autodesk.AutoCAD.DataExtraction;
using Autodesk.AutoCAD.Diagnostics;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.LayerManager;
using Autodesk.AutoCAD.MacroRecorder;
using Autodesk.AutoCAD.Publishing;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Ribbon;

using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using MgdAcDocument = Autodesk.AutoCAD.ApplicationServices.Document;
using AcWindowsNS = Autodesk.AutoCAD.Windows;

#endregion

namespace MyFirstProject.Examples
{
    public class DrawJigger9 : DrawJig
    {
        #region Fields

        private Point3d mCorner1;
        private Point3d mCorner2;

        #endregion

        #region Constructors

        public DrawJigger9(Point3d basePt)
        {
            mCorner1 = basePt;
        }

        #endregion

        #region Properties

        public Point3d Corner1
        {
            get { return mCorner1; }
            set { mCorner1 = value; }
        }

        public Point3d Corner2
        {
            get { return mCorner2; }
            set { mCorner2 = value; }
        }

        public Matrix3d UCS
        {
            get
            {
                return MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem;
            }
        }

        public Point3dCollection Corners
        {
            get
            {
                return new Point3dCollection(
                            new Point3d[]
                            {
                                mCorner1,
                                new Point3d(mCorner1.X, mCorner2.Y, 0),
                                mCorner2,
                                new Point3d(mCorner2.X, mCorner1.Y, 0)
                            }
                        );
            }
        }

        #endregion

        #region Overrides

        protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
        {
            WorldGeometry geo = draw.Geometry;
            if (geo != null)
            {
                geo.PushModelTransform(UCS);

                geo.Polygon(Corners);

                geo.PopModelTransform();
            }

            return true;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions prOptions2 = new JigPromptPointOptions("\nCorner2:");
            prOptions2.UseBasePoint = false;

            PromptPointResult prResult2 = prompts.AcquirePoint(prOptions2);
            if (prResult2.Status == PromptStatus.Cancel || prResult2.Status == PromptStatus.Error)
                return SamplerStatus.Cancel;

            Point3d tmpPt = prResult2.Value.TransformBy(UCS.Inverse());
            if (!mCorner2.IsEqualTo(tmpPt, new Tolerance(10e-10, 10e-10)))
            {
                mCorner2 = tmpPt;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.NoChange;
        }

        #endregion

        #region Commands

        public static DrawJigger9 jigger;
   //     [CommandMethod("TestDrawJigger9")]
        public static void TestDrawJigger9_Method()
        {
            try
            {
                Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
                Editor ed = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                PromptPointOptions prOpt = new PromptPointOptions("\nCorner1:");
                PromptPointResult pr = ed.GetPoint(prOpt);
                if (pr.Status != PromptStatus.OK) return;

                jigger = new DrawJigger9(pr.Value);
                ed.Drag(jigger);

                using (acDoc.LockDocument())
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    Autodesk.AutoCAD.DatabaseServices.Polyline ent = new Autodesk.AutoCAD.DatabaseServices.Polyline();
                    ent.SetDatabaseDefaults();
                    for (int i = 0; i < jigger.Corners.Count; i++)
                    {
                        Point3d pt3d = jigger.Corners[i];
                        Point2d pt2d = new Point2d(pt3d.X, pt3d.Y);
                        ent.AddVertexAt(i, pt2d, 0, db.Plinewid, db.Plinewid);
                    }
                    ent.Closed = true;
                    ent.TransformBy(jigger.UCS);
                    btr.AppendEntity(ent);
                    tr.AddNewlyCreatedDBObject(ent, true);

                    tr.Commit();
                }
            }
            catch (System.Exception ex)
            {
                MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.ToString());
            }
        }

        #endregion

    }
}
