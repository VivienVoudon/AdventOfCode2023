﻿// See https://aka.ms/new-console-template for more information
using System.Security.Principal;

internal class Day5_1 : Day
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

        var transforms = new List<(long sourceStart, long destinationStart, long range)>();
        foreach (var line in inputLines.Skip(3))
        {
            // contain "map" ? it's a header, reinit the transform section
            if (line.Contains("map:"))
            {
                transforms.Clear();
                continue;
            }

            // read the next transformation section
            if(!string.IsNullOrEmpty(line))
            {
                var split = line.Split(' ');
                transforms.Add(new(long.Parse(split[1]), long.Parse(split[0]), long.Parse(split[2])));
            }
            // blank line ? apply the transformation
            else
            {
                Console.WriteLine("===================");
                //foreach (var transform in transforms)
                //    Console.WriteLine($"{transform.destinationStart} {transform.sourceStart} {transform.range}");

                for(var i = 0; i< seeds.Length; i++)
                {
                    
                    //var glop = transforms.Where(t => seeds[i] >= t.sourceStart && seeds[i] <= (t.sourceStart + t.range - 1));
                    //if (glop.Count() > 1)
                    //    Console.WriteLine("Pas glop");

                    var findTransformation = transforms.FirstOrDefault(t => seeds[i] >= t.sourceStart && seeds[i] <= (t.sourceStart + t.range - 1));
                    if (findTransformation != default)
                        seeds[i] = findTransformation.destinationStart + (seeds[i] - findTransformation.sourceStart);

                    Console.WriteLine(seeds[i]);
                }
            }
            
        }

        Console.WriteLine(seeds.OrderBy(s => s).First());
        
    }


}