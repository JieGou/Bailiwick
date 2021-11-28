using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public sealed class Cls_BW_Waps_Atts : Int_BW_AllAtts
    {        
        public string Wap { get; set; }      
        public string Antenna { get; set; }
        public string Mount { get; set; }

        string Int_BW_AllAtts.BlockName { get => "wap"; set => throw new NotImplementedException(); }

        public string Label1 { get; set; }

        public string Label2 { get; set; }
        string Int_BW_AllAtts.Site { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Building { get => "."; set => throw new NotImplementedException(); }

        string Int_BW_AllAtts.Bldg { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Conduit { get => "."; set => throw new NotImplementedException(); }
        public string ApNumber { get; set; }

        string Int_BW_AllAtts.Floor { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.ClosetInfo { get => "."; set => throw new NotImplementedException(); }

        string Int_BW_AllAtts.CableCombinations { get => "."; set => throw new NotImplementedException(); }

        public string WrlsSpecialFeed1 { get; set; }
        public Handle Handle { get; set; }


        public string Brand { get; set; }
        public string Series { get; set; }
        string Int_BW_AllAtts.OldWaoIdent { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Aff { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Label2 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.VoiceTotal { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.DataTotal { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Platform { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.Device { get => "."; set => throw new NotImplementedException(); }
   //     string Int_MMM_AllAtts.WaoType { get => "."; set => throw new NotImplementedException(); }
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

        string Int_BW_AllAtts.SurfaceRaceWay { get => "."; set => throw new NotImplementedException(); }

        string Int_BW_AllAtts.AmReportName { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.AmReportDate { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.AmApNum { get => "."; set => throw new NotImplementedException(); }        
        public string DisplayCode { get; set; }

        string Int_BW_AllAtts.CABLE1 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.CABLE1QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.CABLE2 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.CABLE2QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.CABLE3 { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.CABLE3QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH1CLR { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH1LEN { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH1CAT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH1QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH2CLR { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH2LEN { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH2CAT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH2QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH3CLR { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH3LEN { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH3CAT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH3QUANT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH4CLR { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH4LEN { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH4CAT { get => "."; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.PATCH4QUANT { get => "."; set => throw new NotImplementedException(); }


        public Point3d InsertionPtOfBlock { get; set; }
        //   Point3d Int_MMM_AllAtts.InsertionPtOfBlock { get => new Point3d(); set => throw new NotImplementedException(); }
        //    double Int_MMM_AllAtts.DistFromUpperLeft { get => 0.0; set => throw new NotImplementedException(); }
        string Int_BW_AllAtts.DistanceFromClosetInFeet { get => "."; set => throw new NotImplementedException(); }


        public string IsLongRun { get; set; }

    }
}
