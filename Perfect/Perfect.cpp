#include <iostream>
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>

using namespace std;
using namespace cv;

int main() {
    Mat image = imread("Images\\sc.jpg");
    imshow("First", image);

    if (image.empty()) {
        cout << "Не удалось загрузить изображение." << endl;
        return -1;
    }

    int Rmax = 255;
    int Gmax = 255;
    int Bmax = 255; 
    for (int y = 0; y < image.rows; ++y) {
        for (int x = 0; x < image.cols; ++x) {
            Vec3b& pixel = image.at<Vec3b>(y, x);

            pixel[0] = pixel[0] * (255 / Rmax);
            pixel[1] = pixel[1] * (255 / Gmax);
            pixel[2] = pixel[2] * (255 / Bmax);
        }
    }

    namedWindow("Коррекция бликов", WINDOW_AUTOSIZE);
    imshow("Коррекция бликов", image);
    waitKey(0);

}
