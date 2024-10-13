using System.Windows.Forms;

namespace Advent2023;

public partial class Day22 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        List<Brick> heap = [];

        InputNumbers.ForEach(x => { heap.Add(new(x)); });

        heap.ForEach(b => b.GetBlockers(heap));
        LetFall(heap);
        if (Part1)
            heap.ForEach(b => { result += b.CanDestroy(); });
        else
            result = ChainReaction(heap);
        Output = result.ToString();
    }

    private class Brick
    {
        public (long x, long y, long z) Min { get; private set; }
        public (long x, long y, long z) Max { get; private set; }
        public List<Brick> blockedBy = [];
        public List<Brick> blocks = [];
        private readonly bool isVertical;
        public bool isOnFloor;
        public Brick(long[] inputs)
        {
            Min = (Math.Min(inputs[0], inputs[3]), Math.Min(inputs[1], inputs[4]), Math.Min(inputs[2], inputs[5]));
            Max = (Math.Max(inputs[0], inputs[3]), Math.Max(inputs[1], inputs[4]), Math.Max(inputs[2], inputs[5]));
            isVertical = Min.z != Max.z;
        }
        public void GetBlockers(List<Brick> heap)
        {
            isOnFloor = Min.z == 1;
            foreach (Brick b in heap.Where(b => b.Max.z == Min.z - 1))
            {
                if (IsBlockedBy(b))
                {
                    if (!blockedBy.Contains(b)) blockedBy.Add(b);
                    if (!b.blocks.Contains(this)) b.blocks.Add(this);
                }
            }

        }

        public void Move(List<Brick> heap)
        {
            Min = (Min.x, Min.y, Min.z - 1);
            Max = (Max.x, Max.y, Max.z - 1);
            isOnFloor = Min.z == 1;
            foreach (Brick b in heap.Where(b=>b.blockedBy.Contains(this)))
            {
                b.blockedBy.Remove(this);
                blocks.Remove(b);
            }
            GetBlockers(heap);
        }


        public int CanDestroy()
        {
            // Blocks nothing - OK
            if (blocks.Count == 0) return 1;
            // Anything it blocks is not also blocked by something else - not OK
            if (blocks.Any(b => b.blockedBy.Count == 1)) return 0;
            return 1;
        }

        public bool IsBlockedBy(Brick other)
        {
            if (isVertical)
                return Min.x >= other.Min.x && Min.x <= other.Max.x && Min.y >= other.Min.y && Min.y <= other.Max.y;
            else
                return ((Min.x >= other.Min.x && Min.x <= other.Max.x) || (Max.x >= other.Min.x && Max.x <= other.Max.x) || (Min.x <= other.Min.x && Max.x >= other.Max.x))
                    && ((Min.y >= other.Min.y && Min.y <= other.Max.y) || (Max.y >= other.Min.y && Max.y <= other.Max.y) || (Min.y <= other.Min.y && Max.y >= other.Max.y));
        }
    }

    private static int LetFall(List<Brick> heap)
    {
        HashSet<Brick> moved = [];
        do
        {
            foreach (Brick brick in heap.Where(b => b.blockedBy.Count == 0 && !b.isOnFloor))
            {
                brick.Move(heap);
                moved.Add(brick);
            }
        } while (heap.Where(b => b.blockedBy.Count == 0 && !b.isOnFloor).Any());
        return moved.Count;
    }

    private List<Brick> DeepCopyHeap(List<Brick> heap)
    {
        List<Brick> newHeap = [];
        heap.ForEach(b => newHeap.Add(new Brick([b.Min.x, b.Min.y, b.Min.z, b.Max.x, b.Max.y, b.Max.z])));
        newHeap.ForEach(b => b.GetBlockers(newHeap));
        return newHeap;
    } 

    private int ChainReaction(List<Brick> heap)
    {
        int number = 0;
        for (int i = 0; i < heap.Count; i++)
        {
            // replicate the heap
            List<Brick> temp = DeepCopyHeap(heap);
            Brick brick = temp[i];
            // remove this brick
            temp.Remove(brick);
            brick.blocks = [];
            temp.Where(b => b.blockedBy.Contains(brick)).ForEach(b => b.blockedBy.Remove(brick));
            // do a fall
            number += LetFall(temp);
        }
        return number;
    }
}
