using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class MyButton
    {
        public MyButton(DrawingMode mode, bool enable)
        {
            Mode = mode;
            IsEnable = enable;
        }

        public DrawingMode Mode
        {
            get; set;
        }

        public bool IsEnable
        {
            get; set;
        }
    }

}
