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
        const string INFO_PREFIX = "Select：";
        string _info = INFO_PREFIX;

        public string Info
        {
            get
            {
                return _info;
            }
        }

        public void SetShape(Shape shape)
        {
            if (shape == null)
            {
                _info = INFO_PREFIX;
                return;
            }
            _shape = shape;
            _info = INFO_PREFIX + shape.Info;
        }
    }
}
