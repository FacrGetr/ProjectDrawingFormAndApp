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
            /*
            //底線
            graphics.DrawLine(Point1.X, Point2.Y, Point2.X, Point2.Y);
            //中間の頂點
            Point middlePoint = new Point((Point1.X + Point2.X) / 2, Point1.Y);
            //左邊
            graphics.DrawLine(Point1.X, Point2.Y, middlePoint.X, middlePoint.Y);
            //右邊
            graphics.DrawLine(middlePoint.X, middlePoint.Y, Point2.X, Point2.Y);
            */
        }
    }
}
