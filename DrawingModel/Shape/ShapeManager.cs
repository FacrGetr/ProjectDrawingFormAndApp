﻿using System;
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

        //新增此形狀
        public void Add(Shape shape)
        {
            _shapeList.Reverse();
            _shapeList.Add(shape);
            _shapeList.Reverse();
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
            {
                if (aShape is MyLine)
                    aShape.Draw(graphics);
            }
            foreach (Shape aShape in _shapeList)
            {
                if (!(aShape is MyLine))
                    aShape.Draw(graphics);
            }
            //if(_nowSelectShape != null)
            //    _nowSelectShape.Draw(graphics);
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

        public Shape SelectShape(MyPoint pointer)
        {
            foreach (Shape aShape in _shapeList)
            {
                if (aShape.CatchedBy(pointer))
                {
                    return aShape;
                }
            }
            return null;
        }
    }
}
