﻿using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Reflection;
using System.Threading;
using System.Timers;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        //public class BBox_csv_reader
        //{
        //    //[Name("Image Index")]
        //    //public string image { get; set; }
        //    //[Name("Finding Label")]
        //    //public string label { get; set; }
        //    //[Name("Bbox [x")]
        //    //public double x { get; set; }
        //    //[Name("y")]
        //    //public double y { get; set; }
        //    //[Name("w")]
        //    //public double w { get; set; }
        //    //[Name("h]")]
        //    //public double h { get; set; }
        //}
        //-----------------------

        List<string> image_index = new List<string>();
        List<string> finding_label = new List<string>();
        List<double> my_x = new List<double>();
        List<double> my_y = new List<double>();
        List<double> my_w = new List<double>();
        List<double> my_h = new List<double>();
        // BBox_csv

        List<string> image_index_01 = new List<string>();
        List<string> finding_label_01 = new List<string>();
        List<int> follow_up = new List<int>();
        List<int> patient_ID = new List<int>();
        List<int> patient_Age = new List<int>();
        List<char> patient_Gen = new List<char>();
        List<string> view_Posi = new List<string>();
        List<int> ori_width = new List<int>();
        List<int> ori_height = new List<int>();
        List<double> oripix_x = new List<double>();
        List<double> oripix_y = new List<double>();

        DataTable data_entry_dt = new DataTable();
        //Data_Entry_csv

        private DataTable filtered_data = new DataTable();
        // Finding Labels 열의 각 요소를 콤보박스에 넣기 위해 추출

        public int index = 0;
        public int box_index;

        HashSet<string> filelist_set;

        private readonly string[] folderPath =
        {
            @"C:\Users\HKIT\Desktop\c#_med_images\images_001",
            @"C:\Users\HKIT\Desktop\c#_med_images\images_002",
            @"C:\Users\HKIT\Desktop\c#_med_images\images_003"
        };

        

        List<string> filelist = new List<string>();
        List<string> filter_filelist = new List<string>();
        List<string> have_bbox_list = new List<string>();

        List<string> same_file_str = new List<string>();
        List<int> same_file_cnt = new List<int>();
        public void Load_Image()
        {
            foreach (string path in folderPath)
            {
                filelist.AddRange(Directory.GetFiles(path));
            }
            filelist.Sort();
        }
       
        //public object same_files(List<string> list)
        //{
        //    var samefiles = list
        //                        .GroupBy(x => x)
        //                        .Where(g => g.Count() > 1)
        //                        .Select(x => new { Element = x.Key, Count = x.Count() })
        //                        .ToList();
        //    return samefiles;
        //}

        public void read_BBox_List()
        {
            // BBox csv 파일 읽기......

            using (var streamReader = new StreamReader(@"C:\Users\HKIT\Desktop\c#_med_images\BBox_List_2017.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    using (var csvDataReader = new CsvDataReader(csvReader))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(csvDataReader);

                        // dt 정렬하고 새로운 DataTable에 넣어서 list 에 저장해주기.
                        DataView dv = dt.DefaultView;
                        dv.Sort = "Image Index ASC";

                        DataTable sort_dt = dv.ToTable();

                        foreach (DataColumn column in sort_dt.Columns)
                        {
                            dataGridView1.Columns.Add(column.ColumnName, column.ColumnName);
                        }
                        foreach (DataRow row in sort_dt.Rows)
                        {
                            if (filelist_set.Contains(row["Image Index"].ToString()))
                            {
                                image_index.Add(row["Image Index"].ToString());
                                finding_label.Add(row["Finding Label"].ToString());
                                my_x.Add(Convert.ToDouble(row["Bbox [x"]));
                                my_y.Add(Convert.ToDouble(row["y"]));
                                my_w.Add(Convert.ToDouble(row["w"]));
                                my_h.Add(Convert.ToDouble(row["h]"]));
                                dataGridView1.Rows.Add(row.ItemArray);
                            }
                        }
                        var same_file_list = image_index
                                                .GroupBy(x => x)
                                                .Where(g => g.Count() > 1)
                                                .Select(x => new { Element = x.Key, Count = x.Count() })
                                                .ToList();
                        same_file_str = same_file_list
                                                    .Select(x => x.Element)
                                                    .ToList();
                        same_file_cnt = same_file_list
                                                    .Select(x => x.Count)
                                                    .ToList();
                    }
                }
            }

        }
        public void read_Data_Entry()
        {
            using (var streamReader = new StreamReader(@"C:\Users\HKIT\Desktop\c#_med_images\Data_Entry_2017.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    using (var csvDataReader = new CsvDataReader(csvReader))
                    {                   
                        data_entry_dt.Load(csvDataReader);

                        // dt 정렬하고 새로운 DataTable에 넣어서 list 에 저장해주기.
                        DataView dv = data_entry_dt.DefaultView;
                        dv.Sort = "Image Index ASC";

                        DataTable sort_dt = dv.ToTable();
                        //HashSet<string> fileNamesWithExtensions = new HashSet<string>(filelist.Select(filePath => Path.GetFileName(filePath)));
                        foreach (DataColumn column in sort_dt.Columns)
                        {
                            dataGridView2.Columns.Add(column.ColumnName, column.ColumnName);
                        }
                        foreach (DataRow row in sort_dt.Rows)
                        {
                            //string imageIndex = row["Image Index"].ToString();
                            //string imageNameWithExtension = Path.GetFileName(imageIndex);
                            if (filelist_set.Contains(row["Image Index"].ToString()))
                            {
                                // 나중에 사용하기 쉽게 각각의 데이터 list에 저장해놓기.
                                image_index_01.Add(row["Image Index"].ToString());
                                finding_label_01.Add(row["Finding Labels"].ToString());
                                follow_up.Add(Convert.ToInt16(row["Follow-up #"]));
                                patient_ID.Add(Convert.ToInt16(row["Patient ID"]));
                                patient_Age.Add(Convert.ToInt16(row["Patient Age"]));
                                patient_Gen.Add(Convert.ToChar(row["Patient Gender"]));
                                view_Posi.Add(row["View Position"].ToString());
                                ori_width.Add(Convert.ToInt32(row["OriginalImage[Width"]));
                                ori_height.Add(Convert.ToInt32(row["Height]"]));
                                oripix_x.Add(Convert.ToDouble(row["OriginalImagePixelSpacing[x"]));
                                oripix_y.Add(Convert.ToDouble(row["y]"]));
                                dataGridView2.Rows.Add(row.ItemArray);
                                // -------------------------------
                                //}
                            }
                        }
                        //dataGridView2.DataSource = data_entry_dt;
                    }
                }
            }
        }
        public void file_process()
        {
            Load_Image();
            // 혹시라도 확장자가 .png 가 아닌 다른파일이 껴있으면 그거 삭제해주기.
            foreach (string file in filelist)
            {
                if (Path.GetExtension(file) != ".png")
                {
                    filelist.Remove(file);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            file_process();
            Console.WriteLine("Form1");
            filelist_set = new HashSet<string>(filelist.Select(filePath => Path.GetFileName(filePath)));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            read_BBox_List();
            read_Data_Entry();
            Console.WriteLine("Form1_Load");
            // foreach문이 너무 오래걸려서 HashSet 을 사용헤서 훨씬 빠르게 동작.


            //foreach (string file in filelist)
            //{
            //    foreach (string im_index in image_index)
            //        if (!Path.GetFileName(file).Equals(im_index))
            //        {
            //            Console.WriteLine("delete");
            //            filelist.Remove(file);
            //        }
            //}

            //HashSet<string> imageIndexSet = new HashSet<string>(image_index);

            var filteredFiles = image_index.Where(file =>
            {
                string fileName = Path.GetFileName(file);
                return filelist_set.Contains(fileName);
            });
            have_bbox_list = filteredFiles.ToList();
            foreach (string item in have_bbox_list)
            {
                Console.WriteLine(item);
            }
            //Console.WriteLine(filteredFiles.GetType());
            //Console.WriteLine(have_bbox_list.GetType());

            ////------------------------------------------------------------------

            //Console.WriteLine(filteredFiles.GetType());
            //Console.WriteLine(filteredFiles.Count());
            //Console.WriteLine(filelist.Count);
            //foreach (string i in finding_label)
            //{
            //    Console.WriteLine(i);
            //}
            filter_filelist = filelist;
            Display_image(index);
        }
        private void Display_image(int func_index)
        {
            string now_filename = Path.GetFileName(filelist[func_index]);
            Console.WriteLine(now_filename);
            Mat image = Cv2.ImRead($@"{filelist[func_index]}", ImreadModes.Color);

            label3.Text = "Image Num > " + image_index_01[func_index];
            label4.Text = "Label > " + finding_label_01[func_index];
            label5.Text = "Follw Up > " + follow_up[func_index];
            label6.Text = "Patient ID > " + patient_ID[func_index].ToString();
            label7.Text = "Patiend Age > " + patient_Age[func_index].ToString();
            label8.Text = "Patient Gender > " + patient_Gen[func_index].ToString();

            // Bbox_csv 파일에 포함되는 이미지는 Bbox 쳐주기.......

            if (have_bbox_list.Contains(now_filename))
            {
                int text_int = 0;
                box_index = have_bbox_list.IndexOf(now_filename);
                if (same_file_str.Contains(Path.GetFileName(now_filename)))
                {
                    var same_file_cnt_index = same_file_str.IndexOf(Path.GetFileName(now_filename));
                    Console.Write("box_indexsssssssss" + box_index + "   ");
                    for (int i = box_index; i < box_index + same_file_cnt[same_file_cnt_index]; i++)
                    {
                        Cv2.PutText(image, finding_label[i], new OpenCvSharp.Point(10, 30 + (text_int * 30)), HersheyFonts.HersheySimplex, 1, Scalar.Aqua, 1, LineTypes.AntiAlias);
                        Cv2.Rectangle(image, new OpenCvSharp.Point(my_x[i], my_y[i]), new OpenCvSharp.Point(my_x[i] + my_w[i], my_y[i] + my_h[i]), Scalar.Green, 1, LineTypes.AntiAlias);
                        text_int++;
                        Console.Write(finding_label[i] + "   ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Cv2.PutText(image, finding_label[box_index], new OpenCvSharp.Point(10, 30), HersheyFonts.HersheySimplex, 1, Scalar.Aqua, 1, LineTypes.AntiAlias);
                    Cv2.Rectangle(image, new OpenCvSharp.Point(my_x[box_index], my_y[box_index]), new OpenCvSharp.Point(my_x[box_index] + my_w[box_index], my_y[box_index] + my_h[box_index]), Scalar.Green, 1, LineTypes.AntiAlias);
                    Console.WriteLine("box_index" + box_index + ", " + finding_label[box_index]);
                }
            }

            pic_box.Image = BitmapConverter.ToBitmap(image);
            label1.Text = Path.GetFileName(filelist[index]);
        }


        // Click_Event

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string my_info = dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString();
            foreach (string file in filelist)
            {
                if (my_info == Path.GetFileName(file))
                {
                    index = filelist.IndexOf(file);
                    Display_image(index);
                }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string my_info = dataGridView2.Rows[this.dataGridView2.CurrentCellAddress.Y].Cells[0].Value.ToString();
            foreach (string file in filelist)
            {
                if (my_info == Path.GetFileName(file))
                {
                    index = filelist.IndexOf(file);
                    Display_image(index);
                }
            }
        }

        private void previous_btn_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("cannot");
            }
            else
            {
                index--;
                Display_image(index);
            }
        }
        private void next_btn_Click(object sender, EventArgs e)
        {
            if (index == filelist.Count - 1)
            {
                MessageBox.Show("파일의 끝입니다.");
            }
            else
            {
                index++;
                Display_image(index);
            }
        }
        private void prev_10_Click(object sender, EventArgs e)
        {
            if (index < 9)
            {
                MessageBox.Show("cannot");
            }
            else
            {
                index -= 10;
                Display_image(index);
            }
        }
        private void next_10_Click(object sender, EventArgs e)
        {
            if (index > filelist.Count - 11)
            {
                MessageBox.Show("cannot");
            }
            else
            {
                index += 10;
                Display_image(index);
            }
        }
        private void first_btn_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                MessageBox.Show("cannot");
            }
            else
            {
                index = 0;
                Display_image(index);
            }
        }
        private void last_btn_Click(object sender, EventArgs e)
        {
            if (index == filelist.Count-1)
            {
                MessageBox.Show("파일의 끝입니다.");
            }
            else
            {
                index = filelist.Count - 1;
                Display_image(index);

            }
        }

        private void update_list(DataTable datatable)
        {
            image_index_01.Clear();
            finding_label_01.Clear();
            follow_up.Clear();
            patient_ID.Clear();
            patient_Age.Clear();
            patient_Gen.Clear();
            view_Posi.Clear();
            ori_width.Clear();
            ori_height.Clear();
            oripix_x.Clear();
            oripix_y.Clear();
            foreach (DataRow row in datatable.Rows)
            {
                image_index_01.Add(row["Image Index"].ToString());
                finding_label_01.Add(row["Finding Labels"].ToString());
                follow_up.Add(Convert.ToInt16(row["Follow-up #"]));
                patient_ID.Add(Convert.ToInt16(row["Patient ID"]));
                patient_Age.Add(Convert.ToInt16(row["Patient Age"]));
                patient_Gen.Add(Convert.ToChar(row["Patient Gender"]));
                view_Posi.Add(row["View Position"].ToString());
                ori_width.Add(Convert.ToInt32(row["OriginalImage[Width"]));
                ori_height.Add(Convert.ToInt32(row["Height]"]));
                oripix_x.Add(Convert.ToDouble(row["OriginalImagePixelSpacing[x"]));
                oripix_y.Add(Convert.ToDouble(row["y]"]));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filelist = filter_filelist;
            data_entry_dt.DefaultView.RowFilter = string.Format("Convert([{0}], 'System.String') LIKE '%{1}%'", "Finding Labels", textBox1.Text);
            filtered_data = data_entry_dt.DefaultView.ToTable();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                Console.WriteLine("다지웠네");
                update_list(filtered_data);
            }
            else
            {
                Console.WriteLine("바꿧네");
                update_list(filtered_data);
            }
            HashSet<string> imageIndexSet = new HashSet<string>(image_index_01);

            var filteredFiles = filelist.Where(file =>
            {
                string fileName = Path.GetFileName(file);
                return imageIndexSet.Contains(fileName);
            });
            filelist = filteredFiles.ToList();
            Console.WriteLine(filelist.Count);
        }
    }
}
