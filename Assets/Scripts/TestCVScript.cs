using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class TestCVScript : MonoBehaviour
{
    public Sprite easelSprite;
    public Sprite compareSprite;
    private string path = "C:/Users/Paul/Desktop/";
    private Mat kernel_erode;

    void Start()
    {
        int thickness = 30;
        kernel_erode = new Mat(thickness, thickness, CvType.CV_8UC1);
    }

    void Update()
    {
        if (Input.GetKeyUp("p"))
        {
            CompareTwoImages();
        }
    }

    void CompareTwoImages()
    {
        print("Ratio: " + imageToOriginal(easelSprite.texture, compareSprite.texture));
    }

    float imageToOriginal(Texture2D userTexture, Texture2D originalTexture)
    {
        Mat originalMat = TextureToBWMat(originalTexture);
        Mat userMat = TextureToBWMat(userTexture);
        Imgproc.threshold(userMat, userMat, 70, 255, Imgproc.THRESH_BINARY);

        // Crop images to original
        Mat invertedOg = new Mat();
        Core.bitwise_not(originalMat, invertedOg);
        (Point start, Point end) = CropMatBounds(invertedOg);
        OpenCVForUnity.CoreModule.Rect roi = new OpenCVForUnity.CoreModule.Rect(end, start);
        originalMat = new Mat(originalMat, roi);
        userMat = new Mat(userMat, roi);
        invertedOg = new Mat(invertedOg, roi);

        Imgproc.erode(originalMat, originalMat, kernel_erode);
        Imgproc.erode(userMat, userMat, kernel_erode);
        Imgproc.dilate(invertedOg, invertedOg, kernel_erode);

        Mat differencesMat = new Mat(userMat.rows(), userMat.cols(), CvType.CV_8UC4);
        Core.subtract(userMat, originalMat, differencesMat);

        Mat originalWhite = new Mat();
        Mat croppedDiffWhite = new Mat();
        Core.findNonZero(invertedOg, originalWhite);
        Core.findNonZero(differencesMat, croppedDiffWhite);

        // SaveMatToFile(invertedOg, "original");
        // SaveMatToFile(differencesMat, "diffs");

        float ratio = (float)croppedDiffWhite.rows() / (float)originalWhite.rows();
        return 1f - Mathf.Min(ratio, 1f);
    }

    (Point, Point) CropMatBounds(Mat mat)
    {
        int xStart = 0;
        int xEnd = 0;
        int yStart = 0;
        int yEnd = 0;

        for (int x = 0; x < mat.cols(); x++)
        {
            bool found = false;
            for (int y = 0; y < mat.rows(); y++)
            {
                double pixel = mat.get(y, x)[0];
                if (pixel > 0)
                {
                    xStart = x;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }

        for (int y = 0; y < mat.rows(); y++)
        {
            bool found = false;
            for (int x = 0; x < mat.cols(); x++)
            {
                double pixel = mat.get(y, x)[0];
                if (pixel > 0)
                {
                    yStart = y;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }

        for (int x = mat.cols() - 1; x >= 0; x--)
        {
            bool found = false;
            for (int y = mat.rows() - 1; y >= 0; y--)
            {
                double pixel = mat.get(y, x)[0];
                if (pixel > 0)
                {
                    xEnd = x;
                    found = true;
                    break;
                }

            }
            if (found)
            {
                break;
            }
        }

        for (int y = mat.rows() - 1; y >= 0; y--)
        {
            bool found = false;
            for (int x = mat.cols() - 1; x >= 0; x--)
            {
                double pixel = mat.get(y, x)[0];
                if (pixel > 0)
                {
                    yEnd = y;
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }

        return (new Point(xStart, yStart), new Point(xEnd, yEnd));
    }

    Mat TextureToBWMat(Texture2D texture)
    {
        Mat output = new Mat(texture.height, texture.width, CvType.CV_8UC4);
        Utils.texture2DToMat(texture, output, false);
        Imgproc.cvtColor(output, output, Imgproc.COLOR_BGR2GRAY);
        return output;
    }

    void SaveMatToFile(Mat mat, string fileName = "test")
    {
        Texture2D outputTexture = new Texture2D(mat.cols(), mat.rows());
        Utils.matToTexture2D(mat, outputTexture, false);
        SaveTextureToFile(outputTexture, fileName);
    }

    void SaveTextureToFile(Texture2D texture, string fileName = "test")
    {
        string file = fileName + ".png";
        File.WriteAllBytes(path + file, texture.EncodeToPNG());
        print("Saved " + file);
    }
}
