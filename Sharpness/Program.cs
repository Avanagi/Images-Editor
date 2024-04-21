using System;
using System.Diagnostics;
using System.Drawing;
using AForge.Imaging.Filters;

class Program
{
    static void Main()
    {
        try
        {
            using (Bitmap? image = System.Drawing.Image.FromFile("House.jpg") as Bitmap)
            {
                ShowImage("House.jpg");
                ApplyCustomBlurFilter(image);
                image.Save("custom_blurred_output.jpg");
                ShowImage("custom_blurred_output.jpg");
                ApplyCustomSharpFilter(image);
                image.Save("custom_sharp_output.jpg");
                ShowImage("custom_sharp_output.jpg");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    static void ApplyCustomBlurFilter(Bitmap image)
    {
        int[,] blurMatrix = {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };

        int divisor = 16;

        Convolution filter = new Convolution(blurMatrix, divisor);
        filter.ApplyInPlace(image);
    }

    static void ApplyCustomSharpFilter(Bitmap image)
    {
        int[,] sharpMatrix = {
             {  -1, -1,  -1 },
            { -1,  9, -1 },
            {  -1, -1,  -1 }
        };

        Convolution filter = new Convolution(sharpMatrix);
        filter.ApplyInPlace(image);
    }

    static void ShowImage(string imagePath)
    {
        Process.Start(new ProcessStartInfo(imagePath) { UseShellExecute = true });
    }
}
