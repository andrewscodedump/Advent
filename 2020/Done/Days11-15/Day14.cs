namespace Advent2020;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<long, char> mask = new();
        Dictionary<long, long> registers = new();
        List<long> floaters = new();
        long addressBase = 0;

        foreach (string line in Regex.Replace(Input, @"[mask e[\]]", "").Split(';'))
        {
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
            else if (WhichPart == 1)
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
                List<long> addresses = new() { long.Parse(line.Split('=')[0]) | addressBase };
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
