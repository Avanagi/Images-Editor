using System;
using ImageMagick;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "Table.png";
        string outputFile = "res.png";

        using (MagickImage image = new MagickImage(inputFile))
        {
            image.CannyEdge();
            image.Write(outputFile);
        }
    }
}
