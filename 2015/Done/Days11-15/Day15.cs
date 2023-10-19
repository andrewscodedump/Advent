namespace Advent2015;

public partial class Day15 : Advent.Day
{
    private struct Ingredient { public int Capacity; public int Durability; public int Flavour; public int Texture; public int Calories; }
    public override void DoWork()
    {
        Input = Input.Replace(":", ",").Replace(", ", ",").Replace(" ", ",");
        List<Ingredient> ingredients = new();

        foreach (string ingredient in InputSplit)
            ingredients.Add(new() { Capacity = int.Parse(ingredient.Split(',')[2]), Durability = int.Parse(ingredient.Split(',')[4]), 
                Flavour = int.Parse(ingredient.Split(',')[6]), Texture = int.Parse(ingredient.Split(',')[8]), Calories = int.Parse(ingredient.Split(',')[10]) });

        Output = GetMax(ingredients, 100, 0, 0, 0, 0, 0, 0).ToString();
    }

    private int GetMax(List<Ingredient> ingredients, int spoonsLeft, int current, int capacity, int durability, int flavour, int texture, int calories)
    {
        int best = 0;
        if (spoonsLeft == 0)
            return (capacity < 1 || durability < 1 || flavour < 1 || texture < 1 || (Part2 && calories != 500)) ? 0 : capacity * durability * flavour * texture;

        if (current == ingredients.Count - 1)
            return GetMax(ingredients, 0, current, capacity + (ingredients[current].Capacity * spoonsLeft), durability + (ingredients[current].Durability * spoonsLeft),
                flavour + (ingredients[current].Flavour * spoonsLeft), texture + (ingredients[current].Texture * spoonsLeft),
                calories + (ingredients[current].Calories * spoonsLeft));

        for (int i = 1; i <= spoonsLeft; i++)
            best = Math.Max(GetMax(ingredients, spoonsLeft - i, current + 1, capacity + (ingredients[current].Capacity * i), durability + (ingredients[current].Durability * i),
                flavour + (ingredients[current].Flavour * i), texture + (ingredients[current].Texture * i), calories + (ingredients[current].Calories * i)), best);

        return best;
    }
}
