using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public interface IGraphics
    {
        //畫面淨空
        void ClearAll();

        //畫線
        void DrawLine(double x1, double y1, double x2, double y2);

        //畫方
        void DrawRectangle(double x1, double y1, double x2, double y2);

        //畫三角
        void DrawTriangle(double x1, double y1, double x2, double y2);
    }
}