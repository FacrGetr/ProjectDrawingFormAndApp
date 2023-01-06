using DrawingModel;
using System.Drawing;

namespace DrawingForm.PresentationModel
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        const int PEN_WIDTH = 3;
        const float DASH_SIZE = 3.0f;
        const float DOT_SIZE = 10.0f;
        const float PREFIX = 5.0f;

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
            _graphics.DrawLine(new Pen(Color.Black, PEN_WIDTH), (float)x1, (float)y1, (float)x2, (float)y2);
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
            _graphics.FillRectangle(new SolidBrush(Color.Red), rectangle);
            _graphics.DrawRectangle(new Pen(Color.Black, PEN_WIDTH), rectangle);
        }

        //畫框框，方形的那種
        public void DrawRectangleMarker(double x1, double y1, double x2, double y2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }

            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }

            Pen pen = new Pen(Color.Orange, PEN_WIDTH);
            pen.DashPattern = new float[] { DASH_SIZE, DASH_SIZE };

            double width = x2 - x1;
            double height = y2 - y1;
            Rectangle rectangle = new Rectangle((int)x1, (int)y1, (int)width, (int)height);

            _graphics.DrawRectangle(pen, rectangle);
            Pen blackPen = new Pen(Color.Black, PEN_WIDTH);
            _graphics.FillEllipse(Brushes.White, (float)x1 - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE); 
            _graphics.FillEllipse(Brushes.White, (float)x2 - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE); 
            _graphics.FillEllipse(Brushes.White, (float)x1 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE); 
            _graphics.FillEllipse(Brushes.White, (float)x2 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE); 
            _graphics.DrawEllipse(blackPen, (float)x1 - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)x2 - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)x1 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)x2 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
        }

        //畫三角
        public void DrawTriangle(double x1, double y1, double x2, double y2)
        {
            Point[] triangle = {
                    new Point((int)x1, (int)y2),
                    new Point((int)((x1 + x2) / 2), (int)y1),
                    new Point((int)x2, (int)y2)
            };
            _graphics.FillPolygon(new SolidBrush(Color.Blue), triangle);
            _graphics.DrawPolygon(new Pen(Color.Black, PEN_WIDTH), triangle);
        }

        //畫框框，三角形的那種
        public void DrawTriangleMarker(double x1, double y1, double x2, double y2)
        {
            Pen pen = new Pen(Color.Orange, PEN_WIDTH);
            pen.DashPattern = new float[] { DASH_SIZE, DASH_SIZE };
            double midX = (x1 + x2) / 2;

            Point[] triangle = {
                    new Point((int)x1, (int)y2),
                    new Point((int)midX, (int)y1),
                    new Point((int)x2, (int)y2)
            };

            _graphics.DrawPolygon(pen, triangle);
            Pen blackPen = new Pen(Color.Black, PEN_WIDTH);
            _graphics.FillEllipse(Brushes.White, (float)x1 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.FillEllipse(Brushes.White, (float)midX - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.FillEllipse(Brushes.White, (float)x2 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)x1 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)midX - PREFIX, (float)y1 - PREFIX, DOT_SIZE, DOT_SIZE);
            _graphics.DrawEllipse(blackPen, (float)x2 - PREFIX, (float)y2 - PREFIX, DOT_SIZE, DOT_SIZE);
        }
    }
}
