using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Windows;

namespace MyFirstProject.BW
{
    public class Cls_BW_Ribbon
    {                
        public Cls_BW_Ribbon()
        {
           
        }
          
        public void Gw_Rbn_MyRibbon()
        {
            Autodesk.Windows.RibbonControl ribbonControl = Autodesk.Windows.ComponentManager.Ribbon;

            RibbonTab Tab = new RibbonTab();
            Tab.Title = "Bailiwick";
            Tab.Id = "RibbonSample_TAB_ID";

            ribbonControl.Tabs.Add(Tab);

            #region Bailiwick
            
            // create Ribbon panels
            Autodesk.Windows.RibbonPanelSource panel1Panel = new RibbonPanelSource();
            panel1Panel.Title = "Bailiwick";
            RibbonPanel Panel1 = new RibbonPanel();
            Panel1.Source = panel1Panel;
            Tab.Panels.Add(Panel1);

            RibbonButton pan1button1 = new RibbonButton();
            pan1button1.Text = "BW";
            pan1button1.ShowText = true;
            pan1button1.ShowImage = true;
            pan1button1.Image = Images.GetBitmap(Properties.Resources.brand_logo_sml);
            pan1button1.LargeImage = Images.GetBitmap(Properties.Resources.brand_logo_lrg);
            pan1button1.Orientation = System.Windows.Controls.Orientation.Vertical;
            pan1button1.Size = RibbonItemSize.Large;
            pan1button1.CommandHandler = new RibbonCommandHandler();

            RibbonButton pan1button2 = new RibbonButton();
            pan1button2.Text = "BW Update TB Date";
            pan1button2.ShowText = true;
            pan1button2.ShowImage = true;
            pan1button2.Image = Images.GetBitmap(Properties.Resources.Small);
            pan1button2.LargeImage = Images.GetBitmap(Properties.Resources.large);
            pan1button2.CommandHandler = new RibbonCommandHandler();

            RibbonButton pan1button3 = new RibbonButton();
            pan1button3.Text = "BW Zoom All Layouts";
            pan1button3.ShowText = true;
            pan1button3.ShowImage = true;
            pan1button3.Image = Images.GetBitmap(Properties.Resources.Small);
            pan1button3.LargeImage = Images.GetBitmap(Properties.Resources.large);
            pan1button3.CommandHandler = new RibbonCommandHandler();

            RibbonButton pan1button4 = new RibbonButton();
            pan1button4.Text = "BW Reset All Form Loc's";
            pan1button4.ShowText = true;
            pan1button4.ShowImage = true;
            pan1button4.Image = Images.GetBitmap(Properties.Resources.Small);
            pan1button4.LargeImage = Images.GetBitmap(Properties.Resources.large);
            pan1button4.CommandHandler = new RibbonCommandHandler();

            RibbonButton pan1button5 = new RibbonButton();
            pan1button5.Text = "BW Move Atts In Block";
            pan1button5.ShowText = true;
            pan1button5.ShowImage = true;
            pan1button5.Image = Images.GetBitmap(Properties.Resources.Small);
            pan1button5.LargeImage = Images.GetBitmap(Properties.Resources.large);
            pan1button5.CommandHandler = new RibbonCommandHandler();
               
            RibbonRowPanel pan1row1 = new RibbonRowPanel();

            pan1row1.Items.Add(pan1button2);
            pan1row1.Items.Add(new RibbonRowBreak());
            pan1row1.Items.Add(pan1button3);
            pan1row1.Items.Add(new RibbonRowBreak());
            pan1row1.Items.Add(pan1button4);
            pan1row1.Items.Add(new RibbonRowBreak());
            pan1row1.Items.Add(pan1button5);
            pan1row1.Items.Add(new RibbonRowBreak());

   
            panel1Panel.Items.Add(pan1button1);
            panel1Panel.Items.Add(new RibbonSeparator());
            panel1Panel.Items.Add(pan1row1);

            #endregion
            
            #region MMM

            RibbonPanelSource panel2Panel = new RibbonPanelSource();
            panel2Panel.Title = "MMM";
            RibbonPanel panel2 = new RibbonPanel();
            panel2.Source = panel2Panel;
            Tab.Panels.Add(panel2);
        
            RibbonButton pan2button1 = new RibbonButton();
            pan2button1.Text = "MMM";
            pan2button1.ShowText = true;
            pan2button1.ShowImage = true;
            pan2button1.Image = Images.GetBitmap(Properties.Resources._3m_sml);
            pan2button1.LargeImage = Images.GetBitmap(Properties.Resources._3m_lrg);
            pan2button1.Size = RibbonItemSize.Large;
            pan2button1.Orientation = System.Windows.Controls.Orientation.Vertical;
            pan2button1.CommandHandler = new RibbonCommandHandler();

            panel2Panel.Items.Add(pan2button1);

            #endregion
            
            #region FDX

            RibbonPanelSource panel3Panel = new RibbonPanelSource();
            panel3Panel.Title = "FDX";
            RibbonPanel panel3 = new RibbonPanel();
            panel3.Source = panel3Panel;
            Tab.Panels.Add(panel3);

            RibbonButton pan3button1 = new RibbonButton();
            pan3button1.Text = "FDX";
            pan3button1.ShowText = true;
            pan3button1.ShowImage = true;
            pan3button1.Image = Images.GetBitmap(Properties.Resources.fedExG_sml);
            pan3button1.LargeImage = Images.GetBitmap(Properties.Resources.fedExG_lrg);
            pan3button1.Size = RibbonItemSize.Large;
            pan3button1.Orientation = System.Windows.Controls.Orientation.Vertical;
            pan3button1.CommandHandler = new RibbonCommandHandler();
                
            panel3Panel.Items.Add(pan3button1);

            #endregion
            
            #region AMZ

            RibbonPanelSource pan4Panel = new RibbonPanelSource();
            pan4Panel.Title = "AMZ";
            RibbonPanel Panel4 = new RibbonPanel();
            Panel4.Source = pan4Panel;
            Tab.Panels.Add(Panel4);

            RibbonButton pan4button1 = new RibbonButton();
            pan4button1.Text = "AMZ";
            pan4button1.ShowText = true;
            pan4button1.ShowImage = true;
            pan4button1.Image = Images.GetBitmap(Properties.Resources.amazon_sml);
            pan4button1.LargeImage = Images.GetBitmap(Properties.Resources.amazon_lrg);
            pan4button1.Size = RibbonItemSize.Large;
            pan4button1.Orientation = System.Windows.Controls.Orientation.Vertical;
            pan4button1.CommandHandler = new RibbonCommandHandler();

            pan4Panel.Items.Add(pan4button1);

            #endregion
            
            #region TGT
                      
            RibbonPanelSource panTGTPanel = new RibbonPanelSource();
            panTGTPanel.Title = "TGT";
            RibbonPanel PanelTGT = new RibbonPanel();
            PanelTGT.Source = panTGTPanel;
            Tab.Panels.Add(PanelTGT);

            RibbonButton panTGTbutton1 = new RibbonButton();
            panTGTbutton1.Text = "TGT";
            panTGTbutton1.ShowText = true;
            panTGTbutton1.ShowImage = true;
            panTGTbutton1.Image = Images.GetBitmap(Properties.Resources.target_sml);
            panTGTbutton1.LargeImage = Images.GetBitmap(Properties.Resources.target_lrg);
            panTGTbutton1.Size = RibbonItemSize.Large;
            panTGTbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            panTGTbutton1.CommandHandler = new RibbonCommandHandler();

            panTGTPanel.Items.Add(panTGTbutton1);

            #endregion
            
            #region THD

            RibbonPanelSource panTHDPanel = new RibbonPanelSource();
            panTHDPanel.Title = "THD";
            RibbonPanel PanelTHD = new RibbonPanel();
            PanelTHD.Source = panTHDPanel;
            Tab.Panels.Add(PanelTHD);

            RibbonButton panTHDbutton1 = new RibbonButton();
            panTHDbutton1.Text = "THD";
            panTHDbutton1.ShowText = true;
            panTHDbutton1.ShowImage = true;
            panTHDbutton1.Image = Images.GetBitmap(Properties.Resources.thehomedepot_sml);
            panTHDbutton1.LargeImage = Images.GetBitmap(Properties.Resources.thehomedepot_lrg);
            panTHDbutton1.Size = RibbonItemSize.Large;
            panTHDbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            panTHDbutton1.CommandHandler = new RibbonCommandHandler();

            panTHDPanel.Items.Add(panTHDbutton1);

            #endregion

            #region WLG

            RibbonPanelSource panWLGPanel = new RibbonPanelSource();
            panWLGPanel.Title = "WLG";
            RibbonPanel PanelWLG = new RibbonPanel();
            PanelWLG.Source = panWLGPanel;
            Tab.Panels.Add(PanelWLG);

            RibbonButton panWLGbutton1 = new RibbonButton();
            panWLGbutton1.Text = "WLG";
            panWLGbutton1.ShowText = true;
            panWLGbutton1.ShowImage = true;
            panWLGbutton1.Image = Images.GetBitmap(Properties.Resources.walgreens_sml);
            panWLGbutton1.LargeImage = Images.GetBitmap(Properties.Resources.walgreens_lrg);
            panWLGbutton1.Size = RibbonItemSize.Large;
            panWLGbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            panWLGbutton1.CommandHandler = new RibbonCommandHandler();

            panWLGPanel.Items.Add(panWLGbutton1);

            #endregion

            #region USB

            RibbonPanelSource panUSBPanel = new RibbonPanelSource();
            panUSBPanel.Title = "USB";
            RibbonPanel PanelUSB = new RibbonPanel();
            PanelUSB.Source = panUSBPanel;
            Tab.Panels.Add(PanelUSB);

            RibbonButton panUSBbutton1 = new RibbonButton();
            panUSBbutton1.Text = "USB";
            panUSBbutton1.ShowText = true;
            panUSBbutton1.ShowImage = true;
            panUSBbutton1.Image = Images.GetBitmap(Properties.Resources.usbank_sml);
            panUSBbutton1.LargeImage = Images.GetBitmap(Properties.Resources.usbank_lrg);
            panUSBbutton1.Size = RibbonItemSize.Large;
            panUSBbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            panUSBbutton1.CommandHandler = new RibbonCommandHandler();

            panUSBPanel.Items.Add(panUSBbutton1);

            #endregion

            #region UPS

            RibbonPanelSource panUPSPanel = new RibbonPanelSource();
            panUPSPanel.Title = "UPS";
            RibbonPanel PanelUPS = new RibbonPanel();
            PanelUPS.Source = panUPSPanel;
            Tab.Panels.Add(PanelUPS);

            RibbonButton panUPSbutton1 = new RibbonButton();
            panUPSbutton1.Text = "UPS";
            panUPSbutton1.ShowText = true;
            panUPSbutton1.ShowImage = true;
            panUPSbutton1.Image = Images.GetBitmap(Properties.Resources.ups_sml);
            panUPSbutton1.LargeImage = Images.GetBitmap(Properties.Resources.ups_lrg);
            panUPSbutton1.Size = RibbonItemSize.Large;
            panUPSbutton1.Orientation = System.Windows.Controls.Orientation.Vertical;
            panUPSbutton1.CommandHandler = new RibbonCommandHandler();

            panUPSPanel.Items.Add(panUPSbutton1);

            #endregion

            Tab.IsActive = true;
        }

        public class RibbonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                if (parameter is RibbonButton)
                {
                    RibbonButton button = parameter as RibbonButton;

                    if (button.AutomationName == "BW")
                    {
                        BW.Cls_BW_Main.BW_MainFormShow();
                    }                    
                    if (button.AutomationName == "MMM")
                    {
                        BW.Cls_BW_Main.MMM_HostFormShow();
                    }
                    if (button.AutomationName == "FDX")
                    {
                        BW.Cls_BW_Main.FDX_MainFormShow();
                    }
                    if (button.AutomationName == "AMZ")
                    {
                        BW.Cls_BW_Main.AMZ_MainFormShow();
                    }
                    if (button.AutomationName == "TGT")
                    {
                        BW.Cls_BW_Main.TGT_MainFormShow();
                    }
                    if (button.AutomationName == "THD")
                    {
                        BW.Cls_BW_Main.THD_MainFormShow();
                    }
                    if (button.AutomationName == "WLG")
                    {
                        BW.Cls_BW_Main.WLG_MainFormShow();
                    }
                    if (button.AutomationName == "USB")
                    {
                        BW.Cls_BW_Main.USB_MainFormShow();
                    }
                    if (button.AutomationName == "UPS")
                    {
                        BW.Cls_BW_Main.UPS_MainFormShow();
                    }

                    if (button.AutomationName == "BW Update TB Date")
                    {
                        BW.Cls_BW_Utility.BW_UpdateTitleBlockDateField();
                    }

                    if (button.AutomationName == "BW Zoom All Layouts")
                    {
                        BW.Cls_BW_Utility.BW_ZoomAllLayouts();
                    }

                    if (button.AutomationName == "BW Reset All Form Loc's")
                    {
                        BW.Cls_BW_Utility.applicationSettings.FixAllFormLocations();
                    }

                    if (button.AutomationName == "BW Move Atts In Block")
                    {
                        BW.Cls_BW_MovRotAtt_DrawJig.MoveRotAtt();
                    }

                }
            }
        }

        public static class Images
        {
            public static BitmapImage GetBitmap(Bitmap image)
            {
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Png);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = stream;
                bmp.EndInit();

                return bmp;
            }
        }



    }
}