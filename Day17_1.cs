using AdventOfCode2023.Tooling;
using System;
using System.Drawing;

internal class Day17_1 : Day
{
    protected override int DayCount => 17;

    List<string> grid;
    List<(Point position, int heatLoss)> visitedPoints;
    protected override void Run(string input)
    {
        grid = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        var possiblePaths = new List<(Point position, List<(Direction direction, Point postion)> previousMove, int heatLoss)>
        {
            (new Point(0, 0), new List<(Direction direction, Point postion)>(), 0)
        };

        var lessHeatLoss = int.MaxValue;
        var target = new Point(grid[0].Length-1, grid.Count-1);
        visitedPoints = new List<(Point position, int heatLoss)>() { (new Point(0, 0), 0) };
        var nbLoop = 0;
        while (lessHeatLoss == int.MaxValue)
        {
            var shorterPath = possiblePaths.OrderBy(p => p.heatLoss).First();
            Console.WriteLine($"nbPath: {possiblePaths.Count()}; lessHeat: {shorterPath.heatLoss}");
            //ShowPath(shorterPath);
            foreach (var direction in (Direction[])Enum.GetValues(typeof(Direction)))
            {
                // is it possible ?
                var newPoint = direction switch
                {
                    Direction.Right => new Point(shorterPath.position.X + 1, shorterPath.position.Y),
                    Direction.Top => new Point(shorterPath.position.X, shorterPath.position.Y - 1),
                    Direction.Left => new Point(shorterPath.position.X - 1, shorterPath.position.Y),
                    Direction.Bottom => new Point(shorterPath.position.X, shorterPath.position.Y + 1),
                    _ => throw new ArgumentOutOfRangeException()
                };

                // point out of the grid
                if (newPoint.X < 0 || newPoint.X > grid[0].Length - 1
                                   || newPoint.Y < 0 || newPoint.Y > grid.Count - 1)
                    continue;

                if (shorterPath.previousMove.Any())
                {
                    int nbPreviousMove = shorterPath.previousMove.Count();
                    var lastMove = shorterPath.previousMove[nbPreviousMove -1];
                    // pas de demi tour
                    if (direction == Direction.Left && lastMove.direction == Direction.Right
                        || direction == Direction.Bottom && lastMove.direction == Direction.Top
                        || direction == Direction.Right && lastMove.direction == Direction.Left
                        || direction == Direction.Top && lastMove.direction == Direction.Bottom)
                        continue;

                    // no more than three move in the same direction
                    if (shorterPath.previousMove.Count >= 3
                        && lastMove.direction == direction 
                        && shorterPath.previousMove[nbPreviousMove - 2].direction == direction 
                        && shorterPath.previousMove[nbPreviousMove - 3].direction == direction)
                        continue;
                }

                var newHeatLoss = shorterPath.heatLoss + int.Parse(grid[newPoint.Y][newPoint.X].ToString());
                //var visitedPoint = visitedPoints.FirstOrDefault(p => p.position == newPoint);

                //if (visitedPoint != default && visitedPoint.heatLoss < newHeatLoss)
                //    continue;

                if(shorterPath.previousMove.Any(p => p.postion == newPoint))
                    continue;

                if (newPoint == target)
                    lessHeatLoss = Math.Min(lessHeatLoss, newHeatLoss);
                else
                {

                    var newDirection = new List<(Direction direction, Point postion)>(shorterPath.previousMove)
                    {
                        (direction, newPoint)
                    };
                    possiblePaths.Add((newPoint, newDirection, newHeatLoss));
                    
                    //visitedPoints.Add((newPoint, newHeatLoss));
                    //if (visitedPoint != default)
                    //    visitedPoints.Remove(visitedPoint);
                }
            }
            possiblePaths.Remove(shorterPath);
            //}
            //possiblePaths = nextPaths;
            nbLoop++;
        }

        Console.WriteLine(lessHeatLoss);


    }

    private void ShowPath((Point position, List<(Direction direction, Point postion)> previousMove, int heatLoss) path)
    {
        Console.WriteLine();
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                var visitedPoint = visitedPoints.FirstOrDefault(p => p.position.X == x && p.position.Y == y);
                var s = string.Empty;
                if(path.position.X == x && path.position.Y == y)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    s = path.heatLoss.ToString();
                }
                else if (path.previousMove.Any(p => p.postion.X == x && p.postion.Y == y))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    s = grid[y][x].ToString();
                }
                //else if (visitedPoint != default)
                //{
                //    Console.BackgroundColor = ConsoleColor.Yellow;
                //    Console.ForegroundColor = ConsoleColor.Black;
                //    s = visitedPoint.heatLoss.ToString();
                //}
                else
                    s = grid[y][x].ToString();

                Console.Write(s.PadRight(3));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
            
        }
                
    }
}
