using System.Numerics;

namespace Advent2022;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        Monkey[] monkeys= GetDetails();
        WatchMonkeys(monkeys, Part1 ? 20 : 10_000, Part1);
        Output = GetTopTwo(monkeys).ToString();

    }

    private class Monkey
    {
        public Monkey() => Items = new();
        public Queue<long> Items { get; set; }
        public (string type, long value) Operation { get; set; }
        public long Test { get; set; }
        public long TrueTarget { get; set; }
        public long FalseTarget { get; set; }
        public long Throws { get; set; }
        public (long, long) ProcessNextItem(bool Part1, long multiplier)
        {
            long item = Items.Dequeue();
            item = Operation.type switch
            {
                "square" => item * item,
                "*" => item * Operation.value,
                "+" => item + Operation.value,
                _ => item
            };
            if (Part1) item /= 3;
            item %= multiplier;
            long target = item % Test == 0 ? TrueTarget : FalseTarget;
            Throws++;
            return (item, target);
        }

        public void AddItem(long Item)=>Items.Enqueue(Item);
    }

    private Monkey[] GetDetails()
    {
        long numMonkeys = InputSplit.Where(l=>l.StartsWith("Monkey")).Count();
        Monkey[] monkeys = new Monkey[numMonkeys];
        for (long i = 0; i < numMonkeys; i++) monkeys[i] = new();
        Monkey monkey = new();
        foreach (string line in InputSplit)
        {
            if (line == "") continue;
            string[] bits = line.Split(" ,:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            switch (bits[0])
            {
                case "Monkey":
                    monkey = monkeys[long.Parse(bits[1])];
                    break;
                case "Starting":
                    bits[2..].Select(i => long.Parse(i)).ToList().ForEach(i => monkey.AddItem(i));
                    break;
                case "Operation":
                    if (bits[4] == "*" && bits[5] == "old")
                        monkey.Operation = ("square", 0);
                    else
                        monkey.Operation = (bits[4], long.Parse(bits[5]));
                    break;
                case "Test":
                    monkey.Test = long.Parse(bits[3]);
                    break;
                case "If":
                    if (bits[1] == "true")
                        monkey.TrueTarget = long.Parse(bits[5]);
                    else
                        monkey.FalseTarget = long.Parse(bits[5]);
                    break;
            }
        }
        return monkeys;
    }

    private static void WatchMonkeys(Monkey[] monkeys, long rounds, bool Part1)
    {
        long multiplier = monkeys.Aggregate(1, (long a, Monkey x) => a * x.Test);
        for (long round = 1;round<=rounds;round++)
        {
            foreach (Monkey monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    (long item, long target) = monkey.ProcessNextItem(Part1, multiplier);
                    monkeys[target].AddItem(item);
                }
            }
        }
    }

    private static long GetTopTwo(Monkey[] monkeys)
    {
        var topTwo = monkeys.OrderByDescending(m=>m.Throws).Take(2).ToArray();
        return topTwo[0].Throws * topTwo[1].Throws;
    }
}
