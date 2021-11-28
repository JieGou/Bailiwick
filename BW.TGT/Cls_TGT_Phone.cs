using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.TGT
{
    public sealed class Cls_TGT_Phone : Int_TGT_AllAtts 
    {
        public string CABLE_ID { get; set; }
        string Int_TGT_AllAtts.IDF { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.ASSEMBLY_TYPE { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CSKU { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CABLE_1 { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CABLE_2 { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.DESCRIPTION { get => "."; set => throw new NotImplementedException(); }
        public string OWNER { get; set; }

        string Int_TGT_AllAtts.VLAN { get => "."; set => throw new NotImplementedException(); }

        //

        string Int_TGT_AllAtts.MOUNT_TYPE { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.ITEM_PART_NUM { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.ITEM_DESCRIPTION { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.COLOR_CODE { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.COLOR_NAME { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.SPEAKER_WATTAGE { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.ZONE { get => "."; set => throw new NotImplementedException(); }

        //

        string Int_TGT_AllAtts.Drop_Type { get => "."; set => throw new NotImplementedException(); }

        //

        public string PHONE_EXTENSION { get; set; }
        public string PHONE_TYPE { get; set; }


        public string BlockName { get; set; }

        public Handle Handle { get; set; }

        public Point3d InsertionPtOfBlock { get; set; }
    }
}
