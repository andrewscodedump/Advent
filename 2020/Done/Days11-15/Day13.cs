namespace Advent2020;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        if (Part1)
        {
            List<long> ids = InputNumbers[1].ToList();
            // next time for bus is bus id - (current time mod bus id)
            List<(long id, long wait)> waits = ids.Select(id => (id, id - (InputNumbers[0][0] % id))).ToList();
            (long id, long wait) = TupleExtensions.ToValueTuple(waits.OrderBy(w => w.wait).First().ToTuple());
            Output = (id * wait).ToString();
        }
        else
        {
            int idx = 0; long result = 1, multiplier = 1;
            foreach (string id in Inputs[1].Split(','))
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
