namespace Advent2016;

public partial class Day02 : Advent.Day
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    public override void DoWork()
    {
        int row = Part1 ? 1 : 2;
        int col = Part1 ? 1 : 0;
        string code = string.Empty;

        foreach (string sequence in InputSplit)
        {
            for (int letter = 0; letter < sequence.Length; letter++)
            {
                switch (sequence.Substring(letter, 1))
                {
                    case "U":
                        if ((Part1 && row > 0)
                            || (Part2 && !((row == 0 && col == 2) || (row == 1 && col == 1) || (row == 2 && col == 0) || (row == 1 && col == 3) || (row == 2 && col == 4))))
                            row--;
                        break;
                    case "D":
                        if ((Part1 && row < 2)
                            || (Part2 && !((row == 2 && col == 0) || (row == 3 && col == 1) || (row == 4 && col == 2) || (row == 3 && col == 3) || (row == 2 && col == 4))))
                            row++;
                        break;
                    case "L":
                        if ((Part1 && col > 0)
                            || (Part2 && !((row == 0 && col == 2) || (row == 1 && col == 1) || (row == 2 && col == 0) || (row == 3 && col == 1) || (row == 4 && col == 2))))
                            col--;
                        break;
                    case "R":
                        if ((Part1 && col < 2)
                            || (Part2 && !((row == 0 && col == 2) || (row == 1 && col == 3) || (row == 2 && col == 4) || (row == 3 && col == 3) || (row == 4 && col == 2))))
                            col++;
                        break;
                    default:
                        break;
                }
            }

            if (Part1)
                code += ((row * 3) + col + 1).ToString();
            else
                switch (row)
                {
                    case 0:
                    case 1:
                        code += (col + 1).ToString();
                        break;
                    case 2:
                        code += (col + 5).ToString();
                        break;
                    case 3:
                        code += (col + 9).ToString("X1");
                        break;
                    case 4:
                        code += "D";
                        break;
                    default:
                        break;
                }
        }

        Output = code.ToString();
    }
}
