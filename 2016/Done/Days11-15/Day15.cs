namespace Advent2016;

public partial class Day15 : Advent.Day
{
    public override void DoWork()
    {
        int currentTime = 0;
        List<Disk> disks = [];

        // Populate list of disks
        InputNumbers.ForEach(input => disks.Add(new Disk(input[0], input[1], input[3] - (input[2] % input[1]))));
        if (Part2)
            disks.Add(new Disk(7, 11, 0));

        do
        {
            currentTime++;
            
        } while(!disks.All(disk => disk.IsAtZero(currentTime)));
        Output = currentTime.ToString();
    }

    private sealed class Disk(long number, long positions, long startingPosition)
    {
        public long Number { get; set; } = number;
        public long Positions { get; set; } = positions;
        public long StartingPosition { get; set; } = startingPosition;

        public bool IsAtZero(int releaseTime) => (StartingPosition + ((releaseTime + Number) % Positions)) % Positions == 0;
    }
}
