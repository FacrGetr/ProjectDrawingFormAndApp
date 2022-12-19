using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace DrawingModel
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        MyPoint _firstPoint;
        bool _isPressed = false;
        ShapeManager _shapes = new ShapeManager();
        ButtonManager _buttons = new ButtonManager();
        Shape _hint;
        DrawingMode _nowDrawing = DrawingMode.Line;
        CommandManager _commands = new CommandManager();
        //
        //data binding用
        //
        const string PROPERTY_RECTANGLE_ENABLE = "IsRectangleEnable";
        const string PROPERTY_CLEAR_ENABLE = "IsClearEnable";
        const string PROPERTY_TRIANGLE_ENABLE = "IsTriangleEnable";
        const string PROPERTY_LINE_ENABLE = "IsLineEnable";

        //更改繪圖模式（設定現在要畫什麼）
        public void SetMode(DrawingMode mode)
        {
            _nowDrawing = mode;
            _buttons.SetMode(mode);
            NotifyButtonsChanged();
        }

        //滑鼠按下
        public void PressedPointer(double x1, double y1)
        {
            if (_nowDrawing == DrawingMode.Null)
                return;
            if (x1 > 0 && y1 > 0)
            {
                _firstPoint = new MyPoint(x1, y1);
                _hint = _shapes.CreateNewShape(_nowDrawing);
                _hint.Point1 = _firstPoint;
                _hint.Point2 = _firstPoint;
                _isPressed = true;
            }
        }

        //滑鼠移動
        public void MovedPointer(double x1, double y1)
        {
            if (_isPressed)
            {
                _hint.Point2 = new MyPoint(x1, y1);
                NotifyModelChanged();
            }
        }

        //滑鼠放開
        public void ReleasedPointer(double x1, double y1)
        {
            if (_isPressed)
            {
                _isPressed = false;
                //_shapes.Add(_hint);
                _commands.Execute(new CommandAddNewShape(this, _hint));
                _nowDrawing = DrawingMode.Null;
                EnableButtons();
            }
        }

        void EnableButtons()
        {
            _buttons.EnableAll();
            NotifyButtonsChanged();
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
            NotifyModelChanged();
        }

        public void PopShape()
        {
            _shapes.PopShape();
            NotifyModelChanged();
        }

        //點擊 Clear Button 時，清空畫面上所有圖片，並且將所有按鈕 enable。
        public void ClickClear()
        {
            _isPressed = false;
            //_shapes.Clear();
            //NotifyModelChanged();
            _commands.Execute(new CommandClear(this, _shapes));
            _buttons.EnableAll();
            NotifyButtonsChanged();
            _nowDrawing = DrawingMode.Null;
        }

        public void ClearAllShapes()
        {
            _shapes.Clear();
            NotifyModelChanged();
        }

        //畫圖
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            if (_shapes.NotEmpty)
            {
                _shapes.Draw(graphics);
            }
            if (_isPressed)
                _hint.Draw(graphics);
        }

        //Observer Pattern，通知訂閱者老子變了
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        //單元測試用
        private DrawingMode GetMode()
        {
            return _nowDrawing;
        }

        //單元測試用
        private bool IsPressed()
        {
            return _isPressed;
        }

        //Rectangle按鈕是否可用
        public bool IsRectangleEnable
        {
            get
            {
                return _buttons.IsRectangleEnable;
            }
        }

        //Triangle按鈕是否可用
        public bool IsTriangleEnable
        {
            get
            {
                return _buttons.IsTriangleEnable;
            }
        }

        //Clear按鈕是否可用
        public bool IsClearEnable
        {
            get
            {
                return _buttons.IsClearEnable;
            }
        }

        public bool IsLineEnable
        {
            get
            {
                return _buttons.IsLineEnable;
            }
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _commands.IsRedoEnabled;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _commands.IsUndoEnabled;
            }
        }

        //後悔
        public void Undo()
        {
            _commands.Undo();
        }

        //後悔我的後悔
        public void Redo()
        {
            _commands.Redo();
        }

        //Observer / DataBinding 用
        void NotifyButtonsChanged()
        {
            Notify(PROPERTY_RECTANGLE_ENABLE);
            Notify(PROPERTY_CLEAR_ENABLE);
            Notify(PROPERTY_TRIANGLE_ENABLE);
            Notify(PROPERTY_LINE_ENABLE);
        }

        //Observer / DataBinding 用
        void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
