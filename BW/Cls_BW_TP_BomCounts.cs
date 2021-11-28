using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using OfficeOpenXml;

using MoreLinq;

namespace MyFirstProject.BW
{
    public class Cls_BW_TP_BomCounts
    {
        #region BomCounts

        private static readonly List<ClsCntsPerTrTc> LstClsCntsPerTrTc = new List<ClsCntsPerTrTc>();
        
        /// <summary>
        /// counts all info
        /// </summary>
        private class ClsCntsPerTrTc
        {
            public string Closet { get; set; }
            public int WapCnt { get; set; }
            public int WaoCnt { get; set; }
        }

        private static readonly List<ClsBomCntsByCloset> LstClsBomCntsByCloset = new List<ClsBomCntsByCloset>();
        
        /// <summary>
        /// counts only items on the BOM
        /// </summary>
        private class ClsBomCntsByCloset
        {
            public string Closet { get; set; }
            public BW.Cls_BW_BomCounts_Wap WapCounts { get; set; }
            public BW.Cls_BW_BomCounts_Wao WaoCounts { get; set; }
        }


        public static void BomCountsPerCloset()
        {
            LstClsBomCntsByCloset.Clear();
            //LstClsCntsPerTrTc.Clear();

            List<Cls_BW_TP_Common.ClsClosetFilterList> allClosetsOrdered = RetAllClosetsOrdered();

            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.Clear();
            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.AddRange(Cls_BW_TP_APs.LstAtts_wap_data_2_dual.ToList());

            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.Clear();
            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.AddRange(Cls_BW_TP_WAOs.LstAtts_waos.ToList());

            foreach (var clst in allClosetsOrdered) //Cls_BW_TP_APs.LstAPsClosetFilter)
            {
                ClsBomCntsByCloset clsBomCntsByCloset = new ClsBomCntsByCloset();
                ClsCntsPerTrTc clsCntsPerTrTc = new ClsCntsPerTrTc();
                
                BomCountsWAP(clst.Closet);

                BomCountsWAO(clst.Closet);

                clsBomCntsByCloset.Closet = clst.Closet;
                clsBomCntsByCloset.WapCounts = clsBomCntsWap;
                clsBomCntsByCloset.WaoCounts = clsBomCntsWao;

                clsCntsPerTrTc.Closet = clst.Closet;

                // straight count wap
                clsCntsPerTrTc.WapCnt = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Where(x => x.ClosetInfo == clst.Closet).Count();

                var cbls1 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("single") & x.ClosetInfo == clst.Closet);
                var cbls2 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("dual") & x.ClosetInfo == clst.Closet) * 2;
                var cbls3 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("triple") & x.ClosetInfo == clst.Closet) * 3;
                var cbls4 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("quad") & x.ClosetInfo == clst.Closet) * 4;
                var cbls5 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("quint") & x.ClosetInfo == clst.Closet) * 5;
                var cbls6 = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName.Contains("sextet") & x.ClosetInfo == clst.Closet) * 6;

                // count waos * # cables
                clsCntsPerTrTc.WaoCnt = cbls1 + cbls2 + cbls3 + cbls4 + cbls5 + cbls6;
                
                LstClsBomCntsByCloset.Add(clsBomCntsByCloset); // ommit drops not on bom
                //LstClsCntsPerTrTc.Add(clsCntsPerTrTc);
            }

        }

        private static List<Cls_BW_TP_Common.ClsClosetFilterList> RetAllClosetsOrdered()
        {
            List<Cls_BW_TP_Common.ClsClosetFilterList> allClosets =
                Cls_BW_TP_APs.LstAPsClosetFilter.Union(Cls_BW_TP_WAOs.LstWAOsClosetFilter)
                .DistinctBy(c => c.Closet).ToList();
            
            List<Cls_BW_TP_Common.ClsClosetFilterList> allClosetsOrdered =
                new List<Cls_BW_TP_Common.ClsClosetFilterList>();
            
            var tr = allClosets.Where(x => x.Closet.StartsWith("TR")).ToList();
            tr.Sort((x, y) => x.ClosetFormatSort.CompareTo(y.ClosetFormatSort));
            var tc = allClosets.Where(x => x.Closet.StartsWith("TC")).ToList();
            tc.Sort((x, y) => x.ClosetFormatSort.CompareTo(y.ClosetFormatSort));

            allClosetsOrdered.AddRange(tr);
            allClosetsOrdered.AddRange(tc);
            return allClosetsOrdered;
        }

        private static BW.Cls_BW_BomCounts_Wap clsBomCntsWap { get; set; }
        private static BW.Cls_BW_BomCounts_Wao clsBomCntsWao { get; set; }

        private static int retWAPCount(string closet, string disCode, string antenna)
        {
           var ret = Cls_BW_TP_APs.LstAtts_wap_data_2_dual
                .Count(x => x.Antenna.ContainsCaseInsensitive(antenna) & 
                (x.DisplayCode == disCode | x.WrlsSpecialFeed1 == disCode) & 
                (x.ClosetInfo == closet));

            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom
                .RemoveAll(x => x.Antenna.ContainsCaseInsensitive(antenna) & 
                (x.DisplayCode == disCode | x.WrlsSpecialFeed1 == disCode) & 
                (x.ClosetInfo == closet));

            return ret;
        }

        private static void BomCountsWAP(string closet)
        {
            clsBomCntsWap = new BW.Cls_BW_BomCounts_Wap();

            clsBomCntsWap.Wap_C_DC_2802i = retWAPCount(closet, "C-DC", "2800i");
            clsBomCntsWap.Wap_C_DDK_2802i = retWAPCount(closet, "C-DDK", "2800i");
            clsBomCntsWap.Wap_W_RAM_W_2802i = retWAPCount(closet, "W-RAM-W", "2800i");
            clsBomCntsWap.Wap_W_RAM_O_2802i = retWAPCount(closet, "W-RAM-O", "2800i");
            clsBomCntsWap.Wap_O_RAM_2802i = retWAPCount(closet, "O-RAM", "2800i");
            clsBomCntsWap.Wap_C_C2_N_2802i = retWAPCount(closet, "C-C2-N", "2800i");
            clsBomCntsWap.Wap_C_R_N_2802i = retWAPCount(closet, "C-R-N", "2800i");
            clsBomCntsWap.Wap_W_RAM_N_2802i = retWAPCount(closet, "W-RAM-N", "2800i");
            clsBomCntsWap.Wap_O_RAM_N_2802i = retWAPCount(closet, "O-RAM-N", "2800i");

            clsBomCntsWap.Wap_W_D_W_2802e = retWAPCount(closet, "W-D-W", "2800e");
            clsBomCntsWap.Wap_O_D_W_2802e = retWAPCount(closet, "O-D-W", "2800e");
            clsBomCntsWap.Wap_W_D_N_2802e = retWAPCount(closet, "W-D-N", "2800e");

            clsBomCntsWap.Wap_C_R_2802i = retWAPCount(closet, "C-R", "2800i");
            clsBomCntsWap.Wap_C_R_D28_2802e = retWAPCount(closet, "C-R-D28", "2800e");
            clsBomCntsWap.Wap_C_R_D38_3802e = retWAPCount(closet, "C-R-D38", "3800e");

            clsBomCntsWap.Wap_W_C1_2802e = retWAPCount(closet, "W-C1", "2800e");
            clsBomCntsWap.Wap_C_C1_2802e = retWAPCount(closet, "C-C1", "2800e");

            clsBomCntsWap.Wap_W_C1_D_2802e = retWAPCount(closet, "W-C1-D", "2800e");
            clsBomCntsWap.Wap_C_C1_D_2802e = retWAPCount(closet, "C-C1-D", "2800e");

            clsBomCntsWap.Wap_C_DC_HD_3802i = retWAPCount(closet, "C-DC-HD", "3800i");
            clsBomCntsWap.Wap_C_R_HD_3802i = retWAPCount(closet, "C-R-HD", "3800i");

            clsBomCntsWap.Wap_W_RAM_HD_3802i = retWAPCount(closet, "W-RAM-HD", "3800i");
            clsBomCntsWap.Wap_O_RAM_HD_3802i = retWAPCount(closet, "O-RAM-HD", "3800i");

            clsBomCntsWap.Wap_W_INT_EA_3802p = retWAPCount(closet, "W-INT-EA", "3800p");
            clsBomCntsWap.Wap_W_INT_F_2802i = retWAPCount(closet, "W-INT-F", "2800i");
            clsBomCntsWap.Wap_W_INT_FD_2802e = retWAPCount(closet, "W-INT-FD", "2800e");

            clsBomCntsWap.Wap_W_EXT_F_1572eac = retWAPCount(closet, "W-EXT-F", "1572eac");
            clsBomCntsWap.Wap_O_EXT_F_1572eac = retWAPCount(closet, "O-EXT-F", "1572eac");


            // orig
            //clsBomCntsWap.Wap_C_DC_2802i = retWAPCount(closet, "C-DC", "2802i");
            //clsBomCntsWap.Wap_C_DDK_2802i = retWAPCount(closet, "C-DDK", "2802i");
            //clsBomCntsWap.Wap_W_RAM_W_2802i = retWAPCount(closet, "W-RAM-W", "2802i");            
            //clsBomCntsWap.Wap_W_RAM_O_2802i = retWAPCount(closet, "W-RAM-O", "2802i");
            //clsBomCntsWap.Wap_O_RAM_2802i = retWAPCount(closet, "O-RAM", "2802i");
            //clsBomCntsWap.Wap_C_C2_N_2802i = retWAPCount(closet, "C-C2-N", "2802i");
            //clsBomCntsWap.Wap_C_R_N_2802i = retWAPCount(closet, "C-R-N", "2802i");
            //clsBomCntsWap.Wap_W_RAM_N_2802i = retWAPCount(closet, "W-RAM-N", "2802i");
            //clsBomCntsWap.Wap_O_RAM_N_2802i = retWAPCount(closet, "O-RAM-N", "2802i");

            //clsBomCntsWap.Wap_W_D_W_2802e = retWAPCount(closet, "W-D-W", "2802e");
            //clsBomCntsWap.Wap_O_D_W_2802e = retWAPCount(closet, "O-D-W", "2802e");
            //clsBomCntsWap.Wap_W_D_N_2802e = retWAPCount(closet, "W-D-N", "2802e");

            //clsBomCntsWap.Wap_C_R_2802i = retWAPCount(closet, "C-R", "2802i");
            //clsBomCntsWap.Wap_C_R_D28_2802e = retWAPCount(closet, "C-R-D28", "2802e");
            //clsBomCntsWap.Wap_C_R_D38_3802e = retWAPCount(closet, "C-R-D38", "3802e");

            //clsBomCntsWap.Wap_W_C1_2802e = retWAPCount(closet, "W-C1", "2802e");
            //clsBomCntsWap.Wap_C_C1_2802e = retWAPCount(closet, "C-C1", "2802e");

            //clsBomCntsWap.Wap_W_C1_D_2802e = retWAPCount(closet, "W-C1-D", "2802e");
            //clsBomCntsWap.Wap_C_C1_D_2802e = retWAPCount(closet, "C-C1-D", "2802e");

            //clsBomCntsWap.Wap_C_DC_HD_3802i = retWAPCount(closet, "C-DC-HD", "3802i");
            //clsBomCntsWap.Wap_C_R_HD_3802i = retWAPCount(closet, "C-R-HD", "3802i");

            //clsBomCntsWap.Wap_W_RAM_HD_3802i = retWAPCount(closet, "W-RAM-HD", "3802i");
            //clsBomCntsWap.Wap_O_RAM_HD_3802i = retWAPCount(closet, "O-RAM-HD", "3802i");

            //clsBomCntsWap.Wap_W_INT_EA_3802p = retWAPCount(closet, "W-INT-EA", "3802p");
            //clsBomCntsWap.Wap_W_INT_F_2802i = retWAPCount(closet, "W-INT-F", "2802i");
            //clsBomCntsWap.Wap_W_INT_FD_2802e = retWAPCount(closet, "W-INT-FD", "2802e");                  

            //clsBomCntsWap.Wap_W_EXT_F_1572eac = retWAPCount(closet, "W-EXT-F", "1572eac");
            //clsBomCntsWap.Wap_O_EXT_F_1572eac = retWAPCount(closet, "O-EXT-F", "1572eac");
        }

        private static int retWAOCountVoiceCam(string closet, string blk, string blkDyn, bool notOnBom = false)
        {
            if (notOnBom)
            {
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.ClosetInfo == closet);
                return 0;
            }

            var ret = Cls_BW_TP_WAOs.LstAtts_waos
                .Count(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.ClosetInfo == closet);


            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.ClosetInfo == closet);

            return ret;
        }

        private static int retWAOCountDept(string closet, string blk, string blkDyn, string dept, bool notOnBom = false)
        {
            if (notOnBom)
            {
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.DepartmentSuite == dept &
                x.ClosetInfo == closet);

                return 0;
            }

            var ret = Cls_BW_TP_WAOs.LstAtts_waos
                .Count(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.DepartmentSuite == dept &          
                x.ClosetInfo == closet);


                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.DepartmentSuite == dept &
                x.ClosetInfo == closet);      

            return ret;
        }

        private static int retWAOCountLiqTt(string closet, string blk, string blkDyn, bool notOnBom = false)
        {
            if (notOnBom)
            {
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.Conduit == "LIQUID TIGHT" &
                x.ClosetInfo == closet);
                return 0;
            }

            var ret = Cls_BW_TP_WAOs.LstAtts_waos
                .Count(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.Conduit == "LIQUID TIGHT" &  
                x.ClosetInfo == closet);

   
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == blk |
                x.BlockName == blkDyn &
                x.Conduit == "LIQUID TIGHT" &
                x.ClosetInfo == closet);
      
            return ret;
        }
        
        private static int retWAOCountCardRead(string closet, bool notOnBom = false)
        {
            if (notOnBom)
            {
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == "data 1 single" |
                x.BlockName == "dyn data 1 single" &
                x.Label2 == "CR" &
                x.ClosetInfo == closet);
                return 0;
            }

            var ret = Cls_BW_TP_WAOs.LstAtts_waos
                .Count(x => x.BlockName == "data 1 single" |
                x.BlockName == "dyn data 1 single" &
                x.Label2 == "CR" &
                x.ClosetInfo == closet);

       
                Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom
                .RemoveAll(x => x.BlockName == "data 1 single" |
                x.BlockName == "dyn data 1 single" &
                x.Label2 == "CR" &
                x.ClosetInfo == closet);
  
            return ret;
        }
        
        private static void BomCountsWAO(string closet)
        {
            clsBomCntsWao = new Cls_BW_BomCounts_Wao();

            clsBomCntsWao.Wao_Single_CAT6_Cable_Phone = retWAOCountVoiceCam(closet, "voice 1 single", "dyn voice 1 single", true); //not on bom 

            clsBomCntsWao.Wao_Single_CAT6_Cable_Wall_Phone = retWAOCountVoiceCam(closet, "voice 1 single wall", "dyn voice 1 single wall");

            clsBomCntsWao.Wao_Single_CAT6_Cable_Camera = retWAOCountVoiceCam(closet, "data 1 single camera", "dyn data 1 single camera", true); //not on bom 

            clsBomCntsWao.Wao_Single_CAT6_Cable_CardReader = retWAOCountCardRead(closet, true);  //not on bom 

            //office data
            clsBomCntsWao.Wao_Single_CAT6_Cable_Office = retWAOCountDept(closet, "data 1 single", "dyn data 1 single", "OFFICE");
            clsBomCntsWao.Wao_Dual_CAT6_Cable_Office = retWAOCountDept(closet, "data 2 dual", "dyn data 2 dual", "OFFICE");
            clsBomCntsWao.Wao_Triple_CAT6_Cable_Office = retWAOCountDept(closet, "data 3 triple", "dyn data 3 triple", "OFFICE");
            clsBomCntsWao.Wao_Quad_CAT6_Cable_Office = retWAOCountDept(closet, "data 4 quad", "dyn data 4 quad", "OFFICE");
            clsBomCntsWao.Wao_Quint_CAT6_Cable_Office = retWAOCountDept(closet, "data 5 quint", "dyn data 5 quint", "OFFICE", true); //not on bom          
            clsBomCntsWao.Wao_Sextet_CAT6_Cable_Office = retWAOCountDept(closet, "data 6 sextet", "dyn data 6 sextet", "OFFICE");

            //modulare furniture
            clsBomCntsWao.Wao_Single_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 1 single", "dyn data 1 single", "MODULAR FURNITURE");
            clsBomCntsWao.Wao_Dual_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 2 dual", "dyn data 2 dual", "MODULAR FURNITURE");
            clsBomCntsWao.Wao_Triple_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 3 triple", "dyn data 3 triple", "MODULAR FURNITURE", true); //not on bom 
            clsBomCntsWao.Wao_Quad_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 4 quad", "dyn data 4 quad", "MODULAR FURNITURE", true); //not on bom 
            clsBomCntsWao.Wao_Quint_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 5 quint", "dyn data 5 quint", "MODULAR FURNITURE", true); //not on bom 
            clsBomCntsWao.Wao_Sextet_CAT6_Cable_Modular_Furniture = retWAOCountDept(closet, "data 6 sextet", "dyn data 6 sextet", "MODULAR FURNITURE", true); //not on bom 

            //warehouse
            clsBomCntsWao.Wao_Single_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 1 single", "dyn data 1 single", "WAREHOUSE");
            clsBomCntsWao.Wao_Dual_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 2 dual", "dyn data 2 dual", "WAREHOUSE");
            clsBomCntsWao.Wao_Triple_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 3 triple", "dyn data 3 triple", "WAREHOUSE");
            clsBomCntsWao.Wao_Quad_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 4 quad", "dyn data 4 quad", "WAREHOUSE");
            clsBomCntsWao.Wao_Quint_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 5 quint", "dyn data 5 quint", "WAREHOUSE", true); //not on bom 
            clsBomCntsWao.Wao_Sextet_CAT6_Cable_Warehouse = retWAOCountDept(closet, "data 6 sextet", "dyn data 6 sextet", "WAREHOUSE");

            //LiquidTight
            clsBomCntsWao.Wao_Single_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 1 single", "dyn data 1 single", true); //not on bom 
            clsBomCntsWao.Wao_Dual_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 2 dual", "dyn data 2 dual", true);           //not on bom 
            clsBomCntsWao.Wao_Triple_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 3 triple", "dyn data 3 triple", true);    //not on bom        
            clsBomCntsWao.Wao_Quad_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 4 quad", "dyn data 4 quad", true);         //not on bom 
            clsBomCntsWao.Wao_Quint_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 5 quint", "dyn data 5 quint", true);      //not on bom    
            clsBomCntsWao.Wao_Sextet_CAT6_Cable_Liquid_Tight = retWAOCountLiqTt(closet, "data 6 sextet", "dyn data 6 sextet", true); //not on bom 

            //plc
            clsBomCntsWao.Wao_Single_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 1 single", "dyn plc 1 single", "AUTOMATION", true);     //not on bom    
            clsBomCntsWao.Wao_Dual_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 2 dual", "dyn plc 2 dual", "AUTOMATION", true);        //not on bom 
            clsBomCntsWao.Wao_Triple_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 3 triple", "dyn plc 3 triple", "AUTOMATION", true);   //not on bom          
            clsBomCntsWao.Wao_Quad_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 4 quad", "dyn plc 4 quad", "AUTOMATION", true);       //not on bom   
            clsBomCntsWao.Wao_Quint_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 5 quint", "dyn plc 5 quint", "AUTOMATION", true);     //not on bom    
            clsBomCntsWao.Wao_Sextet_CAT6_Cable_Plc = retWAOCountDept(closet, "plc 6 sextet", "dyn plc 6 sextet", "AUTOMATION", true);  //not on bom        
        }

        public static void ShowBomCountsInDgvByCloset(DataGridView dgvWap, DataGridView dgvWao)
        {
            dgvWap.Rows.Clear();
            dgvWap.Columns.Clear();
            dgvWap.Columns.Add("Closet", "Closet");
            dgvWap.Columns.Add("BomItem", "BomItem");
            dgvWap.Columns.Add("ItemQty", "ItemQty");
            dgvWap.Columns[0].Width = 100;
            dgvWap.Columns[1].Width = 500;
            dgvWap.Columns[2].Width = 100;

            foreach (var res in LstClsBomCntsByCloset)
            {
                PropertyInfo[] properties = typeof(BW.Cls_BW_BomCounts_Wap).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object[] o = { ".", ".", "." };
                    o[0] = res.Closet;
                    o[1] = property.Name;
                    o[2] = property.GetValue(res.WapCounts, null); // get the count
                    dgvWap.Rows.Add(o);
                }
            }
            dgvWap.Refresh();


            dgvWao.Rows.Clear();
            dgvWao.Columns.Clear();
            dgvWao.Columns.Add("Closet", "Closet");
            dgvWao.Columns.Add("BomItem", "BomItem");
            dgvWao.Columns.Add("ItemQty", "ItemQty");
            dgvWao.Columns[0].Width = 100;
            dgvWao.Columns[1].Width = 500;
            dgvWao.Columns[2].Width = 100;

            foreach (var res in LstClsBomCntsByCloset)
            {
                PropertyInfo[] properties = typeof(BW.Cls_BW_BomCounts_Wao).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object[] o = { ".", ".", "." };
                    o[0] = res.Closet;
                    o[1] = property.Name;
                    o[2] = property.GetValue(res.WaoCounts, null); // get the count
                    dgvWao.Rows.Add(o);
                }
            }
            dgvWao.Refresh();
        }
        
        public static void CreateBomXlFileWithTemplateByCloset(string fileName)
        {
            try
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                string xlsxFileName = doc.Name.ToLower();

                xlsxFileName = xlsxFileName.Replace(".dwg", "") + " " +
                    DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " BOM.xlsx";
                
                FileInfo newFile = new FileInfo(xlsxFileName);
                FileInfo templateFile = new FileInfo(fileName);

                using (ExcelPackage xlPackage = new ExcelPackage(newFile, templateFile))
                {
                    ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[3]; //Wireless
                    ExcelWorksheet wsWAO = xlPackage.Workbook.Worksheets[2]; //Cabling

                    xlPackage.Workbook.Worksheets.Add("Not Accounted For");
                    ExcelWorksheet wsNotAccFor = xlPackage.Workbook.Worksheets[5]; //Not Accounted For

                    #region AutoFilled Waps

                    int cntCol = 12; // L

                    foreach (var res in LstClsBomCntsByCloset)
                    {
                        wsWAP.Cells[1, cntCol].Value = res.Closet;

                        wsWAP.Cells[3, cntCol].Value = res.WapCounts.Wap_C_DC_2802i;
                        wsWAP.Cells[4, cntCol].Value = res.WapCounts.Wap_C_DDK_2802i;
                        wsWAP.Cells[5, cntCol].Value = res.WapCounts.Wap_W_RAM_W_2802i;
                        wsWAP.Cells[6, cntCol].Value = res.WapCounts.Wap_W_RAM_O_2802i;
                        wsWAP.Cells[7, cntCol].Value = res.WapCounts.Wap_O_RAM_2802i;
                        wsWAP.Cells[8, cntCol].Value = res.WapCounts.Wap_C_C2_N_2802i;
                        wsWAP.Cells[9, cntCol].Value = res.WapCounts.Wap_C_R_N_2802i;
                        wsWAP.Cells[10, cntCol].Value = res.WapCounts.Wap_W_RAM_N_2802i;
                        wsWAP.Cells[11, cntCol].Value = res.WapCounts.Wap_O_RAM_N_2802i;
                        wsWAP.Cells[12, cntCol].Value = res.WapCounts.Wap_W_D_W_2802e;
                        wsWAP.Cells[13, cntCol].Value = res.WapCounts.Wap_O_D_W_2802e;
                        wsWAP.Cells[14, cntCol].Value = res.WapCounts.Wap_W_D_N_2802e;
                        wsWAP.Cells[15, cntCol].Value = res.WapCounts.Wap_C_R_2802i;
                        wsWAP.Cells[16, cntCol].Value = res.WapCounts.Wap_C_R_D28_2802e;
                        wsWAP.Cells[17, cntCol].Value = res.WapCounts.Wap_C_R_D38_3802e;
                        wsWAP.Cells[18, cntCol].Value = res.WapCounts.Wap_W_C1_2802e;
                        wsWAP.Cells[19, cntCol].Value = res.WapCounts.Wap_C_C1_2802e;
                        wsWAP.Cells[20, cntCol].Value = res.WapCounts.Wap_W_C1_D_2802e;
                        wsWAP.Cells[21, cntCol].Value = res.WapCounts.Wap_C_C1_D_2802e;
                        wsWAP.Cells[22, cntCol].Value = res.WapCounts.Wap_C_DC_HD_3802i;
                        wsWAP.Cells[23, cntCol].Value = res.WapCounts.Wap_C_R_HD_3802i;
                        wsWAP.Cells[24, cntCol].Value = res.WapCounts.Wap_W_RAM_HD_3802i;
                        wsWAP.Cells[25, cntCol].Value = res.WapCounts.Wap_O_RAM_HD_3802i;
                        wsWAP.Cells[26, cntCol].Value = res.WapCounts.Wap_W_INT_EA_3802p;
                        wsWAP.Cells[27, cntCol].Value = res.WapCounts.Wap_W_INT_F_2802i;
                        wsWAP.Cells[28, cntCol].Value = res.WapCounts.Wap_W_INT_FD_2802e;
                        wsWAP.Cells[29, cntCol].Value = res.WapCounts.Wap_W_EXT_F_1572eac;
                        wsWAP.Cells[30, cntCol].Value = res.WapCounts.Wap_O_EXT_F_1572eac;

                        // ClsCntsPerTrTc wapsCount = LstClsCntsPerTrTc.Where(x => x.Closet == res.Closet).FirstOrDefault();

                        ClsBomCntsByCloset wapsCount = LstClsBomCntsByCloset.Where(x => x.Closet == res.Closet).FirstOrDefault();

                        BW.Cls_BW_BomCounts_Wap cnts = wapsCount.WapCounts;
                                        
                        PropertyInfo[] pis = typeof(BW.Cls_BW_BomCounts_Wap).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
                        //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        int wCnt = 0;
                        foreach (var n in pis)
                        {
                            wCnt = wCnt + (int)cnts.GetType().GetProperty(n.Name).GetValue(cnts, null);
                        }

                        if (wCnt != 0 && wCnt < 24) wsWAP.Cells[33, cntCol].Value = 1;
                        if (wCnt >= 24 & wCnt < 48) wsWAP.Cells[34, cntCol].Value = 1;
                        if (wCnt >= 48 & wCnt < 72) wsWAP.Cells[35, cntCol].Value = 1;
                        if (wCnt >= 72 & wCnt < 96) wsWAP.Cells[36, cntCol].Value = 1;
                        

                        cntCol++;                    
                    }


                    Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.Sort((x, y) => x.ApNumber.CompareTo(y.ApNumber));

                    wsNotAccFor.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    wsNotAccFor.Cells[1, 1].Value = "WAP's Not Accounted For:";

                    for (int i = 0; i < Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom.Count(); i++)
                    {
                        wsNotAccFor.Cells[i + 2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        wsNotAccFor.Cells[i + 2, 1].Value =
                            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].ApNumber + ";  " +
                            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].Bldg + ";  " +
                            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].DisplayCode + ";  " +
                            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].Antenna + ";  " +
                            Cls_BW_TP_APs.LstAtts_wap_data_2_dual_NotCountedOnBom[i].Mount;
                    }



                    #endregion


                    #region Autofilled Waos

                    cntCol = 10; // j

                    foreach (var res in LstClsBomCntsByCloset)
                    {
                        wsWAO.Cells[1, cntCol].Value = res.Closet;

                        wsWAO.Cells[3, cntCol].Value =
                            res.WaoCounts.Wao_Single_CAT6_Cable_Wall_Phone; // +
                                                                            //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_Phone +
                                                                            //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_Camera +
                                                                            //Cls_MMM_Main.clsBomCntsWao.Wao_Single_CAT6_Cable_CardReader;

                        wsWAO.Cells[4, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Office;
                        wsWAO.Cells[5, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Office;
                        wsWAO.Cells[6, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Office;
                        wsWAO.Cells[7, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Office;
                        
                        // quint not in autofill bom
                        //wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Quint_CAT6_Cable_Office; 
                        wsWAO.Cells[8, cntCol].Value = res.WaoCounts.Wao_Sextet_CAT6_Cable_Office;
                        
                        // modular furniture
                        wsWAO.Cells[9, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Modular_Furniture;
                        wsWAO.Cells[10, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Modular_Furniture;

                        // warehouse
                        wsWAO.Cells[11, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Warehouse;
                        wsWAO.Cells[12, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Warehouse;
                        wsWAO.Cells[13, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Warehouse;
                        wsWAO.Cells[14, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Warehouse;
                        // quint not on autofill
                        wsWAO.Cells[15, cntCol].Value = res.WaoCounts.Wao_Sextet_CAT6_Cable_Warehouse;

                        //wsWAO.Cells[15, cntCol].Value = res.WaoCounts.Wao_Single_CAT6_Cable_Liquid_Tight;
                        //wsWAO.Cells[16, cntCol].Value = res.WaoCounts.Wao_Dual_CAT6_Cable_Liquid_Tight;
                        //wsWAO.Cells[17, cntCol].Value = res.WaoCounts.Wao_Triple_CAT6_Cable_Liquid_Tight;
                        //wsWAO.Cells[18, cntCol].Value = res.WaoCounts.Wao_Quad_CAT6_Cable_Liquid_Tight;
                        // quint/sextet 'LT' not in autofill bom
                        // wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Quint_CAT6_Cable_Liquid_Tight;
                        // wsWAO.Cells["A5"].Value = Cls_MMM_Main.clsBomCntsWao.Wao_Sextet_CAT6_Cable_Liquid_Tight;
                        
                   //     ClsCntsPerTrTc waosCount = (ClsCntsPerTrTc)LstClsCntsPerTrTc.Where(x => x.Closet == res.Closet).FirstOrDefault();

                        ClsBomCntsByCloset waosCount = LstClsBomCntsByCloset.Where(x => x.Closet == res.Closet).FirstOrDefault();

                        BW.Cls_BW_BomCounts_Wao cnts = waosCount.WaoCounts;
                        
                        PropertyInfo[] pis = typeof(BW.Cls_BW_BomCounts_Wao).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
                        //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        int wCnt = 0;
                        foreach (var n in pis)
                        {
                            wCnt = wCnt + (int)cnts.GetType().GetProperty(n.Name).GetValue(cnts,null);                            
                        }

                        if (wCnt != 0 && wCnt < 24) wsWAO.Cells[18, cntCol].Value = 1;
                        if (wCnt >= 24 & wCnt < 48) wsWAO.Cells[19, cntCol].Value = 1;
                        if (wCnt >= 48 & wCnt < 72) wsWAO.Cells[20, cntCol].Value = 1;
                        if (wCnt >= 72 & wCnt < 96) wsWAO.Cells[21, cntCol].Value = 1;
                        if (wCnt >= 96 & wCnt < 120) wsWAO.Cells[22, cntCol].Value = 1;
                        if (wCnt >= 120 & wCnt < 144) wsWAO.Cells[23, cntCol].Value = 1;

                        cntCol++;
                    }

                    Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.Sort((x, y) => x.Label1.CompareTo(y.Label1));

                    wsNotAccFor.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    wsNotAccFor.Cells[1, 2].Value = "WAO's Not Accounted For:";

                    for (int i = 0; i < Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom.Count(); i++)
                    {
                        wsNotAccFor.Cells[i + 2, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        wsNotAccFor.Cells[i + 2, 2].Value =
                            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].Label1 + ";  " +
                            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].Bldg + ";  " +
                            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].ClosetInfo + ";  " +
                            Cls_BW_TP_WAOs.LstAtts_waos_NotCountedOnBom[i].DataTotal; 
                    }

                    #endregion


                    ///* Set title, Author.. */
                    //xlPackage.Workbook.Properties.Title = "Title: Office Open XML Sample";
                    //xlPackage.Workbook.Properties.Author = "Author: Muhammad Mubashir.";
                    ////xlPackage.Workbook.Properties.SetCustomPropertyValue("EmployeeID", "1147");
                    //xlPackage.Workbook.Properties.Comments = "Sample Record Details";
                    //xlPackage.Workbook.Properties.Company = "TRG Tech.";

                    //Save
                    xlPackage.Save();


                    MessageBox.Show("Bom Saved To: " + xlsxFileName + Environment.NewLine +
                        "Bom Saved for file: " + doc.Database.Filename);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

   
        
        #endregion

        


    }
}
