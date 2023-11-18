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
            text = new TextBox();
            ok = new Button();
            cancel = new Button();
            label = new Label();
            SuspendLayout();
            // 
            // text
            // 
            text.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            text.Location = new Point(6, 29);
            text.MaxLength = 0;
            text.Multiline = true;
            text.Name = "text";
            text.Size = new Size(439, 149);
            text.TabIndex = 0;
            // 
            // ok
            // 
            ok.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ok.Location = new Point(15, 186);
            ok.Name = "ok";
            ok.Size = new Size(87, 27);
            ok.TabIndex = 1;
            ok.Text = "&OK";
            ok.UseVisualStyleBackColor = true;
            ok.Click += Ok_Click;
            // 
            // cancel
            // 
            cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Location = new Point(110, 186);
            cancel.Name = "cancel";
            cancel.Size = new Size(87, 27);
            cancel.TabIndex = 2;
            cancel.Text = "&Cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.Click += Cancel_Click;
            // 
            // label
            // 
            label.Location = new Point(9, 4);
            label.Margin = new Padding(1, 0, 1, 0);
            label.Name = "label";
            label.Size = new Size(433, 21);
            label.TabIndex = 3;
            // 
            // AWInputBox
            // 
            AcceptButton = ok;
            CancelButton = cancel;
            ClientSize = new Size(450, 216);
            Controls.Add(label);
            Controls.Add(cancel);
            Controls.Add(ok);
            Controls.Add(text);
            Name = "AWInputBox";
            StartPosition = FormStartPosition.Manual;
            Text = "Please Enter Text";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox text;
        private Button ok;
        private Button cancel;
        private Label label;
    }
}