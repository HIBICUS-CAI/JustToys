using System;
using System.Collections.Generic;
using System.Text;

namespace SoftRasterizer
{
    public class MathHelper
    {
        public static float Lerp(float a, float b, float t)
        {
            return b + (a - b) * t;
        }

        public static byte Lerp(byte a, byte b, float t)
        {
            return (byte)(b + (a - b) * t);
        }

        public static int Lerp(int a, int b, float t)
        {
            return (int)(b + (a - b) * t);
        }
    }
}
