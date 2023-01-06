using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class LineState : IState
    {

        //游標移動
        public void MovedPointer(Model model, MyPoint point)
        {
            model.SetHintPoint2(point);
        }

        //游標按下
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

        //游標放開
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
