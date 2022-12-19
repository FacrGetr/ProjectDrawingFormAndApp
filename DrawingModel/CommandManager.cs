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
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.UnExecute();
        }

        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception("Cannot Redo exception\n");
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
