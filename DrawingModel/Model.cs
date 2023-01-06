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
        DrawingMode _drawingModeNow = DrawingMode.Select;
        CommandManager _commands = new CommandManager();
        ShapeFactory _shapeFactory = new ShapeFactory();

        //更改繪圖模式（設定現在要畫什麼）
        public void SetMode(DrawingMode mode)
        {
            _drawingModeNow = mode;
            _buttons.SetMode(mode);
            NotifyButtonsChanged();
        }

        //滑鼠按下
        public void PressedPointer(double x1, double y1)
        {
            if (x1 <= 0 || y1 <= 0) return;
            _firstPoint = new MyPoint(x1, y1);
            if (_drawingModeNow == DrawingMode.Select)
            {
                _shapes.SelectTargetShape(_firstPoint);
                NotifyModelChanged();
                Notify("SelectedShapeInfo");
                return;
            }
 
            _isPressed = true;
            _hint = _shapeFactory.CreateNewShape(_drawingModeNow);
            _hint.Point1 = _firstPoint;
            _hint.Point2 = _firstPoint;

            if(_drawingModeNow == DrawingMode.Line)
            {
                Shape selected = _shapes.LineCatchShape(_firstPoint);
                if (selected == null)
                {
                    _isPressed = false;
                    return;
                }
                _hint.ConnectPoint1ToShape(selected);
            }
        }

        //滑鼠移動
        public void MovedPointer(double x1, double y1)
        {
            if (!_isPressed) return;

            _hint.Point2 = new MyPoint(x1, y1);
            NotifyModelChanged();
            
        }

        //滑鼠放開
        public void ReleasedPointer(double x1, double y1)
        {
            if (!_isPressed) return;

            if (_drawingModeNow == DrawingMode.Line)
            {
                Shape selected = _shapes.LineCatchShape(new MyPoint(x1, y1));
                if (selected == null)
                {
                    _isPressed = false;
                    NotifyModelChanged();
                    return;
                }
                _hint.ConnectPoint2ToShape(selected);
            }

            _commands.Execute(new CommandAddNewShape(this, _hint));
            EnableButtons();
            _isPressed = false;
            _drawingModeNow = DrawingMode.Select;
            
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
            EnableButtons();
            _drawingModeNow = DrawingMode.Select;
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
            {
                _hint.Draw(graphics);
            }
        }

        //Observer Pattern，通知訂閱者老子變了
        void NotifyModelChanged()
        {
            _modelChanged?.Invoke();
        }

        //單元測試用
        private DrawingMode GetMode()
        {
            return _drawingModeNow;
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

        public string SelectedShapeInfo
        {
            get
            {
                return _shapes.SelectShapeInfo;
            }
        }

        //後悔
        public void Undo()
        {
            _commands.Undo();
            NotifyButtonsChanged();
        }

        //後悔我的後悔
        public void Redo()
        {
            _commands.Redo();
            NotifyButtonsChanged();
        }

        //Observer / DataBinding 用
        void NotifyButtonsChanged()
        {
            Notify(nameof(IsRectangleEnable));
            Notify(nameof(IsClearEnable));
            Notify(nameof(IsTriangleEnable));
            Notify(nameof(IsLineEnable));
            Notify(nameof(IsUndoEnabled));
            Notify(nameof(IsRedoEnabled));
        }

        //Observer / DataBinding 用
        void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
