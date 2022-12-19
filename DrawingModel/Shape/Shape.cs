﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public abstract class Shape
    {
        public MyPoint Point1
        {
            get; set;
        }
        public MyPoint Point2
        {
            get; set;
        }

        public MyPoint Center
        {
            get
            {
                double x = (Point1.X + Point2.X) / 2;
                double y = (Point1.Y + Point2.Y) / 2;
                return new MyPoint(x, y);
            }
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);
    }
}
