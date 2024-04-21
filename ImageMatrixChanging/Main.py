import cv2
import numpy as np


def main():
    path = "Images\\Screen.png"
    image = cv2.imread(path)

    perspective_matrix = np.array([[1.1, 0.35, 0],
                                    [0.2, 1.1, 0],
                                    [0.00075, 0.0005, 1]], dtype=np.float32)

    result = cv2.warpPerspective(image, perspective_matrix, (image.shape[1], image.shape[0]))

    cv2.imshow("Original", image)
    cv2.imshow("Transformed", result)
    cv2.waitKey(0)
    cv2.destroyAllWindows()


if __name__ == "__main__":
    main()
