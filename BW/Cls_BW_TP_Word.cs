using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Xceed.Words.NET;

namespace MyFirstProject.BW
{
    public static class Cls_BW_TP_Word
    {
        public static void BtnWord_Click_Sub()
        {
            var doc = DocX.Load(@"C:\Users\gwilliams\Desktop\CP_170815\aberdeen sd PI.docx");

            string msg = "";
            // get the last 5 paragraphs
            msg += doc.Paragraphs[doc.Paragraphs.Count - 5].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 4].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 3].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 2].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 1].Text + Environment.NewLine;

            MessageBox.Show(msg);

            var paraFormat = new Xceed.Words.NET.Formatting();
            paraFormat.FontFamily = new Xceed.Words.NET.Font("Courier New");
            paraFormat.Size = 11D;
            paraFormat.Spacing = 0;

            // insert a new paragraph
            doc.InsertParagraph(DateTime.Now.ToShortDateString() + "-This is my first paragraph", false, paraFormat);

            msg = "";
            // get the last 5 paragraphs
            msg += doc.Paragraphs[doc.Paragraphs.Count - 5].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 4].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 3].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 2].Text + Environment.NewLine;
            msg += doc.Paragraphs[doc.Paragraphs.Count - 1].Text + Environment.NewLine;

            MessageBox.Show(msg);

            doc.Save();
        }


        public static string GetReport()
        {
            string msg = "";

            var PiFiles = System.IO.Directory.GetFiles(@"K:\3M", "*pi.docx", SearchOption.AllDirectories).ToList();

            PiFiles.Sort();

            foreach (string s in PiFiles)
            {
                var doc = DocX.Load(s);

                msg += "FileName: " + Path.GetFileNameWithoutExtension(s) + ", ";
                msg += "LastWrite: " + File.GetLastWriteTime(s) + Environment.NewLine;

                int offset = 1;
                int parcnt = 0;

                do
                {
                    string p = doc.Paragraphs[doc.Paragraphs.Count - offset].Text;

                    if (p != null && p != " " && p != "")
                    {
                        msg += p + Environment.NewLine;
                        parcnt++;
                        offset++;
                    }
                    else
                    {
                        offset++;
                    }

                } while (parcnt <= 3); // last 4 paragraphs
                 
                msg += Environment.NewLine;
            }
            return msg;
        }


        public interface IntPi
        {
            string FileName { get; set; }
            string LastWrite { get; set; }
            string Line1 { get; set; }
            string Path { get; set; }
            //string Line3 { get; set; }
            //string Line4 { get; set; }

        }

        public class ClsPi : IntPi
        {
            public string FileName { get; set; }
            public string LastWrite { get; set; }
            public string Line1 { get; set; }
            public string Path { get; set; }
            //public string Line3 { get; set; }
            //public string Line4 { get; set; }

        }

        public static List<ClsPi> GetReport(List<ClsPi> lst, string folder)
        {
            var PiFiles = System.IO.Directory.GetFiles(folder, "*pi.docx", SearchOption.AllDirectories).ToList();

            PiFiles.Sort();

            foreach (string s in PiFiles)
            {
                int offset = 1;
                int parcnt = 0;

                var doc = DocX.Load(s);

                ClsPi _pi = new ClsPi();

                _pi.FileName = Path.GetFileNameWithoutExtension(s).ToUpper();
                _pi.LastWrite = File.GetLastWriteTime(s).ToString();
                _pi.Path = Path.GetDirectoryName(s);


                Table tbls = doc.Tables[0];

                var par = tbls.Paragraphs;
           
                //

                do
                {
                    string p = doc.Paragraphs[doc.Paragraphs.Count - offset].Text;

                    if (p != null && p != " " && p != "")
                    {
                        if (parcnt == 0) { _pi.Line1 = p + "\n"; }
                        if (parcnt == 1) { _pi.Line1 = _pi.Line1 + p + "\n"; }
                        if (parcnt == 2) { _pi.Line1 = _pi.Line1 + p + "\n"; }
                        if (parcnt == 3) { _pi.Line1 = _pi.Line1 + p; }

                        parcnt++;
                        offset++;
                    }
                    else
                    {
                        offset++;
                    }

                } while (parcnt <= 3); // last 4 paragraphs

                lst.Add(_pi);
            }
            return lst;
        }

    }
}
