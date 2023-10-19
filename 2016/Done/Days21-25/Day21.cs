using System;

namespace Advent2016;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        if (Part2)
            Array.Reverse(InputSplit);

        string work = InputSplit[0];
        bool descramble = Part2;
        /*
			* if (Part1)
			stk.Push(work);
		else
			stk.Pop();
			*/
        for (int i = 1; i < InputSplit.Length; i++)
        {
            string[] words = InputSplit[i].Split(' ');
            switch (words[0])
            {
                case "swap": // Reversible
                    work = words[1] == "position" ? SwapPosition(work, int.Parse(words[2]), int.Parse(words[5])) : SwapLetters(work, words[2], words[5]);
                    break;
                case "rotate": // Semi-reversible
                    work = words[1] == "based"
                        ? RotatePosition(work, words[6], descramble)
                        : RotateSteps(work, words[1], int.Parse(words[2]), descramble);
                    break;
                case "reverse": // Reversible
                    work = Reverse(work, int.Parse(words[2]), int.Parse(words[4]));
                    break;
                case "move": // Not reversible
                    work = Move(work, int.Parse(words[2]), int.Parse(words[5]), descramble);
                    break;
                default:
                    break;
            }
            if (!work.Contains('a') || !work.Contains('b') || !work.Contains('c') || !work.Contains('d') || !work.Contains('e') || !work.Contains('f') || !work.Contains('g') || !work.Contains('h'))
            {
                // something's gone wrong - break here
            }
        }

        Output = work.ToString();
    }

    private static string SwapPosition(string input, int x, int y)
    {
        if (x == y) return input;

        string prefix, postfix, middle, letter1, letter2;

        if (x > y)
        {
            prefix = input[..y];
            letter1 = input[x].ToString();
            middle = input[(y + 1)..(x - y - 1)];
            letter2 = input[y].ToString();
            postfix = x < input.Length - 1 ? input[(x + 1)..(input.Length - x - 1)] : "";
        }
        else
        {
            prefix = input[..x];
            letter1 = input[y].ToString();
            middle = input[(x + 1)..(y - x - 1)];
            letter2 = input[x].ToString();
            postfix = x < input.Length - 1 ? input[(y + 1)..(input.Length - y - 1)] : "";
        }
        return prefix + letter1 + middle + letter2 + postfix;
    }

    private static string SwapLetters(string input, string x, string y) => SwapPosition(input, input.IndexOf(x), input.IndexOf(y));

    private static string RotateSteps(string input, string direction, int steps, bool reverse)
    {
        int len = input.Length;
        int dist = steps % input.Length;

        return dist == 0
            ? input
            : (direction == "left" && !reverse) || (direction == "right" && reverse)
            ? input[dist..len] + input[..dist]
            : string.Concat(input.AsSpan(len - dist, dist), input.AsSpan(0, len - dist));
    }

    private static string RotatePosition(string input, string letter, bool reverse)
    {
        int steps = 0;
        if (reverse)
        {
            // This is a horrible bodge - will only work for 8 char PWs
            // Rewrite the rotate to use 1/2 len of PW rather than 4
            int pos = input.IndexOf(letter);
            switch (pos)
            {
                case 1: steps = 1; break;
                case 3: steps = 2; break;
                case 5: steps = 3; break;
                case 7: steps = 4; break;
                case 2: steps = 6; break;
                case 4: steps = 7; break;
                case 6: steps = 8; break;
                case 0: steps = 9; break;
                default: break;
            }
        }
        else
            steps = input.IndexOf(letter) + 1 + (input.IndexOf(letter) >= 4 ? 1 : 0);
        return RotateSteps(input, "right", steps, reverse);
    }

    private static string Reverse(string input, int start, int end)
    {
        int len = input.Length;

        int temp;
        temp = Math.Min(start, end);
        end = Math.Max(start, end);
        start = temp;

        if (start == end)
            return input;

        string prefix = input[..start];
        string postfix = end == len ? "" : input[(end + 1)..(len - end - 1)];
        string middle = input.Substring(start, end - start + 1);

        char[] charArray = middle.ToCharArray();
        Array.Reverse(charArray);
        return prefix + new string(charArray) + postfix;
    }

    private static string Move(string input, int from, int to, bool reverse)
    {
        string prefix, postfix, middle, output;
        int len = input.Length;

        if (from == to) return input;
        if (reverse) (to, from) = (from, to);
        string letter = input[from].ToString();
        if (from < to)
        {
            prefix = input[..from];
            middle = input[(from + 1)..(to - from)];
            postfix = to == len - 1 ? "" : input[(to + 1)..(len - to - 1)];
            output = prefix + middle + letter + postfix;
        }
        else
        {
            prefix = input[..to];
            middle = input[to..from];
            postfix = from == len - 1 ? "" : input[(from + 1)..(len - from - 1)];
            output = prefix + letter + middle + postfix;
        }
        return output;
    }
}
