namespace Advent
{
    partial class AWInputBox
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
            this.text = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            // 
            // text
            // 
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text.Location = new System.Drawing.Point(14, 79);
            this.text.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.text.MaxLength = 0;
            this.text.Multiline = true;
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(1061, 400);
            this.text.TabIndex = 0;
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ok.Location = new System.Drawing.Point(37, 509);
            this.ok.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(212, 73);
            this.ok.TabIndex = 1;
            this.ok.Text = "&OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(266, 509);
            this.cancel.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(212, 73);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "&Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(23, 12);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(1052, 58);
            this.label.TabIndex = 3;
            // 
            // AWInputBox
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(1094, 591);
            this.Controls.Add(this.label);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.text);
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "AWInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Please Enter Text";

        }

        #endregion

        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label label;
    }
}