namespace Everybody2025;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int pointer = 0;
        List<string> names = [.. Inputs[0].Split(',')];

        foreach (string instruction in Inputs[2].Split(','))
        {
            int length = int.Parse(instruction[1..]) % names.Count;
            if (length == 0) continue;
            int direction = instruction.StartsWith('L') ? -1 : 1;
            int clockwise = direction == 1 ? length : names.Count - length;
            switch (WhichPart)
            {
                case 1:
                    pointer = Math.Clamp(pointer + (length * direction), 0, names.Count - 1);
                    break;
                case 2:
                    pointer = (pointer + clockwise) % names.Count;
                    break;
                case 3:
                    (names[0], names[clockwise]) = (names[clockwise], names[0]);
                    break;
            }
        }

        Output = names[pointer].ToString();
    }
}
