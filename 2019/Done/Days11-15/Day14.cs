namespace Advent2019;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        long oreNeeded, fuelProduced = 0;
        Dictionary<string, (long produced, List<(string chemical, long required)> ingredients)> reactions = new();
        Dictionary<string, long> leftovers = new();
        long target = 1000000000000, lowerBound, upperBound, guess, result;

        foreach (string reaction in Inputs)
        {
            (string inputs, string product) = (reaction.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[0], reaction.Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[1]);
            (long produced, string chemical) = (long.Parse(product.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]), product.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            List<(string chemical, long required)> ingredients = new();
            foreach (string ingredient in inputs.Split(',', StringSplitOptions.RemoveEmptyEntries))
                ingredients.Add((ingredient.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1], long.Parse(ingredient.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0])));
            reactions.Add(chemical, (produced, ingredients));
        }
        #endregion Setup Variables and Parse Inputs

        oreNeeded = GetRequired("FUEL", 1, reactions, ref leftovers);

        if (Part2)
        {
            lowerBound = target / oreNeeded; upperBound = lowerBound * 2; guess = lowerBound + ((upperBound - lowerBound) / 2);
            do
            {
                if ((result = GetRequired("FUEL", guess, reactions, ref leftovers)) == target)
                    lowerBound = guess;
                else if (result < target)
                    lowerBound = guess;
                else
                    upperBound = guess;
                guess = lowerBound + ((upperBound - lowerBound) / 2);
            } while (upperBound - lowerBound > 1);
            fuelProduced = lowerBound;
        }

        Output = (Part1 ? oreNeeded : fuelProduced).ToString();
    }

    #region Private Classes and Methods

    private long GetRequired(string chemical, long required, Dictionary<string, (long produced, List<(string chemical, long required)> ingredients)> reactions, ref Dictionary<string, long> leftovers)
    {
        long needed = 0;
        (long produced, List<(string chemical, long required)> ingredients) = reactions[chemical];
        leftovers.TryGetValue(chemical, out long leftover);
        leftovers[chemical] = Math.Max(0, leftover - required);
        required = Math.Max(0, required - leftover);
        long batchesRequired = (required / produced) + (required % produced == 0 ? 0 : 1);
        leftovers[chemical] += (batchesRequired * produced) - required;
        foreach ((string chemical, long required) ingredient in ingredients)
            needed = ingredient.chemical == "ORE" ? ingredient.required * batchesRequired : needed + GetRequired(ingredient.chemical, batchesRequired * ingredient.required, reactions, ref leftovers);
        return needed;
    }

    #endregion Private Classes and Methods
}
