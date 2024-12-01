internal class Day1_1 : Day
{
    protected override int DayCount => 1;

    protected override void Run(string input)
    {
        var lines = input.Split('\r');

        var total = 0;
        foreach (var line in lines)
        {
            int? firstN = null;
            int? lastN = null;
            foreach (var c in line)
            {
                if (int.TryParse(c.ToString(), out var n))
                {
                    if (!firstN.HasValue) firstN = n;

                    lastN = n;
                }
            }

            var lineResult = firstN.ToString() + lastN.ToString();
            Console.WriteLine(lineResult);
            total += int.Parse(lineResult);
        }

        Console.WriteLine($"FinalResult: {total}");
    }
}