namespace Advent2016;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        int currentTime = -1;
        bool success = false;
        List<Disk> disks = new();

        // Populate list of disks
        foreach (string input in Input.Split('|'))
        {
            string[] words = input.Split(new char[] { ',', ' ', '.' });
            disks.Add(new Disk(int.Parse(words[1][1..]), int.Parse(words[3]), int.Parse(words[12]) - (int.Parse(words[6][5..]) % int.Parse(words[3]))));
        }

        while (!success)
        {
            currentTime++;
            success = true;
            foreach (Disk disk in disks)
            {
                success = disk.IsAtZero(currentTime);
                if (!success) break;
            }
        }
        Output = currentTime.ToString();
    }

    private class Disk
    {
        public Disk(int number, int positions, int startingPosition)
        {
            Number = number;
            Positions = positions;
            StartingPosition = startingPosition;
        }
        public int Number { get; set; }
        public int Positions { get; set; }
        public int StartingPosition { get; set; }

        public bool IsAtZero(int releaseTime) => (StartingPosition + ((releaseTime + Number) % Positions)) % Positions == 0;
    }
}
