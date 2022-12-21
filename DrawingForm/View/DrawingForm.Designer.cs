
namespace DrawingForm
{
    partial class DrawingForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this._selectShapeString = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _selectShapeString
            // 
            this._selectShapeString.AutoSize = true;
            this._selectShapeString.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._selectShapeString.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._selectShapeString.Location = new System.Drawing.Point(0, 422);
            this._selectShapeString.Name = "_selectShapeString";
            this._selectShapeString.Size = new System.Drawing.Size(289, 28);
            this._selectShapeString.TabIndex = 0;
            this._selectShapeString.Text = "THERE IS A TEXT BOX HERE";
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._selectShapeString);
            this.Name = "DrawingForm";
            this.Text = "DrawingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _selectShapeString;
    }
}

