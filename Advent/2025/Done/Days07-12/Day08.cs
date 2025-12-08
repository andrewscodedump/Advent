namespace Advent2025;

public class Day08 : Advent.Day
{
    public override void DoWork()
    {
        long connections = 0, result = 0, testConnections = TestMode ? 10 : 1000;
        List<Connector> connectors = [.. InputNumbers.Select(i => new Connector(i[0], i[1], i[2]))];
        HashSet<Circuit> circuits = [];
        (Connector, Connector)[] pairs = [.. connectors.SelectMany((x, i) => connectors.Skip(i + 1), (a, b) => (a, b)).OrderBy(p => p.a.DistanceFrom(p.b))];
        foreach ((Connector first, Connector second) in pairs)
        {
            connections++;
            circuits.Add(first.ConnectTo(second));

            if (WhichPart == 1 && connections == testConnections)
                result = circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (long a, Circuit c) => a * c.Count);
            else if (WhichPart == 2 && circuits.First(c => c.Count != 0).Count == connectors.Count)
                result = first.x * second.x;
            if (result != 0)
                break;
        }
        Output = result;
    }

    private class Connector(long x, long y, long z)
    {
        public long x = x, y = y, z = z;
        public Circuit circuit = null;

        public double DistanceFrom(Connector other)
        {
            long dx = Math.Abs(x - other.x), dy = Math.Abs(y - other.y), dz = Math.Abs(z - other.z);
            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        public Circuit ConnectTo(Connector conn)
        {
            if (circuit is null && conn.circuit is null)
                return new() { { this }, { conn } };
            else if (circuit is null)
            {
                conn.circuit.Add(this);
                return conn.circuit;
            }
            else if (conn.circuit is null)
            {
                circuit.Add(conn);
                return circuit;
            }
            else if (circuit != conn.circuit)
            {
                Circuit toMerge = conn.circuit;
                circuit.Merge(toMerge);
                toMerge.Clear();
                return circuit;
            }
            else return circuit;
        }
    }

    private class Circuit : List<Connector>
    {
        public new void Add(Connector conn)
        {
            base.Add(conn);
            conn.circuit = this;
        }

        public void Merge(List<Connector> mergeWith)
        {
            foreach (Connector conn in mergeWith)
                Add(conn);
            mergeWith.Clear();
        }
    }
}