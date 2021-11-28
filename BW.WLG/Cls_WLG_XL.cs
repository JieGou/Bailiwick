namespace MyFirstProject.BW.WLG
{
    class Cls_WLG_XL
    {
        //ws.Cells[gmRed].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //ws.Cells[gmRed].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
        //ws.Cells[gmRed].Style.Font.Color.SetColor(System.Drawing.Color.Yellow);
        //ws.Cells[gmRed].Style.Font.Bold = true;
 


        //public static void WLG_CreateXlFile()//string fileName)
        //{
        //    try
        //    {
        //        Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

        //        string xlsxFileName = doc.Name.ToLower();

        //        xlsxFileName = xlsxFileName.Replace(".dwg", "") + " " +
        //            DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

        //        FileInfo newFile = new FileInfo(xlsxFileName);
        //        //     FileInfo templateFile = new FileInfo(fileName);

        //        using (ExcelPackage xlPackage = new ExcelPackage(newFile))//, templateFile))
        //        {
        //            ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[1];
        //            //  ExcelWorksheet wsWAO = xlPackage.Workbook.Worksheets[3];


        //            wsWAP.

        //            //#region AutoFilled Waps

        //            //int cntCol = 10; // j

        //            //foreach (var res in LstClsBomCntsByCloset)
        //            //{
        //            //    wsWAP.Cells[1, cntCol].Value = res.Closet;

        //            //    wsWAP.Cells[2, cntCol].Value = res.WapCounts.Wap_C_DC_DropCeilingMount2802i;
        //            //    wsWAP.Cells[3, cntCol].Value = res.WapCounts.Wap_C_DDK_DropDownKitMount2802i;
        //            //    wsWAP.Cells[4, cntCol].Value = res.WapCounts.Wap_W_RAM_W_RightAngleWallMount_Warehouse2802i;
        //            //    wsWAP.Cells[5, cntCol].Value = res.WapCounts.Wap_W_RAM_O_RightAngleWallMount_Office2802i;
        //            //    wsWAP.Cells[6, cntCol].Value = res.WapCounts.Wap_O_RAM_RightAngleColumnMount2802i;
        //            //    wsWAP.Cells[7, cntCol].Value = res.WapCounts.Wap_C_C2_N_C2D2NEMAEnclosure2802i;
        //            //    wsWAP.Cells[8, cntCol].Value = res.WapCounts.Wap_W_D_WallMountwithDirectionalAntenna2802e;
        //            //    wsWAP.Cells[9, cntCol].Value = res.WapCounts.Wap_O_D_ColumnMountwithDirectionalAntenna2802e;
        //            //    wsWAP.Cells[10, cntCol].Value = res.WapCounts.Wap_W_INT_EA_InteriorWallMountwithExternalAntenna3802p;
        //            //    wsWAP.Cells[11, cntCol].Value = res.WapCounts.Wap_q_INT_F_FiberFedInterior2802i;
        //            //    wsWAP.Cells[12, cntCol].Value = res.WapCounts.Wap_q_INT_FD_FiberFedInterior_DirectionalAntenna2802e;
        //            //    wsWAP.Cells[13, cntCol].Value = res.WapCounts.Wap_C_R_RedIronMount2802i;
        //            //    wsWAP.Cells[14, cntCol].Value = res.WapCounts.Wap_C_R_D28_RedIronMountwithDirectionalAntenna2802e;
        //            //    wsWAP.Cells[15, cntCol].Value = res.WapCounts.Wap_C_R_D38_HDRedIronMountwithDirectionalAntenna3802e;
        //            //    wsWAP.Cells[16, cntCol].Value = res.WapCounts.Wap_W_C1_IntrinsicallySafeC1D1_WallMount2802e;
        //            //    wsWAP.Cells[17, cntCol].Value = res.WapCounts.Wap_C_C1_IntrinsicallySafeC1D1_CeilingMount2802e;
        //            //    wsWAP.Cells[18, cntCol].Value = res.WapCounts.Wap_W_EXT_F_OutdoorMountedAP1572eac;
        //            //    wsWAP.Cells[19, cntCol].Value = res.WapCounts.Wap_C_DC_HD_HDDropCeilingMount3802i;
        //            //    wsWAP.Cells[20, cntCol].Value = res.WapCounts.Wap_C_R_HD_HDRedIronMount3802i;
        //            //    wsWAP.Cells[21, cntCol].Value = res.WapCounts.Wap_W_RAM_HD_HDRightAngleWallMount_Office3802i;
        //            //    wsWAP.Cells[22, cntCol].Value = res.WapCounts.Wap_O_RAM_HD_HDRightAngleColumnMount_Warehouse3802i;

        //            //    ClsCntsPerTrTc wapsCount = LstClsCntsPerTrTc.Where(x => x.Closet == res.Closet).FirstOrDefault();

        //            //    if (wapsCount.WapCnt < 12) wsWAP.Cells[24, cntCol].Value = 1;
        //            //    if (wapsCount.WapCnt >= 12 & wapsCount.WapCnt < 24) wsWAP.Cells[25, cntCol].Value = 1;
        //            //    if (wapsCount.WapCnt >= 24 & wapsCount.WapCnt < 36) wsWAP.Cells[26, cntCol].Value = 1;
        //            //    if (wapsCount.WapCnt >= 36 & wapsCount.WapCnt < 48) wsWAP.Cells[27, cntCol].Value = 1;

        //            //    cntCol++;
        //            //}


        //            //Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.Sort((x, y) => x.ApNumber.CompareTo(y.ApNumber));

        //            //wsWAP.Cells[30, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //            //wsWAP.Cells[30, 10].Value = "WAP's Not Accounted For:";

        //            //for (int i = 0; i < Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.Count(); i++)
        //            //{
        //            //    wsWAP.Cells[i + 31, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //            //    wsWAP.Cells[i + 31, 10].Value =
        //            //        Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].ApNumber + ";  " +
        //            //        Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].DisplayCode + ";  " +
        //            //        Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].Antenna + ";  " +
        //            //        Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].Mount;
        //            //}

        //            //#endregion


        //            //#region Autofilled Waos

        //            //cntCol = 8; // h

        //            //foreach (var res in LstClsBomCntsByCloset)
        //            //{
        //            //    // wsWAO.Cells[1, cntCol].Value = res.Closet;

        //            //    wsWAO.Cells[2, cntCol].Value =
        //            //        res.WaoCounts.Wao_Single_CAT6_Cable_Wall_Phone; // +
        //            //                                                        //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_Phone +
        //            //                                                        //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_Camera +
        //            //                                                        //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_CardReader;

        //            //    wsWAO.Cells[3, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Office;
        //            //    wsWAO.Cells[4, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Office;
        //            //    wsWAO.Cells[5, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Office;
        //            //    wsWAO.Cells[6, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Office;
        //            //    // quint not in autofill bom
        //            //    //wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Quint_CAT6_Cable_Office; 
        //            //    wsWAO.Cells[7, cntCol].Value = res.WaoCounts.Wao_Sextet_CAT6_Cable_Office;
           

        //            //    // modular furniture
        //            //    wsWAO.Cells[8, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Modular_Office;
        //            //    wsWAO.Cells[9, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Modular_Office;

        //            //    // warehouse
        //            //    wsWAO.Cells[10, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Warehouse;
        //            //    wsWAO.Cells[11, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Warehouse;
        //            //    wsWAO.Cells[12, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Warehouse;
        //            //    wsWAO.Cells[13, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Warehouse;
        //            //    // quint not on autofill
        //            //    wsWAO.Cells[14, cntCol].Value = res.WaoCounts.Wao_Sextet_CAT6_Cable_Warehouse;

        //            //    wsWAO.Cells[15, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Liquid_Tight;
        //            //    wsWAO.Cells[16, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Liquid_Tight;
        //            //    wsWAO.Cells[17, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Liquid_Tight;
        //            //    wsWAO.Cells[18, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Liquid_Tight;
        //            //    // quint/sextet 'LT' not in autofill bom
        //            //    // wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Quint_CAT6_Cable_Liquid_Tight;
        //            //    // wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Sextet_CAT6_Cable_Liquid_Tight;


        //            //    ClsCntsPerTrTc waosCount = (ClsCntsPerTrTc)LstClsCntsPerTrTc.Where(x => x.Closet == res.Closet).FirstOrDefault();

        //            //    if (waosCount.WaoCnt < 24) wsWAO.Cells[27, cntCol].Value = 1;
        //            //    if (waosCount.WaoCnt >= 24 & waosCount.WaoCnt < 48) wsWAO.Cells[28, cntCol].Value = 1;
        //            //    if (waosCount.WaoCnt >= 48 & waosCount.WaoCnt < 72) wsWAO.Cells[29, cntCol].Value = 1;
        //            //    if (waosCount.WaoCnt >= 72 & waosCount.WaoCnt < 96) wsWAO.Cells[30, cntCol].Value = 1;
        //            //    if (waosCount.WaoCnt >= 96 & waosCount.WaoCnt < 120) wsWAO.Cells[31, cntCol].Value = 1;
        //            //    if (waosCount.WaoCnt >= 120 & waosCount.WaoCnt < 144) wsWAO.Cells[32, cntCol].Value = 1;

        //            //    cntCol++;
        //            //}

        //            //Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.Sort((x, y) => x.Label1.CompareTo(y.Label1));

        //            //wsWAO.Cells[35, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //            //wsWAO.Cells[35, 10].Value = "WAO's Not Accounted For:";

        //            //for (int i = 0; i < Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.Count(); i++)
        //            //{
        //            //    wsWAO.Cells[i + 36, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //            //    wsWAO.Cells[i + 36, 10].Value =
        //            //        Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].Label1 + ";  " +
        //            //        Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].DataTotal; // + ";  " +
        //            //                                                                  // Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].Antenna + ";  " +
        //            //                                                                  // Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].Mount;
        //            //}

        //            //#endregion


        //            ///* Set title, Author.. */
        //            //xlPackage.Workbook.Properties.Title = "Title: Office Open XML Sample";
        //            //xlPackage.Workbook.Properties.Author = "Author: Muhammad Mubashir.";
        //            ////xlPackage.Workbook.Properties.SetCustomPropertyValue("EmployeeID", "1147");
        //            //xlPackage.Workbook.Properties.Comments = "Sample Record Details";
        //            //xlPackage.Workbook.Properties.Company = "TRG Tech.";

        //            //Save
        //            xlPackage.Save();


        //            MessageBox.Show("Bom Saved To: " + xlsxFileName + Environment.NewLine +
        //                "Bom Saved for file: " + doc.Database.Filename);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        
        //public static bool CreateFileXlPull<T>(
        // FileInfo newFile,
        // IEnumerable<T> lst,
        // string tabDesc,
        // string type = ""
        //    )
        //{
        //    try
        //    {
        //        using (ExcelPackage xlPackage = new ExcelPackage(newFile))
        //        {
        //            if (xlPackage.Workbook.Worksheets.Count > 0)
        //                xlPackage.Workbook.Worksheets.Delete(1);

        //            if (type == "")
        //            {
        //                xlPackage.Workbook.Worksheets.Add(tabDesc);
        //            }
        //            else
        //            {
        //                xlPackage.Workbook.Worksheets.Add(tabDesc + " " + type);
        //            }

        //            ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[1];

        //            //wsWAP.Cells["A1"].Value = clsBomCnts.Wap_C_DC_DropCeilingMount2802i;             

        //            PropertyInfo[] pis = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //            //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
        //            //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //            int col = 1;
        //            foreach (var n in pis)
        //            {
        //                wsWAP.Cells[1, col].Value = n.Name;
        //                col++;
        //            }

        //            wsWAP.Cells[2, 1].LoadFromCollection(lst);

        //            wsWAP.Cells.AutoFitColumns();

        //            xlPackage.Save();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //}        
    }
}
