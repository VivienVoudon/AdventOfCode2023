using System.Linq;

internal class Day11_1 : Day
{
    protected override int DayCount => 11;

    List<string> space;
    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        space = new List<string>();

        // Expand for line
        var columnWithGalaxy = new List<int>();
        foreach (var line in inputLines)
        {
            space.Add(line);

            bool findGalaxy = false;
            for(var i  = 0; i < line.Length;i++)
            {
                findGalaxy |= line[i] == '#';
                if (line[i] == '#' && !columnWithGalaxy.Contains(i))
                    columnWithGalaxy.Add(i);
            }

            if(!findGalaxy)
                space.Add(line);
        }

        // Expand for column
        var newSpace = new List<string>();
        foreach (var line in space)
        {
            var newLine = line;
            var pad = 0;
            for (var i = 0; i < line.Length;i++)
            {
                if (!columnWithGalaxy.Contains(i))
                {
                    newLine =  newLine.Insert(i + pad, ".");
                    pad++;
                }
            }
            newSpace.Add(newLine);
        }
        space = newSpace;

        ShowSpace();

        // retrieve all galaxy coord
        var galaxies = new List<(int Y, int X)>();
        for(var y = 0; y < space.Count;y++) 
            for (var x = 0; x < space[0].Length; x++)
            {
                if (space[y][x] == '#')
                    galaxies.Add((y, x));
            }

        var result = 0;
        for (var i = 0; i < galaxies.Count; i++)
            for(var j = i + 1; j < galaxies.Count; j++)
            {
                result += Math.Abs(galaxies[i].X - galaxies[j].X) + Math.Abs(galaxies[i].Y - galaxies[j].Y);
            }

                Console.WriteLine(result);        
    }

    void ShowSpace()
    {
        foreach (var line in space)
            Console.WriteLine(line);
    }
}
