namespace Advent2019;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        int product = 0;
        int width = TestMode ? Part1 ? 3 : 2 : 25, height = TestMode ? 2 : 6, layerSize = width * height;
        int minZeros = Int32.MaxValue;
        char[] image = new string('2', layerSize).ToCharArray();

        #endregion Setup Variables and Parse Inputs

        for (int pos = 0; pos < Input.Length; pos += layerSize)
        {
            string layer = Input.Substring(pos, layerSize);
            int zeros = layer.ToCharArray().Count(c => c == '0');
            if (zeros < minZeros)
            {
                minZeros = zeros;
                product = layer.ToCharArray().Count(c => c == '1') * layer.ToCharArray().Count(c => c == '2');
            }
            for (int i = 0; i < layerSize; i++)
                if (image[i] == '2')
                    image[i] = layer[i];
        }

        for (int y = 0; y < height; y++)
        {
            StringBuilder line = new();
            for (int x = 0; x < width; x++)
                line.Append(image[(y * width) + x] == '0' ? " " : "█");
            Debug.Print(line.ToString());
        }

        string outputString = Part2 && !BatchRun ? AWInputBox("Get Output", "Enter string value displayed in output window", "") : string.Empty;
        Output = Part1 ? product.ToString() : outputString;
    }
}
