namespace Advent2020;

public partial class Day25 : Advent.Day
{
    public override void DoWork()
    {
        (long pk, long loop)[] values = Inputs.Select(long.Parse).Select(p => (p, FindLoop(p))).ToArray();
        long[] keys = values.Zip(values.Reverse(), ((long pk, long loop) first, (long pk, long loop) second) => GetEK(first.pk, second.loop)).ToArray();
        Output = keys[0] != keys[1] ? "Oops - something went  wrong" : keys[0].ToString();
    }

    private static long FindLoop(long pk)
    {
        long value = 1, loopNo;
        for (loopNo = 0; value != pk; loopNo++) value = DoCalc(value, 7);
        return loopNo;
    }

    private static long GetEK(long pk, long loopNo)
    {
        long value = 1;
        for (long i = 0; i < loopNo; i++) value = DoCalc(value, pk);
        return value;
    }

    private static long DoCalc(long value, long seed) => value * seed % 20201227;
}
