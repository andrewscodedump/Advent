namespace Advent2024;

public partial class Day23 : Advent.Day
{
    public override void DoWork()
    {
        
        string[][] connections = Inputs.Select(i=>i.Split('-')).ToArray();
        string[][] triples = [];

        for (int i = 0; i < connections.Length; i++)
        {
            string[] c1 = connections[i];
            for (int j = i + 1; j < connections.Length; j++)
            {
                string[] c2 = connections[j], t = [];
                if (c2.Contains(c1[0])) t = [c1[1], c2[0], c2[1]];
                else if (c2.Contains(c1[1])) t = [c1[0], c2[0], c2[1]];
                if(t.Length == 0) continue;
                for (int k = j + 1; k < connections.Length; k++)
                {
                    string[] c3 = connections[k];
                    if (t.Contains(c3[0]) && t.Contains(c3[1]))
                    {
                        triples = [.. triples, t];
                        break;
                    }
                }
            }
        }

        int result = triples.Count(t=>t.Any(c=>c.StartsWith('t')));

        Output = result.ToString();
    }
}
/*
aq,cg,yn
aq,vc,wq
co,de,ka
co,de,ta
co,ka,ta
de,ka,ta
kh,qp,ub
qp,td,wh
tb,vc,wq
tc,td,wh
td,wh,yn
ub,vc,wq

kh-tc
qp-kh
wh-tc
kh-ub
tc-td
co-tc

qp-kh
td-qp
wh-qp

kh,tc,qp
kh-ub



*/