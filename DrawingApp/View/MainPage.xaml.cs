using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using DrawingModel;
using Windows.UI.Xaml.Data;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace DrawingApp
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Model _model;
        IGraphics _graphics;

        public MainPage()
        {
            InitializeComponent();
            // Model
            _model = new Model();
            // Note: 重複使用_igraphics物件
            _graphics = new View.WindowsStoreGraphicsAdaptor(_canvas);
            // Events
            _canvas.PointerPressed += HandleCanvasPointerPressed;
            _canvas.PointerReleased += HandleCanvasPointerReleased;
            _canvas.PointerMoved += HandleCanvasPointerMoved;
            _clear.Click += HandleClearButtonClick;
            _rectangle.Click += HandleRectangleButtonClick;
            _triangle.Click += HandleTriangleButtonClick;
            _line.Click += HandleLineButtonClick;
            _undo.Click += HandleUndoButtonClick;
            _redo.Click += HandleRedoButtonClick;
            _model._modelChanged += HandleModelChanged;
            _model.PropertyChanged += HandlePropertyChanged;
        }

        private void HandleRedoButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Redo();
        }

        private void HandleUndoButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Undo();
        }

        private void HandleLineButtonClick(object sender, RoutedEventArgs e)
        {
            _model.SetMode(DrawingMode.Line);
        }

        //Observer，設定按鈕開關
        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _clear.IsEnabled = _model.IsClearEnable;
            _rectangle.IsEnabled = _model.IsRectangleEnable;
            _triangle.IsEnabled = _model.IsTriangleEnable;
            _line.IsEnabled = _model.IsLineEnable;
            _undo.IsEnabled = _model.IsUndoEnabled;
            _redo.IsEnabled = _model.IsRedoEnabled;
            _selectedShapeInfo.Text = _model.SelectedShapeInfo;
        }

        //Clear按鈕
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _model.ClickClear();
        }

        //Rectangle按鈕
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _model.SetMode(DrawingMode.Rectangle);
        }

        //Triangle按鈕
        private void HandleTriangleButtonClick(object sender, RoutedEventArgs e)
        {
            _model.SetMode(DrawingMode.Triangle);
        }

        //按下左鍵
        public void HandleCanvasPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _model.PressedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        //放開左鍵
        public void HandleCanvasPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _model.ReleasedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        //移動滑鼠
        public void HandleCanvasPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            _model.MovedPointer(e.GetCurrentPoint(_canvas).Position.X, e.GetCurrentPoint(_canvas).Position.Y);
        }

        //Model（圖形們）有變化
        public void HandleModelChanged()
        {
            _model.Draw(_graphics);
        }
    }
}
