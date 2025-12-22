namespace Everybody2025;

public class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int eaten = 0;
        PopulateMapFromInput(out int width, out int height);
        List<(int, int)> moves = [(1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (2, -1), (-2, 1), (-2, -1)];
        (int, int) start = SimpleMap.First(k => k.Value == 'D').Key;
        HashSet<(int, int)> dragons = [start];
        List<(int, int)> sheep = [.. SimpleMap.Where(k => k.Value == 'S').Select(k => k.Key)];
        List<(int, int)> hides = [.. SimpleMap.Where(k => k.Value == '#').Select(k => k.Key)];
        switch (WhichPart)
        {
            case 1:
                int numMoves = TestMode ? 3 : 4;
                Dictionary<(int, int), int> visited = [];
                Queue<((int, int), (int, int), int)> q = [];
                q.Enqueue((start, (0, 0), 0));
                do
                {
                    ((int x, int y) pos, (int x, int y) move, int soFar) = q.Dequeue();
                    (int x, int y) newPos = (pos.x+move.x,  pos.y+move.y);
                    if (newPos.x < 0 || newPos.x >= width || newPos.y < 0 || newPos.y >= height) continue;
                    if (visited.TryAdd(newPos, soFar))
                    {
                        if (SimpleMap[newPos] == 'S') eaten++;
                    }
                    else
                    {
                        visited[newPos]=Math.Min(visited[newPos], soFar);
                    }
                    if (soFar == numMoves) continue;
                    foreach ((int dx, int dy) nextMove in moves)
                        q.Enqueue((newPos, nextMove, soFar + 1));
                }while(q.Count > 0);

                Output = eaten.ToString();
                break;
            case 2:
                numMoves = TestMode ? 3 : 20;
                for (int i = 0; i < numMoves; i++)
                {
                    HashSet<(int, int)> newDragons = [];
                    foreach ((int x, int y) in dragons)
                    {
                        foreach((int dx, int dy) in moves)
                        {
                            (int nx, int ny) = (x + dx, y + dy);
                            if (nx < 0 || nx >= width || ny < 0 || ny >= height) continue;
                            if(sheep.Contains((nx, ny)) && !hides.Contains((nx, ny)))
                            {
                                eaten++;
                                sheep.Remove((nx, ny));
                            }
                            newDragons.Add((nx, ny));
                        }
                    }
                    dragons = [.. newDragons];

                    List<(int, int)> newSheep = [];
                    for (int s = sheep.Count - 1; s >= 0; s--)
                    {
                        (int x, int y) = sheep[s];
                        if (y + 1 >= height) sheep.RemoveAt(s);
                        if (dragons.Contains((x, y + 1)) && !hides.Contains((x, y + 1)))
                        {
                            eaten++;
                            sheep.RemoveAt(s);
                        }
                        else newSheep.Add((x, y + 1));
                    }
                    sheep = [.. newSheep];
                }

                Output = eaten.ToString();
                break;
            case 3:
                Queue<((int, int), List<(int, int)>, bool, string)> q2 = [];
                HashSet<string> playbooks = [];
                HashSet<string> winningplaybooks = [];
                q2.Enqueue((start, sheep, true, ""));
                do
                {
                    ((int dx, int dy), List<(int, int)> currSheep, bool sheepToMove, string playbook) = q2.Dequeue();
                    if (!playbooks.Add(playbook)) continue;
                    if (sheepToMove)
                    {
                        for (int s = 0; s < currSheep.Count; s++)
                        {
                            (int sx, int sy) = currSheep[s];
                            if (sy + 1 >= height) continue;
                            if ((sx, sy + 1) == (dx, dy) && !hides.Contains((sx, sy + 1)))
                            {
                                if (currSheep.Count > 1) continue;
                                q2.Enqueue(((dx, dy), [.. currSheep], false, $"{playbook} Sx"));
                            }
                            else
                            {
                                List<(int, int)> newSheep = [.. currSheep];
                                newSheep[s] = (sx, sy + 1);
                                q2.Enqueue(((dx, dy), [.. newSheep], false, $"{playbook} S>{(char)(sx + 65)}{sy + 2}"));
                            }
                        }
                    }
                    else
                    {
                        foreach ((int mx, int my) in moves)
                        {
                            (int nx, int ny) = (dx + mx, dy + my);
                            if (nx < 0 || nx >= width || ny < 0 || ny >= height) continue;
                            string newPlaybook = $"{playbook} D>{(char)(nx + 65)}{ny + 1}";
                            List<(int, int)> newSheep = [.. currSheep];
                            if (newSheep.Contains((nx, ny)) && !hides.Contains((nx, ny)))
                            {
                                newSheep.Remove((nx, ny));
                                if (newSheep.Count == 0)
                                {
                                    eaten++;
                                    winningplaybooks.Add(newPlaybook);
                                    continue;
                                }
                            }
                            q2.Enqueue(((nx, ny), [.. newSheep], true, newPlaybook));
                        }
                    }
                } while (q2.Count > 0);
                Output = eaten.ToString();
                break;
        }
    }
}
