using System;
using System.Drawing;
using static laba3.HelperClass;

namespace laba3
{
    class SobelFilter
    {
        private int limit;
        private int[,] _maskX = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        private int[,] _maskY = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        public SobelFilter(int limit)
        {
            this.limit = limit;
        }

        public Bitmap Filter(Bitmap sourcePicture)
        {
            Bitmap outputPicture = new Bitmap(sourcePicture);
            int height = sourcePicture.Height;
            int width = sourcePicture.Width;

            for (int i = 1; i < width; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    int newXR = 0;
                    int newYR = 0;
                    int newXG = 0;
                    int newYG = 0;
                    int newXB = 0; 
                    int newYB = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            Color sourcePixel = sourcePicture.GetPixel(CheckRange(i + hw, width - 1), CheckRange(j + wi, height - 1));
                            newXR += _maskX[wi + 1, hw + 1] * sourcePixel.R;
                            newYR += _maskY[wi + 1, hw + 1] * sourcePixel.R;

                            newXG += _maskX[wi + 1, hw + 1] * sourcePixel.G;
                            newYG += _maskY[wi + 1, hw + 1] * sourcePixel.G;

                            newXB += _maskX[wi + 1, hw + 1] * sourcePixel.B;
                            newYB += _maskY[wi + 1, hw + 1] * sourcePixel.B;
                        }
                    }
                    if (Math.Sqrt(newXR * newXR + newYR * newYR) < limit ||
                        Math.Sqrt(newXG * newXG + newYG * newYG) < limit ||
                        Math.Sqrt(newXB * newXB + newYB * newYB) < limit)
                    {
                        outputPicture.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        outputPicture.SetPixel(i, j, Color.White);
                    }
                }
            }
            return outputPicture;
        }


    }
}
