namespace Advent2019;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        IntCode code = new(InputNumbers[0]);
        int counter = 0, result;
        int targetSize = 100, gridSize = 50; //50
        int x, y = 0;
        bool foundIt = false;

        #endregion Setup Variables and Parse Inputs

        for (x = 0; x < gridSize; x++)
            for (y = 0; y < gridSize; y++)
            {
                code.RunCode([x, y]);
                SimpleMap[(x, y)] = code.Output == 1 ? '#' : '.';
                if (code.Output == 1) counter++;
            }
        //DrawMap(false, true);
        int prevMinX = x;
        do
        {
            x = prevMinX;
            do
            {
                code.RunCode([x, y]);
                if (code.Output == 1)
                {
                    prevMinX = x;
                    code.RunCode([x + targetSize - 1, y]);
                    bool test1 = code.Output == 1;
                    code.RunCode([x + targetSize, y]);
                    bool test2 = code.Output == 0;
                    code.RunCode([x, y + targetSize - 1]);
                    bool test3 = code.Output == 1;
                    code.RunCode([x, y + targetSize]);
                    bool test4 = code.Output == 0;
                    if (foundIt = test1 && test2 && test3 && test4) break;
                    if (test2) break;
                }
                x++;
            } while (!foundIt);
            if (!foundIt) y++;
        } while (!foundIt);
        result = (x * 10000) + y;

        Output = (Part1 ? counter : result).ToString();
    }
}
