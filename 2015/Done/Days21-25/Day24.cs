using System.Linq;
using System.Net;
using Windows.Media.Editing;

namespace Advent2015;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        int compartments = Part1 ? 3 : 4;

        _ = FindValidCombos(InputNumbersSingle.Sum() / compartments, compartments, InputNumbersSingle, 1, out long bestProduct);
        Output = bestProduct.ToString();
    }

    private bool FindValidCombos(long required, int maxLevel, long[] available, int level, out long result)
    {
        int elements = 1;
        long[] valid = Array.Empty<long>();
        bool foundOne = false;
        do
        {
            var validCombinations = available.Combinator(elements).Where(t => t.Sum() == required);
            if (validCombinations.Any())
            {
                if (level == maxLevel)
                {
                    foundOne = true;
                    break;
                }
                foreach(var tester in validCombinations)
                {
                    valid = Array.ConvertAll(tester.ToArray(), t => (long)t);
                    long[] remainder = available.Except(tester).ToArray();
                    foundOne = FindValidCombos(required, maxLevel, remainder, level+1, out _);
                    if (foundOne) break;
                }
            }
            else elements++;
        } while (!foundOne);

        result = valid.Aggregate((long)1, (a, b) => b * a);
        return foundOne;
    }
}

public static class Combo
{
public static IEnumerable<IEnumerable<T>> Combinator<T>
 (this IEnumerable<T> elements, int k)
{
    return k == 0
        ? EnumerableEx.Return(Enumerable.Empty<T>())
        : elements.SelectMany((e, i) =>
            elements.Skip(i + 1)
                .Combinator(k - 1)
                .Select(c => EnumerableEx.Return(e).Concat(c)));
    }
}
