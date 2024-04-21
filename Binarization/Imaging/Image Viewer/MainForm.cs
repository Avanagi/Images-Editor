using System;
using System.Drawing;
using System.Windows.Forms;
using Accord.Imaging.Formats;

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
                try
                {
                    Bitmap originalImage = new Bitmap(openFileDialog.FileName);
                    pictureBox.Image = ImageDecoder.DecodeFromFile(openFileDialog.FileName);

                    int threshold = CalculateThreshold(originalImage);

                    Bitmap binaryImage = ApplyThreshold(originalImage, threshold);

                    pictureBox1.Image = binaryImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int CalculateThreshold(Bitmap image)
        {
            int minBrightness = 255;
            int maxBrightness = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    int brightness = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    if (brightness < minBrightness)
                        minBrightness = brightness;
                    if (brightness > maxBrightness)
                        maxBrightness = brightness;
                }
            }

            int threshold = (minBrightness + maxBrightness) / 2;
            return threshold;
        }

        private Bitmap ApplyThreshold(Bitmap image, int threshold)
        {
            Bitmap binaryImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int brightness = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    if (brightness >= threshold)
                        binaryImage.SetPixel(x, y, Color.White);
                    else
                        binaryImage.SetPixel(x, y, Color.Black);
                }
            }

            return binaryImage;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageDecoder.DecodeFromFile(openFileDialog.FileName);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
