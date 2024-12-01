// See https://aka.ms/new-console-template for more information
using System.Security.Principal;

internal class Day4_2 : Day
{
    protected override int DayCount => 4;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        long result = 0;

        var copies = new int[inputLines.Count()];

        for (var i = 0; i < inputLines.Count(); i++)
        //foreach (var line in inputLines.Select(l => l.Substring(l.IndexOf(':') + 2)))
        {
            copies[i]++;

            var line = inputLines[i].Substring(inputLines[i].IndexOf(':') + 2);

            var winningNumber = line.Split('|')[0].Split(' ').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse).ToList();
            var cardNumbers = line.Split('|')[1].Split(' ').Select(n => n.Trim()).Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse).ToList();

            var y = 1;

            foreach (var number in cardNumbers)
            {
                if (winningNumber.Contains(number))
                {
                    copies[i + y] += copies[i];
                    y++;
                }
            }

            //result += cardResult;
        }

        Console.WriteLine(copies.Sum());
    }


}