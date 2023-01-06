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

        //被選到時怎麼畫
        public override void DrawMarker(IGraphics graphics)
        {
            graphics.DrawRectangleMarker(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }

        //哩叫蝦米名(你叫甚麼名)
        public override string GetTypeName()
        {
            return nameof(MyRectangle);
        }
    }

}
