namespace Advent2021;

public partial class Day16 : Advent.Day
{
    public override void DoWork()
    {
        StringBuilder sb = new StringBuilder();
        foreach(char c in Input)
        {
            int num = Convert.ToInt32(c.ToString(), 16);
            string binary = Convert.ToString(num, 2).PadLeft(4, '0');
            sb.Append(binary);
        }

        Packet packet = new Packet();
        packet.Populate(sb.ToString());

        int result = packet.SumTypes();

        Output = result.ToString();
    }

    private class Packet
    {
        public int Version { get; private set; }
        public int Type { get; private set; }
        public int Value { get; private set; }
        public List<Packet> SubPackets { get; private set; } = new();

        public void Populate(string Input)
        {
            Version = Convert.ToInt32(Input.Substring(0, 3).PadLeft(4, '0'), 2);
            Type = Convert.ToInt32(Input.Substring(3, 3).PadLeft(4, '0'), 2);
            string remainder=Input.Substring(6);
            if (Type == 4)
            {
                Value= GetValue(remainder);
            }
            else
            {
                // It's a subpacket

                //   Lengthtype = next 1
                //   if lt = 0
                //     next 15 is number of bits in subpackets
                //     get subpackets
                //   else
                //     next 11 is number of subpackets
                //     get subpackets
            }
        }

        private int GetValue(string Input)
        {
            int result = 0;
            return result;
        }
        public int SumTypes()
        {
            int sum = 0;
            foreach (Packet packet in SubPackets)
                sum+=packet.SumTypes();
            return sum;
        }
    }
}
