using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class CommandClear : ICommand
    {
        Model _model;
        List<Shape> _shapes = new List<Shape>();

        public CommandClear(Model model, ShapeManager shapes)
        {
            _model = model;
            foreach (Shape shape in shapes)
            {
                _shapes.Add(shape);
            }
        }

        //執行：清除
        public void Execute()
        {
            _model.ClearAllShapes();
        }

        //反執行：把那次清除的一切照順序一個個加回來
        public void UndoExecute()
        {
            foreach (Shape shape in _shapes)
            {
                _model.AddShape(shape);
            }
        }
    }
}
