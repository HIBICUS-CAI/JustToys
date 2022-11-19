using System;

namespace SoftRasterizer
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            const int ByteSize = byte.MaxValue + 1;
            byte[] buffer = new byte[ByteSize];
            for (int i = 0; i < ByteSize; i++)
            {
                buffer[i] = (byte)i;
            }

            var bmp = new Bitmap(800, 800);
            for (int i = 0; i < bmp.Color.Length; i++)
            {
                var baseColor = new ColorRgba8(255, 0, 0, 127);
                bmp.Color[i].SetColor(baseColor);
            }
            var color2d = bmp.GetColorAs2dCoord();
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (x == y)
                    {
                        color2d[x, y].SetColor(255, 255, 255, 255);
                    }
                }
            }
            bmp.SaveTo("./", "temp.bmp");
        }
    }
}
