internal class Day16_2 : Day
{
    protected override int DayCount => 16;

    List<string> map;
    List<Direction>[,] lighten;
    List<Beam> beams;

    public enum Direction
    {
        Right,
        Bottom,
        Left,
        Top
    }
    public class Beam
    {
        public int X;
        public int Y;
        public Direction Direction;
    }

    protected override void Run(string input)
    {
        map = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        

        var startingBeams = new List<Beam>();
        for (var y = 0; y < map.Count; y++)
        {
            startingBeams.Add(new Beam { Y = y, X = -1, Direction = Direction.Right });
            startingBeams.Add(new Beam { Y = y, X = map[0].Length, Direction = Direction.Left });
        }
        for (var x = 0; x < map[0].Length; x++)
        {
            startingBeams.Add(new Beam { Y = -1, X = x, Direction = Direction.Bottom });
            startingBeams.Add(new Beam { Y = map.Count, X = x, Direction = Direction.Top });
        }

        var bestTotal = 0;
        foreach (var startingBeam in startingBeams)
        {
            lighten = new List<Direction>[map.Count, map[0].Length];
            for (var y = 0; y < map.Count; y++)
                for (var x = 0; x < map[0].Length; x++)
                    lighten[y, x] = new List<Direction>();

            beams = new List<Beam>
            {
                startingBeam
            };

            do
            {
                //ShowMap();
                var nextBeams = new List<Beam>();
                foreach (var beam in beams)
                {
                    // Enlight the current case
                    if (beam.Y >= 0 && beam.X >= 0 && beam.Y < map.Count() && beam.X < map[0].Length)
                    {
                        if (lighten[beam.Y, beam.X].Any(o => o == beam.Direction))
                            continue;
                        else
                            lighten[beam.Y, beam.X].Add(beam.Direction);
                    }


                    //move the beam
                    if (beam.Direction == Direction.Left)
                        beam.X--;
                    else if (beam.Direction == Direction.Right)
                        beam.X++;
                    else if (beam.Direction == Direction.Top)
                        beam.Y--;
                    else if (beam.Direction == Direction.Bottom)
                        beam.Y++;

                    //check out of bound
                    if (beam.Y < 0 || beam.X < 0 || beam.Y >= map.Count() || beam.X >= map[0].Length)
                        continue;
                    else
                        nextBeams.Add(beam);

                    //apply the current case
                    var currentCase = map[beam.Y][beam.X];
                    if (currentCase == '/')
                    {
                        beam.Direction = beam.Direction switch
                        {
                            Direction.Right => Direction.Top,
                            Direction.Left => Direction.Bottom,
                            Direction.Top => Direction.Right,
                            Direction.Bottom => Direction.Left,
                        };
                    }
                    else if (currentCase == '\\')
                    {
                        beam.Direction = beam.Direction switch
                        {
                            Direction.Right => Direction.Bottom,
                            Direction.Left => Direction.Top,
                            Direction.Top => Direction.Left,
                            Direction.Bottom => Direction.Right
                        };
                    }
                    else if (currentCase == '-' && (beam.Direction == Direction.Top || beam.Direction == Direction.Bottom))
                    {
                        nextBeams.Add(new Beam() { Direction = Direction.Right, X = beam.X, Y = beam.Y });
                        beam.Direction = Direction.Left;
                    }
                    else if (currentCase == '|' && (beam.Direction == Direction.Right || beam.Direction == Direction.Left))
                    {
                        nextBeams.Add(new Beam() { Direction = Direction.Top, X = beam.X, Y = beam.Y });
                        beam.Direction = Direction.Bottom;
                    }
                }

                beams = nextBeams;
            } while (beams.Any());

            var total = 0;
            for (var y = 0; y < map.Count; y++)
                for (var x = 0; x < map[0].Length; x++)
                    if (lighten[y, x].Any())
                        total++;

            bestTotal = Math.Max(bestTotal, total);
        }
        Console.WriteLine(bestTotal);
    }

    public void ShowMap()
    {
        Console.WriteLine();

        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                if (beams.Any(b => b.Y == y && b.X == x))
                    Console.BackgroundColor = ConsoleColor.Green;

                Console.Write(map[y][x]);

                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();


        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public void ShowLight()
    {
        Console.WriteLine();

        for (var y = 0; y < map.Count; y++)
        {
            var line = "";
            for (var x = 0; x < map[0].Length; x++)
            {
                if (lighten[y, x].Any())
                    line += "#";
                else
                    line += ".";
            }
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }



    //void GoNext(Beam beam)
    //{
    //    ShowMap();

    //    // Enlight the current case
    //    if (beam.Y >= 0 && beam.X >= 0 && beam.Y < map.Count() && beam.X < map[0].Length)
    //        lighten[beam.Y, beam.X]++;

    //    //move the beam
    //    if (beam.Direction == Direction.Left)
    //        beam.X--;
    //    else if (beam.Direction == Direction.Right)
    //        beam.X++;
    //    else if (beam.Direction == Direction.Top)
    //        beam.Y--;
    //    else if (beam.Direction == Direction.Bottom)
    //        beam.Y++;

    //    //check out of bound
    //    if (beam.Y < 0 || beam.X < 0 || beam.Y >= map.Count() || beam.X >= map[0].Length)
    //        return;

    //    //apply the current case
    //    var currentCase = map[beam.Y][beam.X];
    //    if (currentCase == '.')
    //        GoNext(beam);
    //    else if (currentCase == '/')
    //    {
    //        beam.Direction = beam.Direction switch
    //        {
    //            Direction.Right => Direction.Top,
    //            Direction.Left => Direction.Bottom,
    //            Direction.Top => Direction.Right,
    //            _ => Direction.Left
    //        };

    //        GoNext(beam);
    //    }
    //    else if (currentCase == '\\')
    //    {
    //        beam.Direction = beam.Direction switch
    //        {
    //            Direction.Right => Direction.Bottom,
    //            Direction.Left => Direction.Top,
    //            Direction.Top => Direction.Left,
    //            _ => Direction.Right
    //        };

    //        GoNext(beam);
    //    }
    //    else if (currentCase == '-')
    //    {
    //        if (beam.Direction == Direction.Top || beam.Direction == Direction.Bottom)
    //        {
    //            GoNext(new Beam() { Direction = Direction.Right, X = beam.X, Y = beam.Y });
    //            GoNext(new Beam() { Direction = Direction.Left, X = beam.X, Y = beam.Y });
    //        }

    //        GoNext(beam);
    //    }
    //    else if (currentCase == '|')
    //    {
    //        if (beam.Direction == Direction.Right || beam.Direction == Direction.Left)
    //        {
    //            GoNext(new Beam() { Direction = Direction.Top, X = beam.X, Y = beam.Y });
    //            GoNext(new Beam() { Direction = Direction.Bottom, X = beam.X, Y = beam.Y });
    //        }

    //        GoNext(beam);
    //    }
    //}

}
