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

            var bmp = new Bitmap(1600, 1600);
            for (int i = 0; i < bmp.Color.Length; i++)
            {
                bmp.Color[i].R = 255;
                bmp.Color[i].G = 0;
                bmp.Color[i].B = 0;
                bmp.Color[i].A = 127;
            }
            var color2d = bmp.GetColorAs2dCoord();
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (x == y)
                    {
                        color2d[x, y].R = 255;
                        color2d[x, y].G = 255;
                        color2d[x, y].B = 255;
                        color2d[x, y].A = 255;
                    }
                }
            }
            bmp.SaveTo("./", "temp.bmp");
        }
    }
}
