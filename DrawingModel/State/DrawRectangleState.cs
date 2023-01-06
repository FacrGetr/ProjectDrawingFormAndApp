using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawRectangleState : DrawState
    {
        public override void PressedPointer(Model model, MyPoint point)
        {
            model.CreateNewHint(DrawingMode.Rectangle, point);
        }
    }
}
