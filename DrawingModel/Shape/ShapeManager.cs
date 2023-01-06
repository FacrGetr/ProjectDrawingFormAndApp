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

        Shape _nowSelectShape;

        Shape NowSelectShape
        {
            get
            {
                return _nowSelectShape;
            }
            set
            {
                _nowSelectShape = value;
                _shapeInfo.SetShape(_nowSelectShape);
            }
        }

        ShapeInfo _shapeInfo = new ShapeInfo();

        public string SelectShapeInfo
        {
            get
            {
                return _shapeInfo.Info;
            }
        }

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
            _shapeList.Add(shape);
        }

        //將形狀清空
        public void Clear()
        {
            ClearSelection();
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
            if(NowSelectShape != null)
                NowSelectShape.DrawMarker(graphics);
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

        public void SelectTargetShape(MyPoint pointer)
        {
            _shapeList.Reverse();
            foreach (Shape aShape in _shapeList)
            {
                if (aShape.CatchedBy(pointer))
                {
                    NowSelectShape = aShape;
                    break;
                }
                NowSelectShape = null;
            }
            _shapeList.Reverse();
        }

        public Shape CatchShape(MyPoint pointer)
        {
            //為麼這不能用@@
            //return _nowSelectShape;
            _shapeList.Reverse();
            foreach (Shape aShape in _shapeList)
            {
                if (aShape.CatchedBy(pointer))
                {
                    _shapeList.Reverse();
                    return aShape;
                }
            }
            _shapeList.Reverse();
            return null;
        }

        public void ClearSelection()
        {
            NowSelectShape = null;
        }
    }
}
