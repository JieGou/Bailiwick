using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;


namespace MyFirstProject.Examples.TableCreationAndModification
{
    public class Commands
    {
        const double rowHeight = 3.0, colWidth = 5.0;
        const double textHeight = rowHeight * 0.25;

        [CommandMethod("CBA")]
        static public void CreateBlockTableWithAttributes()
        {
            const string symbol = "BANNED";

            var doc = Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;
            
            var db = doc.Database;
            var ed = doc.Editor;

            var pr = ed.GetPoint("\nEnter table insertion point");

            if (pr.Status != PromptStatus.OK)
                return;

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                
                // We'll populate these variables if the drawing contains a block
                // with an attribute we can use

                var banned = ObjectId.Null;
                var bannedAttId = ObjectId.Null;

                if (bt.Has(symbol))
                {
                    // We have found a NOENTRY block
                    banned = bt[symbol];

                    // Now let's check its contents for AttributeDefinitions
                    var btr = tr.GetObject(banned, OpenMode.ForRead) as BlockTableRecord;
                    var attDefClass = RXObject.GetClass(typeof(AttributeDefinition));

                    foreach (var id in btr)
                    {
                        // The ID of the 1st AttDef we find gets stored in a variable
                        if (id.ObjectClass.IsDerivedFrom(attDefClass))
                        {
                            bannedAttId = id;
                            break;
                        }
                    }
                }

                // Create the table, set its style and default row/column size
                var tb = new Table();
                tb.TableStyle = db.Tablestyle;
                tb.SetRowHeight(rowHeight);
                tb.SetColumnWidth(colWidth);
                tb.Position = pr.Value;

                // Set the header cell
                var head = tb.Cells[0, 0];
                head.Value = "Blocks";
                head.Alignment = CellAlignment.MiddleCenter;
                head.TextHeight = textHeight;

                // Insert an additional two columns, the skinny one is for the index
                tb.InsertColumns(0, colWidth, 1);
                tb.InsertColumns(0, colWidth * 0.5, 1);

                // Loop through the blocks in the drawing, creating rows
                foreach (var id in bt)
                {
                    var btr = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);

                    // Only care about user-insertable blocks

                    if (!btr.IsLayout && !btr.IsAnonymous)
                    {
                        // Add a row
                        tb.InsertRows(tb.Rows.Count, rowHeight, 1);

                        var rowIdx = tb.Rows.Count - 1;

                        // The first is the index
                        var first = tb.Cells[rowIdx, 0];

                        // If we have our "no entry" block and attribute - and the index
                        // is an odd number - then use it for the contents

                        if (
                          banned != ObjectId.Null &&
                          bannedAttId != ObjectId.Null &&
                          rowIdx % 2 != 0
                        )
                        {
                            // Set the BTR on the cell and then the value of the block's
                            // attribute to be a string of the row's index

                            first.BlockTableRecordId = banned;
                            first.SetBlockAttributeValue(bannedAttId, rowIdx.ToString());
                        }
                        else
                        {
                            // Otherwise we just set the index as the cell contents
                            first.Value = rowIdx;
                            first.TextHeight = textHeight;
                        }

                        first.Alignment = CellAlignment.MiddleCenter;

                        // The second cell will hold the block name
                        var second = tb.Cells[rowIdx, 1];
                        second.Value = btr.Name;
                        second.Alignment = CellAlignment.MiddleCenter;
                        second.TextHeight = textHeight;

                        // The third will contain a thumbnail of the block
                        var third = tb.Cells[rowIdx, 2];
                        third.BlockTableRecordId = id;
                    }
                }
                
                // Now we add the table to the current space
                var sp =
                  (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                sp.AppendEntity(tb);
                
                // And to the transaction, which we then commit
                tr.AddNewlyCreatedDBObject(tb, true);
                tr.Commit();
            }
        }

        
        [CommandMethod("MBT")]
        static public void ModifyBlockTable()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;

            var ed = doc.Editor;

            // Let's start by asking for a table to be selected
            var per = ed.GetEntity("\nSelect table");
            if (per.Status != PromptStatus.OK)
                return;
            
            // Start our transaction
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                // Open the object and see whether it's table...

                var tb = tr.GetObject(per.ObjectId, OpenMode.ForRead) as Table;

                if (tb != null)
                {
                    // If it is, we need to be able to modify it
                    tb.UpgradeOpen();
                    // Insert our column, making it a bit skinnier than the others
                    tb.InsertColumns(0, colWidth * 0.5, 1);
                    
                    // Populate the cells in this new column (starting with 1,
                    // as if we ask for 0 we'll get the header cell, too)

                    for (int i = 1; i < tb.Rows.Count; i++)
                    {
                        // Insert the index into the cell with appropriate text size
                        // and justification

                        var num = tb.Cells[i, 0];
                        num.Value = i;
                        num.Alignment = CellAlignment.MiddleCenter;
                        num.TextHeight = textHeight;
                    }
                }

                // Commit the transaction
                tr.Commit();
            }
        }



        [CommandMethod("CBT")]
        static public void CreateBlockTable()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;

            var db = doc.Database;
            var ed = doc.Editor;

            var pr = ed.GetPoint("\nEnter table insertion point");

            if (pr.Status != PromptStatus.OK)
                return;

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                
                // Create the table, set its style and default row/column size

                var tb = new Table();
                tb.TableStyle = db.Tablestyle;
                tb.SetRowHeight(rowHeight);
                tb.SetColumnWidth(colWidth);
                tb.Position = pr.Value;

                // Set the header cell
                var head = tb.Cells[0, 0];
                head.Value = "Blocks";
                head.Alignment = CellAlignment.MiddleCenter;
                head.TextHeight = textHeight;                

                // Insert an additional column
                tb.InsertColumns(0, colWidth, 1);

                // Loop through the blocks in the drawing, creating rows
                foreach (var id in bt)
                {
                    var btr = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);

                    // Only care about user-insertable blocks

                    if (!btr.IsLayout && !btr.IsAnonymous)
                    {
                        // Add a row
                        tb.InsertRows(tb.Rows.Count, rowHeight, 1);
                        var rowIdx = tb.Rows.Count - 1;
                        
                        // The first cell will hold the block name

                        var first = tb.Cells[rowIdx, 0];
                        first.Value = btr.Name;
                        first.Alignment = CellAlignment.MiddleCenter;
                        first.TextHeight = textHeight;

                        // The second will contain a thumbnail of the block
                        var second = tb.Cells[rowIdx, 1];
                        second.BlockTableRecordId = id;
                    }
                }

                // Now we add the table to the current space
                var sp =
                  (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                sp.AppendEntity(tb);

                // And to the transaction, which we then commit

                tr.AddNewlyCreatedDBObject(tb, true);
                tr.Commit();
            }
        }
    }
}
