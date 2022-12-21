using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingModelTests
{

    [TestClass()]
    public class MainFormUITest
    {
        private Robot _robot;
        private const string APP_NAME = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private const string APP_TITLE = "小算盤";
        private const string EXPECTED_VALUE = "顯示是 444";
        private const string RESULT_CONTROL_NAME = "CalculatorResults";
        private string targetAppPath;
        private const string TARGET_FORM = "DrawingForm";
        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "DrawingForm";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "DrawingForm.exe");
            _robot = new Robot(targetAppPath, TARGET_FORM);

        }


        /// <summary>
        /// Closes the launched program
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        [TestMethod]
        public void TestClickButtons()
        {
            _robot.ClickButton("Line");
            _robot.ClickButton("Triangle");
            _robot.ClickButton("Rectangle");
            _robot.ClickButton("Clear");
        }

        [TestMethod]
        public void TestDraw()
        {
            _robot.ClickButton("Triangle");
            _robot.DragAndDrop(TARGET_FORM, 200, 200, 300, 300);
            _robot.ClickButton("Rectangle");
            _robot.DragAndDrop(TARGET_FORM, 400, 200, 500, 300);
            _robot.Click(TARGET_FORM, 250, 250);
            //_robot.AssertText("LegacyIAccessible", "Select：Triangle(198, 56, 298, 155)");
            _robot.Click(TARGET_FORM, 450, 250);
            //_robot.AssertText("LegacyIAccessible", "Select：Rectangle(399, 56, 498, 155)");
            _robot.ClickButton("Line");
            _robot.DragAndDrop(TARGET_FORM, 250, 250, 450, 250);
            _robot.ClickButton("Clear");
        }

    }
}
