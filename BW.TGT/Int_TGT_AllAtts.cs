
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.TGT
{
    public interface Int_TGT_AllAtts
    {
        string CABLE_ID { get; set; }
        string IDF { get; set; }
        string ASSEMBLY_TYPE { get; set; }
        string CSKU { get; set; }
        string CABLE_1 { get; set; }
        string CABLE_2 { get; set; }
        string DESCRIPTION { get; set; }
        string OWNER { get; set; }

        //string PV { get; set; }

        //string AV { get; set; }

        //string PTZ { get; set; }


        //string TVS_ORDERABLE_ROW_NUM { get; set; }

        string VLAN { get; set; }
   
        //string ITEM { get; set; }
        //string OTHER { get; set; }


        //

        string MOUNT_TYPE { get; set; }
        string ITEM_PART_NUM { get; set; }
        string ITEM_DESCRIPTION { get; set; }
        string COLOR_CODE { get; set; }
        string COLOR_NAME { get; set; }
        string SPEAKER_WATTAGE { get; set; }
        string ZONE { get; set; }

        //

        string Drop_Type { get; set; }

        //

        string PHONE_EXTENSION { get; set; }
        string PHONE_TYPE { get; set; }


        string BlockName { get; set; }

        Handle Handle { get; set; }

        Point3d InsertionPtOfBlock { get; set; }
    }
}
