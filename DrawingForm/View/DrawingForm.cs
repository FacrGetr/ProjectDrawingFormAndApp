using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DrawingModel;

namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        Model _model;
        Panel _canvas = new DoubleBufferedPanel();
        ToolStripButton _undo;
        ToolStripButton _redo;
        ToolStripButton _save;
        ToolStripButton _load;

        public DrawingForm(Model model)
        {
            InitializeComponent();
            //
            // prepare presentation model and model
            //
            _model = model;
            _model._modelChanged += HandleModelChanged;
            //
            // prepare canvas
            //
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPointerPressed;
            _canvas.MouseUp += HandleCanvasPointerReleased;
            _canvas.MouseMove += HandleCanvasPointerMoved;
            _canvas.Paint += HandleCanvasPaint;
            // Note: setting "_canvas.DoubleBuffered = true" does not work
            Controls.Add(_canvas);
            //
            // prepare clear button
            //
            Button clear = new Button
            {
                Text = "Clear",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            clear.Click += HandleClearButtonClick;
            clear.DataBindings.Add(nameof(Enabled), _model, "IsClearEnable");
            Controls.Add(clear);
            //
            // prepare rectangle button
            //
            Button rectangle = new Button
            {
                Text = "Rectangle",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            rectangle.Click += HandleRectangleButtonClick;
            rectangle.DataBindings.Add(nameof(Enabled), _model, "IsRectangleEnable");
            Controls.Add(rectangle);
            //
            // prepare triangle button
            //
            Button triangle = new Button
            {
                Text = "Triangle",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            triangle.Click += HandleTriangleButtonClick;
            triangle.DataBindings.Add(nameof(Enabled), _model, "IsTriangleEnable");
            Controls.Add(triangle);
            //
            // prepare line button
            //
            Button line = new Button
            {
                Text = "Line",
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            line.Click += HandleLineButtonClick;
            line.DataBindings.Add(nameof(Enabled), _model, "IsLineEnable");
            Controls.Add(line);
            //
            //ToolStrip
            //
            ToolStrip ts = new ToolStrip();
            Controls.Add(ts);
            _undo = new ToolStripButton("Undo", null, HandleUndoButtonClick)
            {
                Enabled = false,
                Owner = ts
            };
            _redo = new ToolStripButton("Redo", null, HandleRedoButtonClick)
            {
                Enabled = false,
                Owner = ts
            };
            _save = new ToolStripButton("Save", null, HandleSaveButtonClick)
            {
                Enabled = true,
                Owner = ts
            };
            _load = new ToolStripButton("Load", null, HandleLoadButtonClick)
            {
                Enabled = true,
                Owner = ts
            };
            //
            //selectString
            //
            _selectShapeString.DataBindings.Add(nameof(Text), _model, "SelectedShapeInfo");
        }

        private void HandleSaveButtonClick(object sender, EventArgs e)
        {
            _model.Save();
        }

        private void HandleLoadButtonClick(object sender, EventArgs e)
        {
            _model.Load();
        }

        //Undo按鈕
        void HandleUndoButtonClick(object sender, System.EventArgs e)
        {
            _model.Undo();
        }

        //Redo按鈕
        void HandleRedoButtonClick(object sender, System.EventArgs e)
        {
            _model.Redo();
        }
        //Line按鈕
        void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _model.SetToLineState();
        }

        //Clear按鈕
        void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.ClickClear();
        }

        //Rectangle按鈕
        void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetToRectangleState();
        }

        //Triangle按鈕
        void HandleTriangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetToTriangleState();
        }

        //左鍵按下去
        void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _model.PressedPointer(e.X, e.Y);
        }

        //左鍵放開來
        void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _model.ReleasedPointer(e.X, e.Y);
        }

        //滑鼠移動
        void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _model.MovedPointer(e.X, e.Y);
        }

        //畫畫
        void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            // e.Graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用e.Graphics，因此，Adaptor不能重複使用
            // 每次都要重新new
            _model.Draw(new PresentationModel.WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        //Model（也就是圖案們）有變動
        void HandleModelChanged()
        {
            Invalidate(true);
            _undo.Enabled = _model.IsUndoEnabled;
            _redo.Enabled = _model.IsRedoEnabled;
        }
    }
}
