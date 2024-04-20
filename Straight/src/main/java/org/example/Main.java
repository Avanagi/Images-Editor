package org.example;

import org.opencv.core.*;
import org.opencv.highgui.HighGui;
import org.opencv.imgcodecs.Imgcodecs;
import org.opencv.imgproc.Imgproc;

public class Main {
    public static void main(String[] args) {
        System.loadLibrary(Core.NATIVE_LIBRARY_NAME);

        String filename = "rc.jpg";

        Mat src = Imgcodecs.imread("rc.jpg", Imgcodecs.IMREAD_GRAYSCALE);

        if (src.empty()) {
            System.out.println("[!] Error: Can't load image: " + filename);
            return;
        }

        Mat lines = new Mat();

        // Детектирование границ
        Mat dst = new Mat();
        Imgproc.Canny(src, dst, 50, 200, 3, false);

        // Нахождение линий
        Imgproc.HoughLinesP(dst, lines, 1, Math.PI / 180, 50);

        // Рисование найденных линий на цветном изображении
        Mat color_dst = new Mat();
        Imgproc.cvtColor(dst, color_dst, Imgproc.COLOR_GRAY2BGR);
        for (int i = 0; i < lines.rows(); i++) {
            double[] val = lines.get(i, 0);
            Imgproc.line(color_dst, new Point(val[0], val[1]), new Point(val[2], val[3]), new Scalar(0, 0, 255), 3,
                    Imgproc.LINE_AA, 0);
        }

        HighGui.imshow("Hough", color_dst);
        HighGui.waitKey(0);
    }
}
