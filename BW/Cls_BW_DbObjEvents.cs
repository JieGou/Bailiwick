using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

using AcadApplication = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MyFirstProject.BW
{

    public static class Cls_BW_DbObjEvents
    {

        [CommandMethod("AddDocColEvent")]
        public static void AddDocEvent()
        {           
            //Document acDoc = AcadApplication.DocumentManager.MdiActiveDocument;

            //acDoc.BeginDocumentClose +=
            //    new DocumentBeginCloseEventHandler(docBeginDocClose);

            AcadApplication.DocumentManager.DocumentActivated -= new DocumentCollectionEventHandler(DocColDocAct);
            AcadApplication.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(DocColDocAct);            
        }


        public static void DocColDocAct(object senderObj, DocumentCollectionEventArgs docColDocActEvtArgs)
        {          
            Document acDoc = AcadApplication.DocumentManager.MdiActiveDocument;

            acDoc.BeginDocumentClose -= new DocumentBeginCloseEventHandler(DocBeginDocClose);

            acDoc.BeginDocumentClose += new DocumentBeginCloseEventHandler(DocBeginDocClose);

            AcadApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage(docColDocActEvtArgs.Document.Name + " was activated.");
        }


        [CommandMethod("RemoveDocEvent")]
        public static void RemoveDocColEvent()
        {
            Document acDoc = AcadApplication.DocumentManager.MdiActiveDocument;
            acDoc.BeginDocumentClose -= new DocumentBeginCloseEventHandler(DocBeginDocClose);

            AcadApplication.DocumentManager.DocumentActivated -= new DocumentCollectionEventHandler(DocColDocAct);
        }


        public static void DocBeginDocClose(object senderObj, DocumentBeginCloseEventArgs docBegClsEvtArgs)
        {
            // Display a message box prompting to continue closing the document
            if (System.Windows.Forms.MessageBox.Show(
                                 "The document is about to be closed." +
                                 "\nDo you want to continue?",
                                 "Close Document",
                                 System.Windows.Forms.MessageBoxButtons.YesNo) ==
                                 System.Windows.Forms.DialogResult.No)
            {
                docBegClsEvtArgs.Veto();
            }
        }



        //public static void DBObjEvent_Cancelled_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nCancelled\n");
        //}

        //public void DBObjEvent_Copied_Handler(object sender, ObjectEventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nCopied\n");
        //}

        //public void DBObjEvent_Erased_Handler(object sender, ObjectErasedEventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nErased\n");
        //}

        //public void DBObjEvent_Goodbye_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nGoodbye\n");
        //}

        //public void DBObjEvent_Modified_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nModified\n");
        //}

        //public void DBObjEvent_ModifiedXData_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nModifiedXData\n");
        //}

        //public void DBObjEvent_ModifyUndone_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nModifyUndone\n");
        //}

        //public void DBObjEvent_ObjectClosed_Handler(object sender, ObjectClosedEventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nObjectClosed\n");
        //}

        //public void DBObjEvent_OpenedForModify_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nOpenedForModify\n");
        //}

        //public void DBObjEvent_Reappended_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nReappended\n");
        //}

        //public void DBObjEvent_SubObjectModified_Handler(object sender, ObjectEventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nSubObjectModified\n");
        //}

        //public void DBObjEvent_Unappended_Handler(object sender, EventArgs e)
        //{
        //    AcadApplication.DocumentManager.GetDocument((sender as DBObject).Database).Editor.WriteMessage("\nUnappended.\n");
        //}


    }

    public static class Cls_BW_PlotManagerEvents
    {
        [CommandMethod("RegisterPlotManagerEvents")]
        public static void RegisterPlotManagerEvents_Method()
        {
            Register();
        }

        [CommandMethod("UnRegisterPlotManagerEvents")]
        public static void UnRegisterPlotManagerEvents_Method()
        {
            UnRegister();
        }

        public static void Register()
        {
            Autodesk.AutoCAD.PlottingServices.PlotReactorManager plotManager = new Autodesk.AutoCAD.PlottingServices.PlotReactorManager();

            plotManager.BeginPlot += new Autodesk.AutoCAD.PlottingServices.BeginPlotEventHandler(PlotManager_BeginPlot);
            plotManager.EndPlot += new Autodesk.AutoCAD.PlottingServices.EndPlotEventHandler(PlotManager_EndPlot);
            plotManager.PlotCancelled += new Autodesk.AutoCAD.PlottingServices.PlotCancelledEventHandler(PlotManager_PlotCancelled);
        }

        public static void UnRegister()
        {
            Autodesk.AutoCAD.PlottingServices.PlotReactorManager plotManager = new Autodesk.AutoCAD.PlottingServices.PlotReactorManager();

            plotManager.BeginPlot -= new Autodesk.AutoCAD.PlottingServices.BeginPlotEventHandler(PlotManager_BeginPlot);
            plotManager.EndPlot -= new Autodesk.AutoCAD.PlottingServices.EndPlotEventHandler(PlotManager_EndPlot);
            plotManager.PlotCancelled -= new Autodesk.AutoCAD.PlottingServices.PlotCancelledEventHandler(PlotManager_PlotCancelled);
        }

        static void PlotManager_PlotCancelled(object sender, EventArgs e)
        {
            AcadApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nPlotCancelled\n");
        }

        static void PlotManager_EndPlot(object sender, Autodesk.AutoCAD.PlottingServices.EndPlotEventArgs e)
        {
            AcadApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nEndPlot\n");
        }

        static void PlotManager_BeginPlot(object sender, Autodesk.AutoCAD.PlottingServices.BeginPlotEventArgs e)
        {
            AcadApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nBeginPlot\n");
        }
    }



}
