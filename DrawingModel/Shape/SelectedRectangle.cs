using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class SelectedRectangle : Shape
    {
        MyRectangle _rectangle;

        public SelectedRectangle(MyRectangle rectangle)
        {
            _rectangle = rectangle;
        }

        public override void Draw(IGraphics graphics)
        {
            //下底
            graphics.DrawLine(_rectangle.Point1.X, _rectangle.Point2.Y,
                                _rectangle.Point2.X, _rectangle.Point2.Y);
            //上底
            graphics.DrawLine(_rectangle.Point1.X, _rectangle.Point1.Y,
                                _rectangle.Point2.X, _rectangle.Point1.Y);
            //左邊
            graphics.DrawLine(_rectangle.Point1.X, _rectangle.Point1.Y,
                                _rectangle.Point1.X, _rectangle.Point2.Y);
            //右邊
            graphics.DrawLine(_rectangle.Point2.X, _rectangle.Point1.Y,
                                _rectangle.Point2.X, _rectangle.Point2.Y);
        }
    }
}
