namespace Advent2018;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        List<(string op, int[] args)> code = new();
        int[] regs = new int[] { 0, 0, 0, 0, 0, 0 };
#pragma warning disable CS0168 // Variable is declared but never used
        int ipVal, ipReg;
#pragma warning restore CS0168 // Variable is declared but never used

        for (int i = 0; i < InputSplit.Length; i++)
        {
            string[] bits = InputSplit[i].Split(new char[] { ' ', '[', ']', ',', '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (bits[0] == "#ip")
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                ipReg = int.Parse(bits[1]);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            else
                code.Add((bits[0], new int[3] { int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3]) }));
        }
        int foundValue, prevValue = 0;
        HashSet<int> validKeys = new();

        #endregion Setup Variables and Parse Inputs

        #region Naive Solution
        // Run interpreter - OK for part 1, way too slow for part 2.
        /*
        do
        {
            (ipVal, regs) = RunCode(code, (ipVal, ipReg, regs));
            if (ipVal == 28)
            {
                foundValue = regs[1];
                if (validKeys.Contains(foundValue)) break;
                validKeys.Add(foundValue);
                prevValue = foundValue;
                if (WhichPart == 1) break;
            }
        }
        while (true);
        //*/
        #endregion Naive Solution

        // Transpiled solution - machine code statements directly translated into C#
        do
        {
            regs[1] = 123;
        l1: regs[1] &= 456;
            if (regs[1] != 72)
                goto l1;
            regs[1] = 0;
        l6: regs[3] = regs[1] | 0x10000;
            regs[1] = 6780005;
        l8: regs[2] = regs[3] & 0xFF;
            regs[1] += regs[2];
            regs[1] &= 0xFFFFFF;
            regs[1] *= 65899;
            regs[1] &= 0xFFFFFF;
            if (regs[3] < 256)
                goto l28;
            regs[2] = 0;
        l18: regs[5] = regs[2] + 1;
            regs[5] *= 256;
            if (regs[5] > regs[3])
                goto l26;
            regs[2]++;
            goto l18;
        l26: regs[3] = regs[2];
            goto l8;
        l28: foundValue = regs[1];
            if (WhichPart == 1) break;
            if (validKeys.Contains(foundValue)) break;
            validKeys.Add(foundValue);
            prevValue = foundValue;
            goto l6;
        } while (true);

        Output = (WhichPart == 1 ? foundValue : prevValue).ToString();
    }
}
