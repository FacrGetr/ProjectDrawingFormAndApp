using DrawingModel;
using System.Drawing;

namespace DrawingForm.PresentationModel
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;

        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        //畫面淨空，的實作
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //畫線
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _graphics.DrawLine(Pens.Black, (float)x1, (float)y1, (float)x2, (float)y2);
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

            double width = x2 - x1;
            double height = y2 - y1;
            Rectangle rectangle = new Rectangle((int)x1, (int)y1, (int)width, (int)height);
            SolidBrush brush = new SolidBrush(Color.Red);
            _graphics.FillRectangle(brush, rectangle);
        }

        //畫三角
        public void DrawTriangle(double x1, double y1, double x2, double y2)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            _graphics.FillPolygon(brush, new Point[] {
                    new Point((int)x1, (int)y2),
                    new Point((int)((x1 + x2) / 2), (int)y1),
                    new Point((int)x2, (int)y2)
                }
            );
        }
    }
}
