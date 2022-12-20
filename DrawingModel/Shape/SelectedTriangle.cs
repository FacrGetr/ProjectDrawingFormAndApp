using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class SelectedTriangle : Shape
    {
        MyTriangle _triangle;

        public SelectedTriangle(ref MyTriangle triangle)
        {
            _triangle = triangle;
        }

        public override void Draw(IGraphics graphics)
        {
            ////底線
            //graphics.DrawLine(  _triangle.Point1.X, _triangle.Point2.Y,
            //                    _triangle.Point2.X, _triangle.Point2.Y);
            ////中間の頂點
            //MyPoint middlePoint = new MyPoint(_triangle.Center.X, _triangle.Point1.Y);
            ////左邊
            //graphics.DrawLine(  _triangle.Point1.X, _triangle.Point2.Y, 
            //                    middlePoint.X, middlePoint.Y);
            ////右邊
            //graphics.DrawLine(  middlePoint.X, middlePoint.Y,
            //                    _triangle.Point2.X, _triangle.Point2.Y);
        }
    }
}
