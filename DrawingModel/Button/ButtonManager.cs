using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ButtonManager : IEnumerable
    {
        MyButton _clear = new MyButton(DrawingMode.Line, true);
        MyButton _rectangle = new MyButton(DrawingMode.Rectangle, true);
        MyButton _triangle = new MyButton(DrawingMode.Triangle, true);
        MyButton _line = new MyButton(DrawingMode.Line, true);
        List<MyButton> _buttonList = new List<MyButton>();

        public ButtonManager()
        {
            _buttonList.Add(_clear);
            _buttonList.Add(_rectangle);
            _buttonList.Add(_triangle);
            _buttonList.Add(_line);
        }

        public bool IsRectangleEnable
        {
            get
            {
                return _rectangle.IsEnable;
            }
        }

        public bool IsTriangleEnable
        {
            get
            {
                return _triangle.IsEnable;
            }
        }

        public bool IsClearEnable
        {
            get
            {
                return _clear.IsEnable;
            }
        }

        public bool IsLineEnable
        {
            get
            {
                return _line.IsEnable;
            }
        }

        //點擊畫圖(Rectangle 或 Triangle Button 時)按鈕時須將另一個按鈕設定為
        //enable，且將本身按 鈕設定為 disable。
        public void SetMode(DrawingMode mode)
        {
            foreach (MyButton button in _buttonList)
            {
                if (button.Mode == mode)
                {
                    button.IsEnable = false;
                } 
                else
                {
                    button.IsEnable = true;
                }
            }
        }

        //將所有按鈕打開
        public void EnableAll()
        {
            foreach (MyButton button in _buttonList)
            {
                button.IsEnable = true;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _buttonList.GetEnumerator();
        }
    }
}
