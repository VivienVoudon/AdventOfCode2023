using System.ComponentModel.DataAnnotations;

internal class Day8_1 : Day
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

        var currentPlace = "AAA";

        var result = 0;
        var i = 0;
        do
        {
            result++;
            if (i == inputLines[0].Length)
                i = 0;

            var direction = inputLines[0][i];

            currentPlace = direction == 'R' ? pathes[currentPlace].Right : pathes[currentPlace].Left;
            i++;

        } while (currentPlace != "ZZZ");

        Console.WriteLine(result);        
    }
}
