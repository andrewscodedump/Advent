namespace Advent2018;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        int output, sum = 1;
        List<(string op, int[] args)> code = new();
        int[] regs = new int[] { WhichPart == 1 ? 0 : 1, 0, 0, 0, 0, 0 };
        int ipVal = 0, ipReg = 0;

        for (int i = 0; i < InputSplit.Length; i++)
        {
            string[] bits = InputSplit[i].Split( ' ');
            if (bits[0] == "#ip")
                ipReg = int.Parse(bits[1]);
            else
                code.Add((bits[0], new int[3] { int.Parse(bits[1]), int.Parse(bits[2]), int.Parse(bits[3]) }));
        }

        #region Naive Solutions
        /* This is the basic solution just using the interpreter
        do
            (ipVal, regs) = RunCode2(code, (ipVal, ipReg, regs));
        while (ipVal < code.Count);

        Output = regs[0].ToString();
        */

        /* This is an interim solution, using a straight C# translation of the mcode
        */

        // This is uses the interpreter to get inputs for the second half of the code, then an optimised solution for the second half
        // (derived by working out what the code is actually trying to do)
        #endregion Naive Solutions

        do
        {
            (ipVal, regs) = RunCode(code, (ipVal, ipReg, regs));
            output = ipVal == 1 ? regs[4] : ipVal >= code.Count ? regs[0] : 0;
        } while ((TestMode && ipVal < code.Count) || (!TestMode && ipVal != 1));

        for (int i = 2; i <= output; i++)
            if (output % i == 0)
                sum += i;

        Output = (TestMode ? output : sum).ToString();
    }
}
