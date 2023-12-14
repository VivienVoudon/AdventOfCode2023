using System.ComponentModel.DataAnnotations;

internal class Day13_2 : Day
{
    protected override int DayCount => 13;

    List<string> grid = new List<string>();
    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        inputLines.Add(string.Empty);

        var iGlobal = 0;

        var result = 0;
        grid.Clear();
        foreach (var line in inputLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                bool findVert = false;
                bool findHorz = false;
                // search horizontal simetry
                for (var i = 0; i < grid.Count() - 1; i++)
                {
                    if (FindSimetryHoriz(i, 0, false))
                    {
                        result += (i + 1) * 100;
                        ShowSimetryHoriz(i);
                        findHorz = true;
                    }
                }

                Console.WriteLine();

                // search horizontal simetry
                for (var i = 0; i < grid[0].Length - 1; i++)
                {
                    if (FindSimetryVert(i, 0, false))
                    {
                        result += (i + 1);
                        ShowSimetryvert(i);
                        findVert = true;
                    }
                }

                if (!findVert && !findHorz)
                    Console.WriteLine("pas glop");

                if (findVert && findHorz)
                    Console.WriteLine("trop glop");

                grid.Clear();
                Console.WriteLine("========================================");
            }
            else
                grid.Add(line);
        }



        Console.WriteLine(result);

    }

    private void ShowSimetryHoriz(int simetryIndex)
    {
        for(var i = 0; i < grid.Count(); i++)
        {
            Console.WriteLine(grid[i]);
            if(i == simetryIndex)
            {
                Console.WriteLine("------------------------");
            }
        }
    }

    private void ShowSimetryvert(int simetryIndex)
    {
        foreach(var line in grid)            
        {
            Console.WriteLine(line.Insert(simetryIndex + 1, "|"));
        }
    }

    private bool FindSimetryHoriz(int startingindex, int gap, bool errorFind)
    {
        if (startingindex - gap < 0 || startingindex + gap + 1 == grid.Count())
            return errorFind;

        var columnInError = new List<int>();
        for (var i = 0; i < grid[0].Length; i++)
        {
            if (grid[startingindex - gap][i] != grid[startingindex + gap + 1][i])
                columnInError.Add(i);
        }

        if (columnInError.Count == 1 && !errorFind)
            return FindSimetryHoriz(startingindex, gap + 1, true);
        else if (columnInError.Count > 0)
            return false;

        return FindSimetryHoriz(startingindex, gap + 1, errorFind);
    }

    private bool FindSimetryVert(int startingindex, int gap, bool errorFind)
    {
        if (startingindex - gap < 0 || startingindex + gap + 1 == grid[0].Length)
            return errorFind;

        var lineInErrors = new List<int>();
        for (var i = 0; i< grid.Count; i++)
        {
            if (grid[i][startingindex - gap] != grid[i][startingindex + gap + 1])
                lineInErrors.Add(i);
        }

        if (lineInErrors.Count == 1 && !errorFind)
            return FindSimetryVert(startingindex, gap + 1, true);
        else if (lineInErrors.Count > 0)
            return false;

        return FindSimetryVert(startingindex, gap + 1, errorFind);
    }
}
