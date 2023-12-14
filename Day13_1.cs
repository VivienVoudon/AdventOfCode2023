internal class Day13_1 : Day
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
            if(string.IsNullOrEmpty(line))
            {
                // search horizontal simetry
                for(var i = 0; i < grid.Count() - 1; i++)
                {
                    if (FindSimetryHoriz(i, 0))
                        result += (i + 1) * 100;
                }

                // search horizontal simetry
                for (var i = 0; i < grid[0].Length - 1; i++)
                {
                    if (FindSimetryVert(i, 0))
                        result += (i + 1);
                }

                grid.Clear();
            }
            else
                grid.Add(line);
        }



        Console.WriteLine(result);
        
    }

    private bool FindSimetryHoriz(int startingindex, int gap)
    {
        if (startingindex - gap < 0 || startingindex + gap + 1 == grid.Count())
            return true;

        if (grid[startingindex - gap] != grid[startingindex + gap + 1])
            return false;

        return FindSimetryHoriz(startingindex, gap + 1);
    }

    private bool FindSimetryVert(int startingindex, int gap)
    {
        if (startingindex - gap < 0 || startingindex + gap + 1 == grid[0].Length)
            return true;

        foreach(var line in grid)
            if (line[startingindex - gap] != line[startingindex + gap + 1])
                return false;

        return FindSimetryVert(startingindex, gap + 1);
    }
}
