namespace Advent2015;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int lighting = 0;
        int[,] grid = new int[1000, 1000];

        foreach (string instruction in InputSplit)
            SwitchLights(grid, ref lighting, ParseInstruction(instruction));

        Output = lighting.ToString();
    }

    private struct Action { public string Operation; public Point Point1; public Point Point2; };
    private static Action ParseInstruction(string baseInstruction)
    {
        string instruction = baseInstruction.Replace("toggle", "toggle  ").Replace("turn on", "turn on ");
        string[] coords = instruction[8..].Split(new string[] { "through ", "," }, StringSplitOptions.None);
        return new Action { Operation = instruction[..8].Trim(), Point1 = new Point(int.Parse(coords[0]), int.Parse(coords[1])), Point2 = new Point(int.Parse(coords[2]), int.Parse(coords[3])) };
    }

    private void SwitchLights(int[,] grid, ref int lighting, Action action)
    {
        int change = 0;
        for (int x = action.Point1.X; x <= action.Point2.X; x++)
            for (int y = action.Point1.Y; y <= action.Point2.Y; y++)
            {
                int currentSetting = grid[x, y];
                bool isLit = currentSetting > 0;
                switch (action.Operation)
                {
                    case "turn on":
                        grid[x, y] = WhichPart == 1 ? 1 : currentSetting + 1;
                        change += WhichPart == 1 && isLit ? 0 : 1;
                        break;
                    case "turn off":
                        grid[x, y] = WhichPart == 1 ? 0 : Math.Max(currentSetting - 1, 0);
                        change -= isLit ? 1 : 0;
                        break;
                    case "toggle":
                        grid[x, y] = WhichPart == 1 ? isLit ? 0 : 1 : currentSetting + 2;
                        change += WhichPart == 1 ? isLit ? -1 : 1 : 2;
                        break;
                    default:
                        break;
                }
            }
        lighting += change;
    }
}
