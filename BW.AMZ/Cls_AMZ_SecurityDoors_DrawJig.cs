using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MyFirstProject.BW.AMZ
{
    public class Cls_AMZ_SecurityDoors_DrawJig : DrawJig
    {
        #region Fields


        private Point3d mBase;
        private Point3d mLocation;
        List<Entity> mEntities;

        #endregion

        #region Constructors


        public Cls_AMZ_SecurityDoors_DrawJig()
        {
        }

        public Cls_AMZ_SecurityDoors_DrawJig(Point3d basePt)
        {
            mBase = basePt.TransformBy(UCS);
            mEntities = new List<Entity>();
        }

        #endregion

        #region Properties

        public Point3d Base
        {
            get { return mLocation; }
            set { mLocation = value; }
        }

        public Point3d Location
        {
            get { return mLocation; }
            set { mLocation = value; }
        }

        public Matrix3d UCS
        {
            get
            {
                return MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem;
            }
        }

        #endregion

        #region Methods

        private static List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> _lst = new List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>();

        private static int _qty;
        private static string _modType;

        public void RunJig(int qty, string modType)
        {
            _qty = qty;
            _modType = modType;

            AMZ_DrawJigger_Method();

            _qty = -1;
            _modType = "";
        }

        public void RunJig(List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> lst)
        {
            _lst = lst;

            AMZ_DrawJigger_Method();

            _lst.Clear();
        }


        public void RunJig()
        {
            AMZ_DrawJigger_Method();
        }

        public void AddEntity(Entity ent)
        {
            mEntities.Add(ent);
        }

        public void TransformEntities()
        {
            Matrix3d mat = Matrix3d.Displacement(mBase.GetVectorTo(mLocation));

            foreach (Entity ent in mEntities)
            {
                ent.TransformBy(mat);
            }
        }

        #endregion

        #region Overrides

        protected override bool WorldDraw(Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
        {
            Matrix3d mat = Matrix3d.Displacement(mBase.GetVectorTo(mLocation));

            WorldGeometry geo = draw.Geometry;
            if (geo != null)
            {
                geo.PushModelTransform(mat);

                foreach (Entity ent in mEntities)
                {
                    geo.Draw(ent);
                }

                geo.PopModelTransform();
            }

            return true;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions prOptions1 = new JigPromptPointOptions("\nNew location:");
            prOptions1.UseBasePoint = false;

            PromptPointResult prResult1 = prompts.AcquirePoint(prOptions1);
            if (prResult1.Status == PromptStatus.Cancel || prResult1.Status == PromptStatus.Error)
                return SamplerStatus.Cancel;

            if (!mLocation.IsEqualTo(prResult1.Value, new Tolerance(10e-10, 10e-10)))
            {
                mLocation = prResult1.Value;
                return SamplerStatus.OK;
            }
            else
                return SamplerStatus.NoChange;
        }

        #endregion

        #region Main Method



        public static Cls_AMZ_SecurityDoors_DrawJig jigger;


        public static void AMZ_DrawJigger_Method()
        {
            try
            {
                Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
                Editor ed = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;
                Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                jigger = new Cls_AMZ_SecurityDoors_DrawJig(new Point3d(-250, 0, 0));

                using (acDoc.LockDocument())
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    Cls_BW_Utility.MakeLayer(db, tr, "t-tlcm-anno");
                    Cls_BW_Utility.MakeLayer(db, tr, "ty-detl");

                    double bulge = 0.0;
                    double startWidth = 0.0;
                    double endWidth = 0.0;

                    int xOff = 0;
                    int yOff = 0;
                    

                    string t = ".";

                    if (_lst.Count != 0)
                    {
                      //  List<BW.AMZ.Cls_AMZ_SecurityDoors_DeviceTag> lst1100 = _lst.Where(x => x.Module == "LNL-1100").ToList();
                      //  List<BW.AMZ.Cls_AMZ_SecurityDoors_DeviceTag> lst1200 = _lst.Where(x => x.Module == "LNL-1200").ToList();
                        List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> lst1320 = _lst.Where(x => x.Module == "LNL-1320").ToList();
                                
                        foreach (BW.AMZ.Cls_AMZ_SecurityDoors_Atts c in lst1320)
                        {
                            Cls_AMZ_SecurityDoors.AMZ_CurrDeviceTagAtts = c;
                            t = Cls_AMZ_SecurityDoors_DrawJig_Entities.CreateEntities_1320(tr, btr, bulge, startWidth, endWidth, t, jigger, xOff, yOff);

                            switch (c.DoorType)
                            {
                                case "TS": yOff -= 100; break;
                                case "ACD": yOff -= 50; break;
                                case "XD": yOff -= 50; break;
                                case "EXD": yOff -= 50; break;
                                case "N/A": yOff -= 50; break;
                                case "NA": yOff -= 50; break;
                            }
                        }


                    }
                    else
                    {
                        xOff = 0;
                        yOff = 0;
                        // t = createEntities_1320(tr, btr, bulge, startWidth, endWidth, t);
                        if (_qty != -1)
                        {
                            for (int i = 0; i < _qty; i++)
                            {
                                Cls_AMZ_SecurityDoors_DrawJig_Entities.AddRec(tr, btr, bulge, startWidth, endWidth, -200, -300, 12, 0, jigger, xOff, yOff);
                                Cls_AMZ_SecurityDoors_DrawJig_Entities.AddLine(tr, btr, -300, -375, 0, jigger, xOff, yOff); // left line
                                Cls_AMZ_SecurityDoors_DrawJig_Entities.AddMText(tr, btr, "\\pxqc;" + _modType, -250, 6, 12, jigger, xOff, yOff);

                                // top line
                                Line l1 = new Line();
                                l1.StartPoint = new Point3d(-250, 12, 0);
                                l1.EndPoint = new Point3d(-250, 38, 0);
                                l1.Layer = "ty-detl";
                                btr.AppendEntity(l1);
                                tr.AddNewlyCreatedDBObject(l1, true);
                                jigger.AddEntity(l1);

                                Matrix3d mat1 = Matrix3d.Displacement(new Vector3d(xOff, yOff, 0));
                                l1.TransformBy(mat1);

                                

                                yOff -= 50;
                            }
                        }
                    }


                    PromptResult jigRes = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.Drag(jigger);
                    if (jigRes.Status == PromptStatus.OK)
                    {
                        jigger.TransformEntities();
                        tr.Commit();
                    }
                    else
                        tr.Abort();
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
