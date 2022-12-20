using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ShapeFactory
    {
        //根據要求 new 一個形狀回去
        public Shape CreateNewShape(DrawingMode mode)
        {
            switch (mode)
            {
                case DrawingMode.Rectangle:
                    return new MyRectangle();
                case DrawingMode.Triangle:
                    return new MyTriangle();
            }
            throw new Exception("無效 DrawingMode");
        }

        public Line CreateNewLine(ref Shape shape1, ref Shape shape2)
        {
            return new Line(ref shape1, ref shape2);
        }
    }
}
