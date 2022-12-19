using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ShapeManager : IEnumerable
    {
        List<Shape> _shapeList = new List<Shape>();

        public bool NotEmpty
        {
            get
            {
                return _shapeList.Count() > 0;
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
            _shapeList.Add(shape);
        }

        //將形狀清空
        public void Clear()
        {
            _shapeList.Clear();
        }

        //畫圖
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapeList)
                aShape.Draw(graphics);
        }

        //移除末端形狀
        public void PopShape()
        {
            _shapeList.RemoveAt(_shapeList.Count - 1);
        }

        public IEnumerator GetEnumerator()
        {
            return _shapeList.GetEnumerator();
        }
    }
}
