using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    class Cls_BW_BlockAttsGetWao
    {  
        public static void GetWaoAttributes(Cls_BW_Waos_Atts clsWAOsAtts, List<AttributeReference> attRef)
        {
            clsWAOsAtts.Label1 = attRef.NoAttInBlockRef("LABEL1").TextString;
            clsWAOsAtts.Label2 = attRef.NoAttInBlockRef("LABEL2").TextString;
            clsWAOsAtts.Site = attRef.NoAttInBlockRef("SITE").TextString;
            clsWAOsAtts.Bldg = attRef.NoAttInBlockRef("BLDG").TextString;
            clsWAOsAtts.Building = attRef.NoAttInBlockRef("BUILDING").TextString;
            clsWAOsAtts.Conduit = attRef.NoAttInBlockRef("CONDUIT").TextString;
            clsWAOsAtts.Floor = attRef.NoAttInBlockRef("FLOOR").TextString;
            clsWAOsAtts.ClosetInfo = attRef.NoAttInBlockRef("CLOSETINFO").TextString;
            clsWAOsAtts.CableCombinations = attRef.NoAttInBlockRef("CABLECOMBINATIONS").TextString;
            clsWAOsAtts.Device = attRef.NoAttInBlockRef("DEVICE").TextString;
            clsWAOsAtts.Aff = attRef.NoAttInBlockRef("AFF").TextString;
            clsWAOsAtts.OldWaoIdent = attRef.NoAttInBlockRef("OLDWAOIDENT").TextString;
            clsWAOsAtts.Single = attRef.NoAttInBlockRef("SINGLE").TextString;
            clsWAOsAtts.Dual = attRef.NoAttInBlockRef("DUAL").TextString;
            clsWAOsAtts.Triple = attRef.NoAttInBlockRef("TRIPLE").TextString;
            clsWAOsAtts.Quad = attRef.NoAttInBlockRef("QUAD").TextString;
            clsWAOsAtts.Quint = attRef.NoAttInBlockRef("QUINT").TextString;
            clsWAOsAtts.Sextet = attRef.NoAttInBlockRef("SEXTET").TextString;
            clsWAOsAtts.Housing = attRef.NoAttInBlockRef("HOUSING").TextString;
            clsWAOsAtts.JackColor = attRef.NoAttInBlockRef("JACKCOLOR").TextString;
            clsWAOsAtts.JackType = attRef.NoAttInBlockRef("JACKTYPE").TextString;

            clsWAOsAtts.CABLE1 = attRef.NoAttInBlockRef("CABLE1").TextString;
            clsWAOsAtts.CABLE1QUANT = attRef.NoAttInBlockRef("CABLE1QUANT").TextString;
            clsWAOsAtts.CABLE2 = attRef.NoAttInBlockRef("CABLE2").TextString;
            clsWAOsAtts.CABLE2QUANT = attRef.NoAttInBlockRef("CABLE2QUANT").TextString;
            clsWAOsAtts.CABLE3 = attRef.NoAttInBlockRef("CABLE3").TextString;
            clsWAOsAtts.CABLE3QUANT = attRef.NoAttInBlockRef("CABLE3QUANT").TextString;

            clsWAOsAtts.PATCH1CLR = attRef.NoAttInBlockRef("PATCH1CLR").TextString;
            clsWAOsAtts.PATCH1LEN = attRef.NoAttInBlockRef("PATCH1LEN").TextString;
            clsWAOsAtts.PATCH1CAT = attRef.NoAttInBlockRef("PATCH1CAT").TextString;
            clsWAOsAtts.PATCH1QUANT = attRef.NoAttInBlockRef("PATCH1QUANT").TextString;

            clsWAOsAtts.PATCH2CLR = attRef.NoAttInBlockRef("PATCH2CLR").TextString;
            clsWAOsAtts.PATCH2LEN = attRef.NoAttInBlockRef("PATCH2LEN").TextString;
            clsWAOsAtts.PATCH2CAT = attRef.NoAttInBlockRef("PATCH2CAT").TextString;
            clsWAOsAtts.PATCH2QUANT = attRef.NoAttInBlockRef("PATCH2QUANT").TextString;

            clsWAOsAtts.PATCH3CLR = attRef.NoAttInBlockRef("PATCH3CLR").TextString;
            clsWAOsAtts.PATCH3LEN = attRef.NoAttInBlockRef("PATCH3LEN").TextString;
            clsWAOsAtts.PATCH3CAT = attRef.NoAttInBlockRef("PATCH3CAT").TextString;
            clsWAOsAtts.PATCH3QUANT = attRef.NoAttInBlockRef("PATCH3QUANT").TextString;

            clsWAOsAtts.PATCH4CLR = attRef.NoAttInBlockRef("PATCH4CLR").TextString;
            clsWAOsAtts.PATCH4LEN = attRef.NoAttInBlockRef("PATCH4LEN").TextString;
            clsWAOsAtts.PATCH4CAT = attRef.NoAttInBlockRef("PATCH4CAT").TextString;
            clsWAOsAtts.PATCH4QUANT = attRef.NoAttInBlockRef("PATCH4QUANT").TextString;

            clsWAOsAtts.DataTotal = attRef.NoAttInBlockRef("DATATOTAL").TextString;
            clsWAOsAtts.VoiceTotal = attRef.NoAttInBlockRef("VOICETOTAL").TextString;
            clsWAOsAtts.DepartmentSuite = attRef.NoAttInBlockRef("DEPARTMENTSUITE").TextString;
            clsWAOsAtts.Platform = attRef.NoAttInBlockRef("PLATFORM").TextString;

            clsWAOsAtts.DistanceFromClosetInFeet = attRef.NoAttInBlockRef("DISTANCEFROMCLOSETINFEET").TextString;
            clsWAOsAtts.IsLongRun = attRef.NoAttInBlockRef("ISLONGRUN").TextString;

        }
 

    }
}
