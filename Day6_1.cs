internal class Day6_1 : Day
{
    protected override int DayCount => 6;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        var raceTimes = inputLines[0].Substring(6).Trim().Split(' ').Where(o => !string.IsNullOrEmpty(o)).Select(int.Parse).ToArray();
        var raceTravelGoals = inputLines[1].Substring(9).Trim().Split(' ').Where(o => !string.IsNullOrEmpty(o)).Select(int.Parse).ToArray();

        var result = 1;
        for (var i = 0; i < raceTimes.Length; i++)
        {
            int oks = 0;
            for(var holdTime = 1; holdTime <= raceTimes[i]; holdTime++)
            {
                if((raceTimes[i] - holdTime) * holdTime > raceTravelGoals[i])
                    oks++;
                else if(oks > 1) 
                    break;
            }

            result *= oks;
        }

        Console.WriteLine(result);
    }
}
