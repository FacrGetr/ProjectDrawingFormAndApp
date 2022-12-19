using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class Line : Shape
    {
        Shape _shape1;
        Shape _shape2;

        public Line(ref Shape shape1, ref Shape shape2)
        {
            _shape1 = shape1;
            _shape2 = shape2;
        }

        public override void Draw(IGraphics graphics)
        {
            MyPoint pointStart = _shape1.Center;
            MyPoint pointEnd = _shape2.Center;
            MyPoint pointCornor1 = new MyPoint((pointStart.X + pointEnd.X / 2), pointStart.Y);
            MyPoint pointCornor2 = new MyPoint((pointStart.X + pointEnd.X / 2), pointEnd.Y);
        }
    }

}
