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
            const string EXCEPTION_MESSAGE = "無效 DrawingMode";
            switch (mode)
            {
                case DrawingMode.Rectangle:
                    return new MyRectangle();
                case DrawingMode.Triangle:
                    return new MyTriangle();
                case DrawingMode.Line:
                    return new MyLine();
            }
            throw new Exception(EXCEPTION_MESSAGE);
        }
    }
}
