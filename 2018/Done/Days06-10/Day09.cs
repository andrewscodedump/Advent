namespace Advent2018;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        long currPlayer = 1;
        long numPlayers = InputNumbersSingle[0];
        long highestMarble = InputNumbersSingle[1] * (Part1 ? 1 : 100);
        long[] scores = new long[numPlayers];
        LinkedList<long> marbles = new();
        LinkedListNode<long> currNode = marbles.AddFirst(0);
        #endregion Setup Variables and Parse Inputs

        for (int marble = 1; marble < highestMarble; marble++)
        {
            if (marble % 23 == 0)
            {
                for (int n = 0; n < 7; n++)
                    currNode = currNode.Previous ?? marbles.Last;
                scores[currPlayer] += marble + currNode.Value;
                LinkedListNode<long> toRemove = currNode;
                currNode = currNode.Next ?? marbles.First;
                marbles.Remove(toRemove);
            }
            else
            {
                currNode = currNode.Next ?? marbles.First;
                currNode = marbles.AddAfter(currNode, marble);
            }
            currPlayer = (currPlayer + 1) % numPlayers;
        }

        Output = scores.Max().ToString();
    }
}
