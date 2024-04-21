from PIL import Image
from PIL import ImageEnhance


def main():
    with Image.open("Images\\Screen.png") as image:
        enhancer = ImageEnhance.Brightness(image)
        image.show()
        im_output = enhancer.enhance(1.5)
        im_output.show()


if __name__ == '__main__':
    main()
