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
            double x1 = val[0];
            double y1 = val[1];
            double x2 = val[2];
            double y2 = val[3];
            Imgproc.line(color_dst, new Point(x1, y1), new Point(x2, y2), new Scalar(0, 0, 255), 3,
                    Imgproc.LINE_AA, 0);
        }

        HighGui.imshow("Hough", color_dst);
        HighGui.waitKey(0);
    }
}
