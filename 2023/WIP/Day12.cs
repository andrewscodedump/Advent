namespace Advent2023;

public partial class Day12 : Advent.Day
{
    static readonly bool debug = false;
    public override void DoWork()
    {
        List<int> rowCounts = [];
        int row = 0;
        foreach (string line in Inputs)
        {
            if (debug) Debug.Print($"{row++}: {line}");
            int rowCount = 0;
            string data = line.Split(' ')[0];
            int[] numbers = Array.ConvertAll(line[(data.Length + 1)..].Split(','), int.Parse);
            if (Part2)
            {
                data = string.Concat(Enumerable.Repeat(data + '?', 5))[..^1];
            }
            if(Part2)
                numbers=Enumerable.Repeat(numbers, 5).SelectMany(n=>n).ToArray();
            rowCount= ProcessRow(data, numbers);

            rowCounts.Add(rowCount);
        }        
        Output = rowCounts.Sum().ToString();
    }

    private static int ProcessRow(string data, int[] numbers)
    {
        if (debug) Debug.Print(data);

        int rowCount = 0;
        Queue<string> queue = new();
        queue.Enqueue(data);
        HashSet<string> foundPatterns = [];
        do
        {
            string current=queue.Dequeue();
            string newLine = "";
            //Regex dots = new(@"\.+");
            //Regex blocks = new(@"([^|\.]+)\[#\?]+([$|\.]+)");
            Regex blocks = new(@"[#\?]+");
            //string[] pattern = dots.Split(current).Where(r => !string.IsNullOrEmpty(r)).ToArray();
            MatchCollection coll = blocks.Matches(current);
            // for each block in the string
            for (int i = 0; i < coll.Count; i++)
            {
                int position = coll[i].Index;
                if (i >= numbers.Length) break;
                string block = coll[i].Value;
                int desiredLength = numbers[i];
                // if it starts in a hash
                if (block.StartsWith('#'))
                {
                    // if the length's OK
                    if (block.Length == desiredLength)
                    {
                        // if it's all hashes already
                        if (!block.Contains('?'))
                        {
                            if (i == numbers.Length - 1)
                            {
                                if (foundPatterns.Add(current))
                                {
                                    if (debug) Debug.Print(current);
                                    rowCount++;
                                }
                            }
                            continue;
                        }
                        else
                        {
                            // set all to hashes and queue
                            newLine = GetNextLine(current, string.Concat(Enumerable.Repeat("#", desiredLength)), position);
                            if (TestLine(newLine, numbers))
                                queue.Enqueue(newLine);
                            break;
                        }
                    }
                    // else if the length's too short
                    else if (block.Length < desiredLength)
                    {
                        // exit
                        break;
                    }
                    else
                    {
                        // set a . to make the length OK (if possible) and queue
                        if (block[desiredLength] == '?')
                        {
                            string newBlock = block[..desiredLength] + '.';
                            if (block.Length > desiredLength) newBlock += block[(desiredLength+1)..];
                            newLine = GetNextLine(current, newBlock, position); ;
                            if (TestLine(newLine, numbers))
                                queue.Enqueue(newLine);
                            break;
                        }

                    }
                }
                else
                {
                    // set first to a hash and queue
                    newLine = GetNextLine(current, '#' + block[1..], position);
                    if(TestLine(newLine, numbers))
                        queue.Enqueue(newLine);
                    // set first to a dot and queue
                    newLine = GetNextLine(current, '.' + block[1..], position);
                    if (TestLine(newLine, numbers))
                        queue.Enqueue(newLine);
                    break;
                }
            }


            /***** Old Way
            //replace the first ? with a #
            string next = current[..current.IndexOf('?')] + '#' + current[(current.IndexOf('?') + 1)..];
            switch(TestLine(next, numbers))
            {
                case 1:
                    //if the result matches the desired output, we're done
                    rowCount++;
                    continue;
                case -1:
                    //if the result is impossible, continue
                    break;
                default:
                    //enqueue the result
                    if (debug) Debug.Print($"{current} - {next}: {current.Length}, {current.IndexOf('?')}");
                    queue.Enqueue(next);
                    break;
            }
            //replace the first ? with a .
            next = current[..current.IndexOf('?')] + '.' + current[(current.IndexOf('?') + 1)..];
            switch (TestLine(next, numbers))
            {
                case 1:
                    //if the result matches the desired output, we're done
                    rowCount++;
                    continue;
                case -1:
                    //if the result is impossible, continue
                    break;
                default:
                    //enqueue the result
                    if (debug) Debug.Print($"{current} - {next}: {current.Length}, {current.IndexOf('?')}");
                    queue.Enqueue(next);
                    break;
            }
            *****/
        } while (queue.Count > 0);

        return rowCount;
    }

    static string GetNextLine(string original, string block, int position)
    {
        return original.Remove(position, block.Length).Insert(position, block);
    }
    static bool TestLine(string line, int[] desired)
    {
        int target = desired.Sum();
        if (line.Count(c => c == '#')>target) return false;
        if (target > line.Count(c => c == '#' || c == '?')) return false;
        Regex dots = new(@"\.+");
        string[] pattern = dots.Split(line).Where(r => !string.IsNullOrEmpty(r)).ToArray();
        if(!line.Contains('?'))
        {
            if (pattern.Length != desired.Length) return false;
            for (int i = 0; i < pattern.Length; i++)
                if (pattern[i].Length != desired[i])
                    return false;
            return true;
        }
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i].Contains('?'))
            {
                if (i >= desired.Length) return true;
                int targetLen = desired[i];
                string tooMany = new('#', targetLen + 1);
                if (pattern[i].StartsWith(tooMany)) return false;
                return true;
            }
            if (i >= desired.Length) return false;
            if (pattern[i].Length != desired[i]) return false;
        }
        // It matches

        return true;
    }

    static int TestLineOld(string line, int[] desired)
    {
        int target = desired.Sum();
        if (line.Count(c => c == '#') > target) return -1;
        if (target > line.Count(c => c == '#' || c == '?')) return -1;
        Regex dots = new(@"\.+");
        string[] pattern = dots.Split(line).Where(r => !string.IsNullOrEmpty(r)).ToArray();
        if (!line.Contains('?'))
        {
            if (pattern.Length != desired.Length) return -1;
            for (int i = 0; i < pattern.Length; i++)
                if (pattern[i].Length != desired[i])
                    return -1;
            return 1;
        }
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i].Contains('?'))
            {
                if (i >= desired.Length) return 0;
                int targetLen = desired[i];
                string tooMany = new('#', targetLen + 1);
                if (pattern[i].StartsWith(tooMany)) return -1;
                return 0;
            }
            if (i >= desired.Length) return -1;
            if (pattern[i].Length != desired[i]) return -1;
        }
        // It matches

        return 1;
    }
}
