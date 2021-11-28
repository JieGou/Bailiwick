using System.ComponentModel;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_SiteInfo : UserControl
    {
        public Uc_BW_SiteInfo()
        {
            InitializeComponent();

            txtBxSite.DataBindings.Add("Text", Cls, "AttSite");
            txtBxBuilding.DataBindings.Add("Text", Cls, "AttBuilding");
            txtBxFloor.DataBindings.Add("Text", Cls, "AttFloor");
            txtBxCloset.DataBindings.Add("Text", Cls, "AttCloset");
        }

        public static readonly ClsSitInfo Cls = new ClsSitInfo();
        

        public class ClsSitInfo : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string _AttSite = "";
            private string _AttBuilding = "";
            private string _AttFloor = "";
            private string _AttCloset = "";

            public string AttCloset
            {
                get { return _AttCloset; }
                set { _AttCloset = value; OnPropertyChanged("AttCloset"); }
            }

            public string AttFloor
            {
                get { return _AttFloor; }
                set { _AttFloor = value; OnPropertyChanged("AttFloor"); }
            }

            public string AttBuilding
            {
                get { return _AttBuilding; }
                set { _AttBuilding = value; OnPropertyChanged("AttBuilding"); }
            }

            public string AttSite
            {
                get { return _AttSite; }
                set { _AttSite = value; OnPropertyChanged("AttSite"); }
            }

            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

        }

    }
}
