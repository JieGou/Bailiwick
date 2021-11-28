using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public interface Int_BW_AllAtts
    {
        string Wap { get; set; }
        string Antenna { get; set; }
        string Mount { get; set; }
        string BlockName { get; set; }
        string Brand { get; set; }
        string Series { get; set; }
        string Label1 { get; set; }
        string Site { get; set; }
        string Building { get; set; }

        string Bldg { get; set; }
        string Conduit { get; set; }
        string ApNumber { get; set; }

        string Floor { get; set; }
        string ClosetInfo { get; set; }
        string OldWaoIdent { get; set; }
        string CableCombinations { get; set; }
        string Aff { get; set; }
        string Label2 { get; set; }
        string VoiceTotal { get; set; }
        string DataTotal { get; set; }
        string Platform { get; set; }
        string Device { get; set; }
   //     string WaoType { get; set; }
        string DepartmentSuite { get; set; }
        string JackType { get; set; }
        string JackColor { get; set; }
        string Housing { get; set; }
        string Single { get; set; }
        string Dual { get; set; }
        string Triple { get; set; }
        string Quad { get; set; }
        string Quint { get; set; }
        string Sextet { get; set; }
        string SpecialFeed2 { get; set; }
        string KeyNote { get; set; }

        string SurfaceRaceWay { get; set; }

        string WrlsSpecialFeed1 { get; set; }
        Handle Handle { get; set; }

        string AmReportName { get; set; }
        string AmReportDate { get; set; }
        string AmApNum { get; set; }
        string DisplayCode { get; set; }

        //plc
        string CABLE1 { get; set; }
        string CABLE1QUANT { get; set; }
        string CABLE2 { get; set; }
        string CABLE2QUANT { get; set; }
        string CABLE3 { get; set; }
        string CABLE3QUANT { get; set; }
        string PATCH1CLR { get; set; }
        string PATCH1LEN { get; set; }
        string PATCH1CAT { get; set; }
        string PATCH1QUANT { get; set; }
        string PATCH2CLR { get; set; }
        string PATCH2LEN { get; set; }
        string PATCH2CAT { get; set; }
        string PATCH2QUANT { get; set; }
        string PATCH3CLR { get; set; }
        string PATCH3LEN { get; set; }
        string PATCH3CAT { get; set; }
        string PATCH3QUANT { get; set; }
        string PATCH4CLR { get; set; }
        string PATCH4LEN { get; set; }
        string PATCH4CAT { get; set; }
        string PATCH4QUANT { get; set; }

        Point3d InsertionPtOfBlock { get; set; }
        //double DistFromUpperLeft { get; set; }
        string DistanceFromClosetInFeet { get; set; }
        string IsLongRun { get; set; }
        
    }
}
