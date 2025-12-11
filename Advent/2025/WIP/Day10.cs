namespace Advent2025;

public class Day10 : Advent.Day
{
    public override void DoWork()
    {
        List<Machine> machines = [];
        foreach(string line in Inputs)
        {
            string[] bits=line.Split(' ');
            int lights = Convert.ToInt32(ReverseString(bits[0])[1..^1].Replace('.', '0').Replace('#', '1'), 2);
            List<int> joltages = [.. bits[^1].Split(['{', '}', ','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)];
            List<List<int>> buttons = [];
            foreach (string s in bits[1..^1])
            {
                buttons.Add([.. s.Split(['(', ')', ','], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)]);
            }
            buttons = [.. buttons.OrderBy(b => b.Count)];
            machines.Add(new(lights, buttons, joltages));
        }

        int processing = 1;
        int result = 0;
        foreach (Machine machine in machines)
        {
            Debug.Print($"Processing #{processing++} of {machines.Count}");
            Dictionary<string, int> viewed = [];
            int best = int.MaxValue;
            Queue<(Machine, int, int, string)> q = [];
            for (int i = 0; i < machine.Buttons.Count; i++)
            {
                Machine newM = machine.Clone();
                q.Enqueue((newM, i, 0, ""));
            }

            do
            {
                (Machine state, int button, int presses, string path) = q.Dequeue();
                presses++;
                path += button.ToString();
                if (presses >= best) continue;
                if (WhichPart == 2 && state.ExcessLights()) continue;

                if (viewed.TryGetValue(state.State(WhichPart == 1), out int count) && count < presses) continue;
                viewed[state.State(WhichPart == 1)] = presses;
                if (state.PressButton(button, WhichPart == 1))
                {
                    best = Math.Min(best, presses);
                    continue;
                }
                for (int i = 0; i < state.Buttons.Count; i++)
                    q.Enqueue((state.Clone(), i, presses, path));

            } while (q.Count > 0);
            result += best;
        }

        Output = result.ToString();
    }

    private class Machine
    {
        public Machine(int lightsTarget, List<List<int>> buttons, List<int> joltageTarget)
        {
            LightsTarget = lightsTarget;
            LightsState = 0;
            Buttons = buttons;
            JoltageTarget = joltageTarget;
            JoltageState = [.. Enumerable.Repeat(0, JoltageTarget.Count)];
        }

        public Machine(int lightsTarget, int lightsState, List<List<int>> buttons, List<int> joltageTarget, List<int> joltageState)
        {
            LightsTarget = lightsTarget;
            LightsState = lightsState;
            Buttons = buttons;
            JoltageTarget = joltageTarget;
            JoltageState = joltageState;
        }

        public int LightsTarget { get; set; }
        public int LightsState { get; set; } = 0;
        public List<List<int>> Buttons { get; set; }
        public List<int> JoltageTarget { get; set; }
        public List<int> JoltageState { get; set; } = [];
        public bool LightsMatch => LightsState == LightsTarget;
        public bool JoltagesMatch => JoltageState.SequenceEqual(JoltageTarget);

        public bool PressButton(int number, bool lights)
        {
            PressButton(number);
            if (lights) return LightsMatch;
            else return JoltagesMatch;
        }
        public void PressButton(int number)
        {
            foreach (int light in Buttons[number])
            {
                LightsState ^= (int)Math.Pow(2, light);
                // LightsState[light] = !LightsState[light];
                JoltageState[light]++;
            }
        }

        public string State(bool lights)
        {
            if(lights) return LightsState.ToString();
            else return string.Join("", JoltageState);
        }

        public Machine Clone()
        {
            return new(LightsTarget, LightsState, Buttons, JoltageTarget, new(JoltageState));
        }

        public bool ExcessLights()
        {
            return JoltageState.Zip(JoltageTarget).Any(z=>z.First>z.Second);
        }
    }
}
