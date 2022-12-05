namespace Advent2022;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int numStacks = (InputSplit[0].Length / 4) + 1;
        Stack<char>[] stacks = new Stack<char>[numStacks];

        for(int rowNum=InputSplit.Length-1; rowNum>=0; rowNum--)
        {
            string row = InputSplit[rowNum];
            if (!row.Contains('[')) continue;
            for(int stack = 0;stack<numStacks; stack++)
            {
                if (stacks[stack] is null) stacks[stack] = new();
                char crate = row[(stack * 4) + 1];
                if (crate != ' ') stacks[stack].Push(crate);
            }
        }

        foreach(string line in InputSplit)
        {
            if (!line.StartsWith("move")) continue;
            string[] words=line.Split(' ');
            int number = int.Parse(words[1]), from = int.Parse(words[3]) - 1, to = int.Parse(words[5]) - 1;
            Stack<char> temp = new();
            for(int i=0;i<number;i++)
            {
                char crate = stacks[from].Pop();
                if (Part1)
                    stacks[to].Push(crate);
                else
                    temp.Push(crate);
            }
            if (Part2)
            {
                do
                {
                    char crate = temp.Pop();
                    stacks[to].Push(crate);
                } while (temp.Count > 0);
            }
        }

        StringBuilder output = new();
        foreach(Stack<char> stack in stacks)
            output.Append(stack.Pop());

        Output = output.ToString();
    }
}
