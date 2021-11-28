using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public class Cls_BW_TP_Common
    {
        public static PromptSelectionResult acSSPrompt { get; set; } = null;

        public static void CalClosetDistAndPLine(bool ShowDistUprLeft, Autodesk.AutoCAD.DatabaseServices.Database database, Transaction transaction)
        {
            double bulge = 0.0;
            double startWidth = 0.0;
            double endWidth = 0.0;

            BlockTable acBlkTbl = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord acBlkTblRecModelSpace = transaction.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;


            List<Point3d> insPnts = new List<Point3d>();

            foreach (var r in Cls_BW_TP_WAOs.LstAtts_waos)
            {
                insPnts.Add(r.InsertionPtOfBlock);
            }

            var vxMax = insPnts.Select(a => a.X).Max();
            var vyMax = insPnts.Select(a => a.Y).Max();
            var vxMin = insPnts.Select(a => a.X).Min();
            var vyMin = insPnts.Select(a => a.Y).Min();

            foreach (var r in Cls_BW_TP_WAOs.LstAtts_waos)
            {
                Point3d p = new Point3d(vxMin, vyMax, 0);
                Point3d ptBlk = new Point3d(r.InsertionPtOfBlock.X, r.InsertionPtOfBlock.Y, r.InsertionPtOfBlock.Z);
                Vector3d DistFromUpperLeft = p.GetVectorTo(ptBlk);

                if (ShowDistUprLeft)
                {
                    using (Polyline acPoly = new Polyline())
                    {
                        acPoly.AddVertexAt(0, new Point2d(vxMin, vyMax), bulge, startWidth, endWidth);
                        acPoly.AddVertexAt(1, new Point2d(ptBlk.X, ptBlk.Y), bulge, startWidth, endWidth);

                        acPoly.Layer = "_WaoULdist";
                        acPoly.ColorIndex = 2; // yellow
                        acBlkTblRecModelSpace.AppendEntity(acPoly);
                        transaction.AddNewlyCreatedDBObject(acPoly, true);
                    }
                }
            }
        }


        public static void SetClosetForBlocks(
            bool RunOrientation,
            int MaxRunLenInFeet,
            bool ShowDistUprLeft,
            bool site,
            bool bldg,
            bool floor, bool CalcLengthsFromCloset
            )
        {
            Cls_BW_TP_APs.LstAtts_wap.Clear();
            Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Clear();

            Cls_BW_TP_WAOs.LstAtts_waos.Clear();

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionResult PromtSelRes = acDocEd.GetSelection(MyFirstProject.BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(BW.Cls_BW_BlksToSelect.GetBlockNames()));

            Point3d basept = new Point3d();

            if (PromtSelRes.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = PromtSelRes.Value;
                ObjectId[] ids = { };

                try
                {
                    ids = acSSet.GetObjectIds();
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                    return;
                }

                if (CalcLengthsFromCloset)
                {
                    basept = acDocEd.GetPoint("\nBase point of Closet: ").Value;
                }

                Cls_BW_TP_Common.SetAttsWithClosetInfo(PromtSelRes, basept, RunOrientation, MaxRunLenInFeet, site, bldg, floor, CalcLengthsFromCloset);
                Cls_BW_TP_APs.FillAPsList(PromtSelRes);
                Cls_BW_TP_WAOs.FillWAOsList(PromtSelRes, ShowDistUprLeft);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        public static void SetAttsWithClosetInfo(
            PromptSelectionResult PromtSelRes,
            Point3d ClosetBasePt,
            bool RunOrientation,
            int MaxRunLenInFeet,
            bool site,
            bool bldg,
            bool floor,
            bool CalcLengthsFromCloset
            )
        {
            SelectionSet acSSet = PromtSelRes.Value;
            ObjectId[] ids = { };

            try
            {
                ids = acSSet.GetObjectIds();
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                return;
            }

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                if (CalcLengthsFromCloset)
                {
                    Cls_BW_Utility.MakeLayer(database, transaction, "_Runs");
                }

                bool isLongRun = false;

                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForWrite);


                    double bulge = 0.0;
                    double startWidth = 0.0;
                    double endWidth = 0.0;

                    Vector3d DistFrom00 = ClosetBasePt.GetVectorTo(new Point3d(0, 0, 0));

                    Vector3d DistFromClosetVector = ClosetBasePt.GetVectorTo(blkRef.Position);

                    double DistanceFromClosetInFeet = Math.Round(
                        ((Math.Abs(DistFromClosetVector.X) + Math.Abs(DistFromClosetVector.Y)) / 12) // feet in decimal
                        , 0, MidpointRounding.AwayFromZero); // round up



                    BlockTable acBlkTbl = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRecModelSpace = transaction.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    if (CalcLengthsFromCloset)
                    {
                        using (Polyline acPoly = new Polyline())
                        {
                            if (RunOrientation)
                            {
                                acPoly.AddVertexAt(0, new Point2d(ClosetBasePt.X, ClosetBasePt.Y), bulge, startWidth, endWidth);

                                acPoly.AddVertexAt(1, new Point2d(DistFromClosetVector.X - DistFrom00.X, ClosetBasePt.Y), bulge, startWidth, endWidth); // change
                                acPoly.AddVertexAt(2, new Point2d(blkRef.Position.X, blkRef.Position.Y), bulge, startWidth, endWidth);
                            }
                            else
                            {
                                acPoly.AddVertexAt(0, new Point2d(ClosetBasePt.X, ClosetBasePt.Y), bulge, startWidth, endWidth);

                                acPoly.AddVertexAt(1, new Point2d(ClosetBasePt.X, DistFromClosetVector.Y - DistFrom00.Y), bulge, startWidth, endWidth); // change
                                acPoly.AddVertexAt(2, new Point2d(blkRef.Position.X, blkRef.Position.Y), bulge, startWidth, endWidth);
                            }

                            acPoly.Layer = "_Runs";

                            isLongRun = false;

                            if (DistanceFromClosetInFeet > MaxRunLenInFeet) //feet
                            {
                                acPoly.ColorIndex = 1; // red    
                                isLongRun = true;
                            }
                            else
                                acPoly.ColorIndex = 3; // green?    

                            acBlkTblRecModelSpace.AppendEntity(acPoly);
                            transaction.AddNewlyCreatedDBObject(acPoly, true);
                        }
                    }

                    FillAttributesClosetCmd(site, bldg, floor, CalcLengthsFromCloset, transaction, isLongRun, blkRef, DistanceFromClosetInFeet);

                }
                transaction.Commit();
            }
        }



        private static void FillAttributesClosetCmd(
            bool site,
            bool bldg,
            bool floor,
            bool CalcLengthsFromCloset,
            Transaction transaction,
            bool isLongRun,
            BlockReference blkRef,
            double DistanceFromClosetInFeet)
        {
            AttributeCollection attCol = blkRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);

                switch (attRef.Tag)
                {
                    case "CLOSETINFO":
                        attRef.TextString = Uc_BW_SiteInfo.Cls.AttCloset;
                        break;

                    case "DISTANCEFROMCLOSETINFEET":
                        if (CalcLengthsFromCloset)
                        {
                            attRef.TextString = DistanceFromClosetInFeet.ToString();
                        }
                        else
                        {
                            attRef.TextString = "0";
                        }
                        break;
                    case "ISLONGRUN":
                        if (CalcLengthsFromCloset)
                        {
                            attRef.TextString = isLongRun.ToString();
                        }
                        else
                        {
                            attRef.TextString = "False";
                        }
                        break;

                    case "SITE":
                        if (site)
                            attRef.TextString = Uc_BW_SiteInfo.Cls.AttSite;
                        break;
                    case "BLDG":
                        if (bldg)
                            attRef.TextString = Uc_BW_SiteInfo.Cls.AttBuilding;
                        break;
                    case "BUILDING":
                        if (bldg)
                            attRef.TextString = Uc_BW_SiteInfo.Cls.AttBuilding;
                        break;
                    case "FLOOR":
                        if (floor)
                            attRef.TextString = Uc_BW_SiteInfo.Cls.AttFloor;
                        break;

                    default:
                        break;
                }
            }
        }


        public class ClsWaoFilterList
        {
            public string Count { get; set; }
            public string BlkName { get; set; }
        }

        public class ClsClosetFilterList
        {
          //  public string Count { get; set; }
            public string Closet { get; set; }

            // for sorting the closet list right
            // example: TR100 ... TR1000
            // so 100 comes before 1000
            public string ClosetFormatSort { get; set; }
        }

    }
}
