using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

using MyFirstProject.BW.MMM;
using MyFirstProject.BW.FDX;
using MyFirstProject.BW.AMZ;
using MyFirstProject.BW.TGT;
using MyFirstProject.BW.THD;
using MyFirstProject.BW.WLG;
using MyFirstProject.BW.USB;
using MyFirstProject.BW.UPS;


// database
// PM>  Install-Package EntityFramework -Version 6.2.0 

// word docs
// PM>  Install-Package DocX -Version 1.1.0

// excel files
// PM>  Install-Package ClosedXML -Version 0.91.0 


namespace MyFirstProject.BW
{
    public static class LinqExtensions
    {
        public class CounterOfT<T>
        {
            public T Key { get; set; }
            public int Count { get; set; }
        }

        public static IQueryable<TKey> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> groupBy) where TSource : class
            => source.GroupBy(groupBy).Where(w => w.Count() > 1).Select(s => s.Key).AsQueryable();

        public static IQueryable<TSource> GetDuplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> groupBy) where TSource : class
            => source.GroupBy(groupBy).Where(w => w.Count() > 1).SelectMany(s => s).AsQueryable();

        public static IQueryable<CounterOfT<TKey>> DuplicatesCounts<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> groupBy) where TSource : class
            => source.GroupBy(groupBy).Where(w => w.Count() > 1).Select(y => new CounterOfT<TKey> { Key = y.Key, Count = y.Count() }).AsQueryable();

        public static IQueryable<Tuple<TKey, int>> DuplicatesCountsAsTuble<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> groupBy) where TSource : class
        => source.GroupBy(groupBy).Where(w => w.Count() > 1).Select(s => Tuple.Create(s.Key, s.Count())).AsQueryable();

    }
    
    public static class Extensions
    {
        /// <summary>
        /// case insensitive string compare
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool ContainsCaseInsensitive(
            this string text,
            string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }



        /// <summary>
        /// returns blank attribute if one does not exist in the autocad block - for attribute extractions
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static AttributeReference NoAttInBlockRef(this IEnumerable<AttributeReference> lst, string tag)
        {
            return lst.SingleOrDefault(x => x.Tag == tag)
                   ?? new AttributeReference();
        }

        public static void LyrsFreezeOrFreezeAllLayers(this Document doc, bool doFreeze, bool freezeZero = false)
        {
            var db = doc.Database;
            var ed = doc.Editor;

            string msg = Environment.NewLine + "The following layers have been thawed:" + Environment.NewLine;

            using (doc.LockDocument())
            using (var tr = db.TransactionManager.StartTransaction())
            {
                var lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                foreach (var ltrId in lt)
                {
                    // Don't try to lock/unlock either the current layer or layer 0
                    // (depending on whether lockZero == true for the latter)

                    if (ltrId != db.Clayer && (freezeZero || ltrId != db.LayerZero))
                    {
                        // Open the layer for write and lock/unlock it
                        var ltr = (LayerTableRecord)tr.GetObject(ltrId, OpenMode.ForWrite);

                        if (ltr.IsFrozen)
                        {
                            msg += ltr.Name + Environment.NewLine;
                        }

                        ltr.IsFrozen = doFreeze;
                        ltr.IsOff = ltr.IsOff; // This is needed to force a graphics update
                        
                    }
                }
                tr.Commit();                
            }

            // These two calls will result in the layer's geometry fading/unfading
            // appropriately
            ed.ApplyCurDwgLayerTableChanges();
            ed.Regen();

            ed.WriteMessage(msg + Environment.NewLine);
        }

        public static void LyrsLockOrUnlockAllLayers(this Document doc, bool dolock, bool lockZero = false)
        {
            var db = doc.Database;
            var ed = doc.Editor;

            string msg = Environment.NewLine + "The following layers have been unlocked:" + Environment.NewLine;

            using (doc.LockDocument())
            using (var tr = db.TransactionManager.StartTransaction())
            {
                var lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                foreach (var ltrId in lt)
                {
                    // Don't try to lock/unlock either the current layer or layer 0
                    // (depending on whether lockZero == true for the latter)

                    if (ltrId != db.Clayer && (lockZero || ltrId != db.LayerZero))
                    {
                        // Open the layer for write and lock/unlock it
                        var ltr = (LayerTableRecord)tr.GetObject(ltrId, OpenMode.ForWrite);

                        if (ltr.IsLocked)
                        {
                            msg += ltr.Name + Environment.NewLine;
                        }

                        ltr.IsLocked = dolock;
                        ltr.IsOff = ltr.IsOff; // This is needed to force a graphics update
                       
                    }
                }
                tr.Commit();                
            }

            // These two calls will result in the layer's geometry fading/unfading
            // appropriately
            ed.ApplyCurDwgLayerTableChanges();
            ed.Regen();

            ed.WriteMessage(msg + Environment.NewLine);
        }

        public static void Zoom(this Editor ed, Extents3d ext)
        {
            if (ed == null)
                throw new ArgumentNullException("ed");

            using (var view = ed.GetCurrentView())
            {
                var wcs2dcs =
                    (Matrix3d.Rotation(-view.ViewTwist, view.ViewDirection, view.Target) *
                    Matrix3d.Displacement(view.Target - Point3d.Origin) *
                    Matrix3d.PlaneToWorld(view.ViewDirection))
                    .Inverse();
                ext.TransformBy(wcs2dcs);
                view.Width = ext.MaxPoint.X - ext.MinPoint.X;
                view.Height = ext.MaxPoint.Y - ext.MinPoint.Y;
                view.CenterPoint = new Point2d(
                    (ext.MaxPoint.X + ext.MinPoint.X) / 2.0,
                    (ext.MaxPoint.Y + ext.MinPoint.Y) / 2.0);
                ed.SetCurrentView(view);
            }
        }

        public static void ZoomExtents(this Editor ed)
        {
            if (ed == null)
                throw new ArgumentNullException("ed");

            Autodesk.AutoCAD.DatabaseServices.Database db = ed.Document.Database; ;
            if ((short)Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("cvport") == 1)
            {
                if (db.Pextmin.X < db.Pextmax.X)
                {
                    ed.Zoom(new Extents3d(db.Pextmin, db.Pextmax));
                }
                else
                {
                    ed.Zoom(new Extents3d(
                        new Point3d(db.Plimmin.X, db.Plimmin.Y, 0.0),
                        new Point3d(db.Plimmax.X, db.Plimmax.Y, 0.0)));
                }
            }
            else
            {
                if (db.Extmin.X < db.Extmax.X)
                {
                    ed.Zoom(new Extents3d(db.Extmin, db.Extmax));
                }
                else
                {
                    ed.Zoom(new Extents3d(
                        new Point3d(db.Limmin.X, db.Limmin.Y, 0.0),
                        new Point3d(db.Limmax.X, db.Limmax.Y, 0.0)));
                }
            }
        }
    }
    

    /// <summary>
    /// Ribbon must be turned on to
    /// show the ribbon on load of dll
    /// </summary>
    public class InitializationNetLoad : Autodesk.AutoCAD.Runtime.IExtensionApplication
    {
        BW.Cls_BW_Ribbon CLS_Ribbon = new Cls_BW_Ribbon();

        public void Initialize()
        {
            CLS_Ribbon.Gw_Rbn_MyRibbon();
        }
       
        /// <summary>
        /// when autocad closes
        /// </summary>
        public void Terminate()
        {               
            CLS_Ribbon = null;
        }
    }


    public static class Cls_BW_Main
    {       
        #region Main Forms

        public static Frm_BW_DrawingPrepForm frm_BW_MainForm;          
        public static void BW_MainFormShow()
        {
            if (frm_BW_MainForm == null)
            {
                frm_BW_MainForm = new Frm_BW_DrawingPrepForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_BW_MainForm, false);
            }
            else
            {
                try
                {
                    frm_BW_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_BW_MainForm = new Frm_BW_DrawingPrepForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_BW_MainForm, false);
                }
            }
        }
        
        public static Frm_MMM_Host_Form frm_MMM_Host_Form;          
        public static void MMM_HostFormShow()
        {
            if (frm_MMM_Host_Form == null)
            {
                frm_MMM_Host_Form = new Frm_MMM_Host_Form();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_MMM_Host_Form, false);
            }
            else
            {
                try
                {
                    frm_MMM_Host_Form.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_MMM_Host_Form = new Frm_MMM_Host_Form();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_MMM_Host_Form, false);
                }
            }
        }
        
        public static Frm_FDX_MainForm frm_FDX_MainForm;           
        public static void FDX_MainFormShow()
        {
            if (frm_FDX_MainForm == null)
            {
                frm_FDX_MainForm = new Frm_FDX_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_FDX_MainForm, false);
            }
            else
            {
                try
                {
                    frm_FDX_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_FDX_MainForm = new Frm_FDX_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_FDX_MainForm, false);
                }
            }
        }

        public static Frm_AMZ_MainForm frm_AMZ_MainForm;
        public static void AMZ_MainFormShow()
        {
            if (frm_AMZ_MainForm == null)
            {
                frm_AMZ_MainForm = new Frm_AMZ_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_AMZ_MainForm, false);
            }
            else
            {
                try
                {
                    frm_AMZ_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_AMZ_MainForm = new Frm_AMZ_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_AMZ_MainForm, false);
                }
            }
        }

        public static Frm_TGT_MainForm frm_TGT_MainForm;
        public static void TGT_MainFormShow()
        {
            if (frm_TGT_MainForm == null)
            {
                frm_TGT_MainForm = new Frm_TGT_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_TGT_MainForm, false);
            }
            else
            {
                try
                {
                    frm_TGT_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_TGT_MainForm = new Frm_TGT_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_TGT_MainForm, false);
                }
            }
        }

        public static Frm_THD_MainForm frm_THD_MainForm;
        public static void THD_MainFormShow()
        {
            if (frm_THD_MainForm == null)
            {
                frm_THD_MainForm = new Frm_THD_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_THD_MainForm, false);
            }
            else
            {
                try
                {
                    frm_THD_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_THD_MainForm = new Frm_THD_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_THD_MainForm, false);
                }
            }
        }

        public static Frm_WLG_MainForm frm_WLG_MainForm;
        public static void WLG_MainFormShow()
        {
            if (frm_WLG_MainForm == null)
            {
                frm_WLG_MainForm = new Frm_WLG_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_WLG_MainForm, false);
            }
            else
            {
                try
                {
                    frm_WLG_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_WLG_MainForm = new Frm_WLG_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_WLG_MainForm, false);
                }
            }
        }
        
        public static Frm_USB_MainForm frm_USB_MainForm;
        public static void USB_MainFormShow()
        {
            if (frm_USB_MainForm == null)
            {
                frm_USB_MainForm = new Frm_USB_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_USB_MainForm, false);
            }
            else
            {
                try
                {
                    frm_USB_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_USB_MainForm = new Frm_USB_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_USB_MainForm, false);
                }
            }
        }
        
        public static Frm_UPS_MainForm frm_UPS_MainForm;
        public static void UPS_MainFormShow()
        {
            if (frm_UPS_MainForm == null)
            {
                frm_UPS_MainForm = new Frm_UPS_MainForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_UPS_MainForm, false);
            }
            else
            {
                try
                {
                    frm_UPS_MainForm.Show();
                }
                catch (ObjectDisposedException ex)
                {
                    frm_UPS_MainForm = new Frm_UPS_MainForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(null, frm_UPS_MainForm, false);
                }
            }
        }




        #endregion

    }

}
