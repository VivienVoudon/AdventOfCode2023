internal class Day6_2 : Day
{
    protected override int DayCount => 6;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        var raceTime = long.Parse(inputLines[0].Substring(6).Replace(" ", ""));
        var raceTravelGoal = long.Parse(inputLines[1].Substring(9).Replace(" ", ""));

        
            int oks = 0;
            for (var holdTime = 1; holdTime <= raceTime; holdTime++)
            {
                if ((raceTime - holdTime) * holdTime > raceTravelGoal)
                    oks++;
                else if (oks > 1)
                    break;
            }

        Console.WriteLine(oks);
    }
}
