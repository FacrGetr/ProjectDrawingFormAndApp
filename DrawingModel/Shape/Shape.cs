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

        Shape _shape1;
        Shape _shape2;

        public void ConnectPoint1ToShape(Shape shape)
        {
            _shape1 = shape;
            Point1 = shape.Center;
            _shape1._shapeChanged += HandleShapeChanged;
        }

        public void ConnectPoint2ToShape(Shape shape)
        {
            _shape2 = shape;
            Point2 = shape.Center;
            _shape2._shapeChanged += HandleShapeChanged;
        }

        void HandleShapeChanged()
        {
            Point1 = _shape1.Center;
            Point2 = _shape2.Center;
        }

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
            MyPoint topLeft = new MyPoint(GetMin(Point1.X, Point2.X),
                                        GetMin(Point1.Y, Point2.Y));
            MyPoint bottonRight = new MyPoint(GetMax(Point1.X, Point2.X),
                                            GetMax(Point1.Y, Point2.Y));

            return (topLeft.X < point.X && point.X < bottonRight.X &&
                    topLeft.Y < point.Y && point.Y < bottonRight.Y);
        }

        double GetMax(double num1, double num2)
        {
            if (num1 > num2) return num1;
            return num2;
        }

        double GetMin(double num1, double num2)
        {
            if (num1 < num2) return num1;
            return num2;
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);

        abstract public void DrawMarker(IGraphics graphics);

        void NotifyShapeChanged()
        {
            _shapeChanged?.Invoke();
        }
    }
}
