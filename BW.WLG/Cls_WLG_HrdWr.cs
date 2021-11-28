using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.WLG
{
    public sealed class Cls_WLG_HrdWr
    {
        //      string Int_TGT_AllAtts.CABLE_ID { get => "."; set => throw new NotImplementedException(); }

        public string Housing { get; set; }
        public string HousingValue { get; set; }             

        public string BlockName { get; set; }
        public Handle Handle { get; set; }
        public Point3d InsertionPtOfBlock { get; set; }
    }
}
