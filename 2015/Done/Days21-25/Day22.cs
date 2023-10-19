namespace Advent2015;

public partial class Day22 : Advent.Day
{
    private struct Spell
    {
        public string Name; public int Mana; public int Damage; public int Heal; public int Armour; public int Recharge; public int Duration; public bool SingleUse;
        public Spell(string name, int mana, int damage, int heal, int armour, int recharge, int duration, bool singleUse)
        {
            Name = name; Mana = mana; Damage = damage; Heal = heal; Armour = armour; Recharge = recharge; Duration = duration; SingleUse = singleUse;
        }

    };
    private struct Player { public int ManaLeft; public int ManaSpent; public int Armour; public int Points; public int Damage; }
    private enum Result { PlayerDead, BossDead, Continue, Terminated }

    public override void DoWork()
    {
        #region Init & Setup

        Dictionary<Spell, int> recurring = new();
        Player player = new(), boss = new();
        List<Spell> spells = new();
        int bestMana = int.MaxValue;
        List<int> testList = new();

        spells.AddRange(new Spell[] { new Spell("Missile", 53, 4, 0, 0, 0, 0, false), new Spell("Drain", 73, 2, 2, 0, 0, 0, false), new Spell("Shield", 113, 0, 0, 7, 0, 6, true), new Spell("Poison", 173, 3, 0, 0, 0, 6, false), new Spell("Recharge", 229, 0, 0, 0, 101, 5, false) });

        #endregion Init & Setup

        int sequenceTarget = 10, sequenceLength = 0, numTries = 0, prevBest = 0;
        bool endLoop;
        int randomTries = 1;

        // "Infinite" loop
        do
        {
            do
            {
                InitialisePlayers(ref player, ref boss, ref testList, ref recurring);
                Result result;
                do
                {
                    if (Part2)
                    {
                        player.Points--;
                        if (player.Points <= 0)
                        {
                            result = Result.PlayerDead;
                            break;
                        }
                    }
                    result = TestMode
                        ? PlayerRound(ref recurring, ref player, ref boss, spells)
                        : PlayerRound(ref recurring, ref player, ref boss, spells, bestMana);
                    if (result != Result.Continue)
                        break;
                    result = BossRound(ref recurring, ref player, ref boss);
                } while (result == Result.Continue);

                if (result == Result.BossDead)
                {
                    numTries++;
                    if (player.ManaSpent < bestMana)
                        bestMana = player.ManaSpent;
                }
                else if (result == Result.Terminated)
                    numTries++;
            } while (numTries <= randomTries);

            endLoop = false;
            if (sequenceLength == 0 || bestMana == prevBest)
            {
                prevBest = bestMana;
                sequenceLength++;
                if (sequenceLength == sequenceTarget)
                    endLoop = true;
                else
                {
                    bestMana = int.MaxValue;
                    numTries = 0;
                }
            }
            else
            {
                sequenceLength = 0;
                numTries = 0;
                prevBest = 0;
                bestMana = int.MaxValue;
                randomTries *= 2;
            }

        } while (!endLoop);
        Output = bestMana.ToString();
    }

    #region Local Procs

    #region Initialise

    private void InitialisePlayers(ref Player player, ref Player boss, ref List<int> testList, ref Dictionary<Spell, int> recurring)
    {
        if (TestMode)
        {
            player.ManaLeft = 250;
            player.ManaSpent = 0;
            player.Points = 10;
            player.Armour = 0;
            player.Damage = 0;
        }
        else
        {
            player.ManaLeft = 500;
            player.ManaSpent = 0;
            player.Points = 50;
            player.Armour = 0;
            player.Damage = 0;
        }
        Input = Input.Replace("Hit ", "").Replace(" ", "");
        foreach (string attr in Input.Split(';'))
        {
            string type = attr.Split(':')[0];
            int val = int.Parse(attr.Split(':')[1]);
            if (type == "Points")
                boss.Points = val;
            else if (type == "Damage")
                boss.Damage = val;
            else if (type == "Armor")
                boss.Armour = val;
        }
        testList.AddRange(new int[] { 4, 2, 1, 3, 0 });
        recurring = new();
    }

    #endregion Initialise

    #region Rounds

    private static Result PlayerRound(ref Dictionary<Spell, int> recurring, ref Player player, ref Player boss, List<Spell> spells) => PlayerRound(ref recurring, ref player, ref boss, spells, int.MaxValue);

    private static Result PlayerRound(ref Dictionary<Spell, int> recurring, ref Player player, ref Player boss, List<Spell> spells, int bestMana)
    {
        Result result;

        // Apply recurring
        result = ApplyRecurring(ref recurring, ref player, ref boss);

        // If boss dead, skip remaining stuff
        if (result != Result.BossDead)
        {
            Spell spellToUse = SelectRandomSpell(spells, player, recurring);
            //Spell spellToUse = selectSpellFromList(rand, spells, player, recurring, ref testList);
            result = spellToUse.Name == null ? Result.PlayerDead : ApplySpell(spellToUse, ref player, ref boss, ref recurring);

            if (player.ManaSpent >= bestMana)
                result = Result.Terminated;
        }

        return result;
    }

    private static Result BossRound(ref Dictionary<Spell, int> recurring, ref Player player, ref Player boss)
    {
        Result result;

        result = ApplyRecurring(ref recurring, ref player, ref boss);
        if (result != Result.BossDead)
        {
            // Apply boss
            player.Points -= Math.Max(boss.Damage - player.Armour, 1);
            // If dead, lose
            if (player.Points <= 0)
            {
                result = Result.PlayerDead;
            }
        }
        return result;
    }

    #endregion Rounds

    #region Select

    private static Spell SelectRandomSpell(List<Spell> spells, Player player, Dictionary<Spell, int> recurring)
    {
        Spell spellToUse = new();

        // Get available spells (if any)
        List<Spell> availSpells = new();
        foreach (Spell spell in spells)
        {
            if (spell.Mana < player.ManaLeft)
                if (!recurring.ContainsKey(spell))
                    availSpells.Add(spell);
        }
        if (availSpells.Count > 0)
        {
            // Pick random weapon from available
            int randSpell = Rand.Next(availSpells.Count);
            //Debug.Print (availSpells.Count + ": " + randSpell);

            spellToUse = availSpells[randSpell];
        }
        return spellToUse;
    }

    #endregion Select

    #region Apply

    private static Result ApplyRecurring(ref Dictionary<Spell, int> recurring, ref Player player, ref Player boss)
    {
        Result result = Result.Continue;
        foreach (KeyValuePair<Spell, int> recur in recurring.ToList())
        {
            Spell spell = recur.Key;
            // Apply spell (for single use, only first time round)
            result = ApplySpell(spell, ref player, ref boss, ref recurring, true, spell.SingleUse && recur.Value == spell.Duration);
            // Check result
            if (result == Result.BossDead)
            {
                break;
            }
            // Decrement usage
            recurring[spell]--;
            if (recurring[spell] == 0)
            {
                if (spell.SingleUse)
                {
                    // Spell is temporary - remove its effect
                    player.Armour -= spell.Armour;
                    player.Damage -= spell.Damage;
                    player.Points -= spell.Heal;
                    player.ManaLeft -= spell.Recharge;
                }
                recurring.Remove(spell);
            }
        }
        return result;
    }

    private static Result ApplySpell(Spell spell, ref Player player, ref Player boss, ref Dictionary<Spell, int> recurring) => ApplySpell(spell, ref player, ref boss, ref recurring, false, false);

    private static Result ApplySpell(Spell spell, ref Player player, ref Player boss, ref Dictionary<Spell, int> recurring, bool isRecurring, bool firstTime)
    {
        if (!isRecurring)
        {
            // It's a new spell
            player.ManaLeft -= spell.Mana;
            player.ManaSpent += spell.Mana;
        }
        if (spell.Duration > 0 && !isRecurring)
        {
            //First time - add it to the list, but don't use it
            recurring.Add(spell, spell.Duration);
        }
        else if (!spell.SingleUse || firstTime)
        {
            if (spell.Damage > 0)
                boss.Points -= Math.Max(spell.Damage - boss.Armour, 1);
            if (boss.Points <= 0)
                return Result.BossDead;

            if (spell.Armour > 0 && player.Armour == 0)
                player.Armour += spell.Armour;

            player.Points += spell.Heal;
            player.ManaLeft += spell.Recharge;
        }
        return Result.Continue;
    }

    #endregion Apply

    #endregion Local Procs
}
