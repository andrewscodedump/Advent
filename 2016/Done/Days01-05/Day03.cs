namespace Advent2016;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        int valid = 0, a, b, c;

        if (Part1)
            foreach (string triangle in InputSplit)
            {
                a = int.Parse(triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                b = int.Parse(triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                c = int.Parse(triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2]);
                if (a + b > c && b + c > a && a + c > b) valid++;
            }
        else
            for (int i = 0; i < InputSplit.Length; i += 3)
                for (int j = 0; j < 3; j++)
                {
                    a = int.Parse(InputSplit[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[j]);
                    b = int.Parse(InputSplit[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[j]);
                    c = int.Parse(InputSplit[i + 2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[j]);
                    if (a + b > c && b + c > a && a + c > b) valid++;
                }
        Output = valid.ToString();
    }
}
