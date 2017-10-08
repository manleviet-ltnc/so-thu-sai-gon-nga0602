﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sở_thú_sài_gòn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox_MouseDown(Object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);
            if (index != -1)
                lb.DoDragDrop(lb.Items[index].ToString(), DragDropEffects.Copy);

        }
        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }
        private void lstDanhSach_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                bool test = false;
                for (int i = 0; i < lstDanhSach.Items.Count; i++)
                {
                    string st = lstDanhSach.Items[i].ToString();
                    string data = e.Data.GetData(DataFormats.Text).ToString();
                    if (data == st)
                        test = true;
                }
                if (test == false)
                {
                    ListBox lb = (ListBox)sender;
                    lb.Items.Add(e.Data.GetData(DataFormats.Text));

                }
            }
        }
        private void Save(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter("Danhsachthu.txt");
            if (write == null) return;
            foreach (var item in lstDanhSach.Items)
                write.WriteLine(item.ToString());
            write.Close();

        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            {
                StreamReader reader = new StreamReader("thumoi.txt");

                if (reader == null)
                    return;

                string input = null;
                while ((input = reader.ReadLine()) != null)
                {
                    lstThuMoi.Items.Add(input);
                }
                reader.Close();

                using (StreamReader rs = new StreamReader("Danhsachthu.txt"))
                {
                    input = null;
                    while ((input = rs.ReadLine()) != null)
                        lstDanhSach.Items.Add(input);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltime.Text = string.Format("Bây giờ là...ngày {0}:{1}:{2} ngày {3} tháng {4} năm {5}",
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second,
                DateTime.Now.Day,
                DateTime.Now.Month,
                DateTime.Now.Year);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            lstDanhSach.Items.Remove(lstDanhSach.SelectedItem);
        }
    }
}
      

