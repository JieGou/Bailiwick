using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW.FDX
{
    public static class FedExRetroBom
    {
        const string appName = "BailiwickFedExRetro";
        const string handPref = "HND:";


        public static void SelectWithXData()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            var tv = new TypedValue(1001, appName);
            SelectionFilter sf = new SelectionFilter(new TypedValue[] { tv });
                
            var res = ed.GetSelection(sf);
 
            LstBom.Clear();

            ObjectIdCollection valid = new ObjectIdCollection();
            ObjectIdCollection invalid = new ObjectIdCollection();

            using (doc.LockDocument())
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                FindValidStripInvalid(tr, tv, res.Value.GetObjectIds(), valid, invalid);

                foreach (ObjectId id in valid)
                {
                    var obj = tr.GetObject(id, OpenMode.ForRead);
                    using (var rb = obj.XData)
                    {
                        if (rb == null)
                        {
                            ed.WriteMessage(
                              "\nEntity does not have XData attached."
                            );
                        }
                        else
                        {
                            BomItem b = new BomItem();

                            int n = 0;

                            foreach (TypedValue tvBom in rb)
                            {
                                if (n == 1)
                                    b.SiteCode = tvBom.Value.ToString();
                                if (n == 2)
                                    b.ApLabel = tvBom.Value.ToString();
                                if (n == 3)
                                    b.Backboard = int.Parse(tvBom.Value.ToString());
                                if (n == 4)
                                    b.Bracket = int.Parse(tvBom.Value.ToString());
                                if (n == 5)
                                    b.Screws = int.Parse(tvBom.Value.ToString());
                                if (n == 6)
                                    b.Flag = int.Parse(tvBom.Value.ToString());
                                if (n == 7)
                                    b.Washers = int.Parse(tvBom.Value.ToString());
                                if (n == 8)
                                    b.ToggleBolts = int.Parse(tvBom.Value.ToString());
                                if (n == 9)
                                    b.BeamClamp = int.Parse(tvBom.Value.ToString());
                                if (n == 10)
                                    b.CableFeet  = int.Parse(tvBom.Value.ToString());
                                if (n == 11)
                                    b.JackCount = int.Parse(tvBom.Value.ToString());
                                if (n == 12)
                                    b.APType  = tvBom.Value.ToString();
                                if (n == 13)
                                    b.DropDown = int.Parse(tvBom.Value.ToString());
                                
                                if (n == 14)
                                    b.OutAP = int.Parse(tvBom.Value.ToString());
                                if (n == 15)
                                    b.OutLug = int.Parse(tvBom.Value.ToString());
                                if (n == 16)
                                    b.OutGndWireFeet = int.Parse(tvBom.Value.ToString());
                                if (n == 17)
                                    b.OutCaddy = int.Parse(tvBom.Value.ToString());
                                                                                                  
                                if (n == 18)
                                    b.Handle = handPref + obj.Handle.ToString();
                                if (n == 19)
                                    b.hnd = obj.Handle;

                                if (n == 20)
                                    b.ApSort = tvBom.Value.ToString();

                                n++;
                            }

                            LstBom.Add(b);
                        }
                    }
                }
                
                tr.Commit();
            }

        }



        private static void FindValidStripInvalid(
          Transaction tr,
          TypedValue root,
          ObjectId[] ids,
          ObjectIdCollection valid,
          ObjectIdCollection invalid,
          bool strip = true
        )
        {
            foreach (var id in ids)
            {
                // Look for the "HND:" value anywhere in our app's
                // XData (this could be changed to look at a specific
                // location)

                bool found = false;

                // Start by opening each object for read and get the XData
                // we care about

                var obj = tr.GetObject(id, OpenMode.ForRead);

                using (var rb = obj.GetXDataForApplication((string)root.Value))
                {
                    // Check just in case something got passed in that doesn't
                    // have our XData                    

                    if (rb != null)
                    {
                        foreach (TypedValue tv in rb)
                        {
                            // If we have a string value...

                            if (tv.TypeCode == 1000)
                            {
                                var val = tv.Value.ToString();

                                // That starts with our prefix...
                                if (val.StartsWith(handPref))
                                {
                                    // And matches the object's handle...
                                    if (val == handPref + obj.Handle.ToString())
                                    {
                                        // ... then it's a valid object
                                        valid.Add(id);
                                        found = true;
                                    }
                                    else
                                        break; // Handle prefix found with bad handle
                                }
                            }
                        }
                    }

                    if (!found)
                    {
                        // We have an invalid handle reference (or none at all).
                        // Optionally strip the XData from this object

                        invalid.Add(id);
                        if (strip)
                        {
                            obj.UpgradeOpen();
                            obj.XData = new ResultBuffer(root);
                        }
                    }
                }
            }
        }
        
           
        public static void GetXData()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;
            
            var opt = new PromptEntityOptions("\nSelect AP Label");
            opt.SetRejectMessage("Not MText");
            opt.AddAllowedClass(typeof(MText), true);
            var res = ed.GetEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
                using (doc.LockDocument())
                using (var tr = doc.TransactionManager.StartTransaction())
                {
                    var obj = tr.GetObject(res.ObjectId, OpenMode.ForRead);
                    using (var rb = obj.XData)
                    {
                        if (rb == null)
                        {
                            ed.WriteMessage(
                              "\nEntity does not have XData attached."
                            );
                        }
                        else
                        {
                            BomItem b = new BomItem();
                            int n = 0;
                            foreach (TypedValue tv in rb)
                            {
                                if (n == 1)
                                    b.SiteCode = tv.Value.ToString();
                                if (n == 2)
                                    b.ApLabel = tv.Value.ToString();
                                if (n == 3)
                                    b.Backboard = (int)tv.Value;
                                if (n == 4)
                                    b.Bracket = (int)tv.Value;
                                if (n == 5)
                                    b.Screws = (int)tv.Value;
                                if (n == 6)
                                    b.Flag = (int)tv.Value;
                                if (n == 7)
                                    b.Washers = (int)tv.Value;
                                if (n == 8)
                                    b.ToggleBolts = (int)tv.Value;
                                if (n == 9)
                                    b.BeamClamp = (int)tv.Value;
                                if (n == 10)
                                    b.CableFeet = (int)tv.Value;
                                if (n == 11)
                                    b.JackCount = (int)tv.Value;
                                if (n == 12)
                                    b.APType = tv.Value.ToString();
                                if (n == 13)
                                    b.DropDown = (int)tv.Value;

                                //
                                if (n == 14)
                                    b.OutAP  = (int)tv.Value;
                                if (n == 15)
                                    b.OutLug  = (int)tv.Value;
                                if (n == 16)
                                    b.OutGndWireFeet  = (int)tv.Value;
                                if (n == 17)
                                    b.OutCaddy  = (int)tv.Value;
                                //

                                if (n == 18)
                                    b.Handle = tv.Value.ToString();

                                if (n == 19)
                                    b.hnd = (Autodesk.AutoCAD.DatabaseServices.Handle)tv.Value;

                                if (n == 20)
                                    b.ApSort = tv.Value.ToString();

                                ed.WriteMessage(
                                  "\nTypedValue {0} - type: {1}, value: {2}",
                                  n++,
                                  tv.TypeCode,
                                  tv.Value
                                );
                            }
                        }
                    }
                }
            }
        }
        

        public static void SetXData()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            // Ask the user to select an entity
            // for which to set XData

            var opt = new PromptEntityOptions("\nSelect AP Label");
            opt.SetRejectMessage("Not MText");
            opt.AddAllowedClass(typeof(MText), true);
            var res = ed.GetEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
                using(doc.LockDocument())
                using (var tr = doc.TransactionManager.StartTransaction())
                {
                    MText obj = (MText)tr.GetObject(res.ObjectId, OpenMode.ForWrite);

                    AddRegAppTableRecord(tr, doc.Database, appName);
                    
                    int ap = 0;

                    if (obj.Text.Contains("AP"))
                    {
                        string[] apNumbr = obj.Text.Split('-');

                        if (apNumbr.Length > 0)
                        {
                            string apN = apNumbr[apNumbr.Length - 1];

                            ap = int.Parse(apN.Replace("AP", ""));                          
                        }
                    }


                    var rb =
                          new ResultBuffer(
                            new TypedValue(1001, appName),

                            new TypedValue(1000, xDataBomAttach.SiteCode), //SiteCode

                            new TypedValue(1000,
                            xDataBomAttach.SiteCode + "-" +
                            xDataBomAttach.APType + "-" +
                            "AP" +

                            ap.ToString().PadLeft(2, '0')
                        ),
                

                        new TypedValue(1000, xDataBomAttach.Backboard ), //Backboard
                        new TypedValue(1000, xDataBomAttach.Bracket ), //Bracket
                        new TypedValue(1000, xDataBomAttach.Screws ), //Screws
                        new TypedValue(1000, xDataBomAttach.Flag ), //Flag
                        new TypedValue(1000, xDataBomAttach.Washers ), //Washers
                        new TypedValue(1000, xDataBomAttach.ToggleBolts ), //TobbleBolts
                        new TypedValue(1000, xDataBomAttach.BeamClamp ), //BeamClamp
                        new TypedValue(1000, xDataBomAttach.CableFeet ), //CableFeet
                        new TypedValue(1000, xDataBomAttach.JackCount ), //JackCount
                        new TypedValue(1000, xDataBomAttach.APType ), //APType
                        new TypedValue(1000, xDataBomAttach.DropDown ), //DropDown

                        new TypedValue(1000, xDataBomAttach.OutAP), //OutAP
                        new TypedValue(1000, xDataBomAttach.OutLug ), //OutLug
                        new TypedValue(1000, xDataBomAttach.OutGndWireFeet ), //OutGndWire
                        new TypedValue(1000, xDataBomAttach.OutCaddy ), //OutCaddy

                        new TypedValue(1000, handPref + obj.Handle.ToString() ),
                        new TypedValue(1000, obj.Handle),

                        new TypedValue(1000, ap.ToString().PadLeft(3, '0')) //ApSort                      

                      );

                    using (rb)
                    {
                        obj.XData = rb;
                    }

                    tr.Commit();
                }
            }
        }
        

        private static void AddRegAppTableRecord(
          Transaction tr,
          Autodesk.AutoCAD.DatabaseServices.Database db,
          string name
        )
        {
            var rat = (RegAppTable)tr.GetObject(db.RegAppTableId, OpenMode.ForRead);

            if (!rat.Has(name))
            {
                rat.UpgradeOpen();
                var ratr = new RegAppTableRecord();
                ratr.Name = name;
                rat.Add(ratr);
                tr.AddNewlyCreatedDBObject(ratr, true);
            }
        }

        public static readonly List<BomItem> LstBom = new List<BomItem>();


        public class BomItem : Int_Bom_Item
        {
            public string SiteCode { get; set; }      
            public string ApLabel { get; set; }
            public int Backboard { get; set; }
            public int Bracket { get; set; }
            public int Screws { get; set; }
            public int Flag { get; set; }
            public int Washers { get; set; }
            public int ToggleBolts { get; set; }
            public int BeamClamp { get; set; }
            public int CableFeet { get; set; }
            public int JackCount { get; set; }
            public string APType { get; set; }
            public int DropDown { get; set; }

            //
            public int OutAP { get; set; }
            public int OutLug { get; set; }           
            public int OutGndWireFeet { get; set; }
            public int OutCaddy { get; set; }
            //

            public string Handle { get; set; }

            public Autodesk.AutoCAD.DatabaseServices.Handle hnd { get; set; }

            public string ApSort { get; set; }

        }

        public interface  Int_Bom_Item
        {
            string SiteCode { get; set; }
            string ApLabel { get; set; }
            int Backboard { get; set; }
            int Bracket { get; set; }
            int Screws { get; set; }
            int Flag { get; set; }
            int Washers { get; set; }
            int ToggleBolts { get; set; }
            int BeamClamp { get; set; }
            int CableFeet { get; set; }
            int JackCount { get; set; }
            string APType { get; set; }
            int DropDown { get; set; }

            //
            int OutAP { get; set; }
            int OutLug { get; set; }
            int OutGndWireFeet { get; set; }
            int OutCaddy { get; set; }
            //

            string Handle { get; set; }

            Autodesk.AutoCAD.DatabaseServices.Handle hnd { get; set; }

            string ApSort { get; set; }

        }


        static readonly List<BomTotals> LstBomTtls = new List<BomTotals>();

        public class BomTotals
        {
            public string ApLabelTtl { get; set; }
            public int BackboardTtl { get; set; }
            public int BracketTtl { get; set; }
            public int ScrewsTtl { get; set; }
            public int FlagTtl { get; set; }
            public int WashersTtl { get; set; }
            public int ToggleBoltsTtl { get; set; }
            public int BeamClampTtl { get; set; }
            public int CableFeetTtl { get; set; }
            public int JackCountTtl { get; set; }
            public int DropDownTtl { get; set; }

            //
            public int OutAPTtl { get; set; }
            public int OutLugTtl { get; set; }
            public int OutGndWireTtl { get; set; }
            public int OutCaddyTtl { get; set; }
            //

        }

        public static List<BomTotals> BomTotalsSub()
        {
            LstBomTtls.Clear();

            BomTotals bt = new BomTotals();
            LstBomTtls.Add(bt);

            LstBomTtls[0].BackboardTtl = LstBom.Sum(x => x.Backboard);
            LstBomTtls[0].BracketTtl = LstBom.Sum(x => x.Bracket);
            LstBomTtls[0].ScrewsTtl = LstBom.Sum(x => x.Screws);
            LstBomTtls[0].FlagTtl = LstBom.Sum(x => x.Flag);     
            LstBomTtls[0].WashersTtl  = LstBom.Sum(x => x.Washers );
            LstBomTtls[0].ToggleBoltsTtl  = LstBom.Sum(x => x.ToggleBolts );
            LstBomTtls[0].BeamClampTtl  = LstBom.Sum(x => x.BeamClamp );
            LstBomTtls[0].CableFeetTtl  = LstBom.Sum(x => x.CableFeet );
            LstBomTtls[0].JackCountTtl  = LstBom.Sum(x => x.JackCount );
            LstBomTtls[0].DropDownTtl = LstBom.Sum(x => x.DropDown );

            LstBomTtls[0].OutAPTtl  = LstBom.Sum(x => x.OutAP );
            LstBomTtls[0].OutLugTtl  = LstBom.Sum(x => x.OutLug );
            LstBomTtls[0].OutGndWireTtl  = LstBom.Sum(x => x.OutGndWireFeet );
            LstBomTtls[0].OutCaddyTtl  = LstBom.Sum(x => x.OutCaddy);


            return LstBomTtls;

        }

        

        public static BomItem xDataBomAttach = new BomItem();

        public static List<BomItem> MakeAttach (     
            string SiteCode,
            string APType,
            bool backboard,
            bool DropDown
            )
        {

            List<BomItem> LstCB = new List<BomItem>();
            xDataBomAttach = new BomItem();

            xDataBomAttach.SiteCode = SiteCode;
            xDataBomAttach.APType = APType;
            xDataBomAttach.ApLabel = "??";

            if (DropDown )
            {
                xDataBomAttach.DropDown = 1;
            }

            if (backboard)
            {
                xDataBomAttach.Backboard = 1;
                xDataBomAttach.Bracket = 1;
                xDataBomAttach.Screws = 2;
                xDataBomAttach.Flag = 1;
                xDataBomAttach.Washers = 4;
                xDataBomAttach.ToggleBolts = 2;
                xDataBomAttach.BeamClamp = 2;

            }

            if (APType == "OFC" )
            {
                xDataBomAttach.JackCount = 2;
                xDataBomAttach.CableFeet = 250;
            }

            if (APType == "OUT")
            {
                xDataBomAttach.JackCount = 2;
                xDataBomAttach.CableFeet = 250;

                xDataBomAttach.OutAP = 1;
                xDataBomAttach.OutLug = 1;
                xDataBomAttach.OutGndWireFeet = 25;
                xDataBomAttach.OutCaddy = 1;
            }


            if (APType == "VAN")
            {
                xDataBomAttach.JackCount = 4;
                xDataBomAttach.CableFeet = 500;
            }

            if (APType == "FLR")
            {
                xDataBomAttach.JackCount = 4;
                xDataBomAttach.CableFeet = 500;
            }

            if (APType == "GWY")
            {
                xDataBomAttach.JackCount = 2;
                xDataBomAttach.CableFeet = 250;
            }

            if (APType == "GRD")
            {
                xDataBomAttach.JackCount = 2;
                xDataBomAttach.CableFeet = 250;
            }

            LstCB.Clear();
            LstCB.Add(xDataBomAttach);

            return LstCB;
        }


    }



}
