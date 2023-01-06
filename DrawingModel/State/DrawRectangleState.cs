using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawRectangleState : DrawState
    {
        //滑鼠按下，畫方形
        public override void PressedPointer(Model model, MyPoint point)
        {
            model.CreateNewHint(DrawingMode.Rectangle, point);
        }
    }
}
