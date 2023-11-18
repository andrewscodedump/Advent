namespace Advent2020;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, (string allergen, int number)> ingredients = new();
        Dictionary<string, List<string>> allergens = new();

        foreach (string food in Inputs)
        {
            string foodIngredients = food.Split(" (contains ")[0];
            string foodAllergens = food.Replace(")", " ").Split(" (contains ")[1];
            foreach (string allergen in foodAllergens.Split(new char[] { ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries))
                if (allergens.ContainsKey(allergen))
                    allergens[allergen].RemoveAll(a => allergens[allergen].Except(foodIngredients.Split(' ')).Contains(a));
                else
                    allergens.Add(allergen, foodIngredients.Split(' ').ToList());
            foreach (string ingredient in foodIngredients.Split(' '))
                ingredients[ingredient] = ingredients.ContainsKey(ingredient) ? ("", ingredients[ingredient].number + 1) : ("", 1);
        }

        while (allergens.Values.Where(l => l.Count > 1).Any())
            foreach (List<string> ingredient in allergens.Values.Where(l => l.Count == 1).ToArray())
                foreach (string multiple in allergens.Keys.Where(a => allergens[a].Count > 1 && allergens[a].Contains(ingredient[0])))
                    allergens[multiple].Remove(ingredient[0]);

        foreach (string allergen in allergens.Keys)
        {
            string ingredient = allergens[allergen][0];
            ingredients[ingredient] = (allergen, ingredients[ingredient].number);
        }

        Output = Part1 ? ingredients.Where(i => i.Value.allergen == "").Sum(i => i.Value.number).ToString() : string.Join(',', ingredients.Where(i => i.Value.allergen != "").OrderBy(i => i.Value.allergen).Select(i => i.Key));
    }
}
