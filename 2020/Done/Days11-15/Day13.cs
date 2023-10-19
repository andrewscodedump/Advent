namespace Advent2020;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        if (Part1)
        {
            List<int> ids = InputSplit[1].Split(',').Where(id => id != "x").Select(int.Parse).ToList();
            // next time for bus is bus id - (current time mod bus id)
            List<(int id, int wait)> waits = ids.Select(id => (id, id - (int.Parse(InputSplit[0]) % id))).ToList();
            (int id, int wait) = TupleExtensions.ToValueTuple(waits.OrderBy(w => w.wait).First().ToTuple());
            Output = (id * wait).ToString();
        }
        else
        {
            int idx = 0; long result = 1, multiplier = 1;
            foreach (string id in InputSplit[1].Split(','))
            {
                if (id != "x")
                {
                    result = DoCalc(result, multiplier, long.Parse(id), idx);
                    multiplier *= long.Parse(id);
                }
                idx++;
            }
            Output = result.ToString();
        }
    }

    static long DoCalc(long result, long multiplier, long test, int increment)
    {
        while (!(((result += multiplier) + increment) % test == 0)) { }
        return result;
    }
}
