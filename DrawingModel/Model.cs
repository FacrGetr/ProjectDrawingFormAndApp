﻿using System.Runtime.CompilerServices;
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
        Shapes _shapes = new Shapes();
        Buttons _buttons = new Buttons();
        Shape _hint;
        DrawingMode _nowDrawing = DrawingMode.Line;
        //data binding用
        const string PROPERTY_RECTANGLE_ENABLE = "IsRectangleEnable";
        const string PROPERTY_CLEAR_ENABLE = "IsClearEnable";
        const string PROPERTY_TRIANGLE_ENABLE = "IsTriangleEnable";

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
                _shapes.Add(_hint);
                NotifyModelChanged();
                _buttons.EnableAll();
                NotifyButtonsChanged();
            }
        }

        //點擊 Clear Button 時，清空畫面上所有圖片，並且將所有按鈕 enable。
        public void Clear()
        {
            _isPressed = false;
            _shapes.Clear();
            NotifyModelChanged();
            _buttons.EnableAll();
            NotifyButtonsChanged();
            _nowDrawing = DrawingMode.Line;
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

        //Observer / DataBinding 用
        void NotifyButtonsChanged()
        {
            Notify(PROPERTY_RECTANGLE_ENABLE);
            Notify(PROPERTY_CLEAR_ENABLE);
            Notify(PROPERTY_TRIANGLE_ENABLE);
        }

        //Observer / DataBinding 用
        void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
