using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class MyLine : Shape
    {
        //畫圖
        public override void Draw(IGraphics graphics)
        {
            MyPoint pointCornor1 = new MyPoint(Point1.X + (Point2.X - Point1.X) / 2, Point1.Y);
            MyPoint pointCornor2 = new MyPoint(Point1.X + (Point2.X - Point1.X) / 2, Point2.Y);

            graphics.DrawLine(Point1.X, Point1.Y,
                                pointCornor1.X, pointCornor1.Y);
            graphics.DrawLine(pointCornor1.X, pointCornor1.Y,
                                pointCornor2.X, pointCornor2.Y);
            graphics.DrawLine(pointCornor2.X, pointCornor2.Y,
                                Point2.X, Point2.Y);
        }

        //被選到時怎麼畫（就單純再畫一次自己）
        public override void DrawMarker(IGraphics graphics)
        {
            Draw(graphics);
        }

        //哩叫蝦米名(你叫甚麼名)
        public override string GetTypeName()
        {
            return nameof(MyLine);
        }
    }

}
