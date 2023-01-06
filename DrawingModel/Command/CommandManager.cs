using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class CommandManager
    {
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
            command.Execute();
        }

        public void Undo()
        {
            if (_undo.Count <= 0)
            {
                throw new Exception();
            }
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.UndoExecute();
        }

        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception();
            ICommand command = _redo.Pop();
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
