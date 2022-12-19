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
        const DrawingMode RECTANGLE = DrawingMode.Rectangle;
        const DrawingMode TRIANGLE = DrawingMode.Triangle;
        bool _modelChanged;
        PrivateObject _privateModel;
        MockGraphics graphics = new MockGraphics();

        [TestInitialize()]
        public void TestInitialize()
        {
            _model = new Model();
            graphics.Reset();
            _modelChanged = false;
            _model._modelChanged += modelDidChanged;
        }

        [TestMethod()]
        public void SwitchModeTest()
        {
            _model.SetMode(LINE);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(LINE, _privateModel.Invoke("GetMode"));
            _model.SetMode(RECTANGLE);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(RECTANGLE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(false, _model.IsRectangleEnable);
            Assert.AreEqual(true, _model.IsTriangleEnable);
            Assert.AreEqual(true, _model.IsClearEnable);
            _model.SetMode(TRIANGLE);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(TRIANGLE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(true, _model.IsRectangleEnable);
            Assert.AreEqual(false, _model.IsTriangleEnable);
            Assert.AreEqual(true, _model.IsClearEnable);
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
            Assert.AreEqual(true, _privateModel.Invoke("IsPressed"));

            TestInitialize();
            _model.PressedPointer(-1, -1);
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));
        }

        [TestMethod()]
        public void PointerMovedTest()
        {
            _model.MovedPointer(10, 10);
            Assert.IsFalse(_modelChanged);

            TestInitialize();
            _model.PressedPointer(1, 1);
            _model.MovedPointer(10, 10);
            Assert.IsTrue(_modelChanged);
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
            _model.Clear();
            _privateModel = new PrivateObject(_model);
            Assert.AreEqual(LINE, _privateModel.Invoke("GetMode"));
            Assert.AreEqual(false, _privateModel.Invoke("IsPressed"));
            Assert.IsTrue(_modelChanged);
        }

        [TestMethod()]
        public void DrawTest()
        {
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidClearAll);
            Assert.IsFalse(graphics.DidDrawSomething);

            graphics.Reset();
            TestInitialize();
            _model.PressedPointer(1, 1);
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidDrawSomething);

            graphics.Reset();
            _model.MovedPointer(5, 5);
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidDrawSomething);

            graphics.Reset();
            _model.ReleasedPointer(10, 10);
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidDrawSomething);

            graphics.Reset();
            _model.Clear();
            _model.Draw(graphics);
            Assert.IsFalse(graphics.DidDrawSomething);

            graphics.Reset();
            _model.SetMode(RECTANGLE);
            _model.PressedPointer(1, 1);
            _model.MovedPointer(5, 5);
            _model.ReleasedPointer(10, 10);
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidDrawSomething);

            graphics.Reset();
            _model.SetMode(TRIANGLE);
            _model.PressedPointer(1, 1);
            _model.MovedPointer(5, 5);
            _model.ReleasedPointer(10, 10);
            _model.Draw(graphics);
            Assert.IsTrue(graphics.DidDrawSomething);
        }

        [TestMethod()]
        public void NotifyTest()
        {
            //呃......我不知道 PropertyChanged 要怎麼單元測試......
            Assert.Fail();
        }

        void modelDidChanged()
        {
            _modelChanged = true;
        }
    }
}