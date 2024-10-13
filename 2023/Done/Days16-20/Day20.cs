namespace Advent2023;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        Queue<(string, string, bool)> queue = [];
        Dictionary<string, Component> circuit = [];
        int high = 0, low = 0, presses = 0;
        long result = 0;
        Dictionary<string, long> keyComponents = [];

        circuit = InitialiseCircuit();
        keyComponents = FindKeyComponents(circuit);

        do
        {
            queue.Enqueue(("broadcaster", "button", false));
            presses++;
            do
            {
                (string destination, string source, bool signal) = queue.Dequeue();
                keyComponents.Keys.Where(k => source == k && !signal).ForEach(k => keyComponents[k] = presses);
                if (keyComponents.All(k => k.Value != 0)) result = keyComponents.Values.Aggregate((acc, val) => acc * val);
                high += signal ? 1 : 0;
                low += signal ? 0 : 1;
                if (result == 0 && circuit.TryGetValue(destination, out Component component))
                    component.Process(signal, source, queue);
            } while (queue.Count > 0 && result == 0);
            if (Part1 && presses == 1000) result = high * low;
        } while (result == 0);

        Output = result.ToString();
    }

    private class Component
    {
        public string Label { get; private set; }
        public string Type { get; private set; }
        public List<string> Targets { get; set; } = [];
        public Dictionary<string, bool> Sources { get; set; } = [];
        public bool State { get; set; } = false;
        public Component(string input)
        {
            string fullLabel = input.Split(" -> ")[0];
            Type = fullLabel[0] switch { '%' => "FlipFlop", '&' => "Conjunction", _ => fullLabel };
            Label = fullLabel[0] switch { '%' or '&' => fullLabel[1..], _ => fullLabel };
            input.Split(" -> ")[1].Split(", ").ForEach(t => Targets.Add(t));
        }

        public void Process(bool input, string source, Queue<(string, string, bool)> queue)
        {
            switch (Type)
            {
                case "FlipFlop":
                    // If input high, nothing happens; if input low, inverts and sends new state
                    if (input) break;
                    State = !State;
                    SendAll(State, queue);
                    break;
                case "Conjunction":
                    // If all sources high, send low, else send high
                    if (Sources.ContainsKey(source))
                        Sources[source] = input;
                    SendAll(Sources.Values.Any(s => !s), queue);
                    break;
                case "button":
                case "broadcaster":
                    // Send input to all outputs
                    SendAll(input, queue);
                    break;
                default: break;
            }
        }

        private void SendAll(bool signal, Queue<(string, string, bool)> queue) => Targets.ForEach(t => queue.Enqueue((t, Label, signal)));
    }

    private Dictionary<string, Component> InitialiseCircuit()
    {
        Dictionary<string, Component> circuit = [];
        circuit.Add("button", new("button -> broadcaster"));
        circuit.Add("rx", new("rx -> nowhere"));
        // Do the conjunctions first since we'll be populating their sources later
        Inputs.Where(i => i[0] == '&').ForEach(i => circuit.Add(i.Split(" -> ")[0][1..], new(i)));
        foreach (string input in Inputs.Where(i => i[0] != '&'))
        {
            string fullLabel = input.Split(" -> ")[0];
            circuit.Add(fullLabel == "broadcaster" ? fullLabel : fullLabel[1..], new(input));
        }
        foreach (Component target in circuit.Values.Where(c => c.Type == "Conjunction"  || c.Label == "rx"))
            foreach (Component source in circuit.Values.Where(s => s.Targets.Contains(target.Label)))
                target.Sources.Add(source.Label, false);
        return circuit;
    }

    Dictionary<string, long> FindKeyComponents(Dictionary<string, Component> circuit)
    {
        Dictionary<string, long> keyComponents = [];
        Queue<string> queue = [];
        queue.Enqueue("rx");
        do
        {
            Component comp = circuit[queue.Dequeue()];
            if (!comp.Sources.Where(s => circuit[s.Key].Type == "Conjunction").Any()) keyComponents.Add(comp.Label, 0);
            else comp.Sources.ForEach(s => queue.Enqueue(s.Key));
        }while (queue.Count > 0);
        return keyComponents;
    } 
}
