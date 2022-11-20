using System;
using System.Drawing;

namespace SoftRasterizer
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            var bmp = new Bitmap(1280, 720, Color.Cyan);
            bmp.SaveTo("./", "temp.bmp");
        }
    }
}
