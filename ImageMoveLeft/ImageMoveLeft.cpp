#include <opencv2/opencv.hpp>
#include <iostream>

using namespace cv;
using namespace std;

int main() {
    Mat image = imread("Images\\Screen.png");

    if (image.empty()) {
        cout << "Не удалось загрузить изображение." << endl;
        return -1;
    }
    
    imshow("Исходное изображение", image);

    //матрица сдвига размером 2x3. Первая строка [1, 0, -400] отвечает за горизонтальный сдвиг (на -400 пикселей влево), 
    // а вторая строка [0, 1, 0] за вертикальный сдвиг (без сдвига).
    Mat shiftMatrix = (Mat_<float>(2, 3) << 1, 0, -400, 0, 1, 0);
    Mat shiftedImage;
    warpAffine(image, shiftedImage, shiftMatrix, image.size());

    imshow("Сдвинутое изображение", shiftedImage);
    waitKey(0);
}
