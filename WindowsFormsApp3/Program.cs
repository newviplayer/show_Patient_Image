using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string folderPath = @"C:\Users\HKIT\Desktop\pyqt\images\images_001";

            //string[] files = Directory.GetFiles(folderPath); // System.String[]
            //List<string> filelist = files.ToList(); // System.Collections.Generic.List`1[System.String]

            //// 혹시라도 확장자가 .png 가 아닌 다른파일이 껴있으면 그거 삭제해주기.
            //foreach (string file in files)
            //{
            //    if (Path.GetExtension(file) != ".png")
            //    {
            //        filelist.Remove(file);
            //    }
            //}

            //Console.WriteLine(filelist.Count);

            Application.Run(new Form1());
        }
    }
}
