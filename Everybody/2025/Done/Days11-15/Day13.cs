namespace Everybody2025;

public class Day13 : Advent.Day
{
    public override void DoWork()
    {
        List<int> wheel = [1], end = [];
        for (int i = 0; i < Inputs.Length; i++)
        {
            string range = Inputs[i];
            int first = int.Parse(range.Split('-')[0]);
            int last = WhichPart == 1 ? first : int.Parse(range.Split("-")[1]);
            if (i % 2 == 0) wheel.AddRange(Enumerable.Range(first, last - first + 1));
            else end.AddRange(Enumerable.Range(first, last - first + 1));
        }
        end.Reverse();
        wheel.AddRange(end);
        long turns = long.Parse(string.Concat(Enumerable.Repeat("2025", WhichPart)));
        long index = turns % wheel.Count;
        Output = wheel[(int)index].ToString();
    }
}