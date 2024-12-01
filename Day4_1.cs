// See https://aka.ms/new-console-template for more information
using System.Security.Principal;

internal class Day4_1 : Day
{
    protected override int DayCount => 4;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        long result = 0;

        foreach (var line in inputLines.Select(l => l.Substring(l.IndexOf(':') + 2)))
        {
            var winningNumber = line.Split('|')[0].Split(' ').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse).ToList();
            var cardNumbers = line.Split('|')[1].Split(' ').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse).ToList();

            long cardResult = 0;

            foreach (var number in cardNumbers)
            {
                if (winningNumber.Contains(number))
                    if (cardResult == 0) cardResult = 1; else cardResult = cardResult * 2;

            }

            result += cardResult;
        }

        Console.WriteLine(result);
    }
}