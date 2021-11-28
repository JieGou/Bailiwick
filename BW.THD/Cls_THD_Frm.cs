using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW.THD
{
    public class Cls_THD_Frm
    {      
        public static readonly List<Cls_THD_Atts> lst_THD_Atts = new List<Cls_THD_Atts>();

        public static readonly List<Cls_THD_DisplayAtts> lst_THD_DisplayAtts = new List<Cls_THD_DisplayAtts>();
        

        public static void THD_SelectBlocks()
        {
            lst_THD_Atts.Clear();

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            TypedValue[] tvs = new TypedValue[] {

              new TypedValue((int)DxfCode.Operator, "<or" ),

              new TypedValue( (int)DxfCode.Operator, "<and" ),
              new TypedValue( (int)DxfCode.BlockName, "data*" ),
              new TypedValue( (int)DxfCode.Operator, "and>" ),

              new TypedValue( (int)DxfCode.Operator, "<and" ),
              new TypedValue( (int)DxfCode.BlockName, "wap*" ),
              new TypedValue( (int)DxfCode.Operator, "and>" ),

              new TypedValue( (int)DxfCode.Operator, "<and" ),
              new TypedValue( (int)DxfCode.BlockName, "CAM*" ),
              new TypedValue( (int)DxfCode.Operator, "and>" ),

              new TypedValue( (int)DxfCode.Operator, "<and" ),
              new TypedValue( (int)DxfCode.BlockName, "PVM*" ),
              new TypedValue( (int)DxfCode.Operator, "and>" ),

              new TypedValue( (int)DxfCode.Operator, "<and" ),
              new TypedValue( (int)DxfCode.BlockName, "HDR*" ),
              new TypedValue( (int)DxfCode.Operator, "and>" ),

              new TypedValue( (int)DxfCode.Operator, "or>" )
            };
            
            SelectionFilter acSelFtr = new SelectionFilter(tvs);

            PromptSelectionResult acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                THD_FillList(acSSPrompt);

                lst_THD_DisplayAtts.Clear();

                foreach(Cls_THD_Atts cls in lst_THD_Atts)
                {
                    if (cls.SetType1 != ".")
                    {
                        Cls_THD_DisplayAtts nCls = new Cls_THD_DisplayAtts();                      
                        nCls.DataTotal = "1"; //cls.DataTotal;
                        nCls.Device = cls.Device;
                        nCls.Name = cls.Name;
                        nCls.Room = cls.Room;
                        nCls.SetType = cls.SetType1;
                        nCls.Label = cls.Label1;
                        lst_THD_DisplayAtts.Add(nCls);
                    }
                    if (cls.SetType2 != ".")
                    {
                        Cls_THD_DisplayAtts nCls = new Cls_THD_DisplayAtts();
                        nCls.DataTotal = "1"; //cls.DataTotal;
                        nCls.Device = cls.Device;
                        nCls.Name = cls.Name;
                        nCls.Room = cls.Room;
                        nCls.SetType = cls.SetType2;
                        nCls.Label = cls.Label2;
                        lst_THD_DisplayAtts.Add(nCls);
                    }
                    if (cls.SetType3 != ".")
                    {
                        Cls_THD_DisplayAtts nCls = new Cls_THD_DisplayAtts();
                        nCls.DataTotal = "1"; //cls.DataTotal;
                        nCls.Device = cls.Device;
                        nCls.Name = cls.Name;
                        nCls.Room = cls.Room;
                        nCls.SetType = cls.SetType3;
                        nCls.Label = cls.Label3;
                        lst_THD_DisplayAtts.Add(nCls);
                    }

                    if (cls.SetType4 != ".")
                    {
                        Cls_THD_DisplayAtts nCls = new Cls_THD_DisplayAtts();
                        nCls.DataTotal = "1"; // cls.DataTotal;
                        nCls.Device = cls.Device;
                        nCls.Name = cls.Name;
                        nCls.Room = cls.Room;
                        nCls.SetType = cls.SetType4;
                        nCls.Label = cls.Label4;
                        lst_THD_DisplayAtts.Add(nCls);
                    }                    

                }
                
                // LstAtts_DevTag.OrderBy(x => x.BlockName).ThenBy(x => x.DoorNumber);

                // blockname not null
                lst_THD_DisplayAtts.Sort((x, y) => x.Label.CompareTo(y.Label));

            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }
        private static void THD_FillList(PromptSelectionResult acSSPrompt)
        {
            SelectionSet acSSet = acSSPrompt.Value;
            ObjectId[] ids = { };

            try
            {
                ids = acSSet.GetObjectIds();
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                return;
            }

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);

                    var Cls_THD_Atts = new Cls_THD_Atts();
                    Cls_THD_Atts.BlockName = blkRef.Name;
                    Cls_THD_Atts.Handle = blkRef.Handle;
                    Cls_THD_Atts.InsertionPtOfBlock = blkRef.Position;
                    lst_THD_Atts.Add(Cls_THD_Atts);

                    AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                        switch (attRef.Tag)
                        {
                            case "COUNT":
                                Cls_THD_Atts.Count = attRef.TextString;
                                break;
                            case "NAME":
                                Cls_THD_Atts.Name = attRef.TextString;
                                break;                         
                            case "DATATOTAL":
                                Cls_THD_Atts.DataTotal = attRef.TextString;
                                break;
                            case "DEVICE":
                                Cls_THD_Atts.Device = attRef.TextString;
                                break;

                            case "LABEL1":
                                Cls_THD_Atts.Label1 = attRef.TextString;
                                break;
                            case "LABEL2":
                                Cls_THD_Atts.Label2 = attRef.TextString;
                                break;
                            case "LABEL3":
                                Cls_THD_Atts.Label3 = attRef.TextString;
                                break;
                            case "LABEL4":
                                Cls_THD_Atts.Label4 = attRef.TextString;
                                break;

                            case "NAME1":
                                Cls_THD_Atts.Name1 = attRef.TextString;
                                break;

                            case "ROOM":
                                Cls_THD_Atts.Room = attRef.TextString;
                                break;

                            case "SETTYPE1":
                                Cls_THD_Atts.SetType1 = attRef.TextString;
                                break;
                            case "SETTYPE2":
                                Cls_THD_Atts.SetType2 = attRef.TextString;
                                break;
                            case "SETTYPE3":
                                Cls_THD_Atts.SetType3 = attRef.TextString;
                                break;
                            case "SETTYPE4":
                                Cls_THD_Atts.SetType4 = attRef.TextString;
                                break;
                        }
                    }
                }
                transaction.Commit();

                // LstAtts_DevTag.OrderBy(x => x.BlockName).ThenBy(x => x.DoorNumber);

                // blockname not null
                lst_THD_Atts.Sort((x, y) => x.Room.CompareTo(y.Room));
            }
        }

    }
}
