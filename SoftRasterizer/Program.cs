using System;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;

namespace SoftRasterizer
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            var bmp = new Bitmap(1280, 720, Color.Black);

            var pos = new Vector3(640.0f, 360.0f, 5.0f);
            var col = Color.FromArgb(255, 255, 255, 255);
            TopologyPainter.DrawPoint(bmp, pos, col);

            bmp.SaveTo("./", "temp.bmp");

            Process.Start("powershell.exe", "./temp.bmp");
        }
    }
}
