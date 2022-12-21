using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DrawingModel
{
    public abstract class Shape
    {
        public event ModelChangedEventHandler _shapeChanged;
        public delegate void ModelChangedEventHandler();

        MyPoint _point1;

        public MyPoint Point1
        {
            get
            {
                return _point1;
            }
            set
            {
                _point1 = value;
                NotifyShapeChanged();
            }
        }

        MyPoint _point2;

        public MyPoint Point2
        {
            get
            {
                return _point2;
            }
            set
            {
                _point2 = value;
                NotifyShapeChanged();
            }
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

        public bool CatchedBy(MyPoint point)
        {
            return (Point1.X < point.X && point.X < Point2.X &&
                    Point1.Y < point.Y && point.Y < Point2.Y);
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);

        void NotifyShapeChanged()
        {
            _shapeChanged?.Invoke();
        }
    }
}
