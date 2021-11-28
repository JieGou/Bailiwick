using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    static class Cls_BW_BlockAttsGetWapDual
    {     
        public static void GetWapData2DualAttributes(Cls_BW_WapsData2Dual_Atts clsAttsWapData2Dual, List<AttributeReference> attRef)
        {            
            clsAttsWapData2Dual.Label1 = attRef.NoAttInBlockRef("LABEL1").TextString;
            clsAttsWapData2Dual.Label2 = attRef.NoAttInBlockRef("LABEL2").TextString;
            clsAttsWapData2Dual.DataTotal = attRef.NoAttInBlockRef("DATATOTAL").TextString;
            clsAttsWapData2Dual.VoiceTotal = attRef.NoAttInBlockRef("VOICETOTAL").TextString;
            clsAttsWapData2Dual.Single = attRef.NoAttInBlockRef("SINGLE").TextString;
            clsAttsWapData2Dual.Dual = attRef.NoAttInBlockRef("DUAL").TextString;
            clsAttsWapData2Dual.Triple = attRef.NoAttInBlockRef("TRIPLE").TextString;
            clsAttsWapData2Dual.Quad = attRef.NoAttInBlockRef("QUAD").TextString;
            clsAttsWapData2Dual.Quint = attRef.NoAttInBlockRef("QUINT").TextString;
            clsAttsWapData2Dual.Sextet = attRef.NoAttInBlockRef("SEXTET").TextString;
            clsAttsWapData2Dual.Housing = attRef.NoAttInBlockRef("HOUSING").TextString;
            clsAttsWapData2Dual.Site = attRef.NoAttInBlockRef("SITE").TextString;
            clsAttsWapData2Dual.Bldg = attRef.NoAttInBlockRef("BLDG").TextString;
            clsAttsWapData2Dual.Building = attRef.NoAttInBlockRef("BUILDING").TextString;
            clsAttsWapData2Dual.Floor = attRef.NoAttInBlockRef("FLOOR").TextString;
            clsAttsWapData2Dual.ClosetInfo = attRef.NoAttInBlockRef("CLOSETINFO").TextString;
            clsAttsWapData2Dual.DepartmentSuite = attRef.NoAttInBlockRef("DEPARTMENTSUITE").TextString;
            clsAttsWapData2Dual.KeyNote = attRef.NoAttInBlockRef("KEYNOTE").TextString;
            clsAttsWapData2Dual.Device = attRef.NoAttInBlockRef("DEVICE").TextString;
            clsAttsWapData2Dual.Platform = attRef.NoAttInBlockRef("PLATFORM").TextString;
            clsAttsWapData2Dual.CableCombinations = attRef.NoAttInBlockRef("CABLECOMBINATIONS").TextString;
            clsAttsWapData2Dual.Aff = attRef.NoAttInBlockRef("AFF").TextString;
            clsAttsWapData2Dual.OldWaoIdent = attRef.NoAttInBlockRef("OLDWAOIDENT").TextString;
            clsAttsWapData2Dual.Wap = attRef.NoAttInBlockRef("WAP").TextString.PadLeft(3, '0');
            clsAttsWapData2Dual.ApNumber = attRef.NoAttInBlockRef("APNUMBER").TextString.PadLeft(3, '0');
            clsAttsWapData2Dual.Antenna = attRef.NoAttInBlockRef("ANTENNA").TextString;
            clsAttsWapData2Dual.Mount = attRef.NoAttInBlockRef("MOUNT").TextString;

            clsAttsWapData2Dual.CABLE1 = attRef.NoAttInBlockRef("CABLE1").TextString;
            clsAttsWapData2Dual.CABLE1QUANT = attRef.NoAttInBlockRef("CABLE1QUANT").TextString;
            clsAttsWapData2Dual.CABLE2 = attRef.NoAttInBlockRef("CABLE2").TextString;
            clsAttsWapData2Dual.CABLE2QUANT = attRef.NoAttInBlockRef("CABLE2QUANT").TextString;
            clsAttsWapData2Dual.CABLE3 = attRef.NoAttInBlockRef("CABLE3").TextString;
            clsAttsWapData2Dual.CABLE3QUANT = attRef.NoAttInBlockRef("CABLE3QUANT").TextString;

            clsAttsWapData2Dual.PATCH1CLR = attRef.NoAttInBlockRef("PATCH1CLR").TextString;
            clsAttsWapData2Dual.PATCH1LEN = attRef.NoAttInBlockRef("PATCH1LEN").TextString;
            clsAttsWapData2Dual.PATCH1CAT = attRef.NoAttInBlockRef("PATCH1CAT").TextString;
            clsAttsWapData2Dual.PATCH1QUANT = attRef.NoAttInBlockRef("PATCH1QUANT").TextString;

            clsAttsWapData2Dual.PATCH2CLR = attRef.NoAttInBlockRef("PATCH2CLR").TextString;
            clsAttsWapData2Dual.PATCH2LEN = attRef.NoAttInBlockRef("PATCH2LEN").TextString;
            clsAttsWapData2Dual.PATCH2CAT = attRef.NoAttInBlockRef("PATCH2CAT").TextString;
            clsAttsWapData2Dual.PATCH2QUANT = attRef.NoAttInBlockRef("PATCH2QUANT").TextString;

            clsAttsWapData2Dual.PATCH3CLR = attRef.NoAttInBlockRef("PATCH3CLR").TextString;
            clsAttsWapData2Dual.PATCH3LEN = attRef.NoAttInBlockRef("PATCH3LEN").TextString;
            clsAttsWapData2Dual.PATCH3CAT = attRef.NoAttInBlockRef("PATCH3CAT").TextString;
            clsAttsWapData2Dual.PATCH3QUANT = attRef.NoAttInBlockRef("PATCH3QUANT").TextString;

            clsAttsWapData2Dual.PATCH4CLR = attRef.NoAttInBlockRef("PATCH4CLR").TextString;
            clsAttsWapData2Dual.PATCH4LEN = attRef.NoAttInBlockRef("PATCH4LEN").TextString;
            clsAttsWapData2Dual.PATCH4CAT = attRef.NoAttInBlockRef("PATCH4CAT").TextString;
            clsAttsWapData2Dual.PATCH4QUANT = attRef.NoAttInBlockRef("PATCH4QUANT").TextString;

            clsAttsWapData2Dual.SurfaceRaceWay = attRef.NoAttInBlockRef("SURFACERACEWAY").TextString;
            clsAttsWapData2Dual.SpecialFeed2 = attRef.NoAttInBlockRef("SPECIALFEED2").TextString;
            clsAttsWapData2Dual.WrlsSpecialFeed1 = attRef.NoAttInBlockRef("WRLSSPECIALFEED1").TextString.ToUpper();
            clsAttsWapData2Dual.AmReportName = attRef.NoAttInBlockRef("AMREPORTNAME").TextString;
            clsAttsWapData2Dual.AmReportDate = attRef.NoAttInBlockRef("AMREPORTDATE").TextString;
            clsAttsWapData2Dual.AmApNum = attRef.NoAttInBlockRef("AMAPNUM").TextString;
            clsAttsWapData2Dual.DisplayCode = attRef.NoAttInBlockRef("DISPLAYCODE").TextString.ToUpper();
            clsAttsWapData2Dual.DistanceFromClosetInFeet = attRef.NoAttInBlockRef("DISTANCEFROMCLOSETINFEET").TextString;
            clsAttsWapData2Dual.IsLongRun = attRef.NoAttInBlockRef("ISLONGRUN").TextString;

        }

    }
}
