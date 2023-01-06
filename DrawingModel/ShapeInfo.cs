using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ShapeInfo
    {
        Shape _shape;
        const string INFO_HEADER = "Select：";
        string _info = INFO_HEADER;

        public string Info
        {
            get
            {
                return _info;
            }
        }

        //設定現在被選的是哪個圖形
        public void SetSelectedShape(Shape shape)
        {
            if (shape == null)
            {
                _info = INFO_HEADER;
                return;
            }
            _shape = shape;
            _info = INFO_HEADER + shape.Info;
        }
    }
}
