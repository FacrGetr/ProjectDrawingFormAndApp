using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    interface Command
    {
        void Execute();
        void UnExecute();
    }

    class CommandAddNewShape : Command
    {
        Model _model;
        Shape _shape;

        public CommandAddNewShape(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        public void Execute()
        {
            _model.AddShape(_shape);
        }

        public void UnExecute()
        {
            _model.PopShape();
        }
    }

    class CommandManager
    {
        Stack<Command> _undo = new Stack<Command>();
        Stack<Command> _redo = new Stack<Command>();

        public void Execute(Command command)
        {
            command.Execute();
            _undo.Push(command);    // push command 進 undo stack
            _redo.Clear();      // 清除redo stack
        }

        public void Undo()
        {
            if (_undo.Count <= 0)
            {
                throw new Exception("Cannot Undo exception\n");
            }
            Command command = _undo.Pop();
            _redo.Push(command);
            command.UnExecute();
        }

        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
            Command command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}
