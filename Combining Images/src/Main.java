import org.opencv.calib3d.Calib3d;
import org.opencv.core.*;
import org.opencv.features2d.DescriptorMatcher;
import org.opencv.features2d.ORB;
import org.opencv.imgcodecs.Imgcodecs;
import org.opencv.imgproc.Imgproc;
import java.util.LinkedList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        System.loadLibrary(Core.NATIVE_LIBRARY_NAME);
        Mat image1 = Imgcodecs.imread("Images\\sc2.png");
        Mat image2 = Imgcodecs.imread("Images\\sc1.png");

        Mat grayImage1 = new Mat();
        Mat grayImage2 = new Mat();
        Imgproc.cvtColor(image1, grayImage1, Imgproc.COLOR_BGR2GRAY);
        Imgproc.cvtColor(image2, grayImage2, Imgproc.COLOR_BGR2GRAY);

        // Создание детекторов и описателей
        ORB orb = ORB.create();

        // Обнаружение ключевых точек и вычисление описаний
        MatOfKeyPoint keypoints1 = new MatOfKeyPoint();
        MatOfKeyPoint keypoints2 = new MatOfKeyPoint();
        orb.detect(grayImage1, keypoints1);
        orb.detect(grayImage2, keypoints2);

        Mat descriptors1 = new Mat();
        Mat descriptors2 = new Mat();
        orb.compute(grayImage1, keypoints1, descriptors1);
        orb.compute(grayImage2, keypoints2, descriptors2);

        // Сопоставление дескрипторов
        DescriptorMatcher matcher = DescriptorMatcher.create(DescriptorMatcher.BRUTEFORCE_HAMMING);
        MatOfDMatch matches = new MatOfDMatch();
        matcher.match(descriptors1, descriptors2, matches);

        // Выбор хороших сопоставлений
        List<DMatch> matchesList = matches.toList();
        double minDist = Double.MAX_VALUE;
        for (DMatch match : matchesList) {
            double dist = match.distance;
            if (dist < minDist) minDist = dist;
        }
        LinkedList<DMatch> goodMatchesList = new LinkedList<>();
        for (DMatch match : matchesList) {
            if (match.distance < Math.max(8 * minDist, 0.02)) {
                goodMatchesList.add(match);
            }
        }

        // Преобразуем списки ключевых точек в списки матчей
        LinkedList<Point> objList = new LinkedList<>();
        LinkedList<Point> sceneList = new LinkedList<>();
        List<KeyPoint> keypoints1List = keypoints1.toList();
        List<KeyPoint> keypoints2List = keypoints2.toList();
        for (DMatch match : goodMatchesList) {
            objList.addLast(keypoints1List.get(match.queryIdx).pt);
            sceneList.addLast(keypoints2List.get(match.trainIdx).pt);
        }

        MatOfPoint2f obj = new MatOfPoint2f();
        obj.fromList(objList);

        MatOfPoint2f scene = new MatOfPoint2f();
        scene.fromList(sceneList);

        // Вычисляем гомографию
        Mat H = Calib3d.findHomography(obj, scene, Calib3d.RANSAC, 2);

        // Сшивка
        Mat result = new Mat();
        Imgproc.warpPerspective(image1, result, H, new Size(image1.cols() + image2.cols(), image1.rows()));
        Mat half = new Mat(result, new Rect(0, 0, image2.cols(), image2.rows()));
        image2.copyTo(half);
        Imgcodecs.imwrite("Images\\result.jpg", result);
    }
}
