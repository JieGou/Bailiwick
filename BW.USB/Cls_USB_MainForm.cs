using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;


namespace MyFirstProject.BW.USB
{
    class Cls_USB_MainForm
    {
        public static void BtnLabelWAOsIndividually_Sub(TextBox txtBx, CheckBox chkBx)
        {          

            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            List<ObjectId> LstObjectIdWap = new List<ObjectId>();
            //List<ObjectId> LstObjectIdWapD = new List<ObjectId>();

            PromptEntityOptions pWap = new PromptEntityOptions("Select WAO:");
            PromptEntityResult perWap;

            //PromptEntityOptions pWApD = new PromptEntityOptions("Select WapDual:");
            //PromptEntityResult perWapD;

            do
            {
                perWap = ed.GetEntity(pWap);
                //perWapD = ed.GetEntity(pWApD);
                {
                    if (perWap.ObjectId != ObjectId.Null)// & perWapD.ObjectId != ObjectId.Null)
                    {
                        LstObjectIdWap.Add(perWap.ObjectId);
                        //    LstObjectIdWapD.Add(perWapD.ObjectId);

                        using (doc.LockDocument())
                        using (Transaction tr = database.TransactionManager.StartTransaction())
                        {
                            BlockReference blkRefWap = (BlockReference)tr.GetObject(perWap.ObjectId, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attColWap = blkRefWap.AttributeCollection;

                            //BlockReference blkRefWapD = (BlockReference)tr.GetObject(perWapD.ObjectId, OpenMode.ForRead);
                            //Autodesk.AutoCAD.DatabaseServices.AttributeCollection attColWapD = blkRefWapD.AttributeCollection;

                            int strt = int.Parse(txtBx.Text);
                            string sufx = "";

                            if (chkBx.Checked)
                            {
                                sufx = "A";
                            }
                            else
                            {
                                sufx = "B";
                            }



                            foreach (ObjectId attId in attColWap)
                            {
                                AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {
                                    //case "APNUMBER":
                                    //    attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                    //    break;
                                    //case "WAP":
                                    //    attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                    //    break;
                                    case "LABEL":
                                        attRef.TextString = txtBx.Text.PadLeft(3, '0') + sufx;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //foreach (ObjectId attId in attColWapD)
                            //{
                            //    AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                            //    switch (attRef.Tag)
                            //    {
                            //        case "APNUMBER":
                            //            attRef.TextString = txtBx.Text.PadLeft(2, '0');
                            //            break;
                            //        case "WAP":
                            //            attRef.TextString = txtBx.Text.PadLeft(2, '0');
                            //            break;
                            //        case "LABEL1":
                            //            attRef.TextString = txtBx.Text.PadLeft(3, '0') + "A/" +
                            //                txtBx.Text.PadLeft(3, '0') + "B";
                            //            break;
                            //        default:
                            //            break;
                            //    }
                            //}

                            tr.Commit();

                            txtBx.Text = (int.Parse(txtBx.Text) + 1).ToString();
                            txtBx.Refresh();
                            
                            chkBx.Checked = !chkBx.Checked;
                            chkBx.Refresh();
                        }
                    }
                }

            } while (perWap.Status == PromptStatus.OK); //& perWapD.Status == PromptStatus.OK);

            //Cls_MMM_Main.LstAtts_waos.Clear();


            //dgvWaos.DataSource = null;
            //dgvWaos.DataSource = Cls_MMM_Main.LstAtts_waos;
            //dgvWaos.Refresh();

       
        }




        public static void SetAttrWidthFactor()
        {
            Document dwg = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            //Pick an attributereference in a block
            PromptNestedEntityOptions opt = new
                PromptNestedEntityOptions("\nPick an attribute:");
            PromptNestedEntityResult res = ed.GetNestedEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
                if (res.ObjectId.ObjectClass.DxfName.ToUpper() == "ATTRIB")
                {
                    ////Ask user to pick a distance as desired width for 
                    ////the attribute to fit in. Based on the block, the width 
                    ////could be a known value
                    //PromptPointOptions popt = new
                    //    PromptPointOptions("\nPick width base point:");
                    //PromptPointResult pres = ed.GetPoint(popt);
                    //if (pres.Status != PromptStatus.OK) return;
                    //Point3d basePt = pres.Value;

                    //PromptDistanceOptions dopt =
                    //    new PromptDistanceOptions("\nPick width: ");
                    //dopt.UseBasePoint = true;
                    //dopt.BasePoint = basePt;

                    //PromptDoubleResult dres = ed.GetDistance(dopt);
                    //if (dres.Status != PromptStatus.OK) return;

                    ////This is the width we want to fit the attribute text's width
                    //double w = dres.Value;

                    double w = 0.9;

                    using (Transaction tran =
                        dwg.TransactionManager.StartTransaction())
                    {
                        AttributeReference att = (AttributeReference)tran.GetObject(
                            res.ObjectId, OpenMode.ForWrite);

                        ////Get attribute's width, assuming it is placed horizontally
                        //double aw = Math.Abs(att.GeometricExtents.MaxPoint.X
                        //    - att.GeometricExtents.MinPoint.X);

                        ////This is the WidthFactor
                        //double factor = w / aw;
                        //att.WidthFactor = factor;

                        att.WidthFactor = w;

                        tran.Commit();
                    }
                }
                else
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Not an attribute!");
                }
            }
        }




    }
}
