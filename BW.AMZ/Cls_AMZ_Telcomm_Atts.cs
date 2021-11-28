
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.AMZ
{
    public sealed class Cls_AMZ_TelComm_Atts : Int_AMZ_TelCommAtts 
    {
        public string IDF { get; set; }
        public string Num { get; set; }
        public string NetDev1 { get; set; }
        public string NetDev2 { get; set; }
        public string DevInfo { get; set; }
        public string Mount { get; set; }
        public string RF6 { get; set; }
        public string AFF { get; set; }
        public string Data { get; set; }
        public string BlockName { get; set; }
        public Handle Handle { get; set; }
        public Point3d InsertionPtOfBlock { get; set; }

    }

}
