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
            // prepare clear button
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
            // prepare clear button
            //
            Button triangle = new Button();
            triangle.Text = "Triangle";
            triangle.Dock = DockStyle.Top;
            triangle.AutoSize = true;
            triangle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            triangle.Click += HandleTriangleButtonClick;
            triangle.DataBindings.Add(ENABLED, _model, "IsTriangleEnable");
            Controls.Add(triangle);
        }

        //Clear按鈕
        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
        }

        //Rectangle按鈕
        public void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetMode(DrawingMode.Rectangle);
        }

        //Triangle按鈕
        public void HandleTriangleButtonClick(object sender, System.EventArgs e)
        {
            _model.SetMode(DrawingMode.Triangle);
        }

        //左鍵按下去
        public void HandleCanvasPointerPressed(object sender, MouseEventArgs e)
        {
            _model.PressedPointer(e.X, e.Y);
        }

        //左鍵放開來
        public void HandleCanvasPointerReleased(object sender, MouseEventArgs e)
        {
            _model.ReleasedPointer(e.X, e.Y);
        }

        //滑鼠移動
        public void HandleCanvasPointerMoved(object sender, MouseEventArgs e)
        {
            _model.MovedPointer(e.X, e.Y);
        }

        //畫畫
        public void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            // e.Graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用e.Graphics，因此，Adaptor不能重複使用
            // 每次都要重新new
            _model.Draw(new PresentationModel.WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        //Model（也就是圖案們）有變動
        public void HandleModelChanged()
        {
            Invalidate(true);
        }
    }
}
