using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    abstract class DrawState : IState
    {
        public void MovedPointer(Model model, MyPoint point)
        {
            model.SetHintPoint2(point);
        }

        public abstract void PressedPointer(Model model, MyPoint point);

        public void ReleasedPointer(Model model, MyPoint point)
        {
            model.AddHintToShapes(point);
            model.SetToPointState();
        }
    }
}
