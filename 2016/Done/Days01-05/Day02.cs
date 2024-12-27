namespace Advent2016;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int row = Part1 ? 1 : 2;
        int col = Part1 ? 1 : 0;
        StringBuilder code = new();

        foreach (string sequence in Inputs)
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
                code.Append((row * 3) + col + 1);
            else
                switch (row)
                {
                    case 0:
                    case 1:
                        code.Append(col + 1);
                        break;
                    case 2:
                        code.Append(col + 5);
                        break;
                    case 3:
                        code.Append((col + 9).ToString("X1"));
                        break;
                    case 4:
                        code.Append('D');
                        break;
                    default:
                        break;
                }
        }

        Output = code.ToString();
    }
}
