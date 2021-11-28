
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.AMZ
{
    public sealed class Cls_AMZ_SecurityDoors_Atts : Int_AMZ_SecurityDoorsAtts 
    {
        public string DoorType { get; set; }
        public string Module { get; set; }
        public string Function { get; set; }
        public string DeviceDesc { get; set; }
        public string Description { get; set; }
        public string DetailNumber { get; set; }
        public string DoorNumber { get; set; }
        public string PortNumber { get; set; }
        public string BoardAddress { get; set; }
        public string Panel { get; set; }
        public string Controller { get; set; }
        public string Isc { get; set; }
        public string SiteCode { get; set; }
        public string Cable_Id { get; set; }
        public string Floor { get; set; }
        public string DeviceID { get; set; }
        public string Device_Type { get; set; }
        public string DoorHwset { get; set; }


        public string ProgramMatrixName { get; set; }
        public string ProgramMatrixDeviceId { get; set; }
        public string Pin { get; set; }
        public string PairedMaster { get; set; }
        public string PairedSlave { get; set; }
        public string LockType { get; set; }
        public string RexType { get; set; }
        public string RexSupervision { get; set; }
        public string AssumeDoorUsed { get; set; }
        public string InputSupervision { get; set; }
        public string Linkage { get; set; }
        public string AuxInputOutputs { get; set; }
        public string Template { get; set; }


        public string BlockName { get; set; }
        public Handle Handle { get; set; }
        public Point3d InsertionPtOfBlock { get; set; }
    }
}
