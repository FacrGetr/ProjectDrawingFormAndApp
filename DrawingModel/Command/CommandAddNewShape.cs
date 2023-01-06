using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class CommandAddNewShape : ICommand
    {
        Model _model;
        Shape _shape;

        public CommandAddNewShape(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        //執行：新增圖形
        public void Execute()
        {
            _model.AddShape(_shape);
        }

        //反執行：吐出一個圖形
        public void UndoExecute()
        {
            _model.PopShape();
        }
    }

}
