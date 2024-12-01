internal class Day15_2 : Day
{
    protected override int DayCount => 15;

    private class Box
    {
        public string label;
        public int focal;
    }

    protected override void Run(string input)
    {
        var instructions = input.Split(new[] { ',' }, StringSplitOptions.None).ToList();

        var total = 0;
        var boxes = new List<Box>[256];
        for (var i = 0; i < boxes.Length; i++)
            boxes[i] = new List<Box>();

        foreach (var instruction in instructions)
        {
            if(instruction.Last() == '-')
            {
                var label = instruction.Substring(0, instruction.Length - 1);
                var hash = HASH(label);
                var lens = boxes[hash].FirstOrDefault(b => b.label == label);
                if(lens != default)
                    boxes[hash].Remove(lens);
            }
            else
            {
                var label = instruction.Split("=")[0];
                var focal = int.Parse(instruction.Split("=")[1]);
                var hash = HASH(label);
                var lens = boxes[hash].FirstOrDefault(b => b.label == label);
                if (lens == default)
                    boxes[hash].Add(new Box { label = label, focal = focal });
                else
                    lens.focal = focal;
            }
        }


        for (var i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].Any())
            {
                //var boxString = string.Join(" ", boxes[i].Select(b => $"[{b.label} {b.focal}]"));
                //Console.WriteLine($"Box {i}: {boxString}");

                for(var j = 0; j < boxes[i].Count; j++)
                {
                    total += (i + 1) * (j + 1) * boxes[i][j].focal;
                }
            }
        }

        Console.WriteLine(total);

    }

    public int HASH(string input)
    {
        var total = 0;
        var currentResult = 0;
        foreach (var c in input)
        {
            currentResult += (int)c;
            currentResult *= 17;
            currentResult = currentResult % 256;
        }
        //Console.WriteLine($"{line} => {currentResult}");
        total += currentResult;
        return total;
    }
}
