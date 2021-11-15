namespace Advent2016;

public partial class Day11 : Advent.Day
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    public override void DoWork()
    {
        int steps, bestSteps = 500, attempts = 0, maxAttempts = 1_000_000, currentFloor, numItems = 0;
        Dictionary<int, List<(string, string)>> floors;

        foreach (string desc in InputSplit)
            foreach (string word in desc.Split(' '))
                if (word.Contains("generator") || word.Contains("microchip"))
                    numItems++;
        do
        {
            bool hitError = false;
            floors = InitializeFloors();
            currentFloor = 0;
            steps = 0;
            Move lastMove = new();
            do
            {
                // Get all valid combinations from this floor
                List<(string, string)> thisFloor = floors[currentFloor];
                List<Move> moves = new();
                List<Move> moves1Up = new();
                List<Move> moves1Down = new();
                List<Move> moves2Up = new();
                List<Move> moves2Down = new();
                Move move = new();

                for (int i = 0; i < thisFloor.Count; i++)
                {
                    (string element, string type) object1 = thisFloor[i];
                    // Try the item on its own
                    if (currentFloor < 3 && CheckMove(object1, floors[currentFloor], floors[currentFloor + 1], lastMove, "Up"))
                    {
                        move.Object1 = object1;
                        move.Object2 = ("", "");
                        move.NewFloor = currentFloor + 1;
                        move.NumberOfItems = 1;
                        move.Direction = "Up";
                        moves1Up.Add(move);
                        moves.Add(move);
                    }
                    if (currentFloor > 0 && CheckMove(object1, floors[currentFloor], floors[currentFloor - 1], lastMove, "Down"))
                    {
                        move.Object1 = object1;
                        move.Object2 = ("", "");
                        move.NewFloor = currentFloor - 1;
                        move.NumberOfItems = 1;
                        move.Direction = "Down";
                        moves1Down.Add(move);
                        moves.Add(move);
                    }
                    // Try combinations
                    for (int j = i + 1; j < thisFloor.Count; j++)
                    {
                        (string element, string type) object2 = thisFloor[j];
                        if (object1.element == object2.element && object1.type == object2.type)
                        { } // It's the same item - do nothing
                        else
                        {
                            if (currentFloor < 3 && CheckMove(object1, object2, floors[currentFloor], floors[currentFloor + 1], lastMove, "Up"))
                            {
                                move.Object1 = object1;
                                move.Object2 = object2;
                                move.NewFloor = currentFloor + 1;
                                move.NumberOfItems = 2;
                                move.Direction = "Up";
                                moves2Up.Add(move);
                                moves.Add(move);
                            }
                            if (currentFloor > 0 && CheckMove(object1, object2, floors[currentFloor], floors[currentFloor - 1], lastMove, "Down"))
                            {
                                move.Object1 = object1;
                                move.Object2 = object2;
                                move.NewFloor = currentFloor - 1;
                                move.NumberOfItems = 2;
                                move.Direction = "Down";
                                moves2Down.Add(move);
                                moves.Add(move);
                            }
                        }
                    }
                }
                // If there are no valid moves, start again (this should be impossible, but keeps happening)
                if (moves1Up.Count + moves1Down.Count + moves2Up.Count + moves2Down.Count == 0)
                {
                    hitError = true;
                    break;
                }
                // Pick one at random
                // Prefer double up to single, prefer single down to double
                move = moves[Rand.Next(moves.Count)];
                lastMove = move;

                // Move it
                List<(string, string)> newFloor = floors[move.NewFloor];
                List<(string, string)> oldFloor = floors[currentFloor];
                newFloor.Add(move.Object1);
                if (move.NumberOfItems == 2)
                    newFloor.Add(move.Object2);
                floors[move.NewFloor] = newFloor;
                oldFloor.Remove(move.Object1);
                if (move.NumberOfItems == 2)
                    oldFloor.Remove(move.Object2);
                floors[currentFloor] = oldFloor;
                currentFloor = move.NewFloor;
                steps++;

                /* 
                // For debugging - print out the current position
                Debug.Print("***** Step " + steps.ToString() + "*****");
                for (int i = 0; i < 4; i++)
                {
                    string debugString = "Floor " + i.ToString() + ": ";
                    if (floors[i].Count == 0)
                        debugString += ": empty";
                    else
                    {
                        for (int j = 0; j < floors[i].Count; j++)
                        {
                            debugString += ((StringPair)floors[i][j]).Value1 + " " + ((StringPair)floors[i][j]).Value2;
                            if (j < floors[i].Count - 1)
                                debugString += ", ";
                        }
                    }
                    Debug.Print(debugString);
                }
                //*/
            } while (floors[3].Count < numItems && steps <= bestSteps);
            if (!hitError && steps < bestSteps)
                bestSteps = steps;
            if (!hitError)
                attempts++;
        } while (attempts < maxAttempts);
        Output = bestSteps.ToString();
    }

    private Dictionary<int, List<(string, string)>> InitializeFloors()
    {
        Dictionary<int, List<(string, string)>> floors = new();
        // Populate the floor setup
        foreach (string desc in InputSplit)
        {
            int floor = 0;
            (string, string) item = ("", "");
            List<(string, string)> floorItems = new();
            string[] words = desc.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "first") floor = 0;
                if (words[i] == "second") floor = 1;
                if (words[i] == "third") floor = 2;
                if (words[i] == "fourth") floor = 3;
                if (words[i] == "contains" || words[i] == "and")
                {
                    if (words[i + 1] == "nothing")
                        break;
                    if (words[i + 3][..9] == "generator" || words[i + 3][..9] == "microchip")
                    {
                        item = (words[i + 2].Replace("-compatible", ""), words[i + 3][..9]);
                    }
                    floorItems.Add(item);
                }
                if (words[i] == "a" && words[i - 1].EndsWith(","))
                {
                    if (words[i + 2][..9] == "generator" || words[i + 2][..9] == "microchip")
                    {
                        item = (words[i + 1].Replace("-compatible", ""), words[i + 2][..9]);
                    }
                    floorItems.Add(item);
                }
            }
            floors[floor] = floorItems;
        }
        return floors;
    }

    private struct Move { public (string element, string type) Object1; public (string element, string type) Object2; public int NewFloor; public int NumberOfItems; public string Direction; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    private static bool CheckMove((string element, string type) object1, (string element, string type) object2, List<(string element, string type)> fromFloor, List<(string element, string type)> toFloor, Move lastMove, string direction)
    {
        bool matchedPair = false, foundMatched, foundUnmatched;

        if (object1.element == object2.element)
            matchedPair = true;

        // We can't under any circumstances move an unmatched pair
        if (object1.element != object2.element && object1.type != object2.type)
            return false;

        // Moving a matched pair down makes no sense (probably)
        if (matchedPair && direction == "Down")
            return false;

        // If we're just reversing the last move, don't allow it
        if (lastMove.Direction != direction)
            if ((lastMove.Object1.element == object1.element && lastMove.Object1.type == object1.type
                && lastMove.Object2.element == object2.element && lastMove.Object2.type == object2.type)
                || (lastMove.Object1.element == object2.element && lastMove.Object1.type == object2.type
                    && lastMove.Object2.element == object1.element && lastMove.Object2.type == object1.type))
                return false;

        // If either item is a chip and there is a non-matching generator where we're going to - bad
        if (object1.type == "microchip" && !matchedPair)
        {
            foundMatched = false;
            foundUnmatched = false;
            foreach ((string element, string type) in toFloor)
            {
                if (type == "generator" && element != object1.element)
                    foundUnmatched = true;
                if (type == "generator" && element == object1.element)
                    foundMatched = true;
            }
            if (foundUnmatched && !foundMatched)
                return false;
        }
        if (object2.type == "microchip" && !matchedPair)
        {
            foundMatched = false;
            foundUnmatched = false;
            foreach ((string element, string type) in toFloor)
            {
                if (type == "generator" && element != object2.element)
                    foundUnmatched = true;
                if (type == "generator" && element == object2.element)
                    foundMatched = true;
            }
            if (foundUnmatched && !foundMatched)
                return false;
        }

        // If either item is a generator and there is a non-paired chip where we're going to - bad
        if (object1.type == "generator" || object1.type == "generator")
        {
            bool foundChip, foundMatch = false;
            foreach ((string element, string type) in toFloor)
            {
                if (type == "microchip")
                {
                    foundChip = true;
                    if (object1.element == element || object2.element == element)
                        foundMatch = true;
                    else
                        foreach ((string element, string type) check2 in toFloor)
                        {
                            if (check2.type == "generator" && check2.element == element)
                                foundMatch = true;
                        }
                    if (foundChip && !foundMatch)
                        return false;
                }
            }
        }

        // If either item is a generator and we're leaving behind its chip and any other generator - bad
        bool chipLeft = false;
        bool foundGen = false;
        if (object1.type == "generator" && !matchedPair)
        {
            foreach ((string element, string type) in fromFloor)
            {
                if ((element != object1.element && type != object1.type) || (element != object2.element && type != object2.type))
                {
                    if (type == "microchip" && element == object1.element)
                        chipLeft = true;
                    if (type == "generator")
                        foundGen = true;
                }
            }
        }
        if (object2.type == "generator" && !matchedPair)
        {
            foreach ((string element, string type) in fromFloor)
            {
                if ((element != object1.element && type != object1.type) || (element != object2.element && type != object2.type))
                {
                    if (type == "microchip" && element == object2.element)
                        chipLeft = true;
                    if (type == "generator")
                        foundGen = true;
                }
            }
        }
        return !chipLeft || !foundGen;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    private static bool CheckMove((string element, string type) item, List<(string element, string type)> fromFloor, List<(string element, string type)> toFloor, Move lastMove, string direction)
    {
        // If we're just reversing the last move, don't allow it
        if (lastMove.Direction != direction)
            if (lastMove.Object1.element == item.element && lastMove.Object1.type == item.type)
                return false;

        // If it's a chip and there is a non-matching generator where we're going to - bad
        if (item.type == "microchip")
        {
            bool foundMatch = false;
            bool foundOther = false;
            foreach ((string element, string type) in toFloor)
            {
                if (type == "generator" && element != item.element)
                    foundOther = true;
                if (type == "generator" && element == item.element)
                    foundMatch = true;
            }
            if (foundOther && !foundMatch)
                return false;
        }

        // If it's a generator and there is a non-paired chip where we're going to - bad
        if (item.type == "generator")
        {
            bool foundChip, foundMatch = false;
            foreach ((string element, string type) in toFloor)
            {
                if (type == "microchip" && element != item.element)
                {
                    foundChip = true;
                    foreach ((string element, string type) check2 in toFloor)
                    {
                        if (check2.type == "generator" && check2.element == element)
                            foundMatch = true;
                    }
                    if (foundChip && !foundMatch)
                        return false;
                }
            }
        }

        // If it's a generator and we're leaving behind its chip and any other generator - bad
        bool chipLeft = false;
        bool foundGen = false;
        if (item.type == "generator")
        {
            foreach ((string element, string type) in fromFloor)
            {
                if (type == "microchip" && element == item.element)
                    chipLeft = true;
                if (type == "generator" && element != item.element)
                    foundGen = true;
            }
        }
        return !chipLeft || !foundGen;
    }
}
