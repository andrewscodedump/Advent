using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Advent2023;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        List<Brick> heap = [];
        InputNumbers.ForEach(x => { heap.Add(new(x)); });

        while(Tick(heap));

        foreach(Brick b in heap)
        {
            List<Brick> test=new(heap);
            test.Remove(b);
            if (!Tick(test)) result++;
        }
        Output = result.ToString();
    }

    private class Brick
    {
        public (long x, long y, long z) Min { get; private set; }
        public (long x, long y, long z) Max { get; private set; }
        private bool isHorizontal;
        public Brick(long[] inputs)
        {
            Min = (Math.Min(inputs[0], inputs[3]), Math.Min(inputs[1], inputs[4]), Math.Min(inputs[2], inputs[5]));
            Max = (Math.Max(inputs[0], inputs[3]), Math.Max(inputs[1], inputs[4]), Math.Max(inputs[2], inputs[5]));
            isHorizontal = Min.z == Max.z;
        }
        public bool Move(List<Brick> heap)
        {
            if(Min.z == 1) return false;
            bool canMove = true;
            if (isHorizontal)
            {
                for (long x = Min.x; x <= Max.x; x++)
                {
                    for (long y = Min.y; y <= Max.y; y++)
                        if (HeapContains(heap, x, y, Min.z - 1))
                        {
                            canMove = false;
                            break;
                        }
                    if (!canMove) break;
                }
            }
            else
            {
                if (HeapContains(heap, Min.x, Min.y, Min.z - 1))
                {
                    canMove = false;
                }
            }
            if (canMove) MoveDown();
            return canMove;
        }
        private void MoveDown()
        {
            Min = (Min.x, Min.y, Min.z - 1);
            Max = (Max.x, Max.y, Max.z - 1);
        }
        public bool Contains(long x, long y, long z) => Min.x <= x && Max.x >= x && Min.y <= y && Max.y >= y && Min.z <= z && Max.z >= z;
    }

    private bool Tick(List<Brick> heap) 
    {
        bool bricksMoved = false;
        foreach(Brick brick in heap)
        {
            if(brick.Move(heap)) bricksMoved = true;
        }
        return bricksMoved;
    }

    private static bool HeapContains(List<Brick> heap, long x, long y, long z)
    {
        foreach(Brick b in heap)
        {
            if(b.Contains(x,y,z))  return true;
        }
        return false;
    }
}
