using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using OfficeOpenXml;

namespace MyFirstProject.BW
{
    public static class Cls_BW_Utility
    {
        #region MyRegion

        public static IEnumerable<int> FindMissingNums(List<string> list)
        {            
            var lints = list.Select(s => int.Parse(s)).ToList();
            var result = Enumerable.Range(0, lints.Count - 1).Except(lints);
            return result;
        }


        #endregion
        #region AppSettings

        public static ApplicationSettings applicationSettings = new ApplicationSettings();

        public class ApplicationSettings
        {
            //   =
            //@"C:\Users\gwilliams\Desktop\CP_170815\WAP Autofill 4.23.18.xlsx"
            //  @"K:\3M\2019\Autofill BOM 8.9.18.xlsx";
            // @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\MMM\009\Internal Project Documents\Site Documents\03 ENG Master Documents\Autofill BOM for CAT6 and 6A V1.xlsx"
            //openFileDialog1.FileName
            private string m_MMM_BomFileName;

            public string MMM_BomFileName
            {
                get { return m_MMM_BomFileName; }
                set
                {
                    if (value != m_MMM_BomFileName)
                    {
                        m_MMM_BomFileName = value;
                    }
                }
            }

            private Point m_UPS_formLocation;
            private Point m_USB_formLocation;
            private Point m_WLG_formLocation;
            private Point m_THD_formLocation;
            private Point m_TGT_formLocation;
            private Point m_BW_formLocation;
            private Point m_MMM_formLocation;
            private Point m_AMZ_formLocation;
            private Point m_FDX_formLocation;


            public Point UPS_FormLocation
            {
                get { return m_UPS_formLocation; }
                set
                {
                    if (value != m_UPS_formLocation)
                    {
                        m_UPS_formLocation = value;
                    }
                }
            }

            public Point USB_FormLocation
            {
                get { return m_USB_formLocation; }
                set
                {
                    if (value != m_USB_formLocation)
                    {
                        m_USB_formLocation = value;
                    }
                }
            }
            public Point WLG_FormLocation
            {
                get { return m_WLG_formLocation; }
                set
                {
                    if (value != m_WLG_formLocation)
                    {
                        m_WLG_formLocation = value;
                    }
                }
            }
            public Point THD_FormLocation
            {
                get { return m_THD_formLocation; }
                set
                {
                    if (value != m_THD_formLocation)
                    {
                        m_THD_formLocation = value;
                    }
                }
            }
            public Point TGT_FormLocation
            {
                get { return m_TGT_formLocation; }
                set
                {
                    if (value != m_TGT_formLocation)
                    {
                        m_TGT_formLocation = value;
                    }
                }
            }
            public Point BW_FormLocation
            {
                get { return m_BW_formLocation; }
                set
                {
                    if (value != m_BW_formLocation)
                    {
                        m_BW_formLocation = value;
                    }
                }
            }
            public Point MMM_FormLocation
            {
                get { return m_MMM_formLocation; }
                set
                {
                    if (value != m_MMM_formLocation)
                    {
                        m_MMM_formLocation = value;
                    }
                }
            }
            public Point FDX_FormLocation
            {
                get { return m_FDX_formLocation; }
                set
                {
                    if (value != m_FDX_formLocation)
                    {
                        m_FDX_formLocation = value;
                    }
                }
            }
            public Point AMZ_FormLocation
            {
                get { return m_AMZ_formLocation; }
                set
                {
                    if (value != m_AMZ_formLocation)
                    {
                        m_AMZ_formLocation = value;
                    }
                }
            }
            
            public void FixAllFormLocations()
            {
                if (Cls_BW_Main.frm_BW_MainForm != null)
                {
                    Cls_BW_Main.frm_BW_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_MMM_Host_Form != null)
                {
                    Cls_BW_Main.frm_MMM_Host_Form.Dispose();
                }
                if (Cls_BW_Main.frm_FDX_MainForm != null)
                {
                    Cls_BW_Main.frm_FDX_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_AMZ_MainForm != null)
                {
                    Cls_BW_Main.frm_AMZ_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_TGT_MainForm != null)
                {
                    Cls_BW_Main.frm_TGT_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_THD_MainForm != null)
                {
                    Cls_BW_Main.frm_THD_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_WLG_MainForm != null)
                {
                    Cls_BW_Main.frm_WLG_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_USB_MainForm != null)
                {
                    Cls_BW_Main.frm_USB_MainForm.Dispose();
                }
                if (Cls_BW_Main.frm_UPS_MainForm != null)
                {
                    Cls_BW_Main.frm_UPS_MainForm.Dispose();
                }

                BW.Cls_BW_Utility.applicationSettings.BW_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.MMM_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.FDX_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.AMZ_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.TGT_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.THD_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.WLG_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.USB_FormLocation = new System.Drawing.Point(0, 0);
                BW.Cls_BW_Utility.applicationSettings.UPS_FormLocation = new System.Drawing.Point(0, 0);

                BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
            }
            
            public void SaveAppSettings()
            {
                StreamWriter myWriter = null;
                XmlSerializer mySerializer = null;
                try
                {
                    mySerializer = new XmlSerializer(
                      typeof(ApplicationSettings));
                    myWriter =
                      new StreamWriter(System.Windows.Forms.Application.LocalUserAppDataPath
                      + @"\myApplication.config", false);

                    mySerializer.Serialize(myWriter, this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (myWriter != null)
                    {
                        myWriter.Close();
                    }
                }

            }

            public bool LoadAppSettings()
            {
                XmlSerializer mySerializer = null;
                FileStream myFileStream = null;
                bool fileExists = false;

                try
                {
                    mySerializer = new XmlSerializer(typeof(ApplicationSettings));
                    FileInfo fi = new FileInfo(System.Windows.Forms.Application.LocalUserAppDataPath + @"\myApplication.config");

                    if (fi.Exists)
                    {
                        myFileStream = fi.OpenRead();

                        applicationSettings = (ApplicationSettings)mySerializer.Deserialize(myFileStream);

                        m_MMM_BomFileName = applicationSettings.m_MMM_BomFileName;

                        m_BW_formLocation = applicationSettings.m_BW_formLocation;
                        m_MMM_formLocation = applicationSettings.m_MMM_formLocation;
                        m_AMZ_formLocation = applicationSettings.m_AMZ_formLocation;
                        m_FDX_formLocation = applicationSettings.m_FDX_formLocation;
                        m_TGT_formLocation = applicationSettings.m_TGT_formLocation;
                        m_THD_formLocation = applicationSettings.m_THD_formLocation;
                        m_WLG_formLocation = applicationSettings.m_WLG_formLocation;
                        m_USB_formLocation = applicationSettings.m_USB_formLocation;
                        m_UPS_formLocation = applicationSettings.m_UPS_formLocation;

                        fileExists = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (myFileStream != null)
                    {
                        myFileStream.Close();
                    }
                }
                return fileExists;
            }
        }

        #endregion


        #region Pi Report (Project Information Word Doc in project folder)

        public static bool CreateFilePiReport<T>(
         FileInfo newFile,
         IEnumerable<T> lst,
         string tabDesc
            )
        {
            try
            {
                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    if (xlPackage.Workbook.Worksheets.Count > 0)
                        xlPackage.Workbook.Worksheets.Delete(1);


                    xlPackage.Workbook.Worksheets.Add(tabDesc);

                    ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[1];

                    //wsWAP.Cells["A1"].Value = clsBomCnts.Wap_C_DC_DropCeilingMount2802i;             

                    PropertyInfo[] pis = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
                    //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    int col = 1;
                    foreach (var n in pis)
                    {
                        wsWAP.Cells[1, col].Value = n.Name;
                        col++;
                    }

                    wsWAP.Cells[2, 1].LoadFromCollection(lst);

                    wsWAP.Column(1).Width = 25;
                    wsWAP.Column(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    wsWAP.Column(2).Width = 25;
                    wsWAP.Column(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    wsWAP.Column(3).Width = 100;
                    wsWAP.Column(3).Style.WrapText = true;
                    wsWAP.Column(4).Width = 50;
                    wsWAP.Column(4).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                    //    wsWAP.Cells.AutoFitColumns();

                    xlPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        #endregion


        #region Attribute Extraction
        
        public static bool CreateFileXlPull<T>(
         FileInfo newFile,
         IEnumerable<T> lst,
         string tabDesc,
         string type = ""
            )
        {
            try
            {
                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    if (xlPackage.Workbook.Worksheets.Count > 0)
                        xlPackage.Workbook.Worksheets.Delete(1);

                    if (type == "")
                    {
                        xlPackage.Workbook.Worksheets.Add(tabDesc);
                    }
                    else
                    {
                        xlPackage.Workbook.Worksheets.Add(tabDesc + " " + type);
                    }

                    ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[1];

                    //wsWAP.Cells["A1"].Value = clsBomCnts.Wap_C_DC_DropCeilingMount2802i;             

                    PropertyInfo[] pis = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
                    //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    int col = 1;
                    foreach (var n in pis)
                    {
                        wsWAP.Cells[1, col].Value = n.Name;
                        col++;
                    }

                    wsWAP.Cells[2, 1].LoadFromCollection(lst);

                    wsWAP.Cells.AutoFitColumns();

                    xlPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        #endregion


        #region checking current space in autocad

        public static bool IsInModel(Document doc)
        {
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            //Editor ed = doc.Editor;

            if (db.TileMode)
                return true;
            else
                return false;
        }

        public static bool IsInLayout(Document doc)
        {
            return !IsInModel(doc);
        }

        public static bool IsInLayoutPaper(Document doc)
        {
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            if (db.TileMode)
                return false;
            else
            {
                if (db.PaperSpaceVportId == ObjectId.Null)
                    return false;
                else if (ed.CurrentViewportObjectId == ObjectId.Null)
                    return false;
                else if (ed.CurrentViewportObjectId == db.PaperSpaceVportId)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsInLayoutViewport(Document doc)
        {
            return IsInLayout(doc) && !IsInLayoutPaper(doc);
        }

        #endregion


        /// <summary>
        /// this was for target
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tb"></param>
        public static void InsertTable(Document doc, Table tb)
        {
            if (doc is null)
            {
                MessageBox.Show("No Drawing open");
                return;
            }

            using (doc.LockDocument())
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                string CurrSpace;
                     
                if (BW.Cls_BW_Utility.IsInModel(doc))
                {
                    CurrSpace = BlockTableRecord.ModelSpace;
                }
                else
                {
                    CurrSpace = BlockTableRecord.PaperSpace;
                }

                BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[CurrSpace], OpenMode.ForWrite);

                btr.AppendEntity(tb);
                tr.AddNewlyCreatedDBObject(tb, true);
                tr.Commit();
            }
        }



        public static ObjectId GetTextStyle(string nam, Document doc, Autodesk.AutoCAD.DatabaseServices.Database db)
        {
            ObjectId txtid = new ObjectId();

            using (doc.LockDocument())
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                TextStyleTable tt = (TextStyleTable)tr.GetObject(db.TextStyleTableId, OpenMode.ForRead, false);

                if (!(tt.Has(nam))) return txtid;

                txtid = tt[nam];
                tr.Abort();
            }

            return txtid;
        }


        public static TypedValue[] CreateFilterListForBlocks(
          List<string> blkNames
        )
        {
            // If we don't have any block names, return null
            if (blkNames.Count == 0)
                return null;

            // If we only have one, return an array of a single value
            if (blkNames.Count == 1)
                return new TypedValue[] {
          new TypedValue(
            (int)DxfCode.BlockName,
           blkNames[0]
          )

        };

            // We have more than one block names to search for...
            // Create a list big enough for our block names plus
            // the containing "or" operators
            List<TypedValue> tvl =
              new List<TypedValue>(blkNames.Count + 2);

            // Add the initial operator
            tvl.Add(
              new TypedValue(
                (int)DxfCode.Operator,
                "<or"
              )
            );

            // Add an entry for each block name, prefixing the
            // anonymous block names with a reverse apostrophe
            foreach (var blkName in blkNames)
            {
                tvl.Add(
                  new TypedValue(
                    (int)DxfCode.BlockName,
                    (blkName.StartsWith("*") ? "`" + blkName : blkName)
                  )
                );
            }

            // Add the final operator
            tvl.Add(
              new TypedValue(
                (int)DxfCode.Operator,
                "or>"
              )
            );
            // Return an array from the list
            return tvl.ToArray();
        }

        public static void MakeLayer(Autodesk.AutoCAD.DatabaseServices.Database database, Transaction transaction, string name)
        {
            LayerTable lt = (LayerTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);

            if (!lt.Has(name))
            {
                LayerTableRecord ltr = new LayerTableRecord();
                ltr.Name = name;
                lt.UpgradeOpen();
                ObjectId ltId = lt.Add(ltr);
                transaction.AddNewlyCreatedDBObject(ltr, true);
            }
        }


        /// <summary>
        /// ShowFormInContainerControl
        /// BW.Cls_BW_Utility.ShowFormInContainerControl(tpLayouts, BW.Cls_BW_Main.frm_BW_MainForm); - check for null
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="frm"></param>        
        public static void ShowFormInContainerControl(Control ctl, Form frm)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            ctl.Controls.Add(frm);
        }


        public static string[] ReturnFilesList()
        {
            string[] s = { "" };

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "dwg files | *.dwg"; //|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog1.FileNames;
            }
            else
                return s;
        }


        public static double Conv_DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double Conv_RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }



        public static void InsertBlock(string FileName, Point3d InsertionPoint, double Rotation = 0)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;

            ObjectId blkid = ObjectId.Null;
            using (Autodesk.AutoCAD.DatabaseServices.Database bdb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true))
            {
                bdb.ReadDwgFile(FileName, System.IO.FileShare.Read, true, "");
                blkid = database.Insert(System.IO.Path.GetFileNameWithoutExtension(FileName), bdb, true);

                using (doc.LockDocument())
                using (Transaction tr = database.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(database.BlockTableId, OpenMode.ForRead);

                    BlockTableRecord btr = default(BlockTableRecord);
                    btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


                    using (btr)
                    {

                        BlockReference bref = new BlockReference(InsertionPoint, blkid);

                        Matrix3d mat = Matrix3d.Identity;

                        bref.TransformBy(mat);

                        bref.Layer = "0";

                        bref.Rotation = Rotation;

                        btr.AppendEntity(bref);

                        tr.AddNewlyCreatedDBObject(bref, true);

                        if (bref.IsDynamicBlock)
                        {

                            DynamicBlockReferencePropertyCollection dynBrefColl = bref.DynamicBlockReferencePropertyCollection;

                            foreach (DynamicBlockReferenceProperty dynBrefProps in dynBrefColl)
                            {
                                if (dynBrefProps.PropertyName.ToUpper() == "VISIBILITY1")
                                {

                                    dynBrefProps.Value = "M36";
                                }
                            }
                        }

                    }
                    tr.Commit();
                }
            }
        }



        public static void BtnZoomToAndSelectBlock_Click_Sub(DataGridView dgv, int colNum)
        {
            ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(
                HostApplicationServices.WorkingDatabase,
                 dgv[colNum, dgv.CurrentRow.Index].Value.ToString());

            ObjectId[] Ids;
            Ids = new ObjectId[1];
            Ids[0] = idD;

            ObjectIdCollection idCol = new ObjectIdCollection(Ids);
            Cls_BW_Utility.ZoomObjects(idCol);
            Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);
        }


        /// <summary>
        /// insert an autocad file into a drawing
        /// </summary>
        /// <param name="dwgFileToImport"></param>
        public static void Gw_InsertFile(string dwgFileToImport)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            ObjectId id;

            string blockName = "testwap";

            using (doc.LockDocument())
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                using (Autodesk.AutoCAD.DatabaseServices.Database tempDb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true))
                {
                    BlockTable bt = (BlockTable)acTrans.GetObject(db.BlockTableId, OpenMode.ForWrite, false);

                    if (!bt.Has(blockName))
                    {
                        if (System.IO.File.Exists(dwgFileToImport))
                        {
                            // 'read in the file into the temp database
                            tempDb.ReadDwgFile(dwgFileToImport, System.IO.FileShare.Read, true, null);
                            //  'insert the tempdb into the current drawing db, id is the new block id
                            id = db.Insert(blockName, tempDb, true);
                        }
                    }
                    else
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Block already exists in drawing!");
                    }
                }
                acTrans.Commit();
            }


        }



        /// <summary>
        /// Returns true if the given Polyline is self-intersecting.
        /// 
        /// This API defines self-intersection as follows:
        /// 
        /// The Polyline is self-intersecting if any of the
        /// following conditions are true:
        /// 
        /// 1.  Any segment intersects any other segment.
        /// 
        /// 2.  Any interior vertex lies directly on any segment.
        ///     or is coincident with any other vertex.
        /// 
        /// 3.  If the polyline is effectively-closed (e.g.,
        ///     the first and last vertices are coincident, but
        ///     the PolyLine's Closed property is false), the
        ///     polyline is considered self-intersecting.
        ///     
        ///  4. A Closed polyline with less than 3 vertices
        ///     is considered to be self-intersecting.
        ///     
        /// The definition is intended to be consistent with the 
        /// REGION command's definition of self-intersecting.
        ///     
        /// You may wish to interpret any of the above differently 
        /// depending on your specific functional requirements.
        /// 
        /// </summary>
        public static bool IsSelfIntersecting(this Autodesk.AutoCAD.DatabaseServices.Polyline pline)
        {
            int numverts = pline.NumberOfVertices;
            if (numverts < 3)
            {
                return pline.Closed;
            }

            Point3dCollection points = new Point3dCollection();

            pline.IntersectWith(pline, Intersect.OnBothOperands, points,
               IntPtr.Zero, IntPtr.Zero);

            if (!pline.Closed)
                numverts -= 2;

            return points.Count != numverts;
        }



        /// <summary>
        /// finds all the controls of a certain type on a form
        /// </summary>
        /// <param name="type"></param>
        /// <param name="formControls"></param>
        /// <param name="controls"></param>
        public static void FindControlsOfType(Type type, Control.ControlCollection formControls, ref List<DataGridView> controls)
        {
            foreach (Control control in formControls)
            {
                if (control.GetType() == type)
                    controls.Add((DataGridView)control);
                if (control.Controls.Count > 0)
                    FindControlsOfType(type, control.Controls, ref controls);
            }
        }

        /// <summary>
        /// sets setting for all datagridviews
        /// </summary>
        /// <param name="dgv"></param>
        public static void DgvSetUp(List<DataGridView> dgv)
        {
            foreach (DataGridView c in dgv)
            {
                // error hndlr for all the grids
                c.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(Cls_BW_DataGridErrHandlr.DataGridView_DataError);

                Cls_BW_Utility.SetDoubleBuffered(c);

                c.MultiSelect = false;
                c.ReadOnly = true;
                c.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                c.AllowUserToAddRows = false;
                c.AllowUserToDeleteRows = false;
                c.Refresh();
            }
        }



        /// <summary>
        /// Creates a pivot view of the provided datatable, that is, all rows
        /// will become columns.
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public static System.Data.DataTable ToPivotTable(System.Data.DataTable sourceTable)
        {
            System.Data.DataTable pivotTable = new System.Data.DataTable();
            // First Column is the params column:
            pivotTable.Columns.Add("Parameter", typeof(string));
            // Next Columns are each line first item value
            foreach (System.Data.DataRow row in sourceTable.Rows)
            {
                string columnName = row.ItemArray[0].ToString();
                string newColumnName = columnName;
                int id = 2;
                while (pivotTable.Columns.Contains(newColumnName))
                {
                    newColumnName = columnName + " [" + id + "]";
                    ++id;
                }
                pivotTable.Columns.Add(newColumnName, typeof(string));
            }
            // Fills empty rows - number of column - first column
            // The first column in the new table is the paramer - the name of the column
            for (int columnIndex = 1; columnIndex < sourceTable.Columns.Count; columnIndex++)
            {
                System.Data.DataRow row = pivotTable.NewRow();
                row[0] = sourceTable.Columns[columnIndex].ColumnName;
                pivotTable.Rows.Add(row);
            }
            // Fills the pivot table by source values
            for (int sourceRowIndex = 0; sourceRowIndex < sourceTable.Rows.Count; sourceRowIndex++)
            {
                // Starts from 1 becuase the 0 column is the row headers in the pivot
                for (int sourceColumnIndex = 1; sourceColumnIndex < sourceTable.Columns.Count; sourceColumnIndex++)
                {
                    pivotTable.Rows[sourceColumnIndex - 1][sourceRowIndex + 1] =
                      sourceTable.Rows[sourceRowIndex].ItemArray[sourceColumnIndex];
                }
            }

            return pivotTable;
        }


        /// <summary>
        /// starts a process
        /// </summary>
        /// <param name="sPath"></param>
        public static void OpenExplorerToDirOrSharePoint(string sPath, bool fldr)
        { 
            if (fldr) // folder
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "explorer";
                info.Arguments = sPath;// + "\\";
                info.UseShellExecute = false;

                Process.Start(info);
            }
            else // sharepoint
            {
                Process.Start(sPath);
            }                   
        }

    



        /// <summary>
        /// Returns Autocad ObjectId from Autocad Handle
        /// </summary>
        /// <param name="database"></param>
        /// <param name="AutocadHandle"></param>
        /// <returns></returns>
        public static ObjectId ObjectIDFromHandle(
        Autodesk.AutoCAD.DatabaseServices.Database database,
        string AutocadHandle)
    {
        // Convert hexadecimal string to 64-bit integer
        long ln = Convert.ToInt64(AutocadHandle, 16);

        // Now create a Handle from the long integer
        Handle hn = new Handle(ln);

        ObjectId id = database.GetObjectId(false, hn, 0);

        return id;
    }

        /// <summary>
        /// Double buffer a windows control
        /// </summary>
        /// <param name="control"></param>
        public static void SetDoubleBuffered(Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        /// <summary>
        /// Zooms up on Autocad ObjectIdCollection
        /// </summary>
        /// <param name="idCol"></param>
        public static void ZoomObjects(ObjectIdCollection idCol, int x = 500, int y = 500)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            using (ViewTableRecord view = ed.GetCurrentView())
            {
                Matrix3d WCS2DCS = Matrix3d.PlaneToWorld(view.ViewDirection);
                WCS2DCS = Matrix3d.Displacement(view.Target - Point3d.Origin) * WCS2DCS;
                WCS2DCS = Matrix3d.Rotation(-view.ViewTwist, view.ViewDirection, view.Target) * WCS2DCS;
                WCS2DCS = WCS2DCS.Inverse();
                Entity ent = (Entity)tr.GetObject(idCol[0], OpenMode.ForRead);
                Extents3d ext = ent.GeometricExtents;
                for (int i = 1; i < idCol.Count; i++)
                {
                    ent = (Entity)tr.GetObject(idCol[i], OpenMode.ForRead);
                    Extents3d tmp = ent.GeometricExtents;
                    ext.AddExtents(tmp);
                }
                ext.TransformBy(WCS2DCS);
                view.Width = (ext.MaxPoint.X - ext.MinPoint.X) + x;
                view.Height = (ext.MaxPoint.Y - ext.MinPoint.Y) + y;
                view.CenterPoint =
                    new Point2d((ext.MaxPoint.X + ext.MinPoint.X) / 2.0, (ext.MaxPoint.Y + ext.MinPoint.Y) / 2.0);
                ed.SetCurrentView(view);
                tr.Commit();
            }
        }


        public static System.Data.DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            List<IDataRecord> list = data.Cast<IDataRecord>().ToList();

            PropertyDescriptorCollection props = null;
            System.Data.DataTable table = new System.Data.DataTable();
            if (list != null && list.Count > 0)
            {
                props = TypeDescriptor.GetProperties(list[0]);
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            if (props != null)
            {
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(values);
                }
            }
            return table;
        }






        public static void BW_ZoomAllLayouts()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            
            if (IsInModel(acDoc))
            {
                MessageBox.Show("Need to Switch to Paper Space.");
                return;
            }

            Editor acDocEd = acDoc.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            //int UCSICON = System.Convert.ToInt32(Application.GetSystemVariable("UCSICON"));                        
            //Application.SetSystemVariable("UCSICON", 3);

            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary lays = acTrans.GetObject(acCurDb.LayoutDictionaryId, OpenMode.ForRead) as DBDictionary;

                acDocEd.WriteMessage("\nLayouts:");

                foreach (DBDictionaryEntry item in lays)
                {
                    Layout l = (Layout)acTrans.GetObject((ObjectId)item.Value, OpenMode.ForWrite);

                    if (l.LayoutName != "Model")
                    {
                        acDocEd.WriteMessage("\n  " + item.Key);

                        LayoutManager.Current.CurrentLayout = l.LayoutName;

                        if (!Cls_BW_Utility.IsInLayoutPaper(acDoc))
                        {
                            acDocEd.WriteMessage("\n  Not in Paperspace");                            
                        }
                        else
                        {
                            acDoc.Editor.ZoomExtents();
                            acDocEd.WriteMessage("\n  ZoomExtents");
                        }
                    }
                }
                acTrans.Commit();
            }
        }


        public static void BW_UpdateTitleBlockDateField()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (IsInModel(doc))
            {
                MessageBox.Show("Need to Switch to Paper Space.");
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            using (DocumentLock acLckDoc = doc.LockDocument())
            using (var tx = db.TransactionManager.StartTransaction())
            {
                try
                {
                    var nod = tx.GetObject(
                        db.NamedObjectsDictionaryId,
                        OpenMode.ForRead) as DBDictionary;

                    if (!nod.Contains("ACAD_FIELDLIST"))
                    {
                        ed.WriteMessage("\nDrawing has no field...");
                        return;
                    }

                    var id = nod.GetAt("ACAD_FIELDLIST");

                    List<TypedValue> dxf = NativeMethods.ImportsR22.AcdbEntGetTypedValues(id);

                    foreach (var entry in dxf)
                    {
                        if (entry.TypeCode == 330)
                        {
                            ObjectId objId = (ObjectId)entry.Value;

                            if (objId.ObjectClass.Name == "AcDbField")
                            {
                                Field field = tx.GetObject(objId, OpenMode.ForWrite) as Field;
                                field.Evaluate();

                                string str = field.GetStringValue();

                                DateTime newDate;
                                bool result = DateTime.TryParse(str, out newDate);

                                if (result)
                                {
                                    ed.WriteMessage("\n - Format: " + field.Format + " Value: " + str);
                                }
                            }
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    acLckDoc.Dispose();
                    tx.Abort();
                }
            }


        }
                

        public static List<Layout> GetLayouts()
        {
            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            //  Document acDoc = AcAp.DocumentManager.MdiActiveDocument;

            List<Layout> layouts = new List<Layout>();

            try
            {
                //  using (acDoc.LockDocument())
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                   
                    DBDictionary layoutDict = (DBDictionary)db.LayoutDictionaryId.GetObject(OpenMode.ForRead);

                    foreach (DBDictionaryEntry entry in layoutDict)
                    {
                        if (entry.Key != "Model")
                        {
                            layouts.Add((Layout)tr.GetObject(entry.Value, OpenMode.ForRead));
                        }


                    }
                    layouts.Sort((l1, l2) => l1.TabOrder.CompareTo(l2.TabOrder));
                }
            }
            catch (System.Exception e)
            {
                //Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
                //ed.WriteMessage("\nError: {0}\n{1}", e.Message, e.StackTrace);
            }
            //finally
            //{
            //    AcAp.SetSystemVariable("BACKGROUNDPLOT", bgp);
            //}

            return layouts;
        }
        

        public static void RemoveAnnotativeProperties(string DxfCode)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                TypedValue[] acTypValAr = new TypedValue[]
                    { new TypedValue((int)Autodesk.AutoCAD.DatabaseServices.DxfCode.Start, DxfCode) };
                      
                SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

                PromptSelectionResult acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    foreach (SelectedObject acSSObj in acSSet)
                    {
                        if (acSSObj != null)
                        {
                            Entity ent = (Entity) acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite);

                            if (ent is BlockReference )// System.Convert.ToString(ent.GetType()) == "Autodesk.AutoCAD.DatabaseServices.BlockReference")
                            {
                                BlockReference br = (BlockReference)acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead);
                                if (br != null)
                                {
                                    string nam = "";
                                    nam = br.Name;

                                    if (br.IsDynamicBlock)
                                    {
                                        BlockTableRecord relName = (BlockTableRecord)acTrans.GetObject(br.DynamicBlockTableRecord, OpenMode.ForRead);
                                        nam = relName.Name;
                                    }

                                    BlockTable bt = (BlockTable)acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead);
                                    foreach (ObjectId ids in bt)
                                    {
                                        BlockTableRecord btr = (BlockTableRecord)acTrans.GetObject(ids, OpenMode.ForRead);
                                        if (btr.Name == nam) //br.Name)
                                        {

                                            btr.UpgradeOpen();
                                            btr.Annotative = AnnotativeStates.False;
                                       //     debug.WriteLine(btr.Name + " Ano = " + btr.Annotative.ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ent.Annotative = AnnotativeStates.False;
                           //     Console.WriteLine(ent.ToString() + " Ano = " + ent.Annotative.ToString());
                            }
                        }
                    }

                    acTrans.Commit();
                }
            }
        }



        #region xml

        
        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        } 
        #endregion


    }
}
