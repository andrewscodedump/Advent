namespace Advent2015;

public partial class Day21 : Advent.Day
{
    private struct GameItem { public string Name; public int Cost; public int Damage; public int Armour; public GameItem(string name, int cost, int damage, int armour) { Name = name; Cost = cost; Damage = damage; Armour = armour; } };

    public override void DoWork()
    {
        int cheapest = int.MaxValue, dearest = 0;
        int bossPoints = 0, bossArmour = 0, bossDamage = 0, playerPoints = 100;
        List<GameItem> weapons = new(), armours = new(), rings = new();

        Input = Input.Replace("Hit ", "").Replace(" ", "");
        foreach (string attr in InputSplit)
        {
            if (attr.Split(':')[0] == "Points") bossPoints = int.Parse(attr.Split(':')[1]);
            else if (attr.Split(':')[0] == "Damage") bossDamage = int.Parse(attr.Split(':')[1]);
            else if (attr.Split(':')[0] == "Armor") bossArmour = int.Parse(attr.Split(':')[1]);
        }
        weapons.AddRange(new GameItem[] { new GameItem("Dagger", 8, 4, 0), new GameItem("Shortsword", 10, 5, 0), new GameItem("Warhammer", 25, 6, 0), new GameItem("Longsword", 40, 7, 0), new GameItem("Longsword", 74, 8, 0) });
        armours.AddRange(new GameItem[] { new GameItem("None", 0, 0, 0), new GameItem("Leather", 13, 0, 1), new GameItem("Chainmail", 31, 0, 2), new GameItem("Splintmail", 53, 0, 3), new GameItem("Bandedmail", 75, 0, 4), new GameItem("Platemail", 102, 0, 5) });
        rings.AddRange(new GameItem[] { new GameItem("None", 0, 0, 0), new GameItem("Damage +1", 25, 1, 0), new GameItem("Damage +2", 50, 2, 0), new GameItem("Damage +3", 100, 3, 0), new GameItem("Defense +1", 20, 0, 1), new GameItem("Defense +2", 40, 0, 2), new GameItem("Defense +3", 80, 0, 3) });

        foreach (GameItem weapon in weapons)
            foreach (GameItem armour in armours)
                foreach (GameItem leftRing in rings)
                    foreach (GameItem rightRing in rings)
                    {
                        if (rightRing.Name == leftRing.Name) continue;
                        int cost = weapon.Cost + armour.Cost + leftRing.Cost + rightRing.Cost;
                        if ((WhichPart == 1 && cost < cheapest) || (WhichPart == 2 && cost > dearest))
                        {
                            int playerRounds = playerPoints / Math.Max(bossDamage - (weapon.Armour + armour.Armour + leftRing.Armour + rightRing.Armour), 1);
                            int bossRounds = bossPoints / Math.Max(weapon.Damage + armour.Damage + leftRing.Damage + rightRing.Damage - bossArmour, 1);

                            if (playerRounds >= bossRounds) cheapest = Math.Min(cost, cheapest);
                            if (bossRounds > playerRounds) dearest = Math.Max(cost, dearest);
                        }
                    }

        Output = (WhichPart == 1 ? cheapest : dearest).ToString();
    }
}
