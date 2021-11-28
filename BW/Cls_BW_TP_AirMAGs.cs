using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    static class Cls_BW_TP_AirMAGs
    {
        static readonly List<Cls_BW_AirMagnetReportInfo> AirMagReportInfoLst = new List<Cls_BW_AirMagnetReportInfo>();
        static readonly List<Cls_BW_AirMagnetAps> AirMagXmlAPsLst = new List<Cls_BW_AirMagnetAps>();
        static readonly SortedList<string, Handle> AirMagLblLst = new SortedList<string, Handle>();

        #region Air Magnet Xml File

        /// <summary>
        /// test / not used 
        /// </summary>
        /// <returns></returns>
        public static List<Cls_BW_AirMagnetAps> BtnTstXML_Click_Sub()
        {
            Cls_BW_Utility.Gw_InsertFile(@"K:\Engineering\3M\2019\wap icon 2019.dwg");

            var myList = AirMagXmlAPsLst.Aggregate(
            new List<Cls_BW_AirMagnetAps>(),
            (newList, element) =>
            {
                if (element.Mount == "W-RAM")
                {
                    element.Mount = "W-RAM-W";
                    element.Antenna = element.Antenna.Replace("2800i", "2802i");
                    element.Note = element.Note.Replace("W-RAM", "W-RAM-W");
                }

                if (element.Mount == "C-DC")
                {
                    element.Antenna = element.Antenna.Replace("2800i", "2802i");
                }

                if (element.Mount == "O-RAM")
                {
                    element.Antenna = element.Antenna.Replace("2800i", "2802i");
                }

                newList.Add(element);
                return newList;
            });
            return myList;
        }


        public static void BtnAirMagRptReadXmlFile_Click_Sub(DataGridView dgvAirMagXmlFile, DataGridView dgvAirMagXmlFileRepInfo)
        {
            //      BW.Cls_BW_Utility.applicationSettings.LoadAppSettings();

            XmlDocument doc = new XmlDocument();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //      openFileDialog1.InitialDirectory = BW.Cls_BW_Utility.applicationSettings.BW_AirMagRepPath;  //@"K:\3M\";

            openFileDialog1.Filter = "xml files | *.xml"; //|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dgvAirMagXmlFile.DataSource = null;
                dgvAirMagXmlFileRepInfo.DataSource = null;

                doc.Load(openFileDialog1.FileName);
            }
            else
            {
                return;
            }

            //doc.Load(@"C:\Users\gwilliams\Desktop\CP_170815\3M_-_Hebron_KY_-_Pre_Survey_XML.xml");

            XmlNodeList finalValues = doc.GetElementsByTagName("FormattedReportObjects");

            AirMagReportInfoLst.Clear();
            AirMagXmlAPsLst.Clear();

            Cls_BW_AirMagnetReportInfo clsAmRep = new Cls_BW_AirMagnetReportInfo();
            Cls_BW_AirMagnetAps clsAmAps = new Cls_BW_AirMagnetAps();

            foreach (XmlNode node in finalValues)
            {
                if (node.HasChildNodes & node.InnerText.StartsWith("ReportTitle1"))
                {
                    clsAmRep = new Cls_BW_AirMagnetReportInfo();
                    var info = node.ChildNodes;
                    clsAmRep.RepTitle = info[0].LastChild.InnerXml;
                    clsAmRep.Surveyor = info[1].LastChild.InnerXml;
                    clsAmRep.Location = info[2].LastChild.InnerXml;
                    clsAmRep.TimeDate = info[3].LastChild.InnerXml;
                    clsAmRep.Company = info[4].LastChild.InnerXml;
                    FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);
                    clsAmRep.FileName = fileInfo.Name;
                    clsAmRep.PathName = fileInfo.DirectoryName;
                    AirMagReportInfoLst.Add(clsAmRep);
                    continue;
                }

                if (node.HasChildNodes & node.InnerText.StartsWith("HEIGHT1"))
                {
                    clsAmAps = new Cls_BW_AirMagnetAps();
                    var info = node.ChildNodes;
                    var aff = info[0].ChildNodes[1].InnerXml;
                    var ap = info[4].LastChild.InnerXml;
                    ap = ap.Replace("(AN)", "");
                    string[] s = ap.Split('-');
                    // Console.WriteLine("AP: " + "AP-" + int.Parse(s[1]).ToString().PadLeft(3,'0'));

                    if (s.Length == 1 | s.Length == 4)
                    {
                        continue;
                    }

                    clsAmAps.Wap = "AP-" + int.Parse(s[1]).ToString().PadLeft(3, '0');
                    clsAmAps.Height = aff;
                    AirMagXmlAPsLst.Add(clsAmAps);
                    continue;
                }
                if (node.HasChildNodes & node.InnerText.StartsWith("Antenna2"))
                {
                    var info = node.ChildNodes;
                    var ant = info[4].LastChild.InnerXml;
                    // Console.WriteLine("Antenna: " + ant);
                    clsAmAps.Antenna = ant;
                    continue;
                }
                if (node.HasChildNodes & node.InnerText.StartsWith("Note2"))
                {
                    var info = node.ChildNodes;
                    var note = info[1].LastChild.InnerXml;
                    note = note.Replace("$---$", "");
                    Console.WriteLine("Note: " + note);
                    string[] mnt = note.Split(' ');
                    // Console.WriteLine("Mount: " + mnt[0]);
                    clsAmAps.Note = note;
                    clsAmAps.Mount = mnt[0];
                    continue;
                }

            }

            AirMagXmlAPsLst.Sort((x, y) => x.Wap.CompareTo(y.Wap));
            dgvAirMagXmlFile.DataSource = AirMagXmlAPsLst;

            dgvAirMagXmlFile.Rows[0].Selected = true;
            dgvAirMagXmlFile.CurrentCell = dgvAirMagXmlFile[0, dgvAirMagXmlFile.CurrentCell.RowIndex];

            dgvAirMagXmlFileRepInfo.DataSource = AirMagReportInfoLst;
            Uc_BW_SiteInfo.Cls.AttSite = AirMagReportInfoLst[0].Location;
            //btnSetValues_Click();

            //BW.Cls_BW_Utility.applicationSettings.BW_AirMagRepPath = openFileDialog1.InitialDirectory;
            //BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }


        #endregion



        public static void BtnZoomToAmLbl_Click_Sub(DataGridView dgvAirMagLbls)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;

            ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(database, dgvAirMagLbls[1, dgvAirMagLbls.CurrentRow.Index].Value.ToString());

            ObjectId[] Ids;
            Ids = new ObjectId[1];
            Ids[0] = id;
            ObjectIdCollection idCol = new ObjectIdCollection(Ids);
            Cls_BW_Utility.ZoomObjects(idCol);
        }

        private class MyMessageFilter : IMessageFilter
        {
            const int WM_KEYDOWN = 0x0100;
            public bool bCanceled = false;

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_KEYDOWN)
                {
                    // Check for the Escape keypress
                    Keys kc = (Keys)(int)m.WParam & Keys.KeyCode;

                    if (m.Msg == WM_KEYDOWN && kc == Keys.Escape)
                    {
                        bCanceled = true;
                    }

                    // Return true to filter all keypresses
                    return true;
                }

                // Return false to let other messages through
                return false;
            }

        }


        public static void BtnInsertAP_Click_Sub(bool chkBxAPsAutoInsByAirMagTxt, DataGridView dgvAirMagXmlFile, DataGridView dgvAirMagLbls)
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            MyMessageFilter filter = new MyMessageFilter();
            System.Windows.Forms.Application.AddMessageFilter(filter);

            if (chkBxAPsAutoInsByAirMagTxt)
            {
                int indx = 0;

                using (var DocLck = doc.LockDocument())
                using (var tr = doc.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    //for each label
                    foreach (KeyValuePair<string, Handle> lbl in AirMagLblLst)
                    {
                        ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(db, lbl.Value.ToString());

                        DBText ent = (DBText)tr.GetObject(id, OpenMode.ForRead);

                        ptForWapJig = ent.AlignmentPoint; //??
                        ptForWapJig = new Point3d(ptForWapJig.X, ptForWapJig.Y - 17.3629, ptForWapJig.Z);

                        InsertingABlock("wap", tr, db, bt, ed, ms);

                        PromptSelectionResult psr = ed.SelectLast();

                        Cls_BW_TP_AirMAGs.FillAPsAtts(indx, psr, tr, db);

                        SelectionSet acSSet = psr.Value;
                        ObjectId[] ids = { };
                        ids = acSSet.GetObjectIds();

                        BlockReference lastBlk = tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
                        ptForWapJig = lastBlk.Position;

                        InsertingABlock("wap data 2 dual", tr, db, bt, ed, ms);

                        psr = ed.SelectLast();

                        Cls_BW_TP_AirMAGs.FillAPsAtts(indx, psr, tr, db);

                        indx++;
                    }
                    tr.Commit();
                }

            }
            else
            {
                // picking each circle
                while (true)
                {
                    System.Windows.Forms.Application.DoEvents();

                    if (filter.bCanceled == true)
                    {
                        ed.WriteMessage("\nLoop cancelled.");
                        break;
                    }

                    if (dgvAirMagXmlFile.CurrentRow.Index == dgvAirMagXmlFile.RowCount - 1 & dgvAirMagXmlFile.CurrentRow != null)
                    {
                        ed.WriteMessage("\nLast AP will be inserted...");
                        filter.bCanceled = true;
                    }

                    if (dgvAirMagLbls.DataSource != null)
                    {
                        BtnZoomToAmLbl_Click_Sub(dgvAirMagLbls);
                    }

                    InsertBlockwithJigInvidualAirMag(dgvAirMagXmlFile, dgvAirMagLbls);

                }
            }

            System.Windows.Forms.Application.RemoveMessageFilter(filter);
        }




        public static void FillAPsAtts(int indx,
            PromptSelectionResult acSSPrompt,
            Transaction transaction,
            Autodesk.AutoCAD.DatabaseServices.Database database)
        {
            SelectionSet acSSet = acSSPrompt.Value;
            ObjectId[] ids = { };

            ids = acSSet.GetObjectIds();

            foreach (ObjectId id1 in ids)
            {
                BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);

                if (blkRef.Name == "wap data 2 dual")
                {
                    var clsAttsWapData2Dual = new Cls_BW_WapsData2Dual_Atts();
                    clsAttsWapData2Dual.Handle = blkRef.Handle;

                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                        PutAttsWapData2Dual(indx, attRef);
                    }
                }

                if (blkRef.Name == "wap")
                {
                    var clsAttsWaps = new Cls_BW_Waps_Atts();
                    clsAttsWaps.Handle = blkRef.Handle;

                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);

                        PutAttsWap(indx, attRef);
                    }
                }

            }
        }

        private static void PutAttsWap(int indx, AttributeReference attRef)
        {          
            switch (attRef.Tag)
            {
                case "WAP":
                    attRef.TextString = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    break;
                case "APNUMBER":
                    attRef.TextString = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    break;
                case "LABEL1":
                    string s = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    attRef.TextString = s + "A/" + s + "B";
                    break;
                case "ANTENNA":
                    attRef.TextString = AirMagXmlAPsLst[indx].Antenna;
                    break;
                case "MOUNT":
                    if (AirMagXmlAPsLst[indx].Note != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Note;
                    }
                    break;
                case "WRLSSPECIALFEED1":
                    if (AirMagXmlAPsLst[indx].Mount != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Mount;
                    }
                    break;
                case "DISPLAYCODE":
                    if (AirMagXmlAPsLst[indx].Mount != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Mount;
                    }
                    break;
                default:
                    break;
            }            
        }

        private static void PutAttsWapData2Dual(int indx, AttributeReference attRef)
        {
            switch (attRef.Tag)
            {
                case "WAP":
                    attRef.TextString = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    break;
                case "APNUMBER":
                    attRef.TextString = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    break;
                case "LABEL1":
                    string s = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    attRef.TextString = s + "A/" + s + "B";
                    break;
                case "SITE":
                    attRef.TextString = Uc_BW_SiteInfo.Cls.AttSite;
                    break;
                case "BLDG":
                    attRef.TextString = Uc_BW_SiteInfo.Cls.AttBuilding;
                    break;
                case "FLOOR":
                    attRef.TextString = Uc_BW_SiteInfo.Cls.AttFloor;
                    break;
                case "CLOSETINFO":
                    attRef.TextString = Uc_BW_SiteInfo.Cls.AttCloset;
                    break;
                case "ANTENNA":
                    attRef.TextString = AirMagXmlAPsLst[indx].Antenna;
                    break;
                case "MOUNT":
                    if (AirMagXmlAPsLst[indx].Note != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Note;
                    }                    
                    break;
                case "WRLSSPECIALFEED1":
                    if (AirMagXmlAPsLst[indx].Mount != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Mount;
                    }
                    break;
                case "AFF":
                    attRef.TextString = AirMagXmlAPsLst[indx].Height;
                    break;

                case "AMREPORTNAME":
                    attRef.TextString = AirMagReportInfoLst[0].RepTitle;
                    break;
                case "AMREPORTDATE":
                    attRef.TextString = AirMagReportInfoLst[0].TimeDate;
                    break;
                case "AMAPNUM":
                    attRef.TextString = AirMagXmlAPsLst[indx].Wap.Split('-')[1];
                    break;
                case "DISPLAYCODE":
                    if (AirMagXmlAPsLst[indx].Mount != null)
                    {
                        attRef.TextString = AirMagXmlAPsLst[indx].Mount;
                    }
                    break;

                default:
                    break;
            }            
        }



        private static Document AirMagDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        private static Autodesk.AutoCAD.DatabaseServices.Database AirMagDb = AirMagDoc.Database;
        private static ObjectIdCollection idCol;

        #region AirMagnets Import block

        public static void BtnAirMagLbls_Click_Sub()
        {
            AirMagDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            AirMagDb = AirMagDoc.Database;
            Editor ed = AirMagDoc.Editor;

            PromptPointResult pt = ed.GetPoint("\nPick insertion point for Air Mags: ");

            TypedValue[] filterlist = new TypedValue[2];
            filterlist[0] = new TypedValue(0, "CIRCLE,TEXT");
            //8 = DxfCode.LayerName
            filterlist[1] = new TypedValue(8, "AIRMAGNET-APs");
            SelectionFilter acSelFtr = new SelectionFilter(filterlist);

            PromptSelectionResult acSSPrompt = ed.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                ObjectId[] ids = acSSPrompt.Value.GetObjectIds();

                idCol = new ObjectIdCollection(ids);

                CreateBlock(pt);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        public static void CreateBlock(PromptPointResult pt)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            using (DocumentLock acLckDoc = doc.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                    string blkName = "";

                    string blockNameBinary = DateTime.Now.ToBinary().ToString();

                    SymbolUtilityServices.ValidateSymbolName(blockNameBinary, false);

                    blkName = blockNameBinary;

                    BlockTableRecord btr = new BlockTableRecord();
                    btr.Name = blkName;

                    btr.Origin = pt.Value; // set insertion point of block

                    bt.UpgradeOpen();
                    ObjectId btrId = bt.Add(btr);
                    tr.AddNewlyCreatedDBObject(btr, true);

                    DBObjectCollection ents = new DBObjectCollection();
                    DBObjectCollection entsTxt = new DBObjectCollection();

                    foreach (ObjectId id in idCol)
                    {
                        Entity ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
                        Entity nEnt = (Entity)ent.Clone();

                        if (nEnt is Circle)
                        {
                            nEnt.ColorIndex = 4;
                            Circle c = (Circle)nEnt;
                            c.Radius = 36;
                        }
                        if (nEnt is DBText)
                        {
                            nEnt.ColorIndex = 4;
                            DBText t = (DBText)nEnt;
                            t.Height = 24;
                            entsTxt.Add(nEnt);
                        }

                        ents.Add(nEnt);
                    }

                    foreach (Entity ent in ents)
                    {
                        btr.AppendEntity(ent);
                        tr.AddNewlyCreatedDBObject(ent, true);
                    }

                    // Add a block reference to the model space
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    BlockReference br = new BlockReference(pt.Value, btrId); //Point3d.Origin

                    ms.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);

                    tr.Commit();

                    ed.WriteMessage("\nCreated block named \"{0}\" containing {1} entities.", blkName, ents.Count);
                }
            }
        }

        public static void ImportBlocks()
        {
            Document destDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor destEd = destDoc.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database destDb = destDoc.Database;

            try
            {
                ObjectIdCollection blockIds = new ObjectIdCollection();

                using (DocumentLock AirMagLckDoc = AirMagDoc.LockDocument())
                {
                    using (Transaction AirMagTrans = AirMagDb.TransactionManager.StartTransaction())
                    {
                        using (DocumentLock destLckDoc = destDoc.LockDocument())
                        {
                            using (Transaction destTrans = destDoc.TransactionManager.StartTransaction())
                            {
                                BlockTable bt = (BlockTable)AirMagTrans.GetObject(AirMagDb.BlockTableId, OpenMode.ForRead, false);

                                foreach (ObjectId btrId in bt)
                                {
                                    BlockTableRecord btr = (BlockTableRecord)AirMagTrans.GetObject(btrId, OpenMode.ForRead, false);

                                    if (!btr.IsAnonymous && !btr.IsLayout)
                                    {
                                        blockIds.Add(btrId);
                                    }

                                    btr.Dispose();
                                }
                                destTrans.Commit();
                            }

                            IdMapping mapping = new IdMapping();

                            destDb.WblockCloneObjects(blockIds, destDb.BlockTableId, mapping, DuplicateRecordCloning.Replace, false);

                            destEd.WriteMessage("\nCopied " + blockIds.Count.ToString() + " block definitions from " + " to the current drawing.");
                        }
                        AirMagTrans.Commit();
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                destEd.WriteMessage("\nError during copy: " + ex.Message);
            }
        }


        #endregion

        const string annoScalesDict = "ACDB_ANNOTATIONSCALES";


        private static Point3d ptForWapJig;



        #region Insert Ap and fill with xml info


        public static void InsertingABlock(
            string BlkName,
            Transaction tr,
            Autodesk.AutoCAD.DatabaseServices.Database db,
            BlockTable bt,
            Editor ed,
            BlockTableRecord ms
            )
        {
            if (!bt.Has(BlkName))
            {
                ed.WriteMessage("\nBlock \"" + BlkName + "\" not found.");
                return;
            }

            var btrBlkOne = (BlockTableRecord)tr.GetObject(bt[BlkName], OpenMode.ForRead);

            Point3d nPt = new Point3d(ptForWapJig.X, ptForWapJig.Y + 17.3629, ptForWapJig.Z);

            var br = new BlockReference(nPt, btrBlkOne.ObjectId);
            br.TransformBy(ed.CurrentUserCoordinateSystem);
            ms.AppendEntity(br);
            tr.AddNewlyCreatedDBObject(br, true);

            if (btrBlkOne.Annotative == AnnotativeStates.True)
            {
                var ocm = db.ObjectContextManager;
                var occ = ocm.GetContextCollection(annoScalesDict);
                br.AddContext(occ.CurrentContext);
            }
            else
            {
                br.ScaleFactors = new Scale3d(br.UnitFactor);
            }

            if (btrBlkOne.HasAttributeDefinitions)
            {
                foreach (ObjectId id in btrBlkOne)
                {
                    var obj = tr.GetObject(id, OpenMode.ForRead);

                    var ad = obj as AttributeDefinition;

                    if (ad != null && !ad.Constant)
                    {
                        var ar = new AttributeReference();

                        ar.SetAttributeFromBlock(ad, br.BlockTransform);
                        ar.TextString = ad.TextString;

                        var arId = br.AttributeCollection.AppendAttribute(ar);

                        tr.AddNewlyCreatedDBObject(ar, true);
                    }

                }
            }

        }







        class BlockJig : EntityJig
        {
            // Member variables
            private Matrix3d _ucs;
            private Point3d _pos;
            private Dictionary<ObjectId, ObjectId> _atts;
            private Transaction _tr;

            // Constructor
            public BlockJig(
              Matrix3d ucs,
              Transaction tr,
              BlockReference br,
              Dictionary<ObjectId, ObjectId> atts
            ) : base(br)
            {
                _ucs = ucs;
                _pos = br.Position;
                _atts = atts;
                _tr = tr;
            }


            protected override bool Update()
            {
                var br = (BlockReference)Entity;

                // Transform to the current UCS
                br.Position = _pos.TransformBy(_ucs);
                if (br.AttributeCollection.Count > 0)
                {
                    foreach (ObjectId id in br.AttributeCollection)
                    {
                        var obj = _tr.GetObject(id, OpenMode.ForRead);
                        var ar = obj as AttributeReference;

                        if (ar != null)
                        {
                            ar.UpgradeOpen();

                            // Open the associated attribute definition
                            var defId = _atts[ar.ObjectId];
                            var obj2 = _tr.GetObject(defId, OpenMode.ForRead);
                            var ad = (AttributeDefinition)obj2;
                            // Use it to set positional information on the
                            // reference
                            ar.SetAttributeFromBlock(ad, br.BlockTransform);
                            ar.AdjustAlignment(br.Database);
                        }
                    }
                }
                return true;
            }


            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                var opts = new JigPromptPointOptions("\nSelect insertion point:");

                opts.BasePoint = Point3d.Origin;
                opts.UserInputControls = UserInputControls.NoZeroResponseAccepted;

                var ppr = prompts.AcquirePoint(opts);
                var ucsPt = ppr.Value.TransformBy(_ucs.Inverse());

                if (_pos == ucsPt)
                    return SamplerStatus.NoChange;
                _pos = ucsPt;
                return SamplerStatus.OK;
            }

            public PromptStatus Run()
            {
                var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return PromptStatus.Error;

                return doc.Editor.Drag(this).Status;
            }
        }


        public static void InsertBlockwithJigInvidualAirMag(DataGridView dgvAirMagXmlFile, DataGridView dgvAirMagLbls)
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            string BlkOne = "wap";

            BlockTable bt;
            BlockTableRecord ms;

            using (var DocLck = doc.LockDocument())
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                if (!bt.Has(BlkOne))
                {
                    ed.WriteMessage("\nBlock \"" + BlkOne + "\" not found.");
                    return;
                }

                ms = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                var btrBlkOne = (BlockTableRecord)tr.GetObject(bt[BlkOne], OpenMode.ForRead);

                var br = new BlockReference(new Point3d(), btrBlkOne.ObjectId);
                br.TransformBy(ed.CurrentUserCoordinateSystem);
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                if (btrBlkOne.Annotative == AnnotativeStates.True)
                {
                    var ocm = db.ObjectContextManager;
                    var occ = ocm.GetContextCollection(annoScalesDict);
                    br.AddContext(occ.CurrentContext);
                }
                else
                {
                    br.ScaleFactors = new Scale3d(br.UnitFactor);
                }

                var atts = new Dictionary<ObjectId, ObjectId>();
                if (btrBlkOne.HasAttributeDefinitions)
                {
                    foreach (ObjectId id in btrBlkOne)
                    {
                        var obj = tr.GetObject(id, OpenMode.ForRead);

                        var ad = obj as AttributeDefinition;

                        if (ad != null && !ad.Constant)
                        {
                            var ar = new AttributeReference();

                            ar.SetAttributeFromBlock(ad, br.BlockTransform);
                            ar.TextString = ad.TextString;

                            var arId = br.AttributeCollection.AppendAttribute(ar);

                            tr.AddNewlyCreatedDBObject(ar, true);

                            atts.Add(arId, ad.ObjectId);
                        }

                    }
                }

                var jig =
                  new BlockJig(
                    ed.CurrentUserCoordinateSystem, tr, br, atts
                  );

                if (jig.Run() != PromptStatus.OK)
                    return;


                PromptSelectionResult psr = ed.SelectLast();

                Cls_BW_TP_AirMAGs.FillAPsAtts(dgvAirMagXmlFile.CurrentRow.Index, psr, tr, db);

                SelectionSet acSSet = psr.Value;
                ObjectId[] ids = { };
                ids = acSSet.GetObjectIds();

                BlockReference lastBlk = tr.GetObject(ids[0], OpenMode.ForRead) as BlockReference;
                ptForWapJig = lastBlk.Position;

                InsertingABlock("wap data 2 dual", tr, db, bt, ed, ms);

                psr = ed.SelectLast();

                Cls_BW_TP_AirMAGs.FillAPsAtts(dgvAirMagXmlFile.CurrentRow.Index, psr, tr, db);

                tr.Commit();

                if (dgvAirMagXmlFile.CurrentRow.Index < dgvAirMagXmlFile.RowCount - 1 & dgvAirMagXmlFile.CurrentRow != null)
                {
                    dgvAirMagXmlFile.Rows[dgvAirMagXmlFile.CurrentRow.Index + 1].Selected = true;
                    dgvAirMagXmlFile.CurrentCell = dgvAirMagXmlFile[0, dgvAirMagXmlFile.CurrentCell.RowIndex + 1];

                    if (dgvAirMagLbls.DataSource != null)
                    {
                        dgvAirMagLbls.Rows[dgvAirMagLbls.CurrentRow.Index + 1].Selected = true;
                        dgvAirMagLbls.CurrentCell = dgvAirMagLbls[0, dgvAirMagLbls.CurrentCell.RowIndex + 1];
                    }
                }
                else
                    ed.WriteMessage("Last AP...");

            }
        }






        public static void BtnGetAirMagLblsForAP_Insert_Click_Sub(bool chkBxMoveAirMagLables, DataGridView dgvAirMagLbls)
        {
            Document Doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database Db = Doc.Database;
            Editor ed = Doc.Editor;

            TypedValue[] filterlist = new TypedValue[2];
            filterlist[0] = new TypedValue(0, "TEXT");
            //8 = DxfCode.LayerName
            filterlist[1] = new TypedValue(8, "AIRMAGNET-APs");
            SelectionFilter acSelFtr = new SelectionFilter(filterlist);

            PromptSelectionResult prmt = ed.GetSelection(acSelFtr);

            if (prmt.Status == PromptStatus.OK)
            {
                AirMagLblLst.Clear();
                ObjectId[] ids = prmt.Value.GetObjectIds();
                ObjectIdCollection idCol = new ObjectIdCollection(ids);

                using (Transaction tr = Db.TransactionManager.StartTransaction())
                {
                    //DBObjectCollection ents = new DBObjectCollection();                        

                    foreach (ObjectId id in idCol)
                    {
                        DBText t = (DBText)tr.GetObject(id, OpenMode.ForRead);
                        string[] lbl = t.TextString.Split('(');
                        string[] apNum = lbl[0].Split('-');
                        string apLbl = "AP-" + apNum[1].PadLeft(3, '0');

                        Cls_BW_TP_AirMAGs.AirMagLblLst.Add(apLbl, t.Handle);
                    }
                    tr.Commit();
                }

                if (chkBxMoveAirMagLables)
                {
                    Autodesk.AutoCAD.Internal.Utils.SelectObjects(ids);
                }

                // LstAtts_wap_data_2_dual.Sort((x, y) => x.Wap.CompareTo(y.Wap));

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = Cls_BW_TP_AirMAGs.AirMagLblLst;
                dgvAirMagLbls.DataSource = bindingSource;

                if (dgvAirMagLbls.CurrentRow.Index < dgvAirMagLbls.RowCount - 1 & dgvAirMagLbls.CurrentRow != null)
                {
                    dgvAirMagLbls.Rows[0].Selected = true;
                    dgvAirMagLbls.CurrentCell = dgvAirMagLbls[0, 0];
                }
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        #endregion
    }
}
