using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.TGT
{
    public sealed class Cls_TGT_Speaker : Int_TGT_AllAtts
    {
        string Int_TGT_AllAtts.CABLE_ID { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.IDF { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.ASSEMBLY_TYPE { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CSKU { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CABLE_1 { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.CABLE_2 { get => "."; set => throw new NotImplementedException(); }        
        string Int_TGT_AllAtts.DESCRIPTION { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.OWNER { get => "."; set => throw new NotImplementedException(); }

        string Int_TGT_AllAtts.VLAN { get => "."; set => throw new NotImplementedException(); }

        //

        public string MOUNT_TYPE { get; set; }
        public string ITEM_PART_NUM { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string COLOR_CODE { get; set; }
        public string COLOR_NAME { get; set; }
        public string SPEAKER_WATTAGE { get; set; }
        public string ZONE { get; set; }

        //

        string Int_TGT_AllAtts.Drop_Type { get => "."; set => throw new NotImplementedException(); }

        string Int_TGT_AllAtts.PHONE_EXTENSION { get => "."; set => throw new NotImplementedException(); }
        string Int_TGT_AllAtts.PHONE_TYPE { get => "."; set => throw new NotImplementedException(); }


        public string BlockName { get; set; }

        public Handle Handle { get; set; }

        public Point3d InsertionPtOfBlock { get; set; }

    }
}
