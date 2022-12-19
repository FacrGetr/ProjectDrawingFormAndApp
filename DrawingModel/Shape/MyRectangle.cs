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
            /*
            //下底
            graphics.DrawLine(Point1.X, Point2.Y, Point2.X, Point2.Y);
            //上底
            graphics.DrawLine(Point1.X, Point1.Y, Point2.X, Point1.Y);
            //左邊
            graphics.DrawLine(Point1.X, Point1.Y, Point1.X, Point2.Y);
            //右邊
            graphics.DrawLine(Point2.X, Point1.Y, Point2.X, Point2.Y);
            */
        }
    }

}
