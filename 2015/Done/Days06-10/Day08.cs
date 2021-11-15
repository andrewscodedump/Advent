namespace Advent2015;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        int shrinkLen = InputSplit.Length * 2;
        int expandLen = InputSplit.Length * 4;

        for (int i = 0; i < Input.Length - 1; i++)
        {
            if (Input[i] == '\\')
            {
                shrinkLen += Input[i + 1] == 'x' ? 3 : 1;
                expandLen += Input[i + 1] == 'x' ? 1 : 2;
                i += Input[i + 1] == 'x' ? 3 : 1;
            }
        }
        Output = (WhichPart == 1 ? shrinkLen : expandLen).ToString();
    }
}
