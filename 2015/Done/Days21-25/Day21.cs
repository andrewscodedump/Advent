namespace Advent2015;

public partial class Day21 : Advent.Day
{
    private struct GameItem(string name, int cost, int damage, int armour) { public string Name = name; public int Cost = cost; public int Damage = damage; public int Armour = armour; };

    public override void DoWork()
    {
        int cheapest = int.MaxValue, dearest = 0;
        long bossPoints, bossArmour, bossDamage, playerPoints = 100;
        List<GameItem> weapons = [], armours = [], rings = [];

        bossPoints = InputNumbers[0][0];
        bossDamage = InputNumbers[1][0];
        bossArmour = InputNumbers[2][0];

        weapons.AddRange([new("Dagger", 8, 4, 0), new("Shortsword", 10, 5, 0), new("Warhammer", 25, 6, 0), new("Longsword", 40, 7, 0), new("Longsword", 74, 8, 0)]);
        armours.AddRange([new("None", 0, 0, 0), new("Leather", 13, 0, 1), new("Chainmail", 31, 0, 2), new("Splintmail", 53, 0, 3), new("Bandedmail", 75, 0, 4), new("Platemail", 102, 0, 5)]);
        rings.AddRange([new("None", 0, 0, 0), new("Damage +1", 25, 1, 0), new("Damage +2", 50, 2, 0), new("Damage +3", 100, 3, 0), new("Defense +1", 20, 0, 1), new("Defense +2", 40, 0, 2), new("Defense +3", 80, 0, 3)]);

        foreach (GameItem weapon in weapons)
            foreach (GameItem armour in armours)
                foreach (GameItem leftRing in rings)
                    foreach (GameItem rightRing in rings)
                    {
                        if (rightRing.Name == leftRing.Name) continue;
                        int cost = weapon.Cost + armour.Cost + leftRing.Cost + rightRing.Cost;
                        if ((Part1 && cost < cheapest) || (Part2 && cost > dearest))
                        {
                            long playerRounds = playerPoints / Math.Max(bossDamage - (weapon.Armour + armour.Armour + leftRing.Armour + rightRing.Armour), 1);
                            long bossRounds = bossPoints / Math.Max(weapon.Damage + armour.Damage + leftRing.Damage + rightRing.Damage - bossArmour, 1);

                            if (playerRounds >= bossRounds) cheapest = Math.Min(cost, cheapest);
                            if (bossRounds > playerRounds) dearest = Math.Max(cost, dearest);
                        }
                    }

        Output = (Part1 ? cheapest : dearest).ToString();
    }
}
