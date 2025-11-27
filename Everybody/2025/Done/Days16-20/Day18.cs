namespace Everybody2025;

public class Day18 : Advent.Day
{
    public override void DoWork()
    {
        (Dictionary<int, Plant> plants, List<List<int>> tests) = GetInputs();
        switch (WhichPart)
        {
            case 1:
                Output = plants[plants.Count].GetEnergy().ToString();
                break;
            case 2:
                Output = tests.Sum(t => RunTest(t, plants)).ToString();
                break;
            case 3:
                // This is the mother of all hacks
                // A value for the best scenario was worked out by hand from a surface
                // analysis of the live data and injected into the input data.
                List<int> candidate = tests[0];
                tests.RemoveAt(0);
                long total = 0;

                long best = RunTest(candidate, plants);
                foreach (List<int> test in tests)
                {
                    long sub = RunTest(test, plants);
                    if (sub > 0)
                        total += best - sub;
                }
                Output = total.ToString();
                break;
        }
    }

    private static long RunTest(List<int> test, Dictionary<int, Plant> plants)
    {
        ResetEnergies(plants);
        for (int i = 0; i < test.Count; i++)
        {
            plants[i + 1].Output = test[i];
        }
        return plants[plants.Count].GetEnergy();

    }

    public class Plant
    {
        public Plant() { }
        public Plant(int id, int thickness)
        {
            ID = id;
            Thickness = thickness;
            Inputs = [];
        }
        public int ID { get; set; }
        public int Thickness { get; set; }
        public List<(Plant, int)> Inputs { get; set; }
        public int Output { get; set; } = int.MaxValue;

        public int GetEnergy()
        {
            if (Output == int.MaxValue)
            {
                int energy = 0;
                foreach ((Plant parent, int thickness) in Inputs)
                {
                    energy += thickness * parent.GetEnergy();
                }
                if (energy < Thickness) Output = 0;
                else Output = energy;
            }
            return Output;
        }
    }

    private static void ResetEnergies(Dictionary<int, Plant> plants)
    {
        foreach(Plant plant in plants.Values)
        {
            if (plant.Inputs.Count > 0) plant.Output = int.MaxValue;
        }
    }

    (Dictionary<int, Plant>, List<List<int>>) GetInputs()
    {
        Dictionary<int, Plant> plants = [];
        List<List<int>> tests = [];
        Plant plant = new();
        List<int> inputs;
        int ID = 0;
        foreach (string line in Inputs)
        {
            if (line == "")
            {
                plants[plant.ID] = plant;
                inputs = [];
                continue;
            }
            string[] bits = line.Split([' ', ':']);
            if (bits[0] == "Plant")
            {
                ID = int.Parse(bits[1]);
                plant = new(ID, int.Parse(bits[4]));
            }
            if (bits[1] == "free")
                plant.Output = int.Parse(bits[5]);
            if (bits[1] == "branch")
                // Assumes no forward references
                plant.Inputs.Add((plants[int.Parse(bits[4])], int.Parse(bits[7])));
            if (bits[0] == "0" || bits[0] == "1")
                tests.Add([.. bits.Select(int.Parse)]);
        }
        plants[ID] = plant;
        return (plants, tests);
    }

}