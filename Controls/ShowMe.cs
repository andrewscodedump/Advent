namespace Advent.Controls;
public partial class ShowMe : Form
{
    private Rectangle parent;
    public string Contents { set { Display.Text = value; Display.Select(0, 0); } }
    public ShowMe()
    {
        parent = Bounds;
        InitializeComponent();
    }

    public Rectangle Centre { set { parent = value; } }

    private void ShowMe_Activated(object sender, EventArgs e)
    {
        Left = parent.Left + (parent.Width / 2) - (Width / 2);
        Top = parent.Top - (parent.Height / 2) + (Height / 2);
    }
}
