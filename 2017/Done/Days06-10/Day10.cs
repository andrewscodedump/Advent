namespace Advent2017;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        string denseHash = "";
        int skip = 0, pos = 0, len = TestMode && Part1 ? 5 : 256, repeats = Part1 ? 1 : 64;
        List<int> work = new(len);
        for (int i = 0; i < len; i++)
            work.Add(i);

        byte[] input = new byte[Input.Length];
        input = Part1
            ? Array.ConvertAll(Input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), byte.Parse)
            : Encoding.ASCII.GetBytes(Input + (char)17 + (char)31 + (char)73 + (char)47 + (char)23);

        for (int j = 0; j < repeats; j++)
            for (int i = 0; i < input.Length; i++)
                Manipulate(work, input[i], ref pos, ref skip);

        if (Part2)
            for (int i = 0; i < 16; i++)
            {
                int hash = 0;
                for (int j = 0; j < 16; j++)
                    hash ^= work[(i * 16) + j];
                denseHash += hash.ToString("x2");
            }

        Output = Part1 ? (work[0] * work[1]).ToString() : denseHash;
    }

    private static void Manipulate(List<int> work, int length, ref int pos, ref int skip)
    {
        for (int i = 0; i < length / 2; i++)
        {
            int temp = work[(pos - i + length - 1) % work.Count];
            work[(pos - i + length - 1) % work.Count] = work[(pos + i) % work.Count];
            work[(pos + i) % work.Count] = temp;
        }
        pos = (pos + length + skip) % work.Count;
        skip++;
    }
}
