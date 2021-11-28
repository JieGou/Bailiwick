using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW.WLG
{
    public interface Int_WLG_AllAtts
    {
        string Status { get; set; }

        string LineNumber { get; set; }
        string CableType { get; set; }
        string CableColor { get; set; }

        string From { get; set; }
        string To { get; set; }     
  
        string System { get; set; }
        string Device { get; set; }        
        string CableLabel { get; set; }

        string JackLabel { get; set; }
        string JackColor { get; set; }
        string Notes { get; set; }

        string Department { get; set; }
        string Port { get; set; }
        string Patch { get; set; }        
    
        string Size { get; set; }


        string OldNumber { get; set; }



        string BlockName { get; set; }
        Handle Handle { get; set; }
        Point3d InsertionPtOfBlock { get; set; }
    }
}
