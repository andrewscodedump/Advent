namespace Everybody2025;

public class Day05 : Advent.Day
{
    public override void DoWork()
    {
        Sword[] swords = [.. InputNumbers.Select(l => new Sword(l))];
        switch (WhichPart)
        {
            case 1:
                Output = swords[0].Quality.ToString();
                break;
            case 2:
                Output = (swords.Max(s => s.Quality) - swords.Min(s => s.Quality)).ToString();
                break;
            case 3:
                Output = swords.OrderDescending().Select((s, i) => (i + 1) * s.Id).Sum().ToString();
                break;
        }
    }

    private sealed class Sword : IComparable<Sword>
    {
        public long Id { get; private set; }
        public long Quality { get; private set; }
        public long[] Levels { get; private set; }
        public Sword(long[] input)
        {
            Id=input[0];
            List<long[]> fishBone = [[0, input[1], 0]];
            StringBuilder result = new(input[1].ToString());
            bool handled = false;
            for (int i = 2; i < input.Length; i++)
            {
                long next = input[i];
                foreach (long[] line in fishBone)
                {
                    if (next < line[1] && line[0] == 0)
                    {
                        line[0] = next;
                        handled = true;
                        break;
                    }
                    if (next > line[1] && line[2] == 0)
                    {
                        line[2] = next;
                        handled = true;
                        break;
                    }
                }
                if (!handled)
                {
                    fishBone.Add([0, next, 0]);
                    result.Append(next);
                }
                handled = false;
            }
            Levels = [.. fishBone.Select(l => long.Parse(string.Concat(l).Replace("0","")))];
            Quality = long.Parse(result.ToString());
        }

        public int CompareTo(Sword other)
        {
            if (Quality.CompareTo(other.Quality) != 0)
            {
                return Quality.CompareTo(other.Quality);
            }
            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i].CompareTo(other.Levels[i]) != 0)
                    return Levels[i].CompareTo(other.Levels[i]);
            }
            return Id.CompareTo(other.Id);
        }
    }
}

