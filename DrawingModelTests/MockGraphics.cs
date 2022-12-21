using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModelTests
{
    class MockGraphics : DrawingModel.IGraphics
    {
        public bool DidClearAll
        {
            get; set;
        }

        public bool DidDrawSomething
        {
            get; set;
        }

        public bool DidDrawMarker
        {
            get; set;
        }

        public bool DidDrawLine
        {
            get; set;
        }

        public void ClearAll()
        {
            DidClearAll = true;
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            DidDrawLine = true;
        }

        public void Reset()
        {
            DidClearAll = false;
            DidDrawSomething = false;
            DidDrawLine = false;
            DidDrawMarker = false;
        }

        public void DrawRectangle(double x1, double y1, double x2, double y2)
        {
            DidDrawSomething = true;
        }
        public void DrawTriangle(double x1, double y1, double x2, double y2)
        {
            DidDrawSomething = true;
        }

        public void DrawRectangleMarker(double x1, double y1, double x2, double y2)
        {
            DidDrawMarker = true;
        }

        public void DrawTriangleMarker(double x1, double y1, double x2, double y2)
        {
            DidDrawMarker = true;
        }
    }
}
