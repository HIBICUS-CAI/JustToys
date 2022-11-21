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
        public static void DrawPoint(Bitmap dist, Vector3 pos, Color color)
        {
            var pointRadius = GraphicConfig.PointSize / 2.0f;
            var end = (int)MathF.Ceiling(pointRadius);
            var start = -end;
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
            endX = endX <= dist.Width - 1 ? endX : dist.Width - 1;
            endY = endY <= dist.Height - 1 ? endY : dist.Height - 1;

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
                    newColor.R = MathHelper.Lerp(
                        newColor.R, originColor.R, factor);
                    newColor.G = MathHelper.Lerp(
                        newColor.G, originColor.G, factor);
                    newColor.B = MathHelper.Lerp(
                        newColor.B, originColor.B, factor);
                    newColor.A = MathHelper.Lerp(
                        newColor.A, originColor.A, factor);

                    canvas[x, y].SetColor(newColor);
                }
            }
        }
    }
}
