namespace Advent2015;

public partial class Day24 : Advent.Day
{
    public override void DoWork()
    {
        List<int> parcels = new();
        int totalWeight = 0, required, compartments = WhichPart == 1 ? 3 : 4, bestWeight = int.MaxValue;
        Queue bfs = new();
        foreach (string parcel in Input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
        {
            int parcelSize = int.Parse(parcel);
            parcels.Add(parcelSize);
            totalWeight += parcelSize;
        }
        required = totalWeight / compartments;

        List<LoadDetails> emptyLoads = new();
        for (int i = 0; i < compartments; i++)
            emptyLoads.Add(new LoadDetails());

        bfs.Enqueue(new Move { Loads = emptyLoads, ParcelsUsed = new(), BestProduct = long.MaxValue, BestWeight = int.MaxValue });

        // Need: current loads, parcels used, best number so far, best product so far
        do
        {
            Move move = (Move)bfs.Dequeue();
            List<int> parcelsUsed = move.ParcelsUsed;
            long bestProduct = move.BestProduct;
            List<LoadDetails> startLoads = move.Loads;

            // Do the checks
            if (parcelsUsed.Count == parcels.Count)
            {
                foreach (LoadDetails load in startLoads)
                    bestWeight = Math.Min(bestWeight, load.TotalWeight);
                continue;
            }

            int nextParcel = 0;
            // Get the next parcel to use
            foreach (int parcel in parcels)
            {
                if (parcelsUsed.Contains(parcel))
                    continue;
                nextParcel = parcel;
                break;
            }
            parcelsUsed.Add(nextParcel);
            // Enqueue it in each of the compartments
            for (int i = 0; i < compartments; i++)
            {
                List<LoadDetails> nextLoad = new(startLoads);
                LoadDetails load = startLoads[i];
                load.TotalWeight += nextParcel;
                if (load.TotalWeight <= required && load.TotalWeight <= bestWeight)
                {
                    load.NumParcels++;
                    load.TotalProduct *= nextParcel;
                    nextLoad[i] = load;
                    Move nextMove = new() { Loads = nextLoad, ParcelsUsed = parcelsUsed, BestProduct = bestProduct, BestWeight = bestWeight };
                    bfs.Enqueue(nextMove);
                }
            }
        }
        while (bfs.Count > 0);
        Output = bestWeight.ToString();
    }

    private struct Move { public List<LoadDetails> Loads; public List<int> ParcelsUsed; public int BestWeight; public long BestProduct; }
    private struct LoadDetails { public int NumParcels; public int TotalWeight; public int TotalProduct; }
}
