using System;
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

        abstract public MyPoint Center
        {
            get;
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);
    }
}
