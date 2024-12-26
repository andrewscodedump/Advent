using System.Collections.Generic;

namespace Advent2023;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        char[] spaceOrHyphen = [' ', '-'];
        Dictionary<string, string> mappingTypes = [];
        List<(string sourceType, long sourceFrom, long sourceTo, long destOffset)> mappings = [];
        long lowestDest = long.MaxValue;
        string type = "", destType = "";
        long[] seeds = Array.ConvertAll(Input.Split(' ')[1..], Convert.ToInt64);

        foreach (string line in Inputs[2..])
        {
            if (line == "") continue;
            if (line.Contains('-'))
            {
                string[] words = line.Split(spaceOrHyphen, StringSplitOptions.RemoveEmptyEntries);
                type = words[0]; destType = words[2];
                mappingTypes[type] = destType;
            }
            else
            {
                long[] numbers = Array.ConvertAll(line.Split([' ']), Convert.ToInt64);
                mappings.Add((type, numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]));
            }
        }

        if (Part1)
        {
            foreach (long seedNumber in seeds)
            {
                type = "seed";
                long sourceNumber = seedNumber;
                do
                {
                    (string sourceType, long sourceFrom, long sourceTo, long destOffset)
                        = mappings.LastOrDefault(m => m.sourceType == type && m.sourceFrom <= sourceNumber && sourceNumber <= m.sourceTo, ("", 0, 0, 0));
                    sourceNumber += destOffset;
                    type = mappingTypes[type];
                } while (type != "location");
                lowestDest = Math.Min(lowestDest, sourceNumber);
            }
        }
        else
        {
            List<(long from, long to)> ranges = [];
            for (int i = 0; i < seeds.Length; i += 2)
            {
                ranges.Add((seeds[i], seeds[i] + seeds[i + 1] - 1));
            }
            type = "seed";
            do
            {
                ranges = GetDestRanges(ranges.OrderBy(r => r.from).ToList(), [.. mappings.Where(m => m.sourceType == type).OrderBy(m => m.sourceFrom)]);
                type = mappingTypes[type];
            } while (type != "location");
            lowestDest = Math.Min(lowestDest, ranges.Min(r => r.from));
        }
        Output = lowestDest.ToString();
    }

    List<(long, long)> GetDestRanges(List<(long, long)> sourceRanges, List<(string sourceType, long sourceFrom, long sourceTo, long destOffset)> mappings)
    {
        List<(long, long)> result = [];
        foreach ((long , long) sourceRange in sourceRanges)
        {
            (long from, long to) testRange = sourceRange;
            bool allDone = false;
            do
            {
                // Find the last mapping where the start is less than the start of the range
                (string sourceType, long sourceFrom, long sourceTo, long destOffset)
                    = mappings.LastOrDefault(m => m.sourceFrom <= testRange.from && testRange.from <= m.sourceTo, ("", 0, 0, 0));
                // There aren't any
                if (sourceType == "")
                {
                    // Does the end fit in any mappings?
                    (sourceType, sourceFrom, sourceTo, destOffset)
                        = mappings.LastOrDefault(m => m.sourceFrom <= testRange.to && testRange.to <= m.sourceTo, ("", 0, 0, 0));
                    if (sourceType == "")
                    {
                        // If there aren't any, add the whole range and end
                        result.Add(testRange);
                        allDone = true;
                    }
                    else
                    {
                        // Add the start of the range, set the range to the end and continue
                        result.Add((testRange.from, sourceFrom - 1));
                        testRange = (sourceFrom, testRange.to);
                    }
                }
                // If the end of the mapping is greater than the end of the range, add the whole range (with offset) and end
                else if (sourceTo >= testRange.to)
                {
                    result.Add((testRange.from + destOffset, testRange.to + destOffset));
                    allDone = true;
                }
                // Otherwise, add from the start of the range to the end of the mapping (with offsets), set the range start to the mapping end plus one and continue
                else
                {
                    //sourceNumber = sourceNumber + destFrom - sourceFrom;
                    result.Add((testRange.from + destOffset, sourceTo + destOffset));
                    testRange = (sourceTo + 1, testRange.to);
                }
            } while (!allDone);
        }
        return result;
    }
}
