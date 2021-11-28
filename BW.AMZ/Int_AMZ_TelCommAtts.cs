

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.AMZ
{
    public interface Int_AMZ_TelCommAtts
    {
        string IDF { get; set; }
        string Num { get; set; }
        string NetDev1 { get; set; }
        string NetDev2 { get; set; }
        string DevInfo { get; set; }
        string Mount { get; set; }
        string RF6 { get; set; }
        string AFF { get; set; }
        string Data { get; set; }

        string BlockName { get; set; }
        Handle Handle { get; set; }
        Point3d InsertionPtOfBlock { get; set; }
    }
}
