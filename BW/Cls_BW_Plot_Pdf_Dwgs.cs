using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.PlottingServices;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MyFirstProject.BW
{
    public class Cls_BW_Plot_Pdf_Dwgs
    {
        public Cls_BW_Plot_Pdf_Dwg pltCls { get; set; }

        string filenameDsd = "";

        List<ClsDrawingLayoutInfo> dtp = new List<ClsDrawingLayoutInfo>();

        class ClsDrawingLayoutInfo
        {
            public string Drawing { get; set; }
            public string Layout { get; set; }
            public int TabOrder { get; set; }
        }

        public void PlotPdf(string driver, string paper, double org1, double org2, List<string> dwgs, string dwgSet = "")
        {
            dtp = new List<ClsDrawingLayoutInfo>();

            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            Document acDoc = AcAp.DocumentManager.MdiActiveDocument;

            filenameDsd = Path.GetDirectoryName(dwgs[0]) + "\\multilpe file plot pdf.dsd";

            foreach (string s in dwgs)
            {
                AcAp.DocumentManager.Open(s);

                acDoc = AcAp.DocumentManager.MdiActiveDocument;
                db = HostApplicationServices.WorkingDatabase;

                pltCls.CreateOrEditPageSetup(driver, paper, org1, org2);

                try
                {
                    using (acDoc.LockDocument())
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        DBDictionary layoutDict = (DBDictionary)db.LayoutDictionaryId.GetObject(OpenMode.ForRead);

                        foreach (DBDictionaryEntry entry in layoutDict)
                        {
                            if (entry.Key != "Model")
                            {
                                Layout l = (Layout)tr.GetObject(entry.Value, OpenMode.ForRead);

                                ClsDrawingLayoutInfo n = new ClsDrawingLayoutInfo();
                                n.Drawing = acDoc.Name;
                                n.Layout = l.LayoutName;
                                n.TabOrder = l.TabOrder;

                                dtp.Add(n);

                                pltCls.AssignPageSetupToLayout(l.LayoutName);
                            }
                        }

                        tr.Commit();
                    }
                    acDoc.Database.SaveAs(s, DwgVersion.Current);
                    acDoc.CloseAndDiscard();
                }
                catch (System.Exception e)
                {
                    Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
                    ed.WriteMessage("\nError: {0}\n{1}", e.Message, e.StackTrace);
                }
            }

            dtp.Sort((x, y) =>
            {
                var ret = x.Drawing.CompareTo(y.Drawing);
                if (ret == 0) ret = x.TabOrder.CompareTo(y.TabOrder);
                return ret;
            });

            PublisherDSD(dwgs, dwgSet, driver);
        }


        public void PublisherDSD(List<string> dwgs, string dwgSet, string driver)
        {
            short bgp = (short)AcAp.GetSystemVariable("BACKGROUNDPLOT");
            AcAp.SetSystemVariable("BACKGROUNDPLOT", 0);

            try
            {
                DsdEntryCollection collection = new DsdEntryCollection();
                DsdEntry entry;

                foreach (ClsDrawingLayoutInfo v in dtp)
                {
                    entry = new DsdEntry();

                    entry.Layout = v.Layout;
                    entry.DwgName = v.Drawing;
                    entry.NpsSourceDwg = v.Drawing;
                    entry.Nps = pltCls.pgSetupName;
                    entry.Title = Path.GetFileNameWithoutExtension(v.Drawing) + "-" + v.Layout;
                    collection.Add(entry);
                }


                DsdData dsd = new DsdData();
                dsd.SetDsdEntryCollection(collection);

                string filename = Path.GetDirectoryName(dwgs[0]) + "\\" + dwgSet.Replace("__ ", " ") + " Set " +
                        DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".pdf";

                dsd.IsSheetSet = false;
                dsd.PromptForDwfName = false;
                dsd.IsHomogeneous = false;
                dsd.SetUnrecognizedData("PwdProtectPublishedDWF", "FALSE");
                dsd.SetUnrecognizedData("PromptForPwd", "FALSE");
                dsd.SheetType = SheetType.MultiPdf;

                dsd.ProjectPath = Path.GetDirectoryName(dwgs[0]);
                dsd.LogFilePath = "c:\\Temp\\logdwf.log";

                dsd.NoOfCopies = 1;
                dsd.DestinationName = filename;
                //     dsd.SheetSetName = "";

                if (System.IO.File.Exists(filenameDsd))
                    System.IO.File.Delete(filenameDsd);

                dsd.WriteDsd(filenameDsd);

                int nbSheets = collection.Count;

                using (PlotProgressDialog progressDlg =
                    new PlotProgressDialog(false, nbSheets, true))
                {
                    progressDlg.set_PlotMsgString(PlotMessageIndex.DialogTitle, "Plot API Progress");
                    progressDlg.set_PlotMsgString(PlotMessageIndex.CancelJobButtonMessage, "Cancel Job");
                    progressDlg.set_PlotMsgString(PlotMessageIndex.CancelSheetButtonMessage, "Cancel Sheet");
                    progressDlg.set_PlotMsgString(PlotMessageIndex.SheetSetProgressCaption, "Job Progress");
                    progressDlg.set_PlotMsgString(PlotMessageIndex.SheetProgressCaption, "Sheet Progress");

                    progressDlg.UpperPlotProgressRange = 100;
                    progressDlg.LowerPlotProgressRange = 0;

                    progressDlg.UpperSheetProgressRange = 100;
                    progressDlg.LowerSheetProgressRange = 0;

                    progressDlg.IsVisible = true;

                    Autodesk.AutoCAD.Publishing.Publisher publisher = Autodesk.AutoCAD.ApplicationServices.Application.Publisher;
                    Autodesk.AutoCAD.PlottingServices.PlotConfigManager.SetCurrentConfig(driver);

                    try
                    {
                        publisher.PublishDsd(filenameDsd, progressDlg);
                    }
                    finally 
                    {
                        MessageBox.Show("Check the log file c:temp - publisher.PublishDsd(filenameDsd, progressDlg);");
                    }
              



                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                AcAp.SetSystemVariable("BACKGROUNDPLOT", bgp);
            }
        }
    }
}