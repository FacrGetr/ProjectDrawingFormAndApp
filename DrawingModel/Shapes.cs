using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    abstract class Shape
    {
        public MyPoint Point1
        {
            get; set;
        }
        public MyPoint Point2
        {
            get; set;
        }

        //畫圖，virtual function
        abstract public void Draw(IGraphics graphics);
    }

    class Shapes
    {
        List<Shape> _shapes = new List<Shape>();

        public bool NotEmpty
        {
            get
            {
                return _shapes.Count() > 0;
            }
        }

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
            return new Line();
        }

        //新增此形狀
        public void Add(Shape shape)
        {
            _shapes.Add(shape);
        }

        //將形狀清空
        public void Clear()
        {
            _shapes.Clear();
        }

        //畫圖
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapes)
                aShape.Draw(graphics);
        }
    }

    class Line : Shape
    {
        //純畫一條線
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(Point1.X, Point1.Y, Point2.X, Point2.Y);
        }
    }

    class MyRectangle : Shape
    {
        //畫方形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(Point1.X, Point1.Y, Point2.X, Point2.Y);
            /*
            //下底
            graphics.DrawLine(Point1.X, Point2.Y, Point2.X, Point2.Y);
            //上底
            graphics.DrawLine(Point1.X, Point1.Y, Point2.X, Point1.Y);
            //左邊
            graphics.DrawLine(Point1.X, Point1.Y, Point1.X, Point2.Y);
            //右邊
            graphics.DrawLine(Point2.X, Point1.Y, Point2.X, Point2.Y);
            */
        }
    }

    class MyTriangle : Shape
    {
        //畫三角形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawTriangle(Point1.X, Point1.Y, Point2.X, Point2.Y);
            /*
            //底線
            graphics.DrawLine(Point1.X, Point2.Y, Point2.X, Point2.Y);
            //中間の頂點
            Point middlePoint = new Point((Point1.X + Point2.X) / 2, Point1.Y);
            //左邊
            graphics.DrawLine(Point1.X, Point2.Y, middlePoint.X, middlePoint.Y);
            //右邊
            graphics.DrawLine(middlePoint.X, middlePoint.Y, Point2.X, Point2.Y);
            */
        }
    }
}
