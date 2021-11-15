namespace Advent;

public partial class AWInputBox : Form
{
    /// <summary>
    /// Title of Input Box
    /// </summary>
    public string Title
    {
        set => Text = value;
    }

    /// <summary>
    /// Text in input area
    /// </summary>
    public string Input
    {
        get => text.Text;
        set => text.Text = value;
    }

    public string Label
    {
        set => label.Text = value;
    }

    /// <summary>
    /// Way in which box was closed
    /// </summary>
    public DialogResult Result { get; private set; }

    /// <summary>
    /// Show the form
    /// </summary>
    new public void Show() => ShowDialog();

    public AWInputBox() => InitializeComponent();

    private void Ok_Click(object sender, EventArgs e)
    {
        Result = text.Text == string.Empty ? DialogResult.Ignore : DialogResult.OK;
        Close();
    }

    private void Cancel_Click(object sender, EventArgs e) => Result = DialogResult.Cancel;
}

public static class AWInputBoxDirect
{
    public static string AWInputBox(string Title, string Prompt, string DefaultValue)
    {
        using AWInputBox inputBox = new()
        {
            Title = Title,
            Input = DefaultValue,
            Label = Prompt
        };
        inputBox.Show();
        return inputBox.Result == DialogResult.Cancel || inputBox.Result == DialogResult.Ignore ? DefaultValue : inputBox.Input;
    }
}
