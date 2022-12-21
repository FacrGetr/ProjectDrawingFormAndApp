using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class MyTriangle : Shape
    {
        //畫三角形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawTriangle(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

        public override void DrawMarker(IGraphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
