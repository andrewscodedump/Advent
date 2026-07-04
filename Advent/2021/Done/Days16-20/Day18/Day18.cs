namespace Advent2021;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        string work = Input;
        int result = 0;
        if (Part1)
        {
            for (int i = 1; i < Inputs.Length; i++)
                work = Add(work, Inputs[i]);
            result = Sum(work);
        }
        else
        {
            for (int i = 0;i< Inputs.Length; i++)
                for (int j = 0;j < Inputs.Length; j++)
                {
                    if (i == j) continue;
                    work=Add(Inputs[i], Inputs[j]);
                    int sum = Sum(work);
                    result=Math.Max(sum, result);
                }
        }

        string Add(string addTo, string toAdd) => Reduce($"[{addTo},{toAdd}]");

        string Reduce(string toReduce)
        {
            bool allDone = false;
            do
            {
                if (Explode(ref toReduce)) continue;
                allDone = !Split(ref toReduce);
            } while (!allDone);
            return toReduce;
        }

        bool Explode(ref string toExplode)
        {
            (int pairStart, int pairEnd) = (0, 0);
            (int prevStart, int prevEnd) = (0, 0);
            (int nextStart, int nextEnd) = (0, 0);
            int first, second, prev = 0, next = 0;

            int level = 0;
            for(int pos = 0; pos < toExplode.Length; pos++)
            {
                if (toExplode[pos] == '[') level++;
                if (toExplode[pos] == ']') level--;
                if (level== 5)
                {
                    pairStart = pos;
                    pairEnd = toExplode.IndexOf(']', pairStart);
                    break;
                }
            }

            if (pairStart == 0) return false;

            string[] pair = toExplode[(pairStart + 1)..pairEnd].Split(',');
            first = int.Parse(pair[0]);
            second = int.Parse(pair[1]);
            for(int i = pairStart - 1; i >= 0; i--)
            {
                if (char.IsDigit(toExplode[i]))
                {
                    prevStart = i;
                    prevEnd = i;
                    prev = toExplode[i] - 48;
                    if (char.IsDigit(toExplode[i - 1]))
                    {
                        prev += (toExplode[i - 1] - 48) * 10;
                        prevStart--;
                        if (char.IsDigit(toExplode[i - 2]))
                        {
                            prev += (toExplode[i - 2] - 48) * 100;
                            prevStart--;
                        }
                    }
                    break;
                }
            }
            for (int i = pairEnd; i < toExplode.Length - 2; i++)
            {
                if (char.IsDigit(toExplode[i]))
                {
                    nextStart = i;
                    nextEnd = i;
                    next = toExplode[i] - 48;
                    if (char.IsDigit(toExplode[i + 1]))
                    {
                        next = (next * 10) + (toExplode[i + 1] - 48);
                        nextEnd++;
                        if (char.IsDigit(toExplode[i + 2]))
                        {
                            next = (next * 10) + (toExplode[i + 2] - 48);
                            nextEnd++;
                        }
                    }
                    break;
                }
            }
            string newVal;
            if (prevStart == 0)
                newVal = $"{toExplode[0..pairStart]}0{toExplode[(pairEnd+1)..nextStart]}{next + second}{toExplode[(nextEnd+1)..]}";
            else if (nextStart == 0)
                newVal = $"{toExplode[0..prevStart]}{prev + first}{toExplode[(prevEnd+1)..pairStart]}0{toExplode[(pairEnd+1)..]}";
            else
                newVal = $"{toExplode[0..prevStart]}{prev + first}{toExplode[(prevEnd+1)..pairStart]}0{toExplode[(pairEnd+1)..nextStart]}{next + second}{toExplode[(nextEnd+1)..]}";
            toExplode = newVal;
            return true;
        }

        bool Split(ref string toSplit)
        {
            int startPos = 0, endPos = 0;
            int number = 0;
            for (int i = 0; i < toSplit.Length - 1; i++)
            {
                if (char.IsDigit(toSplit[i]) && char.IsDigit(toSplit[i + 1]))
                {
                    startPos = i;
                    endPos = startPos + 1;
                    number = ((toSplit[i] - 48) * 10) + toSplit[i + 1] - 48;
                    if (char.IsDigit(toSplit[i + 2]))
                    {
                        number = (number * 10) + toSplit[i + 2] - 48;
                        endPos++;
                    }
                    break;
                }
            }
            if (startPos==0) return false;
            string newVal = $"{toSplit[..startPos]}[{number / 2},{(number / 2) + (number % 2)}]{toSplit[(endPos + 1)..]}";
            toSplit = newVal;
            return true;
        }

        int Sum(string toSum)
        {
            do
            {
                int start = -1; int end = 0;
                for (int i = 0;i < toSum.Length; i++)
                {
                    if (toSum[i] == '[')
                    {
                        start = i;
                    }
                    else if (start != -1 && toSum[i] == ']')
                    {
                        end = i;
                        break;
                    }
                }
                string[] pair = toSum[(start+1)..end].Split(',');
                int first = int.Parse(pair[0]), second = int.Parse(pair[1]);
                toSum = $"{toSum[..start]}{(first * 3) + (second * 2)}{toSum[(end+1)..]}";
            } while (toSum.Contains('['));
            return int.Parse(toSum);
        }

        Output = result.ToString();
    }
}
