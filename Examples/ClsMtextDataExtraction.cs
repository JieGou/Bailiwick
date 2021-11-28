using System;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.Examples
{
    public static class Commands
    {        
        [CommandMethod("FilterMtext")]
        public static void FilterMtextWildcard()
        {
            Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;
            
            TypedValue[] acTypValAr = new TypedValue[2];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "MTEXT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.LayerName, "T-DATA"), 1);
                      
            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.GetSelection(acSelFtr);
            
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                ObjectId[] objIds = acSSet.GetObjectIds();

                using (var Transaction = acDocEd.Document.Database.TransactionManager.StartTransaction())
                {
                    foreach (ObjectId MTextObjId in objIds)
                    {
                        var current_MTextObj = Transaction.GetObject(MTextObjId, OpenMode.ForRead) as MText;
                        acDocEd.WriteMessage(current_MTextObj.Text.Replace("\r\n", " ").Replace("  ", " ") + Environment.NewLine);
                    }

                    acDocEd.WriteMessage("End........");
                }
            }
            else
            {
                Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }
    }
}