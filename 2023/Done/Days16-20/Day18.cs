namespace Advent2023;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        (long x, long y) = (0, 0); (long px, long py) = (0, 0);
        long area = 0, perimeter = 0;

        foreach (string instruction in Inputs)
        {
            char direction = instruction[0];
            int distance = int.Parse(instruction.Split(' ')[1]);
            if (Part2)
            {
                direction = instruction[^2] switch { '0' => 'R', '1' => 'D', '2' => 'L', _ => 'U' };
                distance = int.Parse(instruction[^7..^2], System.Globalization.NumberStyles.HexNumber);
            }
            x += Directions[direction].x * distance; y += DirectionsYDown[direction].y * distance;
            perimeter += distance;
            // Shoelace formula: A = 1 / 2 * sum(xi * yi + 1 - yi * xi + 1)
            area += (px * y) - (py * x);

            (px, py) = (x, y);
        }
        // Join back to the beginning
        area += px - py;
        area /= 2;
        // Add in an adjustment factor for the extra half squares along the perimeter (And the quarter squares at the corners).
        area = area + (perimeter / 2) + 1;
        Output = area.ToString();
    }
}
