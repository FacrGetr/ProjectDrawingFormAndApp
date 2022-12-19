using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Line : Shape
    {
        //純畫一條線
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }
    }

}
