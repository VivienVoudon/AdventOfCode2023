internal class Day9_1 : Day
{
    protected override int DayCount => 9;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(x => x.Split(" ").Select(int.Parse).ToArray())            
            .ToList();


        var result = 0;
        foreach(var line in inputLines)
        {
            var history = new Stack<int[]>();
            history.Push(line);

            //var currentArray = line.ToArray();
            
            do
            {
                var nextArray = history.Peek().ToArray();

                for (var i = 0; i < nextArray.Length - 1; i++)
                    nextArray[i] = nextArray[i+1] - nextArray[i];

                history.Push(nextArray.SkipLast(1).ToArray());
            } while (history.Peek().Distinct().Count() > 1);

            var add = history.Pop().Last();
            do
            {
                add = history.Pop().Last() + add;
            } while (history.Any());

            result += add;
        }


        Console.WriteLine(result);
        
    }
}
