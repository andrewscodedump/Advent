namespace Advent2020;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<long, char> mask = [];
        Dictionary<long, long> registers = [];
        List<long> floaters = [];
        long addressBase = 0;

        foreach (string linein in Inputs)
        {
            string line = Regex.Replace(linein, @"[mask e[\]]", "");
            if (line.StartsWith('='))
            {
                mask.Clear();
                floaters.Clear();
                addressBase = 0;
                for (int i = 1; i < line.Length; i++)
                {
                    if (line[i] == 'X')
                        floaters.Add((long)Math.Pow(2, 36 - i));
                    else
                    {
                        if (line[i] == '1')
                            addressBase += (long)Math.Pow(2, 36 - i);
                        mask.Add((long)Math.Pow(2, 36 - i), line[i]);
                    }
                }
            }
            else if (Part1)
            {
                long reg = long.Parse(line.Split('=')[0]), val = long.Parse(line.Split('=')[1]);
                foreach ((long bit, char operation) in mask)
                    if (operation == '1')
                        val |= bit;
                    else if ((val & bit) != 0)
                        val -= bit;
                registers[reg] = val;
            }
            else
            {
                List<long> addresses = [long.Parse(line.Split('=')[0]) | addressBase];
                foreach (long floater in floaters)
                    foreach (long address in new List<long>(addresses))
                        if ((address & floater) == 0)
                            addresses.Add(address + floater);
                        else
                            addresses.Add(address - floater);
                foreach (long address in addresses)
                    registers[address] = long.Parse(line.Split('=')[1]);
            }
        }
        Output = registers.Values.Sum().ToString();
    }
}
