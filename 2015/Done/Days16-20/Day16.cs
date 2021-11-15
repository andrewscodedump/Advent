namespace Advent2015;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        List<List<(string, int)>> aunts = new();
        List<(string, int)> results = new() { { ("children", 3) }, { ("cats", 7) }, { ("samoyeds", 2) }, { ("pomeranians", 3) }, { ("akitas", 0) }, { ("vizslas", 0) }, { ("goldfish", 5) }, { ("trees", 3) }, { ("cars", 2) }, { ("perfumes", 1) } };
        Input = Input.Replace("Sue ", "Sue:").Replace(" ", "");
        foreach (string auntText in InputSplit)
        {
            Dictionary<string, int> auntOld = new();
            List<(string, int)> aunt = new();
            foreach (string bit in auntText.Split(','))
                aunt.Add((bit.Split(':')[0], int.Parse(bit.Split(':')[1])));
            aunts.Add(aunt);
        }

        int auntNum = 0;
        foreach (List<(string item, int number)> aunt in aunts)
        {
            bool foundHer = true;
            foreach ((string item, int number) in results)
            {
                if (!aunt.Any(a => a.item == item)) continue;
                int auntVal = aunt.Find(a => a.item == item).number;
                if ((WhichPart == 2 && (item == "trees" || item == "cats") && auntVal <= number)
                    || (WhichPart == 2 && (item == "pomeranians" || item == "goldfish") && auntVal >= number)
                    || (WhichPart == 2 && item != "trees" && item != "cats" && item != "pomeranians" && item != "goldfish" && auntVal != number)
                    || (WhichPart == 1 && auntVal != number))
                {
                    foundHer = false;
                    break;
                }
            }
            if (foundHer)
            {
                auntNum = aunt.Find(a => a.item == "Sue").number;
                break;
            }
        }

        Output = auntNum.ToString();
    }
}
