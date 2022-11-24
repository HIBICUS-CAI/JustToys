using System;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

            var ra = new Random();
            for (int i = 0; i < 1000; i++)
            {
                pos.X = ra.Next(0, 1279);
                pos.Y = ra.Next(0, 719);
                var r = ra.Next(0, 255);
                var g = ra.Next(0, 255);
                var b = ra.Next(0, 255);
                col = Color.FromArgb(255, r, g, b);
                TopologyPainter.DrawPoint(bmp, pos, col);
            }

            bmp.SaveTo("./", "temp.bmp");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("powershell.exe", "./temp.bmp");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("bash");
            }
        }
    }
}
