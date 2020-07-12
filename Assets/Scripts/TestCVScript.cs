using System.IO;
using System.Linq;
using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.ImgprocModule;

public class TestCVScript : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;
    public GameObject playAgain;
    public GameObject nextLevel;
    public GameObject exit;
    public static float score;
    public GameObject scoreObj;
    public GameObject scoreText;
    public static bool gameEnded = false;
    public Sprite easelSprite;
    public Sprite compareSprite;
    private string path = "C:/Users/Paul/Desktop/";
    public Color[] colors;

    void Update()
    {
        if (Input.GetKeyUp("p") || gameEnded)
        {
            Destroy(GameObject.FindGameObjectWithTag("Ref"));
            CompareTwoImages();
            gameEnded = false;
        }
    }

    void CompareTwoImages()
    {
        int multiplier = 50;
        float[] scores = colors.Select(
            color =>
            {
                float score = compareImageToOriginal(color);
                print("Score: " + score);
                multiplier -= 10;
                return score * multiplier;
            }
        ).ToArray();
        float finalScore = scores.Sum();
        score = finalScore;
        if (finalScore >= 60f)
        {
            nextLevel.SetActive(true);
            win.SetActive(true);
        }
        else
        {
            lose.SetActive(true);
            playAgain.SetActive(true);
        }
        scoreObj.SetActive(true);
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Your score was " + finalScore.ToString("0.##");
        exit.SetActive(true);
        print("Final score: " + finalScore);
    }

    float compareImageToOriginal(Color color)
    {
        Texture2D userTexture = easelSprite.texture;
        Texture2D originalTexture = compareSprite.texture;
        Scalar colorScalar = ColorToRGBScalar(color);
        Mat originalMat = TextureToMat(originalTexture);
        Mat userMat = TextureToMat(userTexture);

        Mat ogMasked = new Mat();
        Core.inRange(originalMat, colorScalar, colorScalar, ogMasked);

        Mat userMasked = new Mat();
        Core.inRange(userMat, colorScalar, colorScalar, userMasked);

        Imgproc.threshold(userMasked, userMasked, 50, 255, Imgproc.THRESH_BINARY_INV);
        Imgproc.threshold(ogMasked, ogMasked, 50, 255, Imgproc.THRESH_BINARY_INV);

        // Crop images to original
        Mat invertedOg = new Mat();
        Core.bitwise_not(ogMasked, invertedOg);

        (Point start, Point end) = CropMatBounds(invertedOg);
        OpenCVForUnity.CoreModule.Rect roi = new OpenCVForUnity.CoreModule.Rect(end, start);
        ogMasked = new Mat(ogMasked, roi);
        userMasked = new Mat(userMasked, roi);
        invertedOg = new Mat(invertedOg, roi);

        int thickness = color == Color.black ? 20 : 10;
        Mat kernel_erode = new Mat(thickness, thickness, CvType.CV_8UC1);
        Imgproc.erode(ogMasked, ogMasked, kernel_erode);
        Imgproc.erode(userMasked, userMasked, kernel_erode);
        Imgproc.dilate(invertedOg, invertedOg, kernel_erode);

        Mat differencesMat = new Mat(userMasked.rows(), userMasked.cols(), CvType.CV_8UC4);
        Core.subtract(userMasked, ogMasked, differencesMat);

        Mat originalWhite = new Mat();
        Mat croppedDiffWhite = new Mat();
        Core.findNonZero(invertedOg, originalWhite);
        Core.findNonZero(differencesMat, croppedDiffWhite);

        // SaveMatToFile(invertedOg, "inverted" + (color == Color.black ? "black" : "red"));
        // SaveMatToFile(differencesMat, "diffs" + (color == Color.black ? "black" : "red"));

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

    Scalar ColorToRGBScalar(Color color)
    {
        return new Scalar(color.r * 255, color.g * 255, color.b * 255, 255);
    }

    Mat TextureToMat(Texture2D texture)
    {
        Mat output = new Mat(texture.height, texture.width, CvType.CV_8UC4);
        Utils.texture2DToMat(texture, output, false);
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
