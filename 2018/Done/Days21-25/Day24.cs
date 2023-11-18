namespace Advent2018;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        Dictionary<int, Group> groups = new();
        List<int> groupIDs = new();

        string prevprevWord = default, prevWord = default, currWord = default, nextWord = default;
        Group currGroup = new(string.Empty);
        int groupID = 1;
        int winningScore = default;
        bool immuneWins = default;
        int immuneScore = default, infectionScore = default, prevIImmuneScore = default, prevInfectionScore = default;

        foreach (string input in Inputs)
        {
            if (input == "Immune System" || input == "Infection")
                currGroup.Type = input;
            else
            {
                string[] words = input.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                for (int pos = 0; pos < words.Length; pos++)
                {
                    prevprevWord = prevWord;
                    prevWord = currWord;
                    currWord = words[pos];
                    nextWord = pos != words.Length - 1 ? words[pos + 1] : string.Empty;
                    switch (currWord)
                    {
                        case "units":
                            currGroup.Number = int.Parse(prevWord);
                            currGroup.OriginalNumber = int.Parse(prevWord);
                            break;
                        case "hit":
                            currGroup.HitPoints = int.Parse(prevWord);
                            break;
                        case "weak":
                        case "immune":
                            string charType = currWord;
                            List<string> chars = new();
                            pos += 2;
                            do
                            {
                                chars.Add(words[pos++]);
                            } while (!new string[] { "with", "immune", "weak" }.Contains(words[pos]));
                            pos--;
                            if (charType == "weak") currGroup.Weaknesses = chars;
                            else currGroup.Immunities = chars;
                            break;
                        case "damage":
                            currGroup.AttackPoints = int.Parse(prevprevWord);
                            currGroup.AttackType = prevWord;
                            break;
                        case "initiative":
                            currGroup.Initiative = int.Parse(nextWord);
                            break;
                    }
                }
                if (currGroup.Type != string.Empty)
                {
                    groupIDs.Add(groupID);
                    groups.Add(groupID++, currGroup);

                }
                currGroup = new Group(currGroup.Type);
            }
        }
        #endregion Setup Variables and Parse Inputs

        do
        {
            do
            {
                prevInfectionScore = infectionScore; prevIImmuneScore = immuneScore;
                groupIDs.Sort((g1, g2) => (groups[g2].Number * groups[g2].AttackPoints).CompareTo(groups[g1].Number * groups[g1].AttackPoints));
                foreach (Group group in groups.Values) group.Selected = false;
                for (int attackerPos = 0; attackerPos < groupIDs.Count; attackerPos++)
                {
                    int attackerID = groupIDs[attackerPos];
                    Group attacker = groups[attackerID];
                    attacker.Victim = -1;
                    int bestAttack = 0;
                    int attack = 0;
                    if (attacker.Number == 0) continue;
                    for (int victimPos = 0; victimPos < groupIDs.Count; victimPos++)
                    {
                        int victimID = groupIDs[victimPos];
                        if (groups[victimID].Type == attacker.Type || groups[victimID].Number == 0 || groups[victimID].Selected) continue;
                        Group victim = groups[victimID];
                        attack = attacker.AttackPoints * attacker.Number * (victim.Immunities != null && victim.Immunities.Contains(attacker.AttackType) ? 0 : victim.Weaknesses != null && victim.Weaknesses.Contains(attacker.AttackType) ? 2 : 1);
                        if (attack > bestAttack)
                        {
                            bestAttack = attack;
                            attacker.Victim = victimID;
                        }
                    }
                    if (attacker.Victim != -1) groups[attacker.Victim].Selected = true;
                }
                groupIDs.Sort((g1, g2) => groups[g2].Initiative.CompareTo(groups[g1].Initiative));
                for (int attackerPos = 0; attackerPos < groupIDs.Count; attackerPos++)
                {
                    int attack = 0;
                    int attackerID = groupIDs[attackerPos];
                    Group attacker = groups[attackerID];
                    if (attacker.Number == 0 || attacker.Victim == -1 || groups[attacker.Victim].Number == 0) continue;
                    Group victim = groups[attacker.Victim];

                    attack = attacker.AttackPoints * attacker.Number * (victim.Immunities != null && victim.Immunities.Contains(attacker.AttackType) ? 0 : victim.Weaknesses != null && victim.Weaknesses.Contains(attacker.AttackType) ? 2 : 1);

                    int victims = Math.Min(victim.Number, attack / victim.HitPoints);
                    victim.Number -= victims;
                }

                immuneScore = (from g in groups.Values where g.Type == "Immune System" select g.Number).Sum();
                infectionScore = (from g in groups.Values where g.Type == "Infection" select g.Number).Sum();
                winningScore = immuneScore + infectionScore;

            } while (immuneScore > 0 && infectionScore > 0 && (immuneScore != prevIImmuneScore || infectionScore != prevInfectionScore));

            immuneWins = Part1 || infectionScore == 0;
            foreach (Group group in groups.Values)
            {
                group.Number = group.OriginalNumber;
                group.AttackPoints += group.Type == "Immune System" ? 1 : 0;
            }
        } while (!immuneWins);

        Output = winningScore.ToString();
    }

    #region Private Classes and Methods
    private class Group
    {
        public string Type { get; set; }
        public int Number { get; set; }
        public int OriginalNumber { get; set; }
        public int HitPoints { get; set; }
        public List<string> Immunities { get; set; }
        public List<string> Weaknesses { get; set; }
        public int AttackPoints { get; set; }
        public string AttackType { get; set; }
        public int Initiative { get; set; }
        public int Victim { get; set; }
        public bool Selected { get; set; }
        public Group(string Type) { this.Type = Type; Immunities = new List<string>(); Weaknesses = new List<string>(); Victim = -1; }
    }
    #endregion Private Classes and Methods
}
