using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.TGT
{
    public sealed class Cls_TGT_Decoy : Int_TGT_AllAtts
    {
        string Int_TGT_AllAtts.CABLE_ID { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.IDF { get => "."; set => throw new NotImplementedException(); }

        public string ASSEMBLY_TYPE { get; set; }
        public string CSKU { get; set; }

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

        string Int_TGT_AllAtts.PHONE_EXTENSION { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.PHONE_TYPE { get => "."; set => throw new NotImplementedException(); }


        public string BlockName { get; set; }

        public Handle Handle { get; set; }

        public Point3d InsertionPtOfBlock { get; set; }
    }
}
