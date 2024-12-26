namespace Advent2017;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        int numberOfDancers = TestMode ? 5 : 16, numberOfDances = Part1 ? 1 : 1000000000;
        string[] steps = Input.Split(',');
        char[] originalDancers = Enumerable.Range(97, numberOfDancers).Select(d => (char)d).ToArray(), dancers = originalDancers.ToArray();
        string pos1s = string.Empty, pos2s = string.Empty;
        int pos1 = 0, pos2 = 0;

        for (int dance = 1; dance <= numberOfDances; dance++)
            foreach (string step in steps)
            {
                (pos1s, pos2s) = (step[1..].Split('/')[0], step.Contains('/') ? step[1..].Split('/')[1] : "99");
                (pos1, pos2) = (int.TryParse(pos1s, out pos1) ? pos1 : Array.IndexOf(dancers, pos1s[0]), int.TryParse(pos2s, out pos2) ? pos2 : Array.IndexOf(dancers, pos2s[0]));
                (pos1, pos2) = (Math.Min(pos1, pos2), Math.Max(pos1, pos2));

                switch (step[0])
                {
                    case 's':
                        char[] temp = dancers[(numberOfDancers - pos1)..^0];
                        Array.Copy(dancers, 0, dancers, pos1, numberOfDancers - pos1);
                        Array.Copy(temp, dancers, pos1);
                        break;
                    case 'x': case 'p': (dancers[pos1], dancers[pos2]) = (dancers[pos2], dancers[pos1]); break;
                    default: break;
                }

                if (dancers.SequenceEqual(originalDancers)) dance = numberOfDances - (numberOfDances % dance);
            }
        Output = new string(dancers);
    }
}
