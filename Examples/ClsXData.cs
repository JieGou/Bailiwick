using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace MyFirstProject.Examples
{
    public class Commands
    {
        const string appName = "BailiwickFedExRetro";
        const string handPref = "HND:";

        [CommandMethod("GW_SELXD")]
        public void SelectWithXData()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            // We'll filter our selection to only include entities with
            // our XData attached
            
            var tv = new TypedValue(1001, appName);
            var sf = new SelectionFilter(new TypedValue[] { tv });
            
            // Ask the user to select (filtered) entities
            var res = ed.GetSelection(sf);

            if (res.Status != PromptStatus.OK)
                return;

            // We'll collect our valid and invalid IDs in two collections
            var valid = new ObjectIdCollection();
            var invalid = new ObjectIdCollection();

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                FindValidStripInvalid(tr, tv, res.Value.GetObjectIds(), valid, invalid);

                tr.Commit();
            }
            
            ed.WriteMessage(
              "\nFound {0} objects with valid XData, " +
              "stripped {1} objects of invalid XData.",
              valid.Count,
              invalid.Count
            );
        }



        private void FindValidStripInvalid(
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



        [CommandMethod("GW_GXD")]
        public void GetXData()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            // Ask the user to select an entity
            // for which to retrieve XData

            var opt = new PromptEntityOptions("\nSelect entity");
            var res = ed.GetEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
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
                            int n = 0;
                            foreach (TypedValue tv in rb)
                            {
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



        [CommandMethod("GW_SXD")]
        public void SetXData()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            // Ask the user to select an entity
            // for which to set XData

            var opt = new PromptEntityOptions("\nSelect entity");
            var res = ed.GetEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
                using (var tr = doc.TransactionManager.StartTransaction())
                {
                    var obj = tr.GetObject(res.ObjectId, OpenMode.ForWrite);
                    AddRegAppTableRecord(tr, doc.Database, appName);

                    var rb =
                      new ResultBuffer(
                        new TypedValue(1001, appName),
                        new TypedValue(1000, "This is a test string"),
                        new TypedValue(1000, handPref + obj.Handle.ToString())
                      );

                    using (rb)
                    {
                        obj.XData = rb;
                    }

                    tr.Commit();
                }
            }
        }



        private void AddRegAppTableRecord(
          Transaction tr,
          Autodesk.AutoCAD.DatabaseServices.Database db,
          string name
        )
        {
            var rat = (RegAppTable) tr.GetObject(db.RegAppTableId, OpenMode.ForRead);

            if (!rat.Has(name))
            {
                rat.UpgradeOpen();
                var ratr = new RegAppTableRecord();
                ratr.Name = name;
                rat.Add(ratr);
                tr.AddNewlyCreatedDBObject(ratr, true);
            }
        }



    }



}
