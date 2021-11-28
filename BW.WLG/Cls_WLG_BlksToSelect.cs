using System.Collections.Generic;

namespace MyFirstProject.BW.WLG
{
    public static class Cls_WLG_BlksToSelect
    {
        public static List<string> GetBlockNames()
        {
            List<string> blkNames = new List<string>();

            blkNames.Add("datapoint");
            blkNames.Add("Cameras");
            blkNames.Add("Monitors");

            blkNames.Add("Housing");

            //blkNames.Add("IP WALL CLOCK");
            //blkNames.Add("19-84-45u-e");
            //blkNames.Add("blue");
            //blkNames.Add("black");
            //blkNames.Add("white");
            //blkNames.Add("phone");
            //blkNames.Add("camera");
            //blkNames.Add("gray");
            //blkNames.Add("PVM");
            //blkNames.Add("yellow");
            //blkNames.Add("green");
            //blkNames.Add("red");
            //blkNames.Add("cctv");
            //blkNames.Add("speaker06");
            //blkNames.Add("vol-cont06");
            //blkNames.Add("spk-vc06");
            //blkNames.Add("horn06");

            //blkNames.Add("dummy-dome");
            //blkNames.Add("CAM-RDY-DOME");
            //blkNames.Add("FUT-CAM-RDY-DOME");

            return blkNames;
        }

    }
}
