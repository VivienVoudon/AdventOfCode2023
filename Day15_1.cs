using System.Text;

internal class Day15_1 : Day
{
    protected override int DayCount => 15;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { ',' }, StringSplitOptions.None).ToList();

        var total = 0;
        foreach (var line in inputLines)
        {
            var currentResult = 0;
            foreach(var c in line)
            {
                currentResult += (int)c;
                currentResult *= 17;
                currentResult = currentResult % 256;
            }
            //Console.WriteLine($"{line} => {currentResult}");
            total += currentResult;


        }

        Console.WriteLine(total);
        
    }
}
