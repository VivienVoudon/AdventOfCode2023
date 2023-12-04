// See https://aka.ms/new-console-template for more information
using System.Security.Principal;

internal class Day1_2 : Day
{ 
    protected override int DayCount => 2;

    protected override void Run(string input)
    {
        var lines = input.Split('\r');

        var total = 0;

        var digits = new Dictionary<string, int>
        {
            { "one", 1 },
            { "1", 1 },
            {"two", 2 },
            {"2", 2 },
            {"three", 3 },
            {"3", 3 },
            {"four",4 },
            {"4",4 },
            {"five",5 },
            {"5",5 },
            {"six",6 },
            {"6",6 },
            {"seven",7 },
            {"7",7 },
            {"eight", 8 },
            {"8", 8 },
            {"nine", 9},
            {"9", 9},
        };

        foreach (var line in lines)
        {
            int? firstN = null;
            int firstNIndex = int.MaxValue;

            int? lastN = null;
            int lastNIndex = int.MinValue;

            foreach (var digit in digits.Keys)
            {
                var firstIndex = line.IndexOf(digit);
                var lastIndex = line.LastIndexOf(digit);

                if (firstIndex != -1 && firstIndex < firstNIndex)
                {
                    firstN = digits[digit];
                    firstNIndex = firstIndex;
                }

                if (lastIndex != -1 && lastIndex > lastNIndex)
                {
                    lastN = digits[digit];
                    lastNIndex = lastIndex;
                }
            }

            var lineResult = firstN.ToString() + lastN.ToString();
            Console.WriteLine(lineResult);
            total += int.Parse(lineResult);
        }

        Console.WriteLine($"FinalResult: {total}");
    }
}