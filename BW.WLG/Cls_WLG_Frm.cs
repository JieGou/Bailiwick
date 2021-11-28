using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

using MoreLinq;
using OfficeOpenXml;

namespace MyFirstProject.BW.WLG
{
    public class Cls_WLG_Frm
    {


        #region merge Atts with Run List

        public static void BtnMergeLists_Click_V1()
        {
            // add to bottom of list
            var nonNumRes = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.LineNumber.All(c => !"0123456789".Contains(c))).ToList();

            for (int i = 1; i <= 142; i++)
            {
                var res = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.LineNumber == i.ToString().PadLeft(3, '0')).SingleOrDefault();

                if (res != null)
                {
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Used on Prints";

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = res.CableType;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableColor = res.CableColor;

                    //Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = res.From;

                    swFrom(i, res);

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].To = res.To;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].System = res.System;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Device = res.Device;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableLabel = res.CableLabel;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackLabel = res.JackLabel;

                    //Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = res.JackColor;

                    SwJackColor(i, res);

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Notes = res.Notes;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Department = res.Department;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Port = res.Port;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Patch = res.Patch;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Size = res.Size;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].BlockName = res.BlockName;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].InsertionPtOfBlock = res.InsertionPtOfBlock;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Handle = res.Handle;
                }
                else
                {
                    if (Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType == "Open")
                    {
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved";
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Reserved";
                    }
                    else
                    {
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Reserved";
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved-" + Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType;

                        if (Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType == "Reserved-")
                        {
                            Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved";
                        }
                    }
                }
            }

            for (int v = 0; v < nonNumRes.Count; v++)
            {
                nonNumRes[v].Status = "Used on Prints";
                Cls_WLG_Frm.lst_WLG_RunLst.Add(nonNumRes[v]);
                swFrom(Cls_WLG_Frm.lst_WLG_RunLst.Count, nonNumRes[v]);
                SwJackColor(Cls_WLG_Frm.lst_WLG_RunLst.Count, nonNumRes[v]);


            }


        }

        public static void BtnMergeLists_Click_V2()
        {
            // add to bottom of list
            var nonNumRes = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.LineNumber.All(c => !"0123456789".Contains(c))).ToList();

            for (int i = 1; i <= 142; i++)
            {
                var res = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.LineNumber == i.ToString().PadLeft(3, '0')).SingleOrDefault();

                if (res != null)
                {
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Used on Prints";

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = res.CableType;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableColor = res.CableColor;

                    //Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = res.From;

                    swFrom(i, res);

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].To = res.To;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].System = res.System;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Device = res.Device;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableLabel = res.CableLabel;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackLabel = res.JackLabel;

                    //Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = res.JackColor;

                    SwJackColor(i, res);

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Notes = res.Notes;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Department = res.Department;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Port = res.Port;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Patch = res.Patch;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Size = res.Size;

                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].BlockName = res.BlockName;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].InsertionPtOfBlock = res.InsertionPtOfBlock;
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Handle = res.Handle;
                }
                else
                {
                    if (Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType == "Open")
                    {
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved";
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Reserved";
                    }
                    else
                    {
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].Status = "Reserved";
                        Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved-" + Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType;

                        if (Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType == "Reserved-")
                        {
                            Cls_WLG_Frm.lst_WLG_RunLst[i - 1].CableType = "Reserved";
                        }
                    }
                }
            }

            for (int v = 0; v < nonNumRes.Count; v++)
            {
                nonNumRes[v].Status = "Used on Prints";
                Cls_WLG_Frm.lst_WLG_RunLst.Add(nonNumRes[v]);
                swFrom(Cls_WLG_Frm.lst_WLG_RunLst.Count, nonNumRes[v]);
                SwJackColor(Cls_WLG_Frm.lst_WLG_RunLst.Count, nonNumRes[v]);


            }


        }

        private static void swFrom(int i, Cls_WLG_Atts res)
        {
            switch (res.From.ToUpper())
            {
                case "C":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = "Comm. Cabinet";
                    break;
                case "M":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = "Manager's Cabinet";
                    break;
                case "P":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = "Phone Board";
                    break;
                case "A":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].From = "Alarm Panel";
                    break;
            }
        }

        private static void SwJackColor(int i, Cls_WLG_Atts res)
        {
            switch (res.JackColor.ToUpper())
            {
                case "W":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "White";
                    break;
                case "Y":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Yellow";
                    break;
                case "B":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Blue";
                    break;
                case "BK":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Black";
                    break;
                case "R":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Red";
                    break;
                case "GY":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Gray";
                    break;
                case "GN":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Green";
                    break;
                case "O":
                    Cls_WLG_Frm.lst_WLG_RunLst[i - 1].JackColor = "Orange";
                    break;

            }
        }

        #endregion





        #region Fill Atts

        static int _selGridViewRowIndex;

        public static void SetAttsFromRunListGridView(int selItm, bool DataDrop)
        {
            _selGridViewRowIndex = selItm;

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SelectionFilter acSelFtr = BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(Cls_WLG_BlksToSelect.GetBlockNames());

            PromptSelectionResult PromtSelRes = acDocEd.GetSelection(acSelFtr);

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

                SetAtts(PromtSelRes, DataDrop);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

           


        public static void BtnPickOriginalBlock()
        {          
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SelectionFilter acSelFtr = BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(Cls_WLG_BlksToSelect.GetBlockNames());

            PromptSelectionResult PromtSelRes = acDocEd.GetSelection(acSelFtr);
                  
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
                
                WLG_FillList(PromtSelRes, true);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        private static void ActivateGripsOnBlock(Handle handle)
        {
            ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(HostApplicationServices.WorkingDatabase, handle.ToString());

            ObjectId[] Ids;
                   
            Ids = new ObjectId[1];
            Ids[0] = idD;
         //   ObjectIdCollection idCol = new ObjectIdCollection(Ids);
        //    Cls_BW_Utility.ZoomObjects(idCol);
            Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);        
        }


        public static void SetAtts(PromptSelectionResult PromtSelRes, bool DataDrop)
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
                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForWrite);

                    BlockTable acBlkTbl = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRecModelSpace = transaction.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    string JackColor = FillAttributesCmd(transaction, blkRef);

                    if (DataDrop)
                    {
                        SetDynProp(blkRef, JackColor);
                    }

                }
                transaction.Commit();
            }
        }


        public static void SetDynProp(BlockReference blkRef, string JackColor)
        {
            foreach (DynamicBlockReferenceProperty prop in blkRef.DynamicBlockReferencePropertyCollection)
            {
                if (prop.PropertyName == "Visibility1")
                {
                    prop.Value = JackColor;
                }
            }
        }


        private static string FillAttributesCmd(Transaction transaction, BlockReference blkRef)
        {        
            AttributeCollection attCol = blkRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);

                switch (attRef.Tag)
                {
                    case "LINENUMBER":
                        attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].LineNumber;
                        break;
                    case "JACKLABEL":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].JackLabel != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].JackLabel;
                        break;
                    case "CABLEFROMCABINET":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].From != null)
                        {
                            switch (lst_WLG_RunLst[_selGridViewRowIndex].From.ToUpper())
                            {
                                case "COMM. CABINET":                 
                                case "COM CABINET":
                                    attRef.TextString = "C";
                                    break;
                                case "MANAGER'S CABINET":
                                    attRef.TextString = "M";
                                    break;
                                case "PHONE BOARD":
                                    attRef.TextString = "P";
                                    break;
                                default:
                                    attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].From;
                                    break;
                            }
                        }
                        break;
                    case "CABLETODEVICE":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].To != null)  attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].To;
                        break;
                    case "CABLETYPE":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].CableType != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].CableType;
                        break;
                    case "CABLECOLOR":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].CableColor != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].CableColor;
                        break;
                    case "SYSTEM":
                        attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].System;
                        break;
                    case "CABLELABEL":
                        attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].CableLabel;
                        break;
                    //case "DEPARTMENT":
                    //     attRef.TextString = lst_WLG_Atts[_selItm].Department;
                    //    break;
                    case "PORT":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].Port != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].Port;
                        break;
                    case "PATCH":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].Patch != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].Patch;
                        break;
                    case "JACKCOLOR":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].JackColor != null)
                        {
                            switch (lst_WLG_RunLst[_selGridViewRowIndex].JackColor.ToUpper())
                            {
                                case "WHITE":
                                    attRef.TextString = "W";                                    
                                    break;
                                case "YELLOW":
                                    attRef.TextString = "Y";
                                    break;
                                case "BLUE":
                                    attRef.TextString = "B";
                                    break;
                                case "BLACK":
                                    attRef.TextString = "BK";
                                    break;
                                case "RED":
                                    attRef.TextString = "R";
                                    break;
                                case "GRAY":
                                    attRef.TextString = "GY";
                                    break;
                                case "GREEN":
                                    attRef.TextString = "GN";
                                    break;
                                case "ORANGE":
                                    attRef.TextString = "O";
                                    break;
                                default:
                                    attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].JackColor;
                                    break;
                            }                                                        
                        }
                        break;
                    case "DEVICE":
                        if(lst_WLG_RunLst[_selGridViewRowIndex].Device != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].Device;
                        break;
                    case "NOTES":
                        if (lst_WLG_RunLst[_selGridViewRowIndex].Notes != null) attRef.TextString = lst_WLG_RunLst[_selGridViewRowIndex].Notes;
                        break;
                }
            }

            return lst_WLG_RunLst[_selGridViewRowIndex].JackColor;
        }
   
        
        #endregion





        public static void readXLS_V1(string FilePath)
        {
            Cls_WLG_Frm.lst_WLG_RunLst.Clear();

            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                Cls_WLG_Atts atts = new Cls_WLG_Atts();

                for (int row = 2; row <= rowCount; row++)
                {
                    atts = new Cls_WLG_Atts();

                    int col = 1;

                    if (worksheet.Cells[row, col + 1].Value != null) atts.LineNumber = worksheet.Cells[row, col + 1].Value.ToString().Trim().PadLeft(3, '0');

                    if (worksheet.Cells[row, col + 2].Value != null) atts.CableType = worksheet.Cells[row, col + 2].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 3].Value != null) atts.CableColor = worksheet.Cells[row, col + 3].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 4].Value != null) atts.From = worksheet.Cells[row, col + 4].Value.ToString().Trim();
                    if (worksheet.Cells[row, col + 5].Value != null) atts.To = worksheet.Cells[row, col + 5].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 6].Value != null) atts.System = worksheet.Cells[row, col + 6].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 7].Value != null) atts.Device = worksheet.Cells[row, col + 7].Value.ToString().Trim();
                    if (worksheet.Cells[row, col + 8].Value != null) atts.CableLabel = worksheet.Cells[row, col + 8].Value.ToString().Trim();

                    //if (worksheet.Cells[row, col + 9].Value != null) atts.Patch = worksheet.Cells[row, col + 9].Value.ToString().Trim();

                    //if (worksheet.Cells[row, col + 10].Value != null) atts.Port = worksheet.Cells[row, col + 10].Value.ToString().Trim();

                    //if (worksheet.Cells[row, col + 12].Value != null)
                    //{
                    //    atts.JackLabel = worksheet.Cells[row, col + 8].Value.ToString().Trim(); // CableLabel
                    //    atts.JackColor = worksheet.Cells[row, col + 12].Value.ToString().Trim();
                    //}

                    if (worksheet.Cells[row, col + 12].Value != null && worksheet.Cells[row, col + 12].Value.ToString().Contains("-"))
                    {
                        atts.JackLabel = worksheet.Cells[row, col + 12].Value.ToString().Trim();
                        atts.Patch = atts.JackLabel.Split('-')[0];
                        atts.Port = atts.JackLabel.Split('-')[1];
                    }

                    if (worksheet.Cells[row, col + 10].Value != null) atts.JackColor = worksheet.Cells[row, col + 10].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 11].Value != null) atts.Notes = worksheet.Cells[row, col + 11].Value.ToString().Trim();


                    //if (worksheet.Cells[row, col + 14].Value != null && worksheet.Cells[row, col + 14].Value.ToString().Contains("-"))
                    //{
                    //    string s = worksheet.Cells[row, col + 14].Value.ToString().Trim();
                    //    atts.OldNumber = s.Split('-')[0] + "-" + s.Split('-')[1]; //.PadLeft(2, '0');
                    //}


                    if (atts.LineNumber != null)
                    {
                        lst_WLG_RunLst.Add(atts);
                    }

                }
            }


            //    lst_WLG_RunLst.Clear();
            //    lst_WLG_Depts = lst_WLG_Atts.DistinctBy(x => x.System ).ToList();

        }

        public static void readXLS_V2(string FilePath)
        {
            Cls_WLG_Frm.lst_WLG_RunLst.Clear();

            FileInfo existingFile = new FileInfo(FilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = 143; // worksheet.Dimension.End.Row;     //get row count

                Cls_WLG_Atts atts = new Cls_WLG_Atts();

                for (int row = 2; row <= rowCount; row++)
                {
                    atts = new Cls_WLG_Atts();

                    int col = 1;

                    if (worksheet.Cells[row, col + 1].Value != null) atts.LineNumber = worksheet.Cells[row, col + 1].Value.ToString().Trim().PadLeft(3, '0');

                    if (worksheet.Cells[row, col + 2].Value != null) atts.CableType = worksheet.Cells[row, col+2].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 3].Value != null) atts.CableColor = worksheet.Cells[row, col +3].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 4].Value != null) atts.From = worksheet.Cells[row, col + 4].Value.ToString().Trim();
                    if (worksheet.Cells[row, col + 5].Value != null) atts.To = worksheet.Cells[row, col +5].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 6].Value != null) atts.System = worksheet.Cells[row, col + 6].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 7].Value != null) atts.Device = worksheet.Cells[row, col + 7].Value.ToString().Trim();
                    if (worksheet.Cells[row, col + 8].Value != null) atts.CableLabel = worksheet.Cells[row, col + 8].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 9].Value != null) atts.Patch = worksheet.Cells[row, col + 9].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 10].Value != null) atts.Port = worksheet.Cells[row, col + 10].Value.ToString().Trim();

                    if (worksheet.Cells[row, col + 12].Value != null)
                    {
                        atts.JackLabel = worksheet.Cells[row, col + 8].Value.ToString().Trim(); // CableLabel
                        atts.JackColor = worksheet.Cells[row, col + 12].Value.ToString().Trim();
                    }

                    //if (worksheet.Cells[row, col + 12].Value != null && worksheet.Cells[row, col + 12].Value.ToString().Contains("-"))
                    //{
                    //    atts.JackLabel = worksheet.Cells[row, col + 12].Value.ToString().Trim();
                    //    atts.Patch = atts.JackLabel.Split('-')[0];
                    //    atts.Port = atts.JackLabel.Split('-')[1];
                    //}

                    //if (worksheet.Cells[row, col + 10].Value != null) atts.JackColor = worksheet.Cells[row, col + 10].Value.ToString().Trim();

                    //if (worksheet.Cells[row, col + 11].Value != null) atts.Notes = worksheet.Cells[row, col + 11].Value.ToString().Trim();


                    if (worksheet.Cells[row, col + 14].Value != null && worksheet.Cells[row, col + 14].Value.ToString().Contains("-"))
                    {
                        string s = worksheet.Cells[row, col + 14].Value.ToString().Trim();
                        atts.OldNumber = s.Split('-')[0] + "-" + s.Split('-')[1]; //.PadLeft(2, '0');
                    }


                    if (atts.LineNumber != null)
                    {
                        lst_WLG_RunLst.Add(atts);
                    }
                                       
                }
            }


        //    lst_WLG_RunLst.Clear();
        //    lst_WLG_Depts = lst_WLG_Atts.DistinctBy(x => x.System ).ToList();

        }


   //     public static List<Cls_WLG_Atts> lst_WLG_Depts = new List<Cls_WLG_Atts>();

        public static readonly List<Cls_WLG_Atts> lst_WLG_Atts = new List<Cls_WLG_Atts>();



        public static readonly List<Cls_WLG_Atts> lst_WLG_RunLst = new List<Cls_WLG_Atts>();


        public static void WLG_SelectBlocks()
        {          

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            #region old filter
            //TypedValue[] acTypValAr = new TypedValue[4];
            //acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            //acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "<OR"), 1);

            //acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "data*"), 2);
            ////acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Ip Wall Clock"), 3);
            ////acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "TMC"), 4);
            ////acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Video Wall"), 5);

            //acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "OR>"), 3);

            //SelectionFilter acSelFtr = new SelectionFilter(acTypValAr); 
            #endregion

            SelectionFilter acSelFtr = BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(Cls_WLG_BlksToSelect.GetBlockNames());            

            PromptSelectionResult acSSPrompt = acDocEd.GetSelection(acSelFtr);
            
            if (acSSPrompt.Status == PromptStatus.OK)
            {

                WLG_FillList(acSSPrompt, false);

                //     lst_WLG_Atts.Sort((x, y) => x.JackLabel.CompareTo(y.JackLabel));
                //lst_WLG_Atts.OrderBy(x => x.BlockName).ThenBy(x => x.CableLabel);

                lst_WLG_Atts.Sort((x, y) => x.LineNumber.CompareTo(y.LineNumber));

                lst_WLG_HrdWr.Sort((x, y) => x.HousingValue.CompareTo(y.HousingValue));

                //// blockname not null
                //lst_THD_DisplayAtts.Sort((x, y) => x.Label.CompareTo(y.Label));

            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        public static readonly List<Cls_WLG_HrdWr> lst_WLG_HrdWr = new List<Cls_WLG_HrdWr>();

        private static void WLG_FillList(PromptSelectionResult acSSPrompt, bool activateGrips)
        {
            SelectionSet acSSet = acSSPrompt.Value;
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
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Cls_WLG_HrdWr Cls_WLG_HrdWr;
            lst_WLG_HrdWr.Clear();

            Cls_WLG_Atts Cls_WLG_Atts;
            lst_WLG_HrdWr.Clear();

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id1 in ids)
                {          
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);

                    string nam = "";
                    nam = blkRef.Name;

                    if (blkRef.IsDynamicBlock)
                    {
                        BlockTableRecord relName = (BlockTableRecord)transaction.GetObject(blkRef.DynamicBlockTableRecord, OpenMode.ForRead);
                        nam = relName.Name;
                    }

                    if (nam != "Housing")
                    {
                        Cls_WLG_Atts = new Cls_WLG_Atts();
                        Cls_WLG_Atts.BlockName = nam; // blkRef.Name;
                        Cls_WLG_Atts.Handle = blkRef.Handle;
                        Cls_WLG_Atts.InsertionPtOfBlock = blkRef.Position;
                        lst_WLG_Atts.Add(Cls_WLG_Atts);

                        AttributeCollection attCol = blkRef.AttributeCollection;

                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                            switch (attRef.Tag)
                            {
                                case "JACKLABEL":
                                    Cls_WLG_Atts.JackLabel = attRef.TextString;  // string.Format("{0:#-0#}", attRef.TextString);
                                    break;
                                case "CABLEFROMCABINET":
                                    Cls_WLG_Atts.From = attRef.TextString;
                                    break;
                                case "CABLETODEVICE":
                                    Cls_WLG_Atts.To = attRef.TextString;
                                    break;
                                case "CABLETYPE":
                                    Cls_WLG_Atts.CableType = attRef.TextString;
                                    break;
                                case "CABLECOLOR":
                                    Cls_WLG_Atts.CableColor = attRef.TextString;
                                    break;
                                case "SYSTEM":
                                    Cls_WLG_Atts.System = attRef.TextString;
                                    break;
                                case "CABLELABEL":
                                    Cls_WLG_Atts.CableLabel = attRef.TextString;
                                    break;
                                case "DEPARTMENT":
                                    Cls_WLG_Atts.Department = attRef.TextString;
                                    break;
                                case "PORT":
                                    Cls_WLG_Atts.Port = attRef.TextString;
                                    break;
                                case "PATCH":
                                    Cls_WLG_Atts.Patch = attRef.TextString;
                                    break;
                                case "JACKCOLOR":
                                    Cls_WLG_Atts.JackColor = attRef.TextString;
                                    break;
                                case "DEVICE":
                                    Cls_WLG_Atts.Device = attRef.TextString;
                                    break;
                                case "SIZE":
                                    Cls_WLG_Atts.Size = attRef.TextString;
                                    break;
                                case "NOTES":
                                    Cls_WLG_Atts.Notes = attRef.TextString;
                                    break;

                                case "LINENUMBER":
                                    if (attRef.TextString.All(char.IsDigit))
                                    {
                                        Cls_WLG_Atts.LineNumber = attRef.TextString.PadLeft(3, '0');
                                    }
                                    else
                                    {
                                        Cls_WLG_Atts.LineNumber = attRef.TextString;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {

                        Cls_WLG_HrdWr = new Cls_WLG_HrdWr();
                        Cls_WLG_HrdWr.BlockName = nam; // blkRef.Name;
                        Cls_WLG_HrdWr.Handle = blkRef.Handle;
                        Cls_WLG_HrdWr.InsertionPtOfBlock = blkRef.Position;
                        lst_WLG_HrdWr.Add(Cls_WLG_HrdWr);

                        AttributeCollection attCol = blkRef.AttributeCollection;

                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                            switch (attRef.Tag)
                            {
                                case "HOUSING":
                                    Cls_WLG_HrdWr.HousingValue = attRef.TextString;  // string.Format("{0:#-0#}", attRef.TextString);
                                    break;
                            }
                        }


                    }

                    if (activateGrips)
                    {
                        ActivateGripsOnBlock(blkRef.Handle);
                    }
                }       
                transaction.Commit();

                //// LstAtts_DevTag.OrderBy(x => x.BlockName).ThenBy(x => x.DoorNumber);

                //// blockname not null
                //lst_WLG_Atts.Sort((x, y) => x.Room.CompareTo(y.Room));
            }
        }




        /// <summary>
        /// Not used
        /// </summary>
        public static void BtnAutoLabelByColor()
        {
            int strt = 1;
            int patch = 1;
            string[] cab = { "RX", "MGR"};

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {             
                var LstClrFltr = Cls_WLG_Frm.lst_WLG_Atts.DistinctBy(x => x.JackColor).ToList();

                foreach (string c in cab)
                foreach (var acf in LstClrFltr)
                {
                    var itms = Cls_WLG_Frm.lst_WLG_Atts
                        .Where(x => x.JackColor == acf.JackColor & x.Patch == patch.ToString() & x.From == c)
                        //.OrderBy(x => x.Floor)
                        .OrderByDescending(x => x.InsertionPtOfBlock.Y)
                        .ThenBy(x => x.InsertionPtOfBlock.X)
                        .ToList();

                    foreach (var itm in itms)
                    {
                        ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database, itm.Handle.ToString());

                        BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);

                        Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol1 = blkRef1.AttributeCollection;
                        foreach (ObjectId attId in attCol1)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                            switch (attRef.Tag)
                            {
                                case "JACKLABEL":
                                    attRef.TextString = patch + "-" + strt.ToString("0#");
                                        strt++;
                                break;

                                default:
                                    break;
                            }
                        }
                    }
               //     strt = 1;
                }
                transaction.Commit();
            }
        }


    }
}
