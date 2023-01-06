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

        //哩叫蝦米名（你叫什麼名）
        abstract public string GetTypeName();

        //資訊字卡
        public string Info
        {
            get
            {
                return GetTypeName() + '(' + Math.Round(Point1.X) + ", " +
                                            Math.Round(Point1.Y) + ", " +
                                            Math.Round(Point2.X) + ", " +
                                            Math.Round(Point2.Y) + ')';
            }
        }

        Shape _shape1;
        Shape _shape2;

        //把Point1跟這個shape的中心點做binding
        public void ConnectPoint1ToShape(Shape shape)
        {
            _shape1 = shape;
            Point1 = shape.Center;
            _shape1._shapeChanged += HandleShapeChanged;
        }

        //把Point2跟這個shape的中心點做binding
        public void ConnectPoint2ToShape(Shape shape)
        {
            _shape2 = shape;
            Point2 = shape.Center;
            _shape2._shapeChanged += HandleShapeChanged;
        }

        //Observer，更新座標
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

        //被點到了
        public bool CatchedBy(MyPoint point)
        {
            MyPoint topLeft = new MyPoint(GetMin(Point1.X, Point2.X),
                                        GetMin(Point1.Y, Point2.Y));
            MyPoint bottomRight = new MyPoint(GetMax(Point1.X, Point2.X),
                                            GetMax(Point1.Y, Point2.Y));

            return topLeft.X < point.X && point.X < bottomRight.X &&
                    topLeft.Y < point.Y && point.Y < bottomRight.Y;
        }

        //找誰大
        double GetMax(double firstNumber, double secondNumber)
        {
            if (firstNumber > secondNumber)
            {
                return firstNumber;
            }
            return secondNumber;
        }

        //找誰小
        double GetMin(double firstNumber, double secondNumber)
        {
            if (firstNumber < secondNumber)
            {
                return firstNumber;
            }
            return secondNumber;
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);

        //畫被選到時的框，virtual function
        abstract public void DrawMarker(IGraphics graphics);

        //Observer
        void NotifyShapeChanged()
        {
            if (_shapeChanged != null)
            {
                _shapeChanged();
            }
        }
    }
}
