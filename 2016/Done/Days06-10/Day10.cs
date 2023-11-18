namespace Advent2016;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int requiredRobot = -1;
        bool somethingsChanged;

        Dictionary<int, (int first, int second)> robots = new();
        Dictionary<int, int> outputs = new();

        // Do all the gets first
        foreach (string rule in Inputs)
        {
            string[] words = rule.Split(' ');
            if (words[0] == "value")
            {
                int bot = int.Parse(words[5]);
                int input = int.Parse(words[1]);

                if (robots.ContainsKey(bot))
                    robots[bot] = (input, robots[bot].first);
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

                    if (robots.ContainsKey(bot) && robots[bot].second != -1)
                    {
                        int passLowTo = int.Parse(words[6]);
                        int low = Math.Min(robots[bot].first, robots[bot].second);
                        int passHighTo = int.Parse(words[11]);
                        int high = Math.Max(robots[bot].first, robots[bot].second);

                        if (words[5] == "bot")
                            if (robots.ContainsKey(passLowTo))
                                robots[passLowTo] = (low, robots[passLowTo].first);
                            else
                                robots.Add(passLowTo, (low, -1));
                        else if (outputs.ContainsKey(passLowTo))
                            outputs[passLowTo] = low;
                        else
                            outputs.Add(passLowTo, low);

                        if (words[10] == "bot")
                            if (robots.ContainsKey(passHighTo))
                                robots[passHighTo] = (high, robots[passHighTo].first);
                            else
                                robots.Add(passHighTo, (high, -1));
                        else if (outputs.ContainsKey(passHighTo))
                            outputs[passHighTo] = high;
                        else
                            outputs.Add(passHighTo, high);

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
