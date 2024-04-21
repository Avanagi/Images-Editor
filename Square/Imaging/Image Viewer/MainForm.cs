using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Accord.Imaging;
using Accord;

namespace SampleApp
{

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap originalImage = new Bitmap(openFileDialog.FileName);
                pictureBox.Image = originalImage;

                var fastCornersDetector = new FastCornersDetector() { Threshold = 27 };

                UnmanagedImage fastImage = UnmanagedImage.FromManagedImage(originalImage);

                List<Accord.IntPoint> corners = fastCornersDetector.ProcessImage(fastImage);

                using (Graphics g = Graphics.FromImage(originalImage))
                {
                    foreach (IntPoint corner in corners)
                    {
                        g.DrawEllipse(Pens.Yellow, corner.X-3, corner.Y-3, 5, 5);
                    }
                }

                pictureBox.Image = originalImage;
                

                double area = CalculatePolygonArea(corners);
                MessageBox.Show($"Area of polygon: {area}");
            }
        }

        private double CalculatePolygonArea(List<Accord.IntPoint> allPoint)
        {
            double area = 0;
            for (int i = 0; i < allPoint.Count; i++)
            {
                int j = (i + 1) % allPoint.Count;
                Accord.IntPoint a = allPoint[i], b = allPoint[j];

                area += a.X * b.Y - a.Y * b.X;
            }
            return Math.Abs(area) / 2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
