using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class LineState : IState
    {
        public void MovedPointer(Model model, MyPoint point)
        {
            model.SetHintPoint2(point);
        }

        public void PressedPointer(Model model, MyPoint point)
        {
            Shape selected = model.CatchShape(point);
            if (selected == null)
            {
                model.IsPressed = false;
                return;
            }
            model.CreateNewHint(DrawingMode.Line, selected);
        }

        public void ReleasedPointer(Model model, MyPoint point)
        {
            Shape selected = model.CatchShape(point);
            if (selected == null)
            {
                model.IsPressed = false;
                return;
            }
            model.AddHintToShapes(selected);
            model.SetToPointState();
        }
    }
}
