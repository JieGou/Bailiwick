
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.THD
{
    public sealed class Cls_THD_Atts : Int_AllAtts 
    {
        public string Count { get; set; }
        public string Name { get; set; }
        public string DataTotal { get; set; }
        public string Device { get; set; }

        public string Label1 { get; set; }
        public string Label2 { get; set; }
        public string Label3 { get; set; }
        public string Label4 { get; set; }

        public string Name1 { get; set; }

        public string Room { get; set; }

        public string SetType1 { get; set; }
        public string SetType2 { get; set; }
        public string SetType3 { get; set; }
        public string SetType4 { get; set; }


        public string BlockName { get; set; }
        public Handle Handle { get; set; }
        public Point3d InsertionPtOfBlock { get; set; }

    }
}
