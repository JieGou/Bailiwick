using System.Collections.Generic;

namespace MyFirstProject.BW.TGT
{
    public class Cls_TGT_BlksToSelect
    {

        public List<string> GetBlockNames()
        {
            List<string> blkNames = new List<string>();
            
            blkNames.Add("TVS-I*");
            blkNames.Add("TVS-E*");
            blkNames.Add("T-SPK Speaker-Paging");
            blkNames.Add("T-SPK Speaker-Music");

            blkNames.Add("T1-COM-DATA");
            blkNames.Add("T4-COM-DATA");

            ////
            //blkNames.Add("T4 Data");
            //blkNames.Add("T1 Data");
            //blkNames.Add("T4 Data Drop");
            //blkNames.Add("T1 Data Drop");
            //blkNames.Add("T1 LCD Monitor");      
            //blkNames.Add("TVS Wall Access View Monitor");
            //blkNames.Add("TVS2 Covert");
            //blkNames.Add("TVS2 APVM Power Supply-3");
            //blkNames.Add("Phone Placeholder");
            //blkNames.Add("TVS Exterior Cluster");
            //blkNames.Add("TVS Exterior Parapet Mount");
            //blkNames.Add("T1 Existing WLAN Antenna");          
            //blkNames.Add("Mac Location");
            //blkNames.Add("VDP-RF*");
      
            return blkNames;
        }

    }
}
