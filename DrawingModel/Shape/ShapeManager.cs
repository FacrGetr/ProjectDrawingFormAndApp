using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                _shapeInfo.SetSelectedShape(_nowSelectShape);
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
        public void ClearAll()
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
            if (NowSelectShape != null)
                NowSelectShape.DrawMarker(graphics);
        }

        //移除末端形狀
        public void PopShape()
        {
            _shapeList.RemoveAt(_shapeList.Count - 1);
        }

        //給 Foreach 用的
        public IEnumerator GetEnumerator()
        {
            return _shapeList.GetEnumerator();
        }

        //Point 模式點選圖形
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

        //判定有沒有點到東西
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

        //清除選擇
        public void ClearSelection()
        {
            NowSelectShape = null;
        }

        //存檔，寫檔
        public void Save(string saveFileName)
        {
            StreamWriter streamWriter = new StreamWriter(saveFileName);
            foreach (Shape aShape in _shapeList)
            {
                streamWriter.WriteLine(aShape.Info);
            }
            streamWriter.Close();
        }

        //讀檔
        public void Load()
        {

        }
    }
}
