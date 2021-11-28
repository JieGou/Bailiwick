using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;


namespace MyFirstProject.BW.WLG
{
    class Cls_WLG_AC_Tbls
    {
        static public void ModifyTable_WrkStaDevSum()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;

            var ed = doc.Editor;
    
            var per = ed.GetEntity("\nSelect table");

            if (per.Status != PromptStatus.OK)
                return;

            using (doc.LockDocument())
            using (var tr = doc.TransactionManager.StartTransaction())
            {               
                var tb = tr.GetObject(per.ObjectId, OpenMode.ForRead) as Table;

                if (tb != null)
                {
                    tb.UpgradeOpen();

                    // row, col
                    tb.Cells[3, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "GY").Count().ToString();
                    tb.Cells[4, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "GN").Count().ToString();
                    tb.Cells[5, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "R").Count().ToString();
                    tb.Cells[6, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "Y").Count().ToString();
                    tb.Cells[7, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "BK").Count().ToString();
                    tb.Cells[8, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "B").Count().ToString();
                    tb.Cells[9, 2].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "W" & X.Notes != "Wireless AP").Count().ToString();
                    // 10, 2 - time clock row, skip
                    // 11, 2 - sub total row, skip
                    tb.Cells[12, 1].Value = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "W" & X.Notes == "Wireless AP").Count().ToString();                    
                    // 13, 2 - total row, skip
                }

                tr.Commit();
            }
        }


        public static void WLG_InsertTable_CameraSummary()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptPointResult pr = ed.GetPoint("\nEnter table insertion point: ");

            if (pr.Status == PromptStatus.OK)
            {
                Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

                tb.TableStyle = db.Tablestyle;
                tb.SetSize(6, 3);
                tb.SetRowHeight(34.5);
                tb.SetColumnWidth(65);
                tb.Position = pr.Value;

                CellRange mcells = CellRange.Create(tb, 1, 1, 1, 2);
                tb.MergeCells(mcells);
                Cell mc = tb.Cells[1, 0];
                mc.Contents.Add();
                      

                string[,] str = new string[5, 3]; // rows, cols

                str[0, 0] = "CAMERA SUMMARY";
                str[0, 1] = "";
                str[0, 2] = "";

                str[1, 0] = "";
                str[1, 1] = "......";
                str[1, 2] = "";

                str[2, 0] = "DEVICE\r\nTYPE";
                str[2, 1] = "JACK\r\nCOLOR";
                str[2, 2] = "PORT\r\nQTY";

                str[3, 0] = "";
                str[3, 1] = "1";
                str[3, 2] = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "GR").Count().ToString();

                str[4, 0] = "";
                str[4, 1] = "2";
                str[4, 2] = Cls_WLG_Frm.lst_WLG_Atts.Where(X => X.JackColor == "GN").Count().ToString();
                       

                str[11, 0] = "SUB\r\nTOTAL";
                str[11, 1] = "...";
                str[11, 2] = "=Sum(C3:C5)";      

                for (int rows = 0; rows < 16; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        tb.Cells[rows, cols].TextHeight = 9;
                        tb.Cells[rows, cols].TextString = str[rows, cols];
                        tb.Cells[rows, cols].Alignment = CellAlignment.MiddleCenter;
                    }
                }

                tb.Rows[0].Height = 44;
           //     tb.Rows[15].Height = 200;

                tb.GenerateLayout();

                using (doc.LockDocument())
                using (Transaction tr = doc.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    Cell ThumOfBlock;        
       
                    foreach (var id in bt)
                    {
                        var btrTblBlks = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);

                        if (!btrTblBlks.IsLayout && !btrTblBlks.IsAnonymous)
                        {
                            if (btrTblBlks.Name == "TellCommTableBlock_2")
                            {                             
                                ThumOfBlock = tb.Cells[3, 0];
                                ThumOfBlock.BlockTableRecordId = id;                
                            }

                            if (btrTblBlks.Name == "TellCommTableBlock_8")
                            {                                
                                ThumOfBlock = tb.Cells[10, 0];
                                ThumOfBlock.BlockTableRecordId = id;
                            }
                        }
                    }            

                    btr.AppendEntity(tb);
                    tr.AddNewlyCreatedDBObject(tb, true);
                    tr.Commit();
                }
            }
        }


    }
}
