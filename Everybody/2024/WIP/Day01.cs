namespace Everybody2024;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        Dictionary<char, int> scores = new() { { 'A', 0 }, { 'B', 1 }, { 'C', 3 }, { 'D', 5 }, { 'x', 0 } };

        result = Input.Select(l => scores[l]).Sum();
        switch (WhichPart)
        {
            case 2:
                for (int i = 0; i < Input.Length; i += 2)
                    result += Input[i..(i + 2)].Count(l => l == 'x') switch { 0 => 2, _ => 0 };
                break;
            case 3:
                for (int i = 0; i < Input.Length; i += 3)
                    result += Input[i..(i + 3)].Count(l => l == 'x') switch { 0 => 6, 1 => 2, _ => 0 };
                break;
        }

        Output = result.ToString();
    }
}
