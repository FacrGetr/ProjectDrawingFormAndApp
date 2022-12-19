using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    struct MyPoint
    {
        public double X
        {
            get; set;
        }

        public double Y
        {
            get; set;
        }

        public MyPoint(double x1, double y1)
        {
            X = x1;
            Y = y1;
        }
    }
}
