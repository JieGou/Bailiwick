using System;
using System.Data;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW
{
    class Cls_BW_DataGridErrHandlr
    {      
        public static void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            ed.WriteMessage("(DataGridView_DataError) Error happened " + anError.Context.ToString() + Environment.NewLine );

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                ed.WriteMessage("(DataGridView_DataError) Commit error" + Environment.NewLine);
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                ed.WriteMessage("(DataGridView_DataError) Cell change" + Environment.NewLine);
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                ed.WriteMessage("(DataGridView_DataError) Parsing error" + Environment.NewLine);
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                ed.WriteMessage("(DataGridView_DataError) Leave Control Error" + Environment.NewLine);
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "(DataGridView_DataError) An Error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "(DataGridView_DataError) An Error";

                anError.ThrowException = false;
            }
        }


    }
}
