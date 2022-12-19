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

        public void Execute()
        {
            _model.ClearAllShapes();
        }

        public void UnExecute()
        {
            foreach (Shape shape in _shapes)
            {
                _model.AddShape(shape);
            }
        }
    }
}
