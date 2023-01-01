using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using MaterialSkin;
using MaterialSkin.Controls;

namespace UTB
{
    public partial class Form1 : MaterialForm
    {

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        static extern void mouse_event(int a, int b, int c, int d, int swed);

        int leftDown = 0x02;
        int leftUp = 0x04;

        Color oldCol;
        Color newCol;

        Point xy = new Point(Screen.PrimaryScreen.Bounds.Width / 2 + 2, Screen.PrimaryScreen.Bounds.Height / 2 + 2);

        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey900, Primary.LightGreen300, Primary.Green800, Accent.Green700, TextShade.BLACK);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Thread tb = new Thread(trigger) { IsBackground = true };
            tb.Start();
        }

        void trigger()
        {


            while (true)
            {
                if (GetAsyncKeyState(Keys.RButton) < 0)
                {
                    Thread.Sleep(10);
                    oldCol = GetPixel(xy);
                    Thread.Sleep(5);
                    newCol = GetPixel(xy);
                    if (oldCol.Equals(newCol) == false & checkBox2.Checked == false)
                    {
                        Thread.Sleep(1);
                        mouse_event(leftDown, 0, 0, 0, 0);
                        Thread.Sleep(10);
                        mouse_event(leftUp, 0, 0, 0, 0);
                        Thread.Sleep(2000);
                    }

                    if (oldCol.Equals(newCol) == false & checkBox2.Checked == true)
                    {
                        Thread.Sleep(1);
                        mouse_event(leftDown, 0, 0, 0, 0);
                        Thread.Sleep(500);
                        mouse_event(leftUp, 0, 0, 0, 0);
                        Thread.Sleep(2000);
                    }
                }
                Thread.Sleep(2);
            }
        }

        Color GetPixel(Point position)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 0)
            {
                xy = new Point(Screen.PrimaryScreen.Bounds.Width / 1 + 1, Screen.PrimaryScreen.Bounds.Height / 1 + 1);
            }

            if (trackBar1.Value == 1)
            {
                xy = new Point(Screen.PrimaryScreen.Bounds.Width / 2 + 2, Screen.PrimaryScreen.Bounds.Height / 2 + 2);
            }

            if (trackBar1.Value == 2)
            {
                xy = new Point(Screen.PrimaryScreen.Bounds.Width / 3 + 3, Screen.PrimaryScreen.Bounds.Height / 3 + 3);
            }

            if (trackBar1.Value == 3)
            {
                xy = new Point(Screen.PrimaryScreen.Bounds.Width / 4 + 4, Screen.PrimaryScreen.Bounds.Height / 4 + 4);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked) {
                checkBox2.Text = "Shoot for 500ms: ON";
            } else
            {
                checkBox2.Text = "Shoot for 500ms: OFF";
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/jwt2AK8xKr");
        }
    }
}