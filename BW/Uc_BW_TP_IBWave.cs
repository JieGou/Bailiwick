using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_IBWave : UserControl
    {
        public Uc_BW_TP_IBWave()
        {
            InitializeComponent();
        }

        private void BtnReadIBWaveXml_Click(object sender, EventArgs e)
        {
           // DgvReport.DataSource = null;

            //XmlSerializer reader = new XmlSerializer(typeof(AntennasReport));

            //StreamReader file = new StreamReader(@"C:\Users\gwilliams\Desktop\CP_170815\AccessPointsReportv2.xml");

            //AntennasReport overview = (AntennasReport) reader.Deserialize(file);

            ////AntennasReport overview = (AntennasReport)reader.Deserialize(file);

            //file.Close();

            //DgvReport.DataSource = overview.Table1;

            //DgvReport.AutoResizeColumns();





            XmlSerializer reader = new XmlSerializer(typeof(DataSetAccessPoints));

            StreamReader file = new StreamReader(@"C:\Users\gwilliams\Desktop\CP_170815\AccessPointsReportv2.xml");

            DataSetAccessPoints overview = (DataSetAccessPoints)reader.Deserialize(file);
                 
            file.Close();

            DgvReport.DataSource = overview.Items.ToList ();

            DgvReport.AutoResizeColumns();

        }


    }
}
