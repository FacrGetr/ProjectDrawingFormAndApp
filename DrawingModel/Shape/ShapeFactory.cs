using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    class ShapeFactory
    {
        //根據要求 new 一個形狀回去
        public Shape CreateNewShape(DrawingMode mode)
        {
            const string EXCEPTION_MESSAGE = "無效 DrawingMode";
            switch (mode)
            {
                case DrawingMode.Rectangle:
                    return new MyRectangle();
                case DrawingMode.Triangle:
                    return new MyTriangle();
                case DrawingMode.Line:
                    return new MyLine();
            }
            throw new Exception(EXCEPTION_MESSAGE);
        }

        //根據要求 new 一個形狀回去，讀檔
        public Shape CreateNewShapeByInfo(string rawInfoString)
        {
            //去除尾端的 )
            rawInfoString = rawInfoString.Remove(rawInfoString.Length - 1);
            string[] shapeInfo = rawInfoString.Split('(');

            string shapeTypeName = shapeInfo[0];
            Shape shape = HandleTypeNameString(shapeTypeName);

            string[] pointsString = shapeInfo[1].Split(',');
            HandleShapePoints(shape, pointsString);

            return shape;
        }

        Shape HandleTypeNameString(string shapeTypeName)
        {
            const string EXCEPTION_MESSAGE = "無效 ShapeType";
            switch (shapeTypeName)
            {
                case nameof(MyRectangle):
                    return new MyRectangle();
                case nameof(MyTriangle):
                    return new MyTriangle();
                case nameof(MyLine):
                    return new MyLine();
                default:
                    throw new Exception(EXCEPTION_MESSAGE);
            }
        }

        void HandleShapePoints(Shape shape, string[] pointsString)
        {
            double x1 = double.Parse(pointsString[0]);
            double y1 = double.Parse(pointsString[1]);
            double x2 = double.Parse(pointsString[2]);
            double y2 = double.Parse(pointsString[3]);

            shape.Point1 = new MyPoint(x1, y1);
            shape.Point2 = new MyPoint(x2, y2);
        }
    }
}
