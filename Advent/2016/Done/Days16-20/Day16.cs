namespace Advent2016;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        int diskSize = TestMode ? 20 : Part1 ? 272 : 35651584;
        Output = CheckSumify(Dragonify(Input, diskSize)).ToString();
    }

    private static string Dragonify(string input, int requiredLength)
    {
        char[] arr = input.ToCharArray();
        Array.Reverse(arr);
        string reverse = new(arr);
        reverse = reverse.Replace('0', ' ').Replace('1', '0').Replace(' ', '1');
        string work = input + "0" + reverse;

        // Recurse until long enough
        return work.Length >= requiredLength ? work[..requiredLength] : Dragonify(work, requiredLength);
    }

    private static string CheckSumify(string input)
    {
        StringBuilder work = new();
        for (int i = 0; i < input.Length; i += 2)
            if (input[i] == input[i + 1])
                work.Append('1');
            else
                work.Append('0');

        // Recurse until odd number
        return work.Length % 2 == 1 ? work.ToString() : CheckSumify(work.ToString());
    }
}
