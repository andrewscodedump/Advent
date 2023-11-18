namespace Advent2019;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        IntCode code = new(InputNumbersSingle);
        (int x, int y) pos = (0, 0);
        int id;
        Dictionary<(int x, int y), int> screen = new();
        int batX = 0, ballX = 0, score = 0, move = 0;

        #endregion Setup Variables and Parse Inputs

        do
        {
            code.RunCodeWithNoReset(new long[] { move });
            if (code.CodeComplete) break;
            pos.x = (int)code.Output;
            code.RunCodeWithNoReset();
            if (code.CodeComplete) break;
            pos.y = (int)code.Output;
            code.RunCodeWithNoReset();
            if (code.CodeComplete) break;
            id = (int)code.Output;
            if (pos == (-1, 0))
                score = id;
            else
            {
                screen[pos] = id;
                if (id == 3) batX = pos.x;
                if (id == 4) ballX = pos.x;
            }
            move = batX == ballX ? 0 : batX > ballX ? -1 : 1;
        } while (!code.CodeComplete);

        // printScreen(screen);

        Output = (Part1 ? screen.Values.Count(x => x == 2) : score).ToString();
    }

    #region Private Classes and Methods

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private static void PrintScreen(Dictionary<(int x, int y), int> screen)
    {
        int maxX = screen.Keys.Max(x => x.x), maxY = screen.Keys.Max(x => x.y);
        for (int y = 0; y <= maxY; y++)
        {
            StringBuilder s = new();
            for (int x = 0; x <= maxX; x++)
            {
                switch (screen[(x, y)])
                {
                    case 0:
                        s.Append(' ');
                        break;
                    case 1:
                        s.Append('*');
                        break;
                    case 2:
                        s.Append('x');
                        break;
                    case 3:
                        s.Append('_');
                        break;
                    case 4:
                        s.Append('.');
                        break;
                }
            }
            Debug.Print(s.ToString());
        }
    }

    #endregion Private Classes and Methods
}
