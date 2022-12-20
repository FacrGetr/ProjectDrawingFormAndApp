﻿using System;
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

        public Shape CreateNewSelectedShape(Shape shape)
        {
            if (shape is MyRectangle)
            {
                return new SelectedRectangle((MyRectangle)shape);
            } 
            else if (shape is MyTriangle)
            {

            }
            throw new Exception("無效 Shape");
        }
    }
}
