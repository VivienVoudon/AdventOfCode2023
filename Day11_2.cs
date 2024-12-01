internal class Day11_2 : Day
{
    protected override int DayCount => 11;


    List<string> space;
    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        space = inputLines;

        // Expand for line
        var columnWithGalaxy = new List<int>();
        var lineWithGalaxy = new List<int>();
        for (var i = 0; i < inputLines.Count; i++)
        {
            //space.Add(line);

            bool findGalaxy = false;
            for (var j = 0; j < space[i].Length; j++)
            {
                findGalaxy |= space[i][j] == '#';
                if (space[i][j] == '#' && !columnWithGalaxy.Contains(j))
                    columnWithGalaxy.Add(j);
            }

            if (findGalaxy)
                lineWithGalaxy.Add(i);
            else
                space[i] = space[i].Replace(".", "1");
        }

        //Expand for column
        //var newSpace = new List<string>();
        for (var i = 0; i < inputLines.Count; i++)
        {
            var newLine = string.Empty;
            for (var j = 0; j < space[i].Length; j++)
            {
                if (!columnWithGalaxy.Contains(j))
                    newLine += "1";
                else
                    newLine += space[i][j];
            }
            space[i] = newLine;
        }

        ShowSpace();

        // retrieve all galaxy coord
        var galaxies = new List<(int Y, int X)>();
        for (var y = 0; y < space.Count; y++)
            for (var x = 0; x < space[0].Length; x++)
            {
                if (space[y][x] == '#')
                    galaxies.Add((y, x));
            }

        var result = 0;
        var result1000 = 0;
        for (var i = 0; i < galaxies.Count; i++)
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                // parcours chaque case horizontale et chaque case vertical,
                // si on trouve 1 on incremente le gros compteur, sinon le petit

                // horizontale
                for (var k = Math.Min(galaxies[i].X, galaxies[j].X); k < Math.Max(galaxies[i].X, galaxies[j].X); k++)
                {
                    if (space[galaxies[i].Y][k] == '1')
                        result1000++;
                    else
                        result++;
                }

                //vertical
                for (var k = Math.Min(galaxies[i].Y, galaxies[j].Y); k < Math.Max(galaxies[i].Y, galaxies[j].Y); k++)
                {
                    if (space[k][galaxies[i].X] == '1')
                        result1000++;
                    else
                        result++;
                }
            }

        Console.WriteLine(result + 1000000 * result1000);
        //Console.WriteLine(result1000);
    }

    void ShowSpace()
    {
        foreach (var line in space)
            Console.WriteLine(line);
    }
}
