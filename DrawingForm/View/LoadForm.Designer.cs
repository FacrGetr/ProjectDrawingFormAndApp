
namespace DrawingForm
{
    partial class LoadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonConfirm = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._textLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.AutoSize = true;
            this._buttonConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonConfirm.Font = new System.Drawing.Font("微软雅黑", 36F);
            this._buttonConfirm.Location = new System.Drawing.Point(134, 295);
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.Size = new System.Drawing.Size(133, 72);
            this._buttonConfirm.TabIndex = 1;
            this._buttonConfirm.Text = "確定";
            this._buttonConfirm.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Font = new System.Drawing.Font("微软雅黑", 36F);
            this._buttonCancel.Location = new System.Drawing.Point(537, 295);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(133, 72);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "取消";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _textLabel
            // 
            this._textLabel.AutoSize = true;
            this._textLabel.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textLabel.Location = new System.Drawing.Point(207, 98);
            this._textLabel.Name = "_textLabel";
            this._textLabel.Size = new System.Drawing.Size(363, 62);
            this._textLabel.TabIndex = 4;
            this._textLabel.Text = "你確定要讀檔嗎";
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._textLabel);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonConfirm);
            this.Name = "LoadForm";
            this.Text = "讀檔";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _buttonConfirm;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Label _textLabel;
    }
}