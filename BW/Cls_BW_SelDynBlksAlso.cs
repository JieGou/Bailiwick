using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;

namespace MyFirstProject.BW
{
    public static class Cls_BW_SelDynBlksAlso
    { 
        static public SelectionFilter SelectDynamicBlocks(List<string> blkNames)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;
                   
            var tr = doc.TransactionManager.StartTransaction();

            using (tr)
            {
                var bt =
                  (BlockTable)tr.GetObject(
                    doc.Database.BlockTableId,
                    OpenMode.ForRead
                  );

                string blkName = "";

                for (int cnt = 0; cnt < blkNames.Count; cnt ++)
                {
                    blkName = blkNames[cnt];

                    // Start by getting access to our block, if it exists
                    if (!bt.Has(blkName))
                    {
                        ed.WriteMessage(
                          "\nCannot find block called \"{0}\".", blkName
                        );
                        continue;
                    }
                    
                    // Get the anonymous block names
                    var btr =
                      (BlockTableRecord)tr.GetObject(
                        bt[blkName], OpenMode.ForRead
                        );                    

                    if (!btr.IsDynamicBlock)
                    {
                        ed.WriteMessage(
                          "\nCannot find a dynamic block called \"{0}\".", blkName
                        );
                        continue;
                    }

                    // Get the anonymous blocks and add them to our list
                    var anonBlks = btr.GetAnonymousBlockIds();
                    foreach (ObjectId bid in anonBlks)
                    {
                        var btr2 =
                          (BlockTableRecord)tr.GetObject(bid, OpenMode.ForRead);

                        blkNames.Add(btr2.Name);
                        //cnt--;
                    }
                }                
                tr.Commit();
            }

            // Build a conditional filter list so that only
            // entities with the specified properties are
            // selected
            SelectionFilter sf =                
                new SelectionFilter(BW.Cls_BW_Utility.CreateFilterListForBlocks(blkNames));
       
            //   PromptSelectionResult psr = ed.SelectAll(sf);

       //     ed.WriteMessage(
       //  "\nFound {0} entit{1}.",
       //  psr.Value.Count,
       //  (psr.Value.Count == 1 ? "y" : "ies")
       //);
            return sf;      
        }   
    }

}
