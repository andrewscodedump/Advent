namespace Advent2016;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        int numberFound = 0;
        int currentRecord = -1;
        List<string> hashes = new(1001);
        (string, string)[] pairs = new (string, string)[16];
        int matchFoundAt;

        for (int i = 0; i < 16; i++)
            pairs[i] = (new string(i.ToString("x1")[0], 3), new string(i.ToString("x1")[0], 5));

        for (int i = 0; i < 1001; i++)
            hashes.Add(GetHash(Input + i.ToString()));

        do
        {
            currentRecord++;
            int firstMatch = int.MaxValue;
            (string, string) letterFound = ("", "");
            // for 0 to F
            foreach ((string first, string second) pair in pairs)
            {
                // look for triples in current
                if (hashes[0].Contains(pair.first))
                {
                    matchFoundAt = hashes[0].IndexOf(pair.first);
                    if (matchFoundAt < firstMatch)
                    {
                        firstMatch = matchFoundAt;
                        letterFound = pair;
                    }
                }
            }

            // if found look for quintuples in next 1000
            if (firstMatch != int.MaxValue)
                for (int i = 1; i < 1000; i++)
                {
                    if (hashes[i].Contains(letterFound.Item2))
                    {
                        numberFound++;
                        break;
                    }
                }

            // delete current hash, add next
            hashes.RemoveAt(0);
            hashes.Add(GetHash(Input + (currentRecord + 1001).ToString()));
        } while (numberFound < 64);

        Output = currentRecord.ToString();
    }

    private string GetHash(string input)
    {
        string hash = GetMD5Hash(MD5, input);
        if (Part2)
            for (int j = 0; j < 2016; j++)
                hash = GetMD5Hash(MD5, hash);
        return hash;
    }
}
