namespace Advent2019;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        Dictionary<(int, int), int> hull = new();
        IntCode code = new(Input);
        (int x, int y) curPos = (0, 0), min = (0, 0), max = (0, 0);
        char curDir = '^';
        int curColour = WhichPart - 1;

        #endregion Setup Variables and Parse Inputs

        do
        {
            code.RunCodeWithNoReset(new long[] { curColour }); if (code.CodeComplete) break;
            hull[curPos] = (int)code.Output;
            code.RunCodeWithNoReset(); if (code.CodeComplete) break;
            curDir = turns[(curDir, code.Output == 0 ? 'L' : 'R')];
            curPos = (curPos.x + Directions[curDir].Item1, curPos.y + Directions[curDir].Item2);
            min = (Math.Min(min.x, curPos.x), Math.Min(min.y, curPos.y)); max = (Math.Max(max.x, curPos.x), Math.Max(max.y, curPos.y));
            curColour = hull.ContainsKey(curPos) ? hull[curPos] : 0;
        } while (true);

        PrintDictionary(hull, min, max);
        string outputString = WhichPart == 2 && !BatchRun ? GetOutput() : string.Empty;

        Output = WhichPart == 1 ? hull.Count.ToString() : outputString;
    }

    #region Private Classes and Methods

    private static void PrintDictionary(Dictionary<(int x, int y), int> dict, (int x, int y) lowerBounds, (int x, int y) upperBounds)
    {
        for (int y = upperBounds.y; y >= lowerBounds.y; y--)
        {
            StringBuilder line = new();
            for (int x = lowerBounds.x; x <= upperBounds.x; x++)
                line.Append((dict.ContainsKey((x, y)) ? dict[(x, y)] : 0) == 0 ? " " : "█");
            Debug.Print(line.ToString());
        }
    }

    private static string GetOutput()
    {
        return AWInputBox("Get Output", "Enter string value displayed in output window", "");
    }
    #endregion Private Classes and Methods
}
