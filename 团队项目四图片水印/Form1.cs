using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 团队项目四图片水印
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Image imgOrigin;
        Image imgWaterMark;
        string imgFileName; 
        string imgFileName1;
        string imgFileNameGroup;
        Graphics g;
        OpenFileDialog ofd;
        OpenFileDialog ofd2;
        int WaterMarKClickCount = 0;
        int PicCount = 0;
        float WaterMarkLocationX=200;
        float WaterMarkLocationY=150;
        Color c;
        

        //打开一张图片
        private void button1_Click(object sender, EventArgs e)
        {
            PicCount = 1;
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\桌面设计\\团队项目四图片水印\\imgs";
            ofd.Filter = "图片文件(*.jpg;*.png)|*.jpg;*.png";
            //当选择取消时
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            //获得文件名
            imgFileName = ofd.FileName;

            //获得图片
            imgOrigin = Image.FromFile(imgFileName);
            //将图片显示
            pictureBox1.BackgroundImage = imgOrigin;
          

        }

        //获得水印图片
        private void button2_Click(object sender, EventArgs e)
        {
            WaterMarKClickCount = 1;
            ofd2 = new OpenFileDialog();
            ofd2.InitialDirectory = "C:\\桌面设计\\团队项目四图片水印\\imgs";
            ofd2.Filter = "图片文件(*.jpg;*.png)|*.jpg;*.png";
            //当选择取消时
            if (ofd2.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            
                //获得文件名
                 imgFileName1 = ofd2.FileName;

                //获得图片
                imgWaterMark = Image.FromFile(imgFileName1);

                g = Graphics.FromImage(pictureBox1.BackgroundImage);

                g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
            
                pictureBox1.Refresh();
           
        }

        //保存加水印后图片
        private void button3_Click(object sender, EventArgs e)
        {

            //一张图片时
            if (ofd.FileNames.Length == 1)
            {
                g = Graphics.FromImage(imgOrigin);

                if (imgFileName != "")
                {
                    g = Graphics.FromImage(imgOrigin);
                   
                    g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
                    
                    pictureBox1.Refresh();

                }
                Image imgAfter = pictureBox1.BackgroundImage;

                string[] str = imgFileName.Split('\\');
                
                imgAfter.Save(Path.Combine(@"C:\桌面设计\团队项目四图片水印\imgAfter", str[4]));
            }
            // 批量保存
            else
            {
                
                foreach (string fileName in ofd.FileNames)
                {
                    imgFileNameGroup = fileName;

                    //获得图片
                    Image imgGroup = Image.FromFile(imgFileNameGroup);
                    g = Graphics.FromImage(imgGroup);
                    g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
                   
                    pictureBox1.BackgroundImage = imgGroup;
                    pictureBox1.Refresh();
                    Image img1 = pictureBox1.BackgroundImage;
                    string[] str = imgFileNameGroup.Split('\\');
                    img1.Save(Path.Combine(@"C:\桌面设计\团队项目四图片水印\imgAfter", str[4]));

                }

            }
            //保存成功信息提示
            label3.Visible = true;

        }

        //批量加载图片
        private void button4_Click(object sender, EventArgs e)
        {
            PicCount = 1;
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\桌面设计\\团队项目四图片水印\\imgs";
            ofd.Filter = "图片文件(*.jpg;*.png)|*.jpg;*.png";
            //多选属性
            ofd.Multiselect = true;
            //当选择取消时
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            //显示最后一张图片
            int length = ofd.FileNames.Length;
            pictureBox1.BackgroundImage = Image.FromFile(ofd.FileNames[length - 1]);
           
        }
        //透明度选择
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //确保选择图片和水印，否则弹出警告信息
            if (PicCount == 0 || WaterMarKClickCount == 0)
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
                
                Bitmap bmp = new Bitmap(imgWaterMark);
                Bitmap bmpNew = new Bitmap(imgWaterMark.Width, imgWaterMark.Height);
                Color pixel;
                for (int i = 0; i < imgWaterMark.Width; i++)
                {
                    for (int j = 0; j < imgWaterMark.Height; j++)
                    {
                        pixel = bmp.GetPixel(i, j);
                        int r1 = pixel.R;
                        int g1 = pixel.G;
                        int b1 = pixel.B;

                        bmpNew.SetPixel(i, j, Color.FromArgb(trackBar1.Value, r1, g1, b1));

                    }
                }

                imgWaterMark = bmpNew;
               if (ofd.FileNames.Length == 1)
                {
                    g = Graphics.FromImage(imgOrigin);
                    g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
                    pictureBox1.Refresh();

                }
                else
                {
                    foreach (string fileName in ofd.FileNames)
                    {
                        imgFileNameGroup = fileName;

                        //获得图片
                        Image imgGroup = Image.FromFile(imgFileNameGroup);
                        g = Graphics.FromImage(imgGroup);
                        g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
                        pictureBox1.BackgroundImage = imgGroup;
                        pictureBox1.Refresh();
                    }
                }
            }
        }

        //获取水印x坐标
        private void WaterMarkLocX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WaterMarkLocationX = Convert.ToSingle(WaterMarkLocX.Text);
                SendKeys.Send("{tab}");

            }
        }
        //获取水印y坐标
        private void WaterMarkLocY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                WaterMarkLocationY = Convert.ToSingle(WaterMarkLocY.Text);
                //清除画布
                Bitmap bmp2 = new Bitmap(imgWaterMark);
                for(int i = 0; i < imgWaterMark.Width; i++)
                {
                    for(int j = 0; j < imgWaterMark.Height; j++)
                    {
                      c = bmp2.GetPixel(i, j);
                    }
                }
                g.Clear(c);
                //重新读取文件绘画
                if (ofd.FileNames.Length == 1)
                {

                    imgOrigin = Image.FromFile(imgFileName);
                    pictureBox1.BackgroundImage = imgOrigin;
                }
                else
                {
                    int length = ofd.FileNames.Length;
                    pictureBox1.BackgroundImage = Image.FromFile(ofd.FileNames[length - 1]);
                }
                imgWaterMark = Image.FromFile(imgFileName1);

                g = Graphics.FromImage(pictureBox1.BackgroundImage);
                g.DrawImage(imgWaterMark, WaterMarkLocationX, WaterMarkLocationY);
              
                pictureBox1.Refresh();

            }
        }
        //关闭
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

