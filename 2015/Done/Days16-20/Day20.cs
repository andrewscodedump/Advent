namespace Advent2015;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        long testValue = InputNumbers[0][0];
        bool foundIt = false;
        int house = 0, firstElf = 1, stopAfter = 50;
        int presentsPerHouse = Part1 ? 10 : 11;
        do
        {
            house++;
            if (Part2 && house > firstElf * stopAfter)
                firstElf++;
            int presents = 0;
            for (int elf = firstElf; elf <= house; elf++)
            {
                if (house % elf == 0)
                {
                    presents += elf * presentsPerHouse;
                    if (presents >= testValue)
                    {
                        foundIt = true;
                        break;
                    }
                }
            }
        } while (!foundIt);

        Output = house.ToString();
    }
}
