using System.Collections.Generic;

namespace MyFirstProject.BW
{
    public static class Cls_BW_BlksToSelect
    {
        public static List<string> GetBlockNames()
        {
            List<string> blkNames = new List<string>();

            blkNames.Add("wap");
            blkNames.Add("wap data 2 dual");
            blkNames.Add("voice 1 single");
            blkNames.Add("voice 1 single wall");
            blkNames.Add("data 1 single camera");
            blkNames.Add("data 1 single");
            blkNames.Add("data 2 dual");
            blkNames.Add("data 3 triple");
            blkNames.Add("data 4 quad");
            blkNames.Add("data 5 quint");
            blkNames.Add("data 6 sextet");
            blkNames.Add("fiber 6 sextet");
            blkNames.Add("plc 1 single");
            blkNames.Add("plc 2 dual");
            blkNames.Add("plc 3 triple");
            blkNames.Add("plc 4 quad");
            blkNames.Add("plc 5 quint");
            blkNames.Add("plc 6 sextet");

            blkNames.Add("dyn voice 1 single");
            blkNames.Add("dyn voice 1 single wall");
            blkNames.Add("dyn data 1 single camera");
            blkNames.Add("dyn data 1 single");
            blkNames.Add("dyn data 2 dual");
            blkNames.Add("dyn data 3 triple");
            blkNames.Add("dyn data 4 quad");
            blkNames.Add("dyn data 5 quint");
            blkNames.Add("dyn data 6 sextet");

            blkNames.Add("dyn plc 1 single");
            blkNames.Add("dyn plc 2 dual");
            blkNames.Add("dyn plc 3 triple");
            blkNames.Add("dyn plc 4 quad");
            blkNames.Add("dyn plc 5 quint");
            blkNames.Add("dyn plc 6 sextet");

            return blkNames;
        }
    }
}
