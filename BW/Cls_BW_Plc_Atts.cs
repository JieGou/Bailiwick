using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Cls_BW_Plc_Atts : Int_BW_AllAtts
    {
        string Int_BW_AllAtts.Wap { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Antenna { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Mount { get => "."; set => throw new NotImplementedException(); }

        public string BlockName { get; set; }

        public string Label1 { get; set; }

        public string Label2 { get; set; }
        public string Site { get; set; }
        public string Building { get; set; }

        public string Bldg { get; set; }
        public string Conduit { get; set; }
        string Int_BW_AllAtts.ApNumber { get => "."; set => throw new NotImplementedException(); }

        public string Floor { get; set; }
        public string ClosetInfo { get; set; }

        public string CableCombinations { get; set; }

        string Int_BW_AllAtts.SurfaceRaceWay { get => "."; set => throw new NotImplementedException(); }

        string Int_BW_AllAtts.WrlsSpecialFeed1 { get => "."; set => throw new NotImplementedException(); }
        public Handle Handle { get; set; }


        string Int_BW_AllAtts.Brand { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Series { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.OldWaoIdent { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Aff { get => "."; set => throw new NotImplementedException(); }
   //     string Int_BW_AllAtts.Label2 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.VoiceTotal { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.DataTotal { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Platform { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Device { get => "."; set => throw new NotImplementedException(); }
    //    string Int_MMM_AllAtts.WaoType { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.DepartmentSuite { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.JackType { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.JackColor { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Housing { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Single { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Dual { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Triple { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Quad { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Quint { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Sextet { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.SpecialFeed2 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.KeyNote { get => "."; set => throw new NotImplementedException(); }

        string Int_BW_AllAtts.AmReportName { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.AmReportDate { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.AmApNum { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.DisplayCode { get => "."; set => throw new NotImplementedException(); }

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
     //   public double DistFromUpperLeft { get; set; }
        public string DistanceFromClosetInFeet { get; set; }
        public string IsLongRun { get; set; }



    }
}
