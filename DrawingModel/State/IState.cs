using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public interface IState
    {
        //游標點擊
        void PressedPointer(Model model, MyPoint point);

        //游標移動
        void MovedPointer(Model model, MyPoint point);

        //游標放開
        void ReleasedPointer(Model model, MyPoint point);
    }
}
