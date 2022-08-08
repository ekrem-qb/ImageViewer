using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ImageViewer
{
    public partial class Form1 : Form
    {
        Image image;

        public Form1()
        {
            InitializeComponent();
        }

        void ImageLoad()
        {
            try
            {
                openFileDialog1.ShowDialog();
            }
            catch (FileNotFoundException)
            {
            }

            try
            {
                image = Image.FromFile(openFileDialog1.FileName);

                pictureBox1.Image = image;

                timer1.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Select an image file");
                Application.Exit();
            }

            Point screenCenter = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (pictureBox1.Width / 2), (Screen.PrimaryScreen.WorkingArea.Height) / 2 - (pictureBox1.Height / 2));
            pictureBox1.Location = screenCenter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ImageLoad();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Width == image.Width)
            {
                timer1.Stop();
            }
            else if (pictureBox1.Width < image.Width)
            {
                pictureBox1.Width += 2;

                pictureBox1.Left --;
            }
            else if (pictureBox1.Width > image.Width)
            {
                pictureBox1.Width -= 2;

                pictureBox1.Left ++;
            }

            if (pictureBox1.Height == image.Height)
            {
                timer1.Stop();
            }
            else if (pictureBox1.Height < image.Height)
            {
                pictureBox1.Height += 2;

                pictureBox1.Top --;
            }
            else if (pictureBox1.Height > image.Height)
            {
                pictureBox1.Height -= 2;

                pictureBox1.Top ++;
            }
        }


            /*-------Window Dragging-------

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case 0x84:
                        base.WndProc(ref m);
                        if ((int)m.Result == 0x1)
                            m.Result = (IntPtr)0x2;
                        return;
                }

                base.WndProc(ref m);
            }*/

            private void Form1_MouseWheel(object sender, MouseEventArgs e)
            {
                if (e.Delta < 0)
                {
                    pictureBox1.Height -= (int)(image.Height * 0.05);
                    pictureBox1.Width -= (int)(image.Width * 0.05);

                    pictureBox1.Top += (int)(image.Height * 0.025);
                    pictureBox1.Left += (int)(image.Width * 0.025);
                }
                else
                {
                    pictureBox1.Height += (int)(image.Height * 0.05);
                    pictureBox1.Width += (int)(image.Width * 0.05);

                    pictureBox1.Top -= (int)(image.Height * 0.025);
                    pictureBox1.Left -= (int)(image.Width * 0.025);
                }
            }

        private void Form1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool isDragging = false;
        int currentX, currentY;

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Cursor = Cursors.Cross;

                pictureBox1.Top += (e.Y - currentY);
                pictureBox1.Left += (e.X - currentX);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            Cursor = Cursors.Default;
        }

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            ImageLoad();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            currentX = e.X;
            currentY = e.Y;
        }
    }
}
