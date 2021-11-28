using Autodesk.AutoCAD.Runtime;

namespace MyFirstProject.BW
{
    public class Cls_BW_BlksToJig
    {       

        #region card Reader / camera

        [CommandMethod("c1")]
        public static void Card1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "LABEL2",
                "CR",
                "",
                "",
                "dyn data 1 single"
                );
        }
        [CommandMethod("c2")]
        public static void Camera1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "",
                "",
                "",
                "",
                "dyn data 1 single camera"
                );
        }

        #endregion


        #region voice

        [CommandMethod("v1")]
        public static void Voice1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "",
                "",
                "",
                "",
                "dyn voice 1 single"
                );
        }
        [CommandMethod("v2")]
        public static void Voice2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "LABEL2",
                "WALL",
                "DEPARTMENTSUITE",
                "WALL PHONE",
                "dyn voice 1 single wall"
                );
        }
        #endregion


        #region waos

        //warehouse
        [CommandMethod("ww1")]
        public static void Wao1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "WAREHOUSE",
            "dyn data 1 single"
            );
        }
        [CommandMethod("ww2")]
        public static void Wao2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "WAREHOUSE",
            "dyn data 2 dual"
            );
        }
        [CommandMethod("ww3")]
        public static void Wao3()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "WAREHOUSE",
            "dyn data 3 triple"
            );
        }
        [CommandMethod("ww4")]
        public static void Wao4()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "WAREHOUSE",
            "dyn data 4 quad"
            );
        }
        [CommandMethod("ww5")]
        public static void Wao5()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "WAREHOUSE",
            "dyn data 5 quint"
            );
        }
        [CommandMethod("ww6")]
        public static void Wao6()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "",
             "",
             "DEPARTMENTSUITE",
             "WAREHOUSE",
             "dyn data 6 sextet"
             );
        }

        //office
        [CommandMethod("ow1")]
        public static void oWao1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "OFFICE",
            "dyn data 1 single"
            );
        }
        [CommandMethod("ow2")]
        public static void oWao2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "OFFICE",
            "dyn data 2 dual"
            );
        }
        [CommandMethod("ow3")]
        public static void oWao3()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "OFFICE",
            "dyn data 3 triple"
            );
        }
        [CommandMethod("ow4")]
        public static void oWao4()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "OFFICE",
            "dyn data 4 quad"
            );
        }
        [CommandMethod("ow5")]
        public static void oWao5()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "OFFICE",
            "dyn data 5 quint"
            );
        }
        [CommandMethod("ow6")]
        public static void oWao6()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "",
             "",
             "DEPARTMENTSUITE",
             "OFFICE",
             "dyn data 6 sextet"
             );
        }

        //modular funiture
        [CommandMethod("mw1")]
        public static void mWao1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "MODULAR FURNITURE",
            "dyn data 1 single"
            );
        }
        [CommandMethod("mw2")]
        public static void mWao2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "MODULAR FURNITURE",
            "dyn data 2 dual"
            );
        }
        [CommandMethod("mw3")]
        public static void mWao3()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "MODULAR FURNITURE",
            "dyn data 3 triple"
            );
        }
        [CommandMethod("mw4")]
        public static void mWao4()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "MODULAR FURNITURE",
            "dyn data 4 quad"
            );
        }
        [CommandMethod("mw5")]
        public static void mWao5()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
            "LABEL1",
            "",
            "",
            "DEPARTMENTSUITE",
            "MODULAR FURNITURE",
            "dyn data 5 quint"
            );
        }
        [CommandMethod("mw6")]
        public static void mWao6()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "",
             "",
             "DEPARTMENTSUITE",
             "MODULAR FURNITURE",
             "dyn data 6 sextet"
             );
        }
        #endregion


        #region waos liguid tight

        [CommandMethod("l1")]
        public static void Waol1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "CONDUIT",
             "LIQUID TIGHT",
             "",
             "",
             "dyn data 1 single"
             );
        }
        [CommandMethod("l2")]
        public static void Waol2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "CONDUIT",
             "LIQUID TIGHT",
             "",
             "",
             "dyn data 2 dual"
             );
        }
        [CommandMethod("l3")]
        public static void Waol3()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
             "LABEL1",
             "CONDUIT",
             "LIQUID TIGHT",
             "",
             "",
             "dyn data 3 triple"
             );
        }
        [CommandMethod("l4")]
        public static void Waol4()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
              "LABEL1",
              "CONDUIT",
              "LIQUID TIGHT",
              "",
              "",
              "dyn data 4 quad"
              );
        }
        [CommandMethod("l5")]
        public static void Waol5()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
              "LABEL1",
              "CONDUIT",
              "LIQUID TIGHT",
              "",
              "",
              "dyn data 5 quint"
              );
        }
        [CommandMethod("l6")]
        public static void Waol6()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
              "LABEL1",
              "CONDUIT",
              "LIQUID TIGHT",
              "",
              "",
              "dyn data 6 sextet"
              );
        }
       
        #endregion


        #region plc's

        [CommandMethod("p1")]
        public static void Plc1()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
               "LABEL1",
               "",
               "",
               "DEPARTMENTSUITE",
               "AUTOMATION",
               "dyn plc 1 single"
               );
        }
        [CommandMethod("p2")]
        public static void Plc2()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
               "LABEL1",
               "",
               "",
               "DEPARTMENTSUITE",
               "AUTOMATION",
               "dyn plc 2 dual"
               );
        }
        [CommandMethod("p3")]
        public static void Plc3()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
               "LABEL1",
               "",
               "",
               "DEPARTMENTSUITE",
               "AUTOMATION",
               "dyn plc 3 triple"
               );
        }
        [CommandMethod("p4")]
        public static void Plc4()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "",
                "",
                "DEPARTMENTSUITE",
                "AUTOMATION",
                "dyn plc 4 quad"
                );
        }
        [CommandMethod("p5")]
        public static void Plc5()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "",
                "",
                "DEPARTMENTSUITE",
                "AUTOMATION",
                "dyn plc 5 quint"
                );
        }
        [CommandMethod("p6")]
        public static void Plc6()
        {
            Cls_BW_DynamicBlockAlignmentJig.BlockAttributeJig_Method(
                "LABEL1",
                "",
                "",
                "DEPARTMENTSUITE",
                "AUTOMATION",
                "dyn plc 6 sextet"
                );
        }
      
        #endregion


    }
}
