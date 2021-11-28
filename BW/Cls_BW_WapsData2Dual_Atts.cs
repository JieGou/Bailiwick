using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public sealed class Cls_BW_WapsData2Dual_Atts : Int_BW_AllAtts
    {
        public string Wap { get; set; }
        public string Antenna { get; set; }
        public string Mount { get; set; }

        string Int_BW_AllAtts.BlockName { get => "wap data 2 dual"; set => throw new NotImplementedException(); }

        public string Label1 { get; set; }

        public string Site { get; set; }
        public string Building { get; set; }

        public string Bldg { get; set; }
        string Int_BW_AllAtts.Conduit { get => "."; set => throw new NotImplementedException(); }
        public string ApNumber { get; set; }

        public string Floor { get; set; }
        public string ClosetInfo { get; set; }

        public string CableCombinations { get; set; }

        public string WrlsSpecialFeed1 { get; set; }
        public Handle Handle { get; set; }


        string Int_BW_AllAtts.Brand { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Series { get => "."; set => throw new NotImplementedException(); }
        public string OldWaoIdent { get; set; }
        public string Aff { get; set; }
        public string Label2 { get; set; }
        public string VoiceTotal { get; set; }
        public string DataTotal { get; set; }
        public string Platform { get; set; }
        public string Device { get; set; }
        public string WaoType { get; set; }
        public string DepartmentSuite { get; set; }
        public string JackType { get; set; }
        public string JackColor { get; set; }
        public string Housing { get; set; }
        public string Single { get; set; }
        public string Dual { get; set; }
        public string Triple { get; set; }
        public string Quad { get; set; }
        public string Quint { get; set; }
        public string Sextet { get; set; }
        public string SpecialFeed2 { get; set; }
        public string KeyNote { get; set; }

        public string SurfaceRaceWay { get; set; }

        public string AmReportName { get; set; }
        public string AmReportDate { get; set; }
        public string AmApNum { get; set; }
        public string DisplayCode { get; set; }

        public string CABLE1 { get; set; }
        public string CABLE1QUANT { get; set; }
        public string CABLE2 { get; set; }
        public string CABLE2QUANT { get; set; }
        public string CABLE3 { get; set; }
        public string CABLE3QUANT { get; set; }
        public string PATCH1CLR { get; set; }
        public string PATCH1LEN { get; set; }
        public string PATCH1CAT { get; set; }
        public string PATCH1QUANT { get; set; }
        public string PATCH2CLR { get; set; }
        public string PATCH2LEN { get; set; }
        public string PATCH2CAT { get; set; }
        public string PATCH2QUANT { get; set; }
        public string PATCH3CLR { get; set; }
        public string PATCH3LEN { get; set; }
        public string PATCH3CAT { get; set; }
        public string PATCH3QUANT { get; set; }
        public string PATCH4CLR { get; set; }
        public string PATCH4LEN { get; set; }
        public string PATCH4CAT { get; set; }
        public string PATCH4QUANT { get; set; }

        public Point3d InsertionPtOfBlock { get; set; }
      //  double Int_MMM_AllAtts.DistFromUpperLeft { get => 0.0; set => throw new NotImplementedException(); }
        public string DistanceFromClosetInFeet { get; set; }
        public string IsLongRun { get; set; }

    }
}
