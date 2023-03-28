using System;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

class Program
    {
    static void Main(string[] args)
        {
        // Replace with the target color you want to track
        var targetColor = new Scalar(0, 0, 255); // Red in BGR color space

        var windowName = "Target Tracker";
        Cv2.NamedWindow(windowName);

        while (true)
            {
            var screen = CaptureScreen();
            var targetFound = ProcessScreen(screen, targetColor);
            if (targetFound)
                {
                Console.WriteLine("Target found!");
                // Replace with the action you want to take when the target is found
                }

            Cv2.ImShow(windowName, screen);
            Cv2.WaitKey(1);
            }
        }

    static Bitmap CaptureScreen()
        {
        var screenBounds = Screen.PrimaryScreen.Bounds;
        var screen = new Bitmap(screenBounds.Width, screenBounds.Height);
        using (var g = Graphics.FromImage(screen))
            {
            g.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size, CopyPixelOperation.SourceCopy);
            }
        return screen;
        }

    static bool ProcessScreen(Bitmap screen, Scalar targetColor)
        {
        using (var mat = BitmapConverter.ToMat(screen))
            {
            var hsv = new Mat();
            Cv2.CvtColor(mat, hsv, ColorConversionCodes.BGR2HSV);

            var lower = new Scalar(targetColor.Val0 - 10, 100, 100);
            var upper = new Scalar(targetColor.Val0 + 10, 255, 255);
            var mask = new Mat();
            Cv2.InRange(hsv, lower, upper, mask);

            var contours = new Point[][] { };
            var hierarchy = new HierarchyIndex[] { };
            Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            if (contours.Length > 0)
                {
                Cv2.DrawContours(mat, contours, -1, new Scalar(0, 255, 0), 2);
                return true;
                }

            return false;
            }
        }
    }