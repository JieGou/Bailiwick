using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.WLG
{
    public sealed class Cls_WLG_Atts : Int_WLG_AllAtts 
    {

        //      string Int_TGT_AllAtts.CABLE_ID { get => "."; set => throw new NotImplementedException(); }

        public string Status { get; set; }

        public string LineNumber { get; set; }
        public string CableType { get; set; }
        public string CableColor { get; set; }
              
        public string From { get; set; }
        public string To { get; set; }
     
     
        public string System { get; set; }
        public string Device { get; set; }
        public string CableLabel { get; set; }

        public string JackLabel { get; set; }
        public string JackColor { get; set; }
        public string Notes { get; set; }

        public string Department { get; set; }
        public string Port { get; set; }
        public string Patch { get; set; }  
    
        public string Size { get; set; }

        public string OldNumber { get; set; }


        public string BlockName { get; set; }
        public Handle Handle { get; set; }
        public Point3d InsertionPtOfBlock { get; set; }
    }
}
