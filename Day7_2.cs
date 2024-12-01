internal class Day7_2 : Day
{
    protected override int DayCount => 7;

    protected override void Run(string input)
    {
        var hands = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
           .Select(l => new { Hand = l.Split(" ")[0], income = int.Parse(l.Split(" ")[1]) }).ToArray();


        var orderedHand = hands.OrderBy(h =>
        {
            var cards = h.Hand.GroupBy(c => c);

            if (cards.Count() == 1)
                return 7; //five of kind

            if (cards.Any(o => o.Key == 'J'))
            {
                var mostValueInHand = cards.Where(o => o.Key != 'J').OrderByDescending(o => o.Count()).First().Key;
                cards = h.Hand.Replace('J', mostValueInHand).GroupBy(c => c);
            }

            if (cards.Count() == 1)
                return 7; //five of kind

            if (cards.Count() == 2)
            {
                if (cards.First().Count() == 1 || cards.First().Count() == 4)
                    return 6; //carré

                return 5; //full
            }


            if (cards.Count() == 3)
            {
                if (cards.Any(o => o.Count() == 3))
                    return 4; // brelan

                return 3; // double pair
            }

            if (cards.Count() == 4)
                return 2; // pair

            return 1; // hauteur
        })
            .ThenBy(h => Value(h.Hand[0]))
            .ThenBy(h => Value(h.Hand[1]))
            .ThenBy(h => Value(h.Hand[2]))
            .ThenBy(h => Value(h.Hand[3]))
            .ThenBy(h => Value(h.Hand[4]))
            .ToArray();

        var result = 0;
        for (var i = 1; i <= hands.Count(); i++)
        {
            var hand = orderedHand[i - 1];
            Console.WriteLine($"{string.Join("", hand.Hand.OrderBy(o => o).ToArray())} ({hand.Hand})");
            result += i * hand.income;
        }

        Console.WriteLine(result);

    }

    int Value(char c)
    {
        if (c == 'A') return 14;
        if (c == 'K') return 13;
        if (c == 'Q') return 12;
        if (c == 'J') return 1;
        if (c == 'T') return 10;
        return int.Parse(c.ToString());
    }
}
