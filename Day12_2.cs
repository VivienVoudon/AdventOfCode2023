internal class Day12_2 : Day
{
    protected override int DayCount => 12;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(l => new
            {
                springs = l.Split(' ')[0] + '?' + l.Split(' ')[0] + '?' + l.Split(' ')[0] + '?' + l.Split(' ')[0] + '?' + l.Split(' ')[0],
                conditions = (l.Split(' ')[1]+','+ l.Split(' ')[1] + ',' + l.Split(' ')[1] + ',' + l.Split(' ')[1] + ',' + l.Split(' ')[1]).Split(',').Select(int.Parse).ToArray()
            })
            .ToList();

        var result = 0;
        Console.WriteLine(inputLines.Count() + " lignes");
        for(var l = 0; l < inputLines.Count(); l++) 
        {
            var row = inputLines[l];
            //var glop = new int[row.springs.Count(c => c == '?')];
            var nbQuestion = row.springs.Count(c => c == '?');
            for (var i = 0; i < Math.Pow(2, nbQuestion); i++)
            {
                // create the case
                var binary = Convert.ToString(i, 2).PadLeft(nbQuestion, '0');

                var testCase = string.Empty;
                var k = 0;
                for (var j = 0; j < row.springs.Length; j++)
                {
                    if (row.springs[j] == '?')
                    {
                        if (binary[k] == '0')
                            testCase += "#";
                        else
                            testCase += ".";

                        k++;
                    }
                    else
                        testCase += row.springs[j];
                }

                if (IsCorrect(testCase, row.conditions))
                    result++;
            }
        }

        Console.WriteLine(result);
    }

    bool IsCorrect(string springs, int[] conditions)
    {
        if (springs.Count(c => c == '#') != conditions.Sum())
            return false;

        var iConditions = 0;
        for (var i = 0; i < springs.Length; i++)
        {
            if (springs[i] == '#')
            {
                for (var j = 1; j < conditions[iConditions]; j++)
                {
                    if (i + j >= springs.Length || springs[i + j] != '#')
                        return false;
                }

                i = i + conditions[iConditions];
                iConditions++;
            }

            if (iConditions == conditions.Length)
                return i >= springs.Length || !springs.Substring(i + 1).Contains('#');
        }

        return false;
    }
}
