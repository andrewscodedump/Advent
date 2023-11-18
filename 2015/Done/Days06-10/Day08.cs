namespace Advent2015;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        string joined = string.Join("",Inputs);
        int shrinkLen = Inputs.Length * 2;
        int expandLen = Inputs.Length * 4;

        for (int i = 0; i < joined.Length - 1; i++)
        {
            if (joined[i] == '\\')
            {
                shrinkLen += joined[i + 1] == 'x' ? 3 : 1;
                expandLen += joined[i + 1] == 'x' ? 1 : 2;
                i += joined[i + 1] == 'x' ? 3 : 1;
            }
        }
        Output = (Part1 ? shrinkLen : expandLen).ToString();
    }
}
