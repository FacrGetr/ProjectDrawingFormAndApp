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
        const string ENABLED = "Enabled";
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
            Button clear = new Button();
            clear.Text = "Clear";
            clear.Dock = DockStyle.Top;
            clear.AutoSize = true;
            clear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            clear.Click += HandleClearButtonClick;
            clear.DataBindings.Add(ENABLED, _model, "IsClearEnable");
            Controls.Add(clear);
            //
            // prepare rectangle button
            //
            Button rectangle = new Button();
            rectangle.Text = "Rectangle";
            rectangle.Dock = DockStyle.Top;
            rectangle.AutoSize = true;
            rectangle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rectangle.Click += HandleRectangleButtonClick;
            rectangle.DataBindings.Add(ENABLED, _model, "IsRectangleEnable");
            Controls.Add(rectangle);
            //
            // prepare triangle button
            //
            Button triangle = new Button();
            triangle.Text = "Triangle";
            triangle.Dock = DockStyle.Top;
            triangle.AutoSize = true;
            triangle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            triangle.Click += HandleTriangleButtonClick;
            triangle.DataBindings.Add(ENABLED, _model, "IsTriangleEnable");
            Controls.Add(triangle);
            //
            // prepare line button
            //
            Button line = new Button();
            line.Text = "Line";
            line.Dock = DockStyle.Top;
            line.AutoSize = true;
            line.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            line.Click += HandleLineButtonClick;
            line.DataBindings.Add(ENABLED, _model, "IsLineEnable");
            Controls.Add(line);
            //
            //ToolStrip
            //
            ToolStrip ts = new ToolStrip();
            Controls.Add(ts);
            //
            //undo
            //
            ToolStripButton undo = new ToolStripButton("Undo", null, HandleUndoButtonClick);
            undo.Enabled = false;
            ts.Items.Add(undo);
            //
            //redo
            //
            ToolStripButton redo = new ToolStripButton("Redo", null, HandleRedoButtonClick);
            redo.Enabled = false;
            ts.Items.Add(redo);
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
        //Clear按鈕
        void HandleLineButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
        }

        //Clear按鈕
        void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
        }

        //Rectangle按鈕
        void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetMode(DrawingMode.Rectangle);
        }

        //Triangle按鈕
        void HandleTriangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetMode(DrawingMode.Triangle);
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
        }
    }
}
