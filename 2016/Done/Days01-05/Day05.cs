﻿namespace Advent2016;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int currentNumber = 0, found = 0;
        string password = "";
        char[] password2 = new char[8];

        do
        {
            string test = Input + currentNumber.ToString();
            string hash = GetMD5Hash(MD5, test);
            if (hash.StartsWith("00000"))
                if (WhichPart == 1)
                {
                    password += hash.Substring(5, 1);
                    found++;
                }

                else
                {
                    int pos = int.Parse(hash.Substring(5, 1), System.Globalization.NumberStyles.AllowHexSpecifier);
                    if (pos < 8 && password2[pos] == '\0')
                    {
                        password2[pos] = hash[6];
                        found++;
                    }
                }
            currentNumber++;
        } while (found < 8);

        Output = WhichPart == 1 ? password.ToString() : new string(password2);
    }
}
