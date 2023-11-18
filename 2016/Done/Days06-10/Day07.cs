namespace Advent2016;

public partial class Day07 : Advent.Day
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    public override void DoWork()
    {
        bool inside = false;
        int okNumber = 0;

        if (Part1)
        {
            foreach (string address in Inputs)
            {
                bool isOK = false;

                for (int pos = 0; pos < address.Length - 3; pos++)
                {
                    if (address.Substring(pos, 1) == "[")
                    {
                        inside = true;
                        continue;
                    }
                    else if (address.Substring(pos, 1) == "]")
                    {
                        inside = false;
                        continue;
                    }
                    else if (address.Substring(pos, 4).Contains('[') || address.Substring(pos, 4).Contains('['))
                        continue;
                    else if (address.Substring(pos, 1) == address.Substring(pos + 3, 1)
                        && address.Substring(pos + 1, 1) == address.Substring(pos + 2, 1)
                        && address.Substring(pos, 1) != address.Substring(pos + 1, 1))
                    {
                        if (inside)
                        {
                            isOK = false;
                            break;
                        }
                        else
                            isOK = true;
                    }
                }
                if (isOK)
                    okNumber++;
            }
        }

        else
        {
            foreach (string address in Inputs)
            {
                ArrayList inner = new();
                ArrayList outer = new();

                for (int pos = 0; pos < address.Length - 2; pos++)
                {
                    if (address.Substring(pos, 1) == "[")
                    {
                        inside = true;
                        continue;
                    }
                    else if (address.Substring(pos, 1) == "]")
                    {
                        inside = false;
                        continue;
                    }
                    else if (address.Substring(pos, 3).Contains('[') || address.Substring(pos, 3).Contains(']'))
                        continue;
                    else if (address.Substring(pos, 1) == address.Substring(pos + 2, 1)
                        && address.Substring(pos, 1) != address.Substring(pos + 1, 1))
                    {
                        string reverse = string.Concat(address.AsSpan(pos + 1, 1), address.AsSpan(pos, 1), address.AsSpan(pos + 1, 1));
                        if (inside && !inner.Contains(address.Substring(pos, 3)))
                        {
                            inner.Add(address.Substring(pos, 3));
                        }
                        else if (!inside && !outer.Contains(reverse))
                            outer.Add(reverse);
                    }
                }
                foreach (string test in outer)
                    if (inner.Contains(test))
                    {
                        okNumber++;
                        break;
                    }
            }
        }
        Output = okNumber.ToString();
    }
}
