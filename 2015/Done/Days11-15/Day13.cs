namespace Advent2015;

public partial class Day13 : Advent.Day
{
    private struct Seat { public string nextPerson; public List<string> done; public int totalSoFar; public string lastPerson; }
    public override void DoWork()
    {
        List<string> people = new();
        Dictionary<(string,string), int> differences = new();
        int maxHappiness = 0;
        Input = Input.Replace(".", "").Replace("would gain ", "+").Replace("would lose ", "-").Replace("happiness units by sitting next to ", "");
        if (WhichPart == 2) people.Add("Me");
        foreach (string couple in InputSplit)
        {
            string[] parts = couple.Split(' ');
            if (!people.Contains(parts[0]))
                people.Add(parts[0]);
            differences[(parts[0], parts[2])] = int.Parse(parts[1]);
            if (WhichPart == 2)
            {
                differences[(parts[0], "Me")] = 0;
                differences[("Me", parts[0])] = 0;
            }
        }

        Stack dfs = new();
        dfs.Push(new Seat { done = new List<string>() });
        do
        {
            Seat seat = (Seat)dfs.Pop();
            if (!string.IsNullOrEmpty(seat.lastPerson))
                seat.totalSoFar += differences[(seat.lastPerson, seat.nextPerson)] + differences[(seat.nextPerson, seat.lastPerson)];
            if (seat.done.Count == people.Count)
            {
                seat.totalSoFar+= differences[(seat.nextPerson, seat.done[0])] + differences[(seat.done[0], seat.nextPerson)];
                maxHappiness = Math.Max(maxHappiness, seat.totalSoFar);
            }
            else foreach(string person in people)
                {
                    if (seat.done.Contains(person)) continue;
                    List<string> newDone = new(seat.done) { person };
                    dfs.Push(new Seat { done = newDone, totalSoFar = seat.totalSoFar, nextPerson = person, lastPerson = seat.nextPerson });
                }
        } while (dfs.Count > 0);
        Output = maxHappiness.ToString();
    }
}
