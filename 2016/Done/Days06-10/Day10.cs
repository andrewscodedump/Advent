namespace Advent2016;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int requiredRobot = -1;
        bool somethingsChanged;

        Dictionary<int, (int first, int second)> robots = [];
        Dictionary<int, int> outputs = [];

        // Do all the gets first
        foreach (string rule in Inputs)
        {
            string[] words = rule.Split(' ');
            if (words[0] == "value")
            {
                int bot = int.Parse(words[5]);
                int input = int.Parse(words[1]);

                if (robots.TryGetValue(bot, out (int first, int second) value))
                    robots[bot] = (input, value.first);
                else
                    robots.Add(bot, (input, -1));
            }
        }

        // Then do the shuffles - keep looping until there's nothing more to do
        do
        {
            somethingsChanged = false;
            foreach (string rule in Inputs)
            {
                string[] words = rule.Split(' ');
                if (words[0] == "bot")
                {
                    int bot = int.Parse(words[1]);

                    if (robots.TryGetValue(bot, out (int first, int second) value) && value.second != -1)
                    {
                        int passLowTo = int.Parse(words[6]);
                        int low = Math.Min(value.first, value.second);
                        int passHighTo = int.Parse(words[11]);
                        int high = Math.Max(value.first, value.second);

                        if (words[5] == "bot")
                            if (robots.TryGetValue(passLowTo, out (int first, int second) value2))
                                robots[passLowTo] = (low, value2.first);
                            else
                                robots.Add(passLowTo, (low, -1));
                        else if (!outputs.TryAdd(passLowTo, low))
                            outputs[passLowTo] = low;
                        if (words[10] == "bot")
                            if (robots.TryGetValue(passHighTo, out (int first, int second) value3))
                                robots[passHighTo] = (high, value3.first);
                            else
                                robots.Add(passHighTo, (high, -1));
                        else if (!outputs.TryAdd(passHighTo, high))
                            outputs[passHighTo] = high;
                        robots[bot] = (-1, -1);

                        if ((TestMode && low == 2 && high == 5) || (!TestMode && low == 17 && high == 61))
                            requiredRobot = bot;

                        somethingsChanged = true;
                    }
                }
            }
        } while (somethingsChanged);

        Output = Part1 ? requiredRobot.ToString() : (outputs[0] * outputs[1] * outputs[2]).ToString();
    }
}
