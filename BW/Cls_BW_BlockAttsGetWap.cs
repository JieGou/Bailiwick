using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    class Cls_BW_BlockAttsGetWap
    {         
        public static void GetWapAttributes(Cls_BW_Waps_Atts clsAttsWaps, List<AttributeReference> attRef)
        {
            clsAttsWaps.Wap = attRef.NoAttInBlockRef("WAP").TextString.PadLeft(3, '0');
            clsAttsWaps.ApNumber = attRef.NoAttInBlockRef("APNUMBER").TextString.PadLeft(3, '0');
            clsAttsWaps.Label1 = attRef.NoAttInBlockRef("LABEL1").TextString;
            clsAttsWaps.Antenna = attRef.NoAttInBlockRef("ANTENNA").TextString;
            clsAttsWaps.Mount = attRef.NoAttInBlockRef("MOUNT").TextString;
            clsAttsWaps.WrlsSpecialFeed1 = attRef.NoAttInBlockRef("WRLSSPECIALFEED1").TextString.ToUpper();
            clsAttsWaps.DisplayCode = attRef.NoAttInBlockRef("DISPLAYCODE").TextString.ToUpper();
            clsAttsWaps.Brand = attRef.NoAttInBlockRef("BRAND").TextString;
            clsAttsWaps.Series = attRef.NoAttInBlockRef("SERIES").TextString;
        }
 
    }
}
