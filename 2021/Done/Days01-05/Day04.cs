﻿namespace Advent2021;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        List<Board> boards = new();
        int score = 0, lineNumber = 0;
        Board board = new();

        foreach (List<int> line in InputSplit[1..].Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()))
        {
            board.AddLine(line);
            if (++lineNumber % 5 == 0)
            {
                boards.Add(board);
                board = new();
            }
        }

        foreach (int ball in InputSplit[0].Split(',').Select(int.Parse).ToList())
        {
            for (int boardNumber = boards.Count - 1; boardNumber >= 0; boardNumber--)
                if ((score = boards[boardNumber].CheckNumber(ball)) != 0)
                {
                    if (Part1) break;
                    boards.RemoveAt(boardNumber);
                }
            if (Part1 && score != 0) break;
        }

        Output = score.ToString();
    }

}

public class Board
{
    readonly List<List<int>> lines = new();
    int unselectedCount = 0;

    public void AddLine(List<int> row)
    {
        lines.Add(row);
        if (lines.Count == 5) AddCols();
        unselectedCount += row.Sum();
    }
    public int CheckNumber(int checkNumber)
    {
        int score = 0;
        bool alreadyRemoved = false;

        foreach(List<int> line in lines)
            for (int number = line.Count - 1; number >= 0; number--)
                if (line[number] == checkNumber)
                {
                    if (!alreadyRemoved)
                    {
                        unselectedCount -= checkNumber;
                        alreadyRemoved = true;
                    }
                    line.RemoveAt(number);
                    if (line.Count == 0 && score == 0) score = unselectedCount * checkNumber;
                }
        return score;
    }

    private void AddCols()
    {
        for (int colNum = 0; colNum < 5; colNum++)
            lines.Add(lines.GetRange(0,5).Select(l=>l[colNum]).ToList());
    }
}
