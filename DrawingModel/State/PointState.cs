using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class PointState : IState
    {
        //游標移動
        public void MovedPointer(Model model, MyPoint point)
        {
            throw new NotImplementedException();
        }

        //游標按下
        public void PressedPointer(Model model, MyPoint point)
        {
            model.SelectTargetShape(point);
            model.IsPressed = false;
        }

        //游標放開
        public void ReleasedPointer(Model model, MyPoint point)
        {
            throw new NotImplementedException();
        }
    }
}
