using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class DrawTriangleState : DrawState
    {
        //游標按下：畫三角
        public override void PressedPointer(Model model, MyPoint point)
        {
            model.CreateNewHint(DrawingMode.Triangle, point);
        }
    }
}
