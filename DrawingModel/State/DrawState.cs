using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    abstract class DrawState : IState
    {
        //游標移動：更新hint的屁屁
        public void MovedPointer(Model model, MyPoint point)
        {
            model.SetHintPoint2(point);
        }

        //游標按下：去問我孩子們該幹麻不要看我
        public abstract void PressedPointer(Model model, MyPoint point);

        //游標放開：把hint塞進圖形s裡
        public void ReleasedPointer(Model model, MyPoint point)
        {
            model.AddHintToShapes(point);
            model.SetToPointState();
        }
    }
}
