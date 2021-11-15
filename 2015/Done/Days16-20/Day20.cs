namespace Advent2015;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        int testValue = int.Parse(Input);
        bool foundIt = false;
        int house = 0, firstElf = 1, stopAfter = 50;
        int presentsPerHouse = WhichPart == 1 ? 10 : 11;
        do
        {
            house++;
            if (WhichPart == 2 && house > firstElf * stopAfter)
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
