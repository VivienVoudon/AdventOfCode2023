internal class Day8_2 : Day
{
    protected override int DayCount => 8;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        var pathes = inputLines.Skip(2).ToDictionary(l => l.Substring(0, 3), l => new
        {
            Left = l.Substring(7, 3),
            Right = l.Substring(12, 3),
        });

        var places = pathes.Where(p =>  p.Key[2] == 'A').Select(o => o.Key).ToArray();

        var result = 0;
        var i = 0;
        var j = 5;
        // 0 = 20093
        // 1 = 12169
        // 2 = 13301
        // 3 = 20659
        // 4 = 16697
        // 5 = 17263
        int iterationSinceLastGood = 0;
        int previous = 0;
        do
        {
            result++;
            iterationSinceLastGood++;
            if (i == inputLines[0].Length)
                i = 0;

            var direction = inputLines[0][i];


            //for (var j = 0; j < places.Length; j++)
                places[j] = direction == 'R' ? pathes[places[j]].Right : pathes[places[j]].Left;

            if (places[j].EndsWith('Z'))
            {
                Console.WriteLine(iterationSinceLastGood);

                if(iterationSinceLastGood == previous)
                    Console.WriteLine();

                previous = iterationSinceLastGood
                iterationSinceLastGood = 0;

            }
            //var ok = places.Count(p => p[2] == 'Z');
            //if (ok > 2)
            //    Console.WriteLine($"{string.Join(" ", places)} {ok} {result}");

            i++;

            //} while (places[j][2] != 'Z');
        } while (places.Any(p => !p.EndsWith('Z')));

        Console.WriteLine(result);

    }
}
