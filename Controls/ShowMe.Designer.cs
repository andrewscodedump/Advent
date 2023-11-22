namespace Advent.Controls;

partial class ShowMe
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
        Display = new TextBox();
        SuspendLayout();
        // 
        // Display
        // 
        Display.Dock = DockStyle.Fill;
        Display.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
        Display.Location = new Point(0, 0);
        Display.Multiline = true;
        Display.Name = "Display";
        Display.ReadOnly = true;
        Display.ScrollBars = ScrollBars.Both;
        Display.Size = new Size(314, 484);
        Display.TabIndex = 0;
        // 
        // ShowMe
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(314, 484);
        Controls.Add(Display);
        Name = "ShowMe";
        Text = "ShowMe";
        Activated += ShowMe_Activated;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox Display;
}