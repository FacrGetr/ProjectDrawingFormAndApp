using DrawingModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace DrawingApp.View
{
    // Windows Store App的繪圖方式採用"物件"模型(與Windows Forms完全不同)
    // 當繪圖時，必須先建立"圖形物件"，再將"圖形物件"加入畫布的Children，此後該圖形就會被畫出來
    // 由於畫布管理其Children，因此有以下優缺點
    //   優點：畫布可以自行處理OnPaint()，而使用者則省掉處理OnPaint()的麻煩
    //   缺點：繪圖時必須先建立"圖形物件"；清除某圖形時，必須刪除Children中對應的物件
    class WindowsStoreGraphicsAdaptor : IGraphics
    {
        Canvas _canvas;

        public WindowsStoreGraphicsAdaptor(Canvas canvas)
        {
            this._canvas = canvas;
        }

        //清除畫布
        public void ClearAll()
        {
            // 清除Children也就清除畫布
            _canvas.Children.Clear();
        }

        //畫線
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            // 先建立圖形物件
            Windows.UI.Xaml.Shapes.Line line = new Windows.UI.Xaml.Shapes.Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            // 將圖形物件加入Children
            _canvas.Children.Add(line);
        }

        //畫方
        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }

            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }

            Windows.UI.Xaml.Shapes.Rectangle rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
            rectangle.Width = x2 - x1;
            rectangle.Height = y2 - y1;
            rectangle.Fill = new SolidColorBrush(Colors.Blue);
            Canvas.SetLeft(rectangle, x1);
            Canvas.SetTop(rectangle, y1);
            _canvas.Children.Add(rectangle);
        }

        //畫三角
        public void DrawTriangle(double x1, double y1, double x2, double y2)
        {
            Windows.UI.Xaml.Shapes.Polygon polygon = new Windows.UI.Xaml.Shapes.Polygon();
            PointCollection points = new PointCollection();
            points.Add(new Point(x1, y2));
            points.Add(new Point((x1 + x2) / 2, y1));
            points.Add(new Point(x2, y2));
            polygon.Points = points;
            polygon.Fill = new SolidColorBrush(Colors.Red);
            _canvas.Children.Add(polygon);
        }
    }
}