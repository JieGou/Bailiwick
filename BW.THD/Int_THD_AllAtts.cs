

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.THD
{
    public interface Int_AllAtts
    {
        string Count { get; set; }
        string Name { get; set; }
        string DataTotal { get; set; }
        string Device { get; set; }

        string Label1 { get; set; }
        string Label2 { get; set; }
        string Label3 { get; set; }
        string Label4 { get; set; }

        string Name1 { get; set; }

        string Room { get; set; }

        string SetType1 { get; set; }
        string SetType2 { get; set; }
        string SetType3 { get; set; }
        string SetType4 { get; set; }

        string BlockName { get; set; }
        Handle Handle { get; set; }
        Point3d InsertionPtOfBlock { get; set; }
    }
}
