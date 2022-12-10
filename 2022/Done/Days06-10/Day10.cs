namespace Advent2022;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int register = 1, sum = 0, pointer = 0;
        StringBuilder screen = new();
        bool addRunning = false;

        for (int pixel = 1; pixel <= 40 * 6; pixel++)
        {
            int col = (pixel - 1) % 40;
            if (col == 19)
                sum += register * pixel;
            screen.Append((register - 1 == col || register == col || register + 1 == col) ? '█' : ' ');
            if (col == 39)
                screen.AppendLine("");
            string instr = InputSplit[pointer];
            if (instr == "noop")
                pointer++;
            else if (addRunning)
            {
                register += Convert.ToInt32(instr.Split(' ')[1]);
                addRunning= false;
                pointer++;
            }
            else
                addRunning= true;
        }
        Debug.Print(screen.ToString() );

        string outputString = WhichPart == 2 && !BatchRun && !TestMode ? AWInputBox("Get Output", "Enter string value displayed in output window", "") : string.Empty;
        Output = Part1 ? sum.ToString() : outputString;
    }
}
