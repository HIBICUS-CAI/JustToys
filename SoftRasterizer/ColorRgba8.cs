using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SoftRasterizer
{
    internal class ColorRgba8
    {
        byte m_r;
        byte m_g;
        byte m_b;
        byte m_a;

        public byte R { get => m_r; set => m_r = value; }
        public byte G { get => m_g; set => m_g = value; }
        public byte B { get => m_b; set => m_b = value; }
        public byte A { get => m_a; set => m_a = value; }

        public ColorRgba8(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorRgba8(ColorRgba8 color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public ColorRgba8(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public static Color AsBuildInColor(ColorRgba8 color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static ColorRgba8 FromBuildInColor(Color color)
        {
            return new ColorRgba8(color);
        }

        public void SetColor(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public void SetColor(ColorRgba8 color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public void SetColor(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }
    }
}
