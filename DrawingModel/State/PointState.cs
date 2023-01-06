using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class PointState : IState
    {
        public void MovedPointer(Model model, MyPoint point)
        {
            throw new NotImplementedException();
        }

        public void PressedPointer(Model model, MyPoint point)
        {
            model.SelectTargetShape(point);
            model.IsPressed = false;
        }

        public void ReleasedPointer(Model model, MyPoint point)
        {
            throw new NotImplementedException();
        }
    }
}
