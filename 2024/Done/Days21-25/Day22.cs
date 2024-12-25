namespace Advent2024;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<(int, int, int, int), int> sequences = [];
        long result = InputNumbers.Sum(n => PopulateSequences(n[0], Part2, sequences));
        Output = (Part1 ? result : sequences.Values.Max()).ToString();
    }

    private static long PopulateSequences(long secret, bool populateSequences, Dictionary<(int, int, int, int), int> sequences)
    {
        (int a, int b, int c, int d) seq = (0, 0, 0, 0);
        HashSet<(int, int, int, int)> used = [];
        for (int i = 0; i < 2000; i++)
        {
            long newSecret = Encrypt(secret); int newNumber = (int)newSecret % 10;
            if (populateSequences)
            {
                seq = (seq.b, seq.c, seq.d, newNumber - (int)(secret % 10));
                if (i > 3 && used.Add(seq) && !sequences.TryAdd(seq, newNumber))
                    sequences[seq] += newNumber;
            }
            secret = newSecret;
        }
        return secret;
    }

    private static long Encrypt(long input)
    {
        long output = ((input * 64) ^ input) % 16777216;
        output = (output ^ (output / 32)) % 16777216;
        return (output ^ (output * 2048)) % 16777216;
    }
}
