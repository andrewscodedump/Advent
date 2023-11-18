namespace Advent2020;

public partial class Day04 : Advent.Day
{
    public override void DoWork() => Output = Inputs.Sum(k => PassportValid(k)).ToString();

    int PassportValid(string kvps) => (kvps.Contains("cid:") ? 0 : 1) + (Part1 ? kvps.Split(',').Length : kvps.Split(',').Sum(k => EntryValid(k))) == 8 ? 1 : 0;

    static int EntryValid(string kvp)
    {
        string key = kvp.Split(':')[0], value = kvp.Split(':')[1];
        if (key == "byr" && int.TryParse(value, out int intTest) && intTest >= 1920 && intTest <= 2002) return 1;
        if (key == "iyr" && int.TryParse(value, out intTest) && intTest >= 2010 && intTest <= 2020) return 1;
        if (key == "eyr" && int.TryParse(value, out intTest) && intTest >= 2020 && intTest <= 2030) return 1;
        if (key == "hgt" && value.EndsWith("cm") && int.TryParse(value[0..^2], out intTest) && intTest >= 150 && intTest <= 193) return 1;
        if (key == "hgt" && value.EndsWith("in") && int.TryParse(value[0..^2], out intTest) && intTest >= 59 && intTest <= 76) return 1;
        if (key == "hcl" && value.StartsWith("#") && value.Length == 7
            && int.TryParse(value[1..], System.Globalization.NumberStyles.HexNumber, default, out intTest) && intTest <= 0xFFFFFF) return 1;
        if (key == "ecl" && new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value)) return 1;
        if (key == "pid" && value.Length == 9 && int.TryParse(value, out _)) return 1;
        if (key == "cid") return 1;
        return 0;
    }
}
