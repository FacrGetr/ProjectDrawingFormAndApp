using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModelTests
{
    [TestClass()]
    public class ModelTests
    {
        Model _model;
        const DrawingMode LINE = DrawingMode.Line;
        const DrawingMode POINT = DrawingMode.Point;
        const DrawingMode RECTANGLE = DrawingMode.Rectangle;
        const DrawingMode TRIANGLE = DrawingMode.Triangle;
        bool _modelChanged;
        PrivateObject _privateModel;
        MockGraphics _graphics = new MockGraphics();

        [TestInitialize()]
        public void TestInitialize()
        {
            _model = new Model();
            _graphics.Reset();
            _modelChanged = false;
            _model._modelChanged += modelDidChanged;
        }

        [TestMethod()]
        public void SwitchModeTest()
        {
            _model.SetToLineState();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(LINE, _privateModel.Invoke("GetMode"));
            _model.SetToRectangleState();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(RECTANGLE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(false, _model.IsRectangleEnable);
            Assert.AreEqual(true, _model.IsTriangleEnable);
            Assert.AreEqual(true, _model.IsClearEnable);
            Assert.AreEqual(true, _model.IsLineEnable);
            _model.SetToTriangleState();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(TRIANGLE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(true, _model.IsRectangleEnable);
            Assert.AreEqual(false, _model.IsTriangleEnable);
            Assert.AreEqual(true, _model.IsClearEnable);
            Assert.AreEqual(true, _model.IsLineEnable);
            _model.SetToLineState();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(LINE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(true, _model.IsRectangleEnable);
            Assert.AreEqual(true, _model.IsTriangleEnable);
            Assert.AreEqual(true, _model.IsClearEnable);
            Assert.AreEqual(false, _model.IsLineEnable);
        }

        [TestMethod()]
        public void PointerPressedTest()
        {
            _model.PressedPointer(0, 0);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));

            TestInitialize();
            _model.PressedPointer(1, 1);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));

            TestInitialize();
            _model.PressedPointer(-1, -1);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));


            TestInitialize();
            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(true, _privateModel.Invoke("IsPressed"));
        }

        [TestMethod()]
        public void PointerMovedTest()
        {
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawSomething);

            TestInitialize();
            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            Assert.IsTrue(_modelChanged);
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawSomething);
        }

        [TestMethod()]
        public void PointerReleasedTest()
        {
            _model.ReleasedPointer(10, 10);
            Assert.IsFalse(_modelChanged);

            TestInitialize();
            _model.PressedPointer(1, 1);
            _model.ReleasedPointer(10, 10);
            Assert.IsTrue(_modelChanged);

            TestInitialize();
            _model.MovedPointer(1, 1);
            _model.ReleasedPointer(10, 10);
            Assert.IsFalse(_modelChanged);
        }

        [TestMethod()]
        public void ClearTest()
        {
            _model.ClickClear();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(POINT, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));
            Assert.IsTrue(_modelChanged);
        }

        [TestMethod()]
        public void DrawTest()
        {
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidClearAll);
            Assert.IsFalse(_graphics.DidDrawSomething);

            _graphics.Reset();
            TestInitialize();
            _model.ClickClear();
            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawSomething);

            _graphics.Reset();
            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(5, 5);
            _model.ReleasedPointer(10, 10);
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawSomething);

            _graphics.Reset();
            _model.SetToTriangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(5, 5);
            _model.ReleasedPointer(10, 10);
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawSomething);
        }

        [TestMethod()]
        public void TestDrawLine()
        {
            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            _model.ReleasedPointer(10, 10);

            _model.SetToTriangleState();
            _model.PressedPointer(15, 15);
            _model.MovedPointer(30, 30);
            _model.ReleasedPointer(30, 30);

            _model.SetToLineState();
            _model.PressedPointer(50, 50);
            _model.MovedPointer(10, 10);
            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawLine);

            _model.PressedPointer(5, 5);
            _model.MovedPointer(50, 50);
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawLine);
            _graphics.Reset();

            _model.ReleasedPointer(50, 50);
            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawLine);

            _model.SetToLineState();
            _model.PressedPointer(5, 5);
            _model.MovedPointer(20, 20);
            _model.ReleasedPointer(20, 20);

            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawLine);
        }

        [TestMethod()]
        public void TestUndoRedo()
        {
            Assert.IsFalse(_model.IsUndoEnabled);
            Assert.IsFalse(_model.IsRedoEnabled);

            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            _model.ReleasedPointer(10, 10);

            Assert.IsTrue(_model.IsUndoEnabled);
            Assert.IsFalse(_model.IsRedoEnabled);

            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawSomething);
            _graphics.Reset();

            _model.Undo();
            Assert.IsFalse(_model.IsUndoEnabled);
            Assert.IsTrue(_model.IsRedoEnabled);

            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawSomething);

            _model.Redo();
            Assert.IsTrue(_model.IsUndoEnabled);
            Assert.IsFalse(_model.IsRedoEnabled);

            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawSomething);
        }

        [TestMethod()]
        public void TestDrawMarker()
        {
            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            _model.ReleasedPointer(10, 10);

            _model.SetToPointState();
            _model.PressedPointer(5, 5);
            _model.Draw(_graphics);
            Assert.IsTrue(_graphics.DidDrawMarker);
            _graphics.Reset();

            _model.PressedPointer(50, 50);

            _model.Draw(_graphics);
            Assert.IsFalse(_graphics.DidDrawMarker);
        }

        [TestMethod()]
        public void TestSelectedShapeInfo()
        {
            Assert.AreEqual("Select：", _model.SelectedShapeInfo);

            _model.SetToRectangleState();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            _model.ReleasedPointer(10, 10);

            _model.PressedPointer(5, 5);

            Assert.AreEqual("Select：Rectangle(1, 1, 10, 10)", _model.SelectedShapeInfo);

        }

        void modelDidChanged()
        {
            _modelChanged = true;
        }
    }
}