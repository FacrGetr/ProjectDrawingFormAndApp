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

        //淨空
        public void ClearAll()
        {
            _undo.Clear();
            _redo.Clear();
        }

        //執行
        public void Execute(ICommand command)
        {
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
            command.Execute();
        }

        //後悔
        public void Undo()
        {
            const string EXCEPTION_MESSAGE = "不能 Undo";
            if (_undo.Count <= 0)
            {
                throw new Exception(EXCEPTION_MESSAGE);
            }
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.UndoExecute();
        }

        //後悔我的後悔
        public void Redo()
        {
            const string EXCEPTION_MESSAGE = "不能 Redo";
            if (_redo.Count <= 0)
            {
                throw new Exception(EXCEPTION_MESSAGE);
            }

            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
        }

        //我可以後悔嗎
        public bool IsUndoEnabled
        {
            get
            {
                return _undo.Count != 0;
            }
        }

        //我可以後悔我的後悔嗎
        public bool IsRedoEnabled
        {
            get
            {
                return _redo.Count != 0;
            }
        }
    }
}
