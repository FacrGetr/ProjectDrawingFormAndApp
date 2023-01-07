using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DrawingModel
{
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        ShapeManager _shapes = new ShapeManager();
        ButtonManager _buttons = new ButtonManager();
        Shape _hint;
        CommandManager _commands = new CommandManager();
        ShapeFactory _shapeFactory = new ShapeFactory();
        IState _state = new PointState();
        FileHandler _fileHandler = new FileHandler();

        public bool IsPressed { get; set; } = false;

        public bool IsLoadEnabled { get; set; } = false;

        public GoogleDriveService Service
        {
            get
            {
                return _fileHandler.Service;
            }
        }

        //存檔
        public async void Save()
        {
            _shapes.Save(_fileHandler.SaveFileName);
            await Task.Run(_fileHandler.Upload);
            IsLoadEnabled = true;
            NotifyModelChanged();
        }

        //讀檔
        public void Load()
        {
            _shapes.ClearAll();
            _commands.ClearAll();
            _fileHandler.LoadFile(this);
            NotifyModelChanged();
        }

        //設定按鈕狀態
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
            IsPressed = true;
            _state.PressedPointer(this, new MyPoint(x1, y1));
        }

        //滑鼠移動
        public void MovedPointer(double x1, double y1)
        {
            if (!IsPressed)
            {
                return;
            }
            _state.MovedPointer(this, new MyPoint(x1, y1));
            NotifyModelChanged();
        }

        //滑鼠放開
        public void ReleasedPointer(double x1, double y1)
        {
            if (!IsPressed)
            {
                return;
            }
            _state.ReleasedPointer(this, new MyPoint(x1, y1));
            IsPressed = false;
            EnableButtons();
            NotifyModelChanged();
        }

        //有沒有點到圖?
        public Shape CatchShape(MyPoint point)
        {
            return _shapes.CatchShape(point);
        }

        //生成新 Hint，第一個點是個圖
        public void CreateNewHint(DrawingMode mode, Shape shape)
        {
            _hint = _shapeFactory.CreateNewShape(mode);
            _hint.ConnectPoint1ToShape(shape);
        }

        //生成新 Hint，第一個點是個座標
        public void CreateNewHint(DrawingMode mode, MyPoint firstPoint)
        {
            _hint = _shapeFactory.CreateNewShape(mode);
            _hint.Point1 = firstPoint;
            _hint.Point2 = firstPoint;
        }

        //設定 Hint 的屁股座標
        public void SetHintPoint2(MyPoint point)
        {
            _hint.Point2 = point;
        }

        //新增 Hint 到圖形們裡，終點給的是座標
        public void AddHintToShapes(MyPoint endPoint)
        {
            _hint.Point2 = endPoint;
            _commands.Execute(new CommandAddNewShape(this, _hint));
        }

        //新增 Hint 到圖形們裡，終點給的是圖形
        public void AddHintToShapes(Shape endShape)
        {
            _hint.ConnectPoint2ToShape(endShape);
            _commands.Execute(new CommandAddNewShape(this, _hint));
        }

        //新增圖形進圖形們裡
        public void AddShape(string shapeInfo)
        {
            Shape shape = _shapeFactory.CreateNewShapeByInfo(shapeInfo);
            AddShape(shape);
        }

        //新增圖形進圖形們裡
        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
            NotifyModelChanged();
        }

        //吐掉一個圖形
        public void PopShape()
        {
            _shapes.PopShape();
            NotifyModelChanged();
        }

        //轉換為選取模式
        public void SetToPointState()
        {
            SetButtonsMode(DrawingMode.Point);
            _state = new PointState();
        }

        //轉換為畫方形模式
        public void SetToRectangleState()
        {
            SetButtonsMode(DrawingMode.Rectangle);
            _state = new DrawRectangleState();
        }

        //轉換為畫三角形模式
        public void SetToTriangleState()
        {
            SetButtonsMode(DrawingMode.Triangle);
            _state = new DrawTriangleState();
        }

        //轉換為畫線模式
        public void SetToLineState()
        {
            SetButtonsMode(DrawingMode.Line);
            _state = new LineState();
        }

        //選擇圖形
        public void SelectTargetShape(MyPoint point)
        {
            _shapes.SelectTargetShape(point);
            NotifyModelChanged();
            Notify(nameof(SelectedShapeInfo));
        }

        //打開按鈕們
        void EnableButtons()
        {
            _buttons.EnableAll();
            NotifyButtonsChanged();
        }

        //點擊 Clear Button 時，清空畫面上所有圖片，並且將所有按鈕 enable。
        public void ClickClear()
        {
            IsPressed = false;
            //_shapes.Clear();
            //NotifyModelChanged();
            _commands.Execute(new CommandClear(this, _shapes));
            EnableButtons();
            _state = new PointState();
        }

        //清除所有圖形
        public void ClearAllShapes()
        {
            _shapes.ClearAll();
            NotifyModelChanged();
        }

        //畫出圖形
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            if (_shapes.NotEmpty)
            {
                _shapes.Draw(graphics);
            }
            if (IsPressed && _hint != null)
            {
                _hint.Draw(graphics);
            }
        }

        //Observer Pattern，通知訂閱者老子變了
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
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

        //Line按鈕是否可用
        public bool IsLineEnable
        {
            get
            {
                return _buttons.IsLineEnable;
            }
        }

        //Redo按鈕是否可用
        public bool IsRedoEnabled
        {
            get
            {
                return _commands.IsRedoEnabled;
            }
        }

        //Undo按鈕是否可用
        public bool IsUndoEnabled
        {
            get
            {
                return _commands.IsUndoEnabled;
            }
        }

        //現在選取中的圖形，的資訊字卡
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
