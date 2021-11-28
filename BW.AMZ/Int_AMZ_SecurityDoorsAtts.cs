using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.AMZ
{
    public interface Int_AMZ_SecurityDoorsAtts
    {
        string DoorType { get; set; }
        string Module { get; set; }
        string Function { get; set; }
        string DeviceDesc { get; set; }
        string Description { get; set; }
        string DetailNumber { get; set; }
        string DoorNumber { get; set; }
        string PortNumber { get; set; }
        string BoardAddress { get; set; }
        string Panel { get; set; }
        string Controller { get; set; }
        string Isc { get; set; }
        string SiteCode { get; set; }
        string Cable_Id { get; set; }
        string Floor { get; set; }
        string DeviceID { get; set; }
        string Device_Type { get; set; }
        string DoorHwset { get; set; }


        string ProgramMatrixName { get; set; }
        string ProgramMatrixDeviceId { get; set; }
        string Pin { get; set; }
        string PairedMaster { get; set; }
        string PairedSlave { get; set; }
        string LockType { get; set; }
        string RexType { get; set; }
        string RexSupervision { get; set; }
        string AssumeDoorUsed { get; set; }
        string InputSupervision { get; set; }
        string Linkage { get; set; }
        string AuxInputOutputs { get; set; }
        string Template { get; set; }




        string BlockName { get; set; }
        Handle Handle { get; set; }
        Point3d InsertionPtOfBlock { get; set; }

    }
}
