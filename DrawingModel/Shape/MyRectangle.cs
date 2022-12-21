using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class MyRectangle : Shape
    {
        //畫方形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

        public override void DrawMarker(IGraphics graphics)
        {
            graphics.DrawRectangleMarker(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

        public override string GetTypeName()
        {
            return "Rectangle";
        }
    }

}
