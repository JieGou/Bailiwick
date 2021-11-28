using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;

using System.IO;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    /// <summary>
    /// special request for cottage grove
    /// </summary>
    public class Cls_BW_TP_TextExtract
    {
        public static readonly List<Cls_BW_Plc_Atts> LstTxtExtract = new List<Cls_BW_Plc_Atts>();



        private static void SaveDataToFile_Click()
        {
            string xlsxFileName;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc is null)
            {
                MessageBox.Show("No Drawing open");
                return;
            }

            xlsxFileName = doc.Name; //.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + //" " + cBxTitleBlockChoices.Text + " " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<Int_BW_AllAtts>(newFile, LstTxtExtract, "XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }


        public static void BtnTextExtractSpecial_Click()
        {

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue[] acTypValAr = new TypedValue[2];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "MTEXT,TEXT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.LayerName, "E-Text,EText,E-TEXT,ETEXT"), 1);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                LstTxtExtract.Clear();
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                SelectionSet acSSet = acSSPrompt.Value;

                ObjectId[] objIds = acSSet.GetObjectIds();

                using (doc.LockDocument())
                using (var Transaction = acDocEd.Document.Database.TransactionManager.StartTransaction())
                {
                    foreach (ObjectId ObjId in objIds)
                    {
                        var entity = Transaction.GetObject(ObjId, OpenMode.ForWrite) as Entity;

                        var typ = entity.Id.ObjectClass.DxfName;

                        switch (typ)
                        {
                            case "MTEXT":
                                //Debug.Print(string.Format("\\MTEXT: {0} {1}", entity.ToString(), typ));
                                procMText((MText)entity, doc, Transaction, ObjId);
                                break;

                            case "TEXT":
                                //Debug.Print(string.Format("\\TEXT: {0} {1}", entity.ToString(), typ));
                                procText((DBText)entity, doc, Transaction, ObjId);
                                break;
                        }
                    }

                    acDocEd.WriteMessage("End........");
                    Transaction.Commit();
                }

                SaveDataToFile_Click();
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }

        }

        private static void procText(DBText entity, Document doc, Transaction Transaction, ObjectId ObjId)
        {
            //acDocEd.WriteMessage(current_MTextObj.Text.Replace("\r\n", " ").Replace("  ", " ") + Environment.NewLine);

            string ns = entity.TextString.Replace("\r\n", " ").Replace("  ", " ");

            Cls_BW_Plc_Atts ats = new Cls_BW_Plc_Atts();

            if (ns.StartsWith("16") )
            {
                                     
                ats.Label1 = ns;
                entity.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByColor, 1);         

                ats.Bldg = Path.GetFileNameWithoutExtension(doc.Name);
            }
            else
            {
                ats.Label1 = ns;
                entity.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByColor, 1);
            }
            LstTxtExtract.Add(ats);
        }

        private static void procMText(MText entity, Document doc, Transaction Transaction, ObjectId ObjId)
        {
            //acDocEd.WriteMessage(current_MTextObj.Text.Replace("\r\n", " ").Replace("  ", " ") + Environment.NewLine);

            string ns = entity.Text.Replace("\r\n", " ").Replace("  ", " ");

            Cls_BW_Plc_Atts ats = new Cls_BW_Plc_Atts();

            if (ns.StartsWith("TR") | ns.StartsWith("TC"))
            {
                var s = ns.Split(' ');

                if (s.Length == 2)
                {
                    ats.Label1 = s[0];
                    ats.Label2 = s[1];
                    entity.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByColor, 3);
                }
                else
                {
                    ats.Label1 = ns;
                    entity.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByColor, 1);
                }

                ats.Bldg = Path.GetFileNameWithoutExtension(doc.Name);
            }
            else
            {
                ats.Label1 = ns;
                entity.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByColor, 1);
            }
            LstTxtExtract.Add(ats);
        }



    }
}
