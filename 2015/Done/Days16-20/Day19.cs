namespace Advent2015;

public partial class Day19 : Advent.Day
{
    public override void DoWork() => Output = Part1 ? DoPart1() : DoPart2();

    private string DoPart1()
    {
        string[] inputs = Inputs;
        string startMolecule = inputs[^1];
        string[,] convs = new string[inputs.Length - 1, 2];
        Dictionary<string, int> outputs = [];
        for (int pos = 0; pos < inputs.Length - 1; pos++)
        {
            convs[pos, 0] = inputs[pos].Split(" => ")[0];
            convs[pos, 1] = inputs[pos].Split(" => ")[1];
        }

        for (int pos = 0; pos < startMolecule.Length; pos++)
        {
            string atom = startMolecule[pos].ToString();
            if (pos < startMolecule.Length - 1 && startMolecule[pos + 1].ToString().ToLower() == startMolecule[pos + 1].ToString())
            {
                atom += startMolecule[pos + 1].ToString();
            }
            for (int swap = 0; swap < convs.Length / 2; swap++)
            {
                if (atom == convs[swap, 0])
                {
                    string newMolecule = pos == 0
            ? convs[swap, 1] + startMolecule[atom.Length..]
            : startMolecule[..pos] + convs[swap, 1] + startMolecule[(pos + atom.Length)..];
                    if (outputs.TryGetValue(newMolecule, out int value))
                        outputs[newMolecule] = ++value;
                    else
                        outputs.Add(newMolecule, 1);
                }
            }
        }
        return outputs.Count.ToString();
    }

    private string DoPart2()
    {
        string molecule;
        List<string> inputs = [.. Inputs];
        string finalMolecule = inputs[^1];
        inputs.RemoveAt(inputs.Count - 1);
        int moves;
        int bestMoves = int.MaxValue;

        List<(string atom, string after)> convsRev = [];
        foreach (string conv in inputs)
        {
            string inp = conv.Split(" => ")[0];
            string outp = conv.Split(" => ")[1];
            convsRev.Add((outp, inp));
        }

        int sequenceTarget = 10;
        int sequenceLength = 0;
        int numTries = 0;
        int prevBest = 0;
        int randomTries = 1;

        bool endLoop;
        do
        {
            do
            {
                do
                {
                    moves = 0;
                    molecule = finalMolecule;
                    int pos = -1;
                    do
                    {
                        //Pick a conv at random
                        RandomizeList(convsRev);
                        for (int i = 0; i < convsRev.Count; i++)
                        {
                            string atom = convsRev[i].atom;
                            string after = convsRev[i].after;
                            //Find the first occurrence and replace it
                            pos = molecule.IndexOf(after);
                            if (pos == -1)
                                continue;
                            moves++;
                            molecule = molecule[..pos] + atom + molecule[(pos + after.Length)..];
                            break;
                        }
                        //Loop until none found
                    } while (pos != -1);

                    //Loop until e
                } while (molecule != "e");
                bestMoves = Math.Min(moves, bestMoves);
                numTries++;
            } while (numTries < randomTries);

            endLoop = false;
            if (sequenceLength == 0 || bestMoves == prevBest)
            {
                prevBest = bestMoves;
                sequenceLength++;
                if (sequenceLength == sequenceTarget)
                    endLoop = true;
                else
                {
                    bestMoves = int.MaxValue;
                    numTries = 0;
                }
            }
            else
            {
                sequenceLength = 0;
                numTries = 0;
                prevBest = 0;
                bestMoves = int.MaxValue;
                randomTries *= 2;
            }
        } while (!endLoop);
        return bestMoves.ToString();
    }

    protected static void RandomizeList<T>(List<T> listIn)
    {
        Random rnd = new();
        for (int pos = 0; pos < listIn.Count; pos++)
        {
            int swapPos = rnd.Next(pos, listIn.Count);
            (listIn[swapPos], listIn[pos]) = (listIn[pos], listIn[swapPos]);
        }
    }
}
