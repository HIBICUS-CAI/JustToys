using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace SoftRasterizer
{
    internal class Bitmap
    {
        int m_width;
        int m_height;
        ColorRgba8[] m_canvas;

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Canvas = new ColorRgba8[Width * Height];
            for (int i = 0; i < Canvas.Length; i++)
            {
                Canvas[i] = new ColorRgba8(0, 0, 0, 0);
            }
        }

        public Bitmap(int width, int height, byte r, byte g, byte b, byte a)
        {
            Width = width;
            Height = height;
            Canvas = new ColorRgba8[Width * Height];
            for (int i = 0; i < Canvas.Length; i++)
            {
                Canvas[i] = new ColorRgba8(r, g, b, a);
            }
        }

        public Bitmap(int width, int height, ColorRgba8 baseColor)
        {
            Width = width;
            Height = height;
            Canvas = new ColorRgba8[Width * Height];
            for (int i = 0; i < Canvas.Length; i++)
            {
                Canvas[i] = new ColorRgba8(baseColor);
            }
        }

        public Bitmap(int width, int height, Color baseColor)
        {
            Width = width;
            Height = height;
            Canvas = new ColorRgba8[Width * Height];
            for (int i = 0; i < Canvas.Length; i++)
            {
                Canvas[i] = new ColorRgba8(baseColor);
            }
        }

        public void SaveTo(string filePath, string fileName)
        {
            var finalPath = filePath + fileName;
            var stream = File.Open(finalPath, FileMode.OpenOrCreate);

            stream.WriteByte((byte)'B');
            stream.WriteByte((byte)'M');

            int fileSize = 14 + 12 + Height * ((Width * 3 + 3) / 4) * 4;
            byte[] fileSizeOrReservedBytes = BitConverter.GetBytes(fileSize);
            stream.Write(fileSizeOrReservedBytes, 0, 4);
            fileSize = 0;
            fileSizeOrReservedBytes = BitConverter.GetBytes(fileSize);
            stream.Write(fileSizeOrReservedBytes, 0, 4);
            fileSize = 14 + 12;
            fileSizeOrReservedBytes = BitConverter.GetBytes(fileSize);
            stream.Write(fileSizeOrReservedBytes, 0, 4);

            int coreHeadSize = 12;
            short width = (short)Width;
            short height = (short)Height;
            short planes = 1;
            short bits = 24;

            stream.Write(BitConverter.GetBytes(coreHeadSize), 0, 4);
            stream.Write(BitConverter.GetBytes(width), 0, 2);
            stream.Write(BitConverter.GetBytes(height), 0, 2);
            stream.Write(BitConverter.GetBytes(planes), 0, 2);
            stream.Write(BitConverter.GetBytes(bits), 0, 2);

            var byteBuffer = new byte[Width * Height * 3];
            for (int i = 0; i < Canvas.Length; i++)
            {
                float alpha = Canvas[i].A / 255.0f;
                byte r8 = (byte)((float)Canvas[i].R * alpha);
                byte g8 = (byte)((float)Canvas[i].G * alpha);
                byte b8 = (byte)((float)Canvas[i].B * alpha);

                byteBuffer[i * 3 + 0] = b8;
                byteBuffer[i * 3 + 1] = g8;
                byteBuffer[i * 3 + 2] = r8;
            }

            if (Width * 3 % 4 != 0)
            {
                var pad = new byte[4 - Width * 3 % 4];
                for (int i = 0; i < pad.Length; i++)
                {
                    pad[i] = 0;
                }
                for (int i = Height - 1; i >= 0; i--)
                {
                    stream.Write(byteBuffer, Width * 3 * i, Width * 3);
                    stream.Write(pad, 0, pad.Length);
                }
            }
            else
            {
                for (int i = Height - 1; i >= 0; i--)
                {
                    stream.Write(byteBuffer, Width * 3 * i, Width * 3);
                }
            }

            stream.Close();
        }

        public ColorRgba8 GetColorAt(int x, int y)
        {
            return Canvas[y * Width + x];
        }

        public ColorRgba8[,] GetCanvasAs2dCoord()
        {
            var canvas2DCoord = new ColorRgba8[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    canvas2DCoord[x, y] = Canvas[y * Width + x];
                }
            }

            return canvas2DCoord;
        }

        public bool MapCanvasBy2dCoord(ColorRgba8[,] data)
        {
            if (data.GetLength(0) != Width || data.GetLength(1) != Height)
            {
                return false;
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Canvas[y * Width + x] = data[x, y];
                }
            }
            return true;
        }

        public int Width { get => m_width; set => m_width = value; }
        public int Height { get => m_height; set => m_height = value; }
        public ColorRgba8[] Canvas { get => m_canvas; set => m_canvas = value; }
    }
}
