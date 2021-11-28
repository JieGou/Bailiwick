using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.PlottingServices;
using Autodesk.AutoCAD.Publishing;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MyFirstProject.BW
{

    /// <summary>
    /// publish all ps layouts in the open drawing
    /// </summary>
    public class Cls_BW_Plot_Pdf_Dwg
    {
        public string pgSetupName = "_Bailiwick_PDF";

        private string dwgFile;
        private string pdfFile;
        private string dsdFile;
        private string outputDir;

        private int sheetNum;

        IEnumerable<Layout> layouts; // for MultiSheetsPdfPlot

        private const string LOG = "publish.log";
        
        public void PlotPdf(string DateFormat, List<Layout> layoutsForPlot, string driver, string paper, double org1, double org2, 
            bool PlotLineWeights = false, string dwgSet = "",  bool AllLayouts = true)
        {
            CreateOrEditPageSetup(driver, paper, org1, org2, PlotLineWeights);

            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            Document acDoc = AcAp.DocumentManager.MdiActiveDocument;

            short bgp = (short)AcAp.GetSystemVariable("BACKGROUNDPLOT");
            try
            {
                AcAp.SetSystemVariable("BACKGROUNDPLOT", 0);

                using (acDoc.LockDocument())
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    List<Layout> layouts = new List<Layout>();

                    if (layoutsForPlot.Count == 0) // get from dwg
                    {                       
                        DBDictionary layoutDict = (DBDictionary)db.LayoutDictionaryId.GetObject(OpenMode.ForRead);

                        foreach (DBDictionaryEntry entry in layoutDict)
                        {
                            if (entry.Key != "Model")
                            {
                                if (AllLayouts)
                                {
                                    layouts.Add((Layout)tr.GetObject(entry.Value, OpenMode.ForRead));
                                }
                                else
                                {
                                    if (((Layout)tr.GetObject(entry.Value, OpenMode.ForRead)).TabSelected == true)
                                    {
                                        layouts.Add((Layout)tr.GetObject(entry.Value, OpenMode.ForRead));
                                    }
                                }
                            }
                        }
                        layouts.Sort((l1, l2) => l1.TabOrder.CompareTo(l2.TabOrder));
                    }
                    else
                    {
                        layouts.AddRange(layoutsForPlot); // use list on form
                    }

                    string filename;
                                
                    if (dwgSet == "")
                    {
                        filename = acDoc.Name.Replace(".dwg", " ") + ".pdf";
                    }
                    else // survey set, design set install set ... drop down list
                    {
                        filename = acDoc.Name.Replace(".dwg", "") + dwgSet.Replace("__ ", " ") + " Set " +
                            DateTime.Now.ToString(DateFormat, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + 
                            ".pdf";
                    }

                    Cls_BW_Plot_Pdf_Dwg plotter = new Cls_BW_Plot_Pdf_Dwg();
                    plotter.MultiSheetsPdfPlot(filename, layouts);
                    plotter.Publish();

                    tr.Commit();
                }
            }
            catch (System.Exception e)
            {
                Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nError: {0}\n{1}", e.Message, e.StackTrace);
            }
            finally
            {
                AcAp.SetSystemVariable("BACKGROUNDPLOT", bgp);
            }
        }

        private void MultiSheetsPdfPlot(string pdfFile, IEnumerable<Layout> layouts)
        {
            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            Document acDoc = AcAp.DocumentManager.MdiActiveDocument;

            this.dwgFile = acDoc.Name;
            this.pdfFile = pdfFile;
            this.outputDir = Path.GetDirectoryName(this.pdfFile);
            this.dsdFile = Path.ChangeExtension(this.pdfFile, "dsd");
            this.layouts = layouts;
        }

        private void Publish()
        {
            if (TryCreateDSD())
            {
                Publisher publisher = AcAp.Publisher;
                PlotProgressDialog plotDlg = new PlotProgressDialog(false, this.sheetNum, true);
                publisher.PublishDsd(this.dsdFile, plotDlg);
                plotDlg.Destroy();
                File.Delete(this.dsdFile);
            }
        }

        private bool TryCreateDSD()
        {
            using (DsdData dsd = new DsdData())
            using (DsdEntryCollection dsdEntries = CreateDsdEntryCollection(this.layouts))
            {
                if (dsdEntries == null || dsdEntries.Count <= 0) return false;

                if (!Directory.Exists(this.outputDir))
                {
                    Directory.CreateDirectory(this.outputDir);
                }

                this.sheetNum = dsdEntries.Count;

                dsd.SetDsdEntryCollection(dsdEntries);

                dsd.SetUnrecognizedData("PwdProtectPublishedDWF", "FALSE");
                dsd.SetUnrecognizedData("PromptForPwd", "FALSE");
                dsd.SheetType = SheetType.MultiDwf;
                dsd.NoOfCopies = 1;
                dsd.DestinationName = this.pdfFile;
                dsd.IsHomogeneous = false;
                dsd.LogFilePath = Path.Combine(this.outputDir, LOG);

                PostProcessDSD(dsd);

                return true;
            }
        }

        private DsdEntryCollection CreateDsdEntryCollection(IEnumerable<Layout> layouts)
        {
            DsdEntryCollection entries = new DsdEntryCollection();

            foreach (Layout layout in layouts)
            {
                AssignPageSetupToLayout(layout.LayoutName);

                DsdEntry dsdEntry = new DsdEntry();
                dsdEntry.DwgName = this.dwgFile;
                dsdEntry.Layout = layout.LayoutName;
                dsdEntry.Title = Path.GetFileNameWithoutExtension(this.dwgFile) + "-" + layout.LayoutName;

                //dsdEntry.Nps = pgSetupName; // layout.TabOrder.ToString();
                //dsdEntry.NpsSourceDwg = this.dwgFile;

                entries.Add(dsdEntry);
            }
            return entries;
        }

        private void PostProcessDSD(DsdData dsd)
        {
            string str, newStr;
            string tmpFile = Path.Combine(this.outputDir, "temp.dsd");

            dsd.WriteDsd(tmpFile);

            using (StreamReader reader = new StreamReader(tmpFile, Encoding.Default))
            using (StreamWriter writer = new StreamWriter(this.dsdFile, false, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    if (str.Contains("Has3DDWF"))
                    {
                        newStr = "Has3DDWF=0";
                    }
                    else if (str.Contains("OriginalSheetPath"))
                    {
                        newStr = "OriginalSheetPath=" + this.dwgFile;
                    }
                    else if (str.Contains("Type"))
                    {
                        newStr = "Type=6";
                    }
                    else if (str.Contains("OUT"))
                    {
                        newStr = "OUT=" + this.outputDir;
                    }
                    else if (str.Contains("IncludeLayer"))
                    {
                        newStr = "IncludeLayer=TRUE";
                    }
                    else if (str.Contains("PromptForDwfName"))
                    {
                        newStr = "PromptForDwfName=FALSE";
                    }
                    else if (str.Contains("LogFilePath"))
                    {
                        newStr = "LogFilePath=" + Path.Combine(this.outputDir, LOG);
                    }
                    else
                    {
                        newStr = str;
                    }
                    writer.WriteLine(newStr);
                }
            }
            File.Delete(tmpFile);
        }
        
        public void AssignPageSetupToLayout(string layName)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayoutManager acLayoutMgr = LayoutManager.Current;

                Layout acLayout = acTrans.GetObject(acLayoutMgr.GetLayoutId(layName), OpenMode.ForRead) as Layout; //acLayoutMgr.CurrentLayout),

                DBDictionary acPlSet = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead) as DBDictionary;

                if (acPlSet.Contains(pgSetupName) == true)
                {
                    PlotSettings plSet = acPlSet.GetAt(pgSetupName).GetObject(OpenMode.ForRead) as PlotSettings;

                    acLayout.UpgradeOpen();
                    acLayout.CopyFrom(plSet);

                    acTrans.Commit();
                }
                else
                {
                    acTrans.Abort();
                }
            }

            acDoc.Editor.Regen();
        }
                
        public void CreateOrEditPageSetup(string driver, string paper, double org1, double org2, bool PlotLineWeights = false)
        {         
            Document acDoc = AcAp.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary plSets = acTrans.GetObject(acCurDb.PlotSettingsDictionaryId, OpenMode.ForRead) as DBDictionary;
                DBDictionary vStyles = acTrans.GetObject(acCurDb.VisualStyleDictionaryId, OpenMode.ForRead) as DBDictionary;

                PlotSettings acPlSet = default(PlotSettings);
                bool createNew = false;
                
                LayoutManager acLayoutMgr = LayoutManager.Current;

                Layout acLayout = acTrans.GetObject(acLayoutMgr.GetLayoutId(acLayoutMgr.CurrentLayout), OpenMode.ForRead) as Layout;
                             
                if (plSets.Contains(pgSetupName) == false)
                {
                    createNew = true;

                    acPlSet = new PlotSettings(acLayout.ModelType);
                    acPlSet.CopyFrom(acLayout);

                    acPlSet.PlotSettingsName = pgSetupName;
                    acPlSet.AddToPlotSettingsDictionary(acCurDb);
                    acTrans.AddNewlyCreatedDBObject(acPlSet, true);
                }
                else
                {
                    acPlSet = plSets.GetAt(pgSetupName).GetObject(OpenMode.ForWrite) as PlotSettings;
                }
                
                try
                {
                    PlotSettingsValidator acPlSetVdr = PlotSettingsValidator.Current;

                    acPlSetVdr.SetPlotConfigurationName(acPlSet, driver, paper);

                    if (acLayout.ModelType == false)
                    {
                        acPlSetVdr.SetPlotType(acPlSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Layout);
                    }
                    else
                    {
                        acPlSetVdr.SetPlotType(acPlSet, Autodesk.AutoCAD.DatabaseServices.PlotType.Extents);

                        acPlSetVdr.SetPlotCentered(acPlSet, true);
                    }

                    // Use SetPlotWindowArea with PlotType.Window
                    //acPlSetVdr.SetPlotWindowArea(plSet,
                    //                             new Extents2d(New Point2d(0.0, 0.0),
                    //                             new Point2d(9.0, 12.0)));

                    // Use SetPlotViewName with PlotType.View
                    //acPlSetVdr.SetPlotViewName(plSet, "MyView");
                    
                    acPlSetVdr.SetPlotOrigin(acPlSet, new Point2d(org1 * 25.4, org2 * 25.4));
                    
                    acPlSetVdr.SetUseStandardScale(acPlSet, true);
                    acPlSetVdr.SetStdScaleType(acPlSet, StdScaleType.StdScale1To1);
                    acPlSetVdr.SetPlotPaperUnits(acPlSet, PlotPaperUnit.Inches);
                    
                    acPlSet.ShowPlotStyles = true;
                    
                    // Rebuild plotter, plot style, and canonical media lists 
                    // (must be called before setting the plot style)
                    acPlSetVdr.RefreshLists(acPlSet);
                              
                    acPlSet.ShadePlot = PlotSettingsShadePlotType.AsDisplayed;

                    acPlSet.ShadePlotResLevel = ShadePlotResLevel.Normal;

                    acPlSet.PrintLineweights = PlotLineWeights;
                    acPlSet.ScaleLineweights = false;

                    acPlSet.PlotTransparency = true;
                    acPlSet.PlotPlotStyles = true;
                    acPlSet.DrawViewportsFirst = true;
                    //acPlSet.CurrentStyleSheet

                    // Use only on named layouts - Hide paperspace objects option
                    // plSet.PlotHidden = true;
                                       
                    acPlSetVdr.SetPlotRotation(acPlSet, PlotRotation.Degrees090);
                    
                    if (acCurDb.PlotStyleMode == true)
                    {
                        acPlSetVdr.SetCurrentStyleSheet(acPlSet, "acad.ctb");
                    }
                    else
                    {
                        acPlSetVdr.SetCurrentStyleSheet(acPlSet, "acad.stb");
                    }

                    acPlSetVdr.SetZoomToPaperOnUpdate(acPlSet, true);
                }
                catch (Autodesk.AutoCAD.Runtime.Exception es)
                {
                    System.Windows.Forms.MessageBox.Show(es.Message);
                }

                acTrans.Commit();

                if (createNew == true)
                {
                    acPlSet.Dispose();
                }
            }
        }
        
    }


}