using System;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_Blocks : UserControl
    {
        public Uc_BW_TP_Blocks()
        {
            InitializeComponent();
        }

        private void BtnDumpAttsAllBlocks_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_Blocks.ListBlockAtts();
        }

        private void BtnOrderAllAttributes_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_Blocks.BlockDefAttributeSort();
        }

        private void BtnBlockRefAttributeSortAndAddMissingAttsFromDef_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_Blocks.BlockRefAttributeSort(Cls_BW_TP_Common.acSSPrompt);
        }

        private void BtnMoveInBlock_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            BW.Cls_BW_MovRotAtt_DrawJig.MoveRotAtt();
            this.ParentForm.ParentForm.Show();
        }


        private void BtnAttributeTable_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// does not work with dynamic blocks as is
            /// </summary>
            Cls_BW_TP_Blocks.Commands.BlockAttributeTable();
        }

        private void BtnMatchAtts_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_Blocks.MatchAtts();
            this.ParentForm.ParentForm.Show();
        }



    }
}
