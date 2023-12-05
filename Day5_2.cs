// See https://aka.ms/new-console-template for more information
using System.Security.Principal;

internal class Day5_2 : Day
{
    protected override int DayCount => 5;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        /*
seed-to-soil map:
50 98 2 
52 50 48 => seed 50 become soil 98
         */

        var seeds = inputLines[0].Substring(7).Split(' ').Select(long.Parse).ToArray();

        var tmpSeed = new List<long>();
        for (int i = 0; i < seeds.Length; i = i + 2)
        {
            tmpSeed.AddRange(Enumerable.Range(seeds[i], seeds[i+1]).ToArray());
        }

        seeds = tmpSeed.ToArray();
        var transforms = new List<(int sourceStart, int destinationStart, int range)>();
        foreach (var line in inputLines.Skip(3))
        {
            // contain "map" ? it's a header, reinit the transform section
            if (line.Contains("map:"))
            {
                transforms.Clear();
                continue;
            }

            // read the next transformation section
            if (!string.IsNullOrEmpty(line))
            {
                var split = line.Split(' ');
                transforms.Add(new(int.Parse(split[1]), int.Parse(split[0]), int.Parse(split[2])));
            }
            // blank line ? apply the transformation
            else
            {
                //Console.WriteLine("===================");

                for (var i = 0; i < seeds.Length; i++)
                {
                    var findTransformation = transforms.FirstOrDefault(t => seeds[i] >= t.sourceStart && seeds[i] <= (t.sourceStart + t.range - 1));
                    if (findTransformation != default)
                        seeds[i] = findTransformation.destinationStart + (seeds[i] - findTransformation.sourceStart);

                    //Console.WriteLine(seeds[i]);
                }
            }

        }

        Console.WriteLine(seeds.OrderBy(s => s).First());


    }
}