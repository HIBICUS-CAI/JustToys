using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace SoftRasterizer
{
    internal class TopologyPainter
    {
        public static void DrawPoint(Vector3 pos, Bitmap dist, Color color)
        {
            var pointRadius = GraphicConfig.PointSize / 2.0f;
            var start = (int)-pointRadius - 1;
            var end = (int)pointRadius + 1;
            var centerX = (int)pos.X;
            var centerY = (int)pos.Y;
            var centerV = new Vector2(centerX, centerY);
            var aroundV = new Vector2();

            var startX = centerX + start;
            var endX = centerX + end;
            var startY = centerY + start;
            var endY = centerY + end;
            startX = startX >= 0 ? startX : 0;
            startY = startY >= 0 ? startY : 0;
            endX = endX <= dist.Width ? endX : dist.Width;
            endY = endY <= dist.Height ? endY : dist.Height;

            var canvas = dist.GetCanvasAs2dCoord();

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    aroundV.X = x;
                    aroundV.Y = y;
                    var distance = Vector2.Distance(aroundV, centerV);
                    var factor = distance - pointRadius;
                    if (factor < -0.5f)
                    {
                        factor = 1.0f;
                    }
                    else if (factor > 0.5f)
                    {
                        factor = 0.0f;
                    }
                    else
                    {
                        factor = 0.5f - factor;
                    }
                    var originColor = canvas[x, y];
                    var newColor = new ColorRgba8(color);
                    newColor.R = (byte)(newColor.R * factor +
                        originColor.R * (1.0f - factor));
                    newColor.G = (byte)(newColor.G * factor +
                        originColor.G * (1.0f - factor));
                    newColor.B = (byte)(newColor.B * factor +
                        originColor.B * (1.0f - factor));
                    newColor.A = (byte)(newColor.A * factor +
                        originColor.A * (1.0f - factor));

                    canvas[x, y].SetColor(newColor);
                }
            }
        }
    }
}
