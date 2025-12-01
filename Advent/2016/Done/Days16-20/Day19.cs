namespace Advent2016;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        if (Part1)
        {
            Queue q = new();

            for (int i = 1; i <= InputNumbers[0][0]; i++)
                q.Enqueue(i);

            do
            {
                // Move the first in the queue back to the end
                q.Enqueue(q.Dequeue());
                // And throw away the next one
                q.Dequeue();
            }
            while (q.Count > 1);
            Output = q.Dequeue().ToString();
        }
        else
        {
            Queue winners = new(), losers = new();

            long inputNumber = InputNumbers[0][0];
            for (int i = 1; i <= inputNumber / 2; i++)
                winners.Enqueue(i);
            for (long i = (inputNumber / 2) + 1; i <= inputNumber; i++)
                losers.Enqueue(i);

            do
            {
                // Reject the first one from the candidates queue
                losers.Dequeue();
                // Move the first in the winners queue to the end of the prospects one
                losers.Enqueue(winners.Dequeue());
                // If the queues are unbalanced, balance them
                if (losers.Count - winners.Count == 2)
                    winners.Enqueue(losers.Dequeue());
            }
            while (losers.Count + winners.Count > 1);
            Output = ((int)losers.Dequeue()).ToString();
        }
    }
}
