using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public interface IState
    {
        void PressedPointer(Model model, MyPoint point);

        void MovedPointer(Model model, MyPoint point);

        void ReleasedPointer(Model model, MyPoint point);
    }
}
