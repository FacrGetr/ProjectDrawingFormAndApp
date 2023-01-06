using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;

namespace DrawingModel
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        bool _isPressed = false;
        ShapeManager _shapes = new ShapeManager();
        ButtonManager _buttons = new ButtonManager();
        Shape _hint;
        CommandManager _commands = new CommandManager();
        ShapeFactory _shapeFactory = new ShapeFactory();
        IState _state = new PointState();

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        void SetButtonsMode(DrawingMode mode)
        {
            _buttons.SetMode(mode);
            NotifyButtonsChanged();
        }

        //滑鼠按下
        public void PressedPointer(double x1, double y1)
        {
            if (x1 <= 0 || y1 <= 0)
            {
                return;
            }

            _isPressed = true;
            _state.PressedPointer(this, new MyPoint(x1, y1));
        }

        //滑鼠移動
        public void MovedPointer(double x1, double y1)
        {
            if (!_isPressed)
            {
                return;
            }

            _state.MovedPointer(this, new MyPoint(x1, y1));

            NotifyModelChanged();
        }

        //滑鼠放開
        public void ReleasedPointer(double x1, double y1)
        {
            if (!_isPressed)
            {
                return;
            }

            _state.ReleasedPointer(this, new MyPoint(x1, y1));
            _isPressed = false;

            EnableButtons();

            NotifyModelChanged();
        }

        public Shape CatchShape(MyPoint point)
        {
            return _shapes.CatchShape(point);
        }

        public void CreateNewHint(DrawingMode mode, Shape shape)
        {
            _hint = _shapeFactory.CreateNewShape(mode);
            _hint.ConnectPoint1ToShape(shape);
        }

        public void CreateNewHint(DrawingMode mode, MyPoint firstPoint)
        {
            _hint = _shapeFactory.CreateNewShape(mode);
            _hint.Point1 = firstPoint;
            _hint.Point2 = firstPoint;
        }

        public void ChangeHintPoint2(MyPoint point)
        {
            _hint.Point2 = point;
        }

        public void SetToPointState()
        {
            SetButtonsMode(DrawingMode.Point);
            _state = new PointState();
        }

        public void SetToRectangleState()
        {
            SetButtonsMode(DrawingMode.Rectangle);
            _state = new DrawRectangleState();
        }

        public void SetToTriangleState()
        {
            SetButtonsMode(DrawingMode.Triangle);
            _state = new DrawTriangleState();
        }

        public void SetToLineState()
        {
            SetButtonsMode(DrawingMode.Line);
            _state = new LineState();
        }

        public void SelectTargetShape(MyPoint point)
        {
            _shapes.SelectTargetShape(point);
            NotifyModelChanged();
            //原本想寫 Notify(nameof(SelectedShapeInfo)); 但不知為何 BadSmell 會閃退
            Notify("SelectedShapeInfo");
        }

        void EnableButtons()
        {
            _buttons.EnableAll();
            NotifyButtonsChanged();
        }

        public void AddHintToShapes(MyPoint endPoint)
        {
            _hint.Point2 = endPoint;
            _commands.Execute(new CommandAddNewShape(this, _hint));
        }

        public void AddHintToShapes(Shape endShape)
        {
            _hint.ConnectPoint2ToShape(endShape);
            _commands.Execute(new CommandAddNewShape(this, _hint));
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
            _state = new PointState();
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

        public bool IsPressed
        {
            get
            {
                return _isPressed;
            }
            set
            {
                _isPressed = value;
            }
        }

        //後悔
        public void Undo()
        {
            _shapes.ClearSelection();
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
