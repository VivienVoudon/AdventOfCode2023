using System.Collections;
using System.Drawing;

internal class Day10_2 : Day
{
    protected override int DayCount => 10;

    List<string> grid;
    int[,] bigGrid;

    protected override void Run(string input)
    {
        grid = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        var fillingGrid = new int[grid.Count, grid.First().Length];


        var startingPosition = (y: -1, x: -1);
        for (var y = 0; y < grid.Count; y++)
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'S')
                    startingPosition = (y, x);

            }

        // first step done manually
        //for Test
        //var previousPosition = (startingPosition.y, x: startingPosition.x + 1);
        //grid[startingPosition.y] = grid[startingPosition.y].Replace("S", "F");
        // For run
        var previousPosition = (startingPosition.y, x: startingPosition.x - 1);
        grid[startingPosition.y] = grid[startingPosition.y].Replace("S", "J");

        var currentPosition = startingPosition;
        int step = 0;
        do
        {
            fillingGrid[currentPosition.y, currentPosition.x] = 1;
            var currentShape = grid[currentPosition.y][currentPosition.x];

            var nextPosition = (y: -1, x: -1);
            switch (currentShape)
            {
                case '|':
                    if (previousPosition.y > currentPosition.y)
                        nextPosition = (y: currentPosition.y - 1, x: currentPosition.x);
                    else
                        nextPosition = (y: currentPosition.y + 1, x: currentPosition.x);
                    break;

                case '-':
                    if (previousPosition.x > currentPosition.x)
                        nextPosition = (y: currentPosition.y, x: currentPosition.x - 1);
                    else
                        nextPosition = (y: currentPosition.y, x: currentPosition.x + 1);
                    break;

                case 'J':
                    if (previousPosition.x == currentPosition.x - 1)
                        nextPosition = (y: currentPosition.y - 1, x: currentPosition.x);
                    else
                        nextPosition = (y: currentPosition.y, x: currentPosition.x - 1);
                    break;

                case 'L':
                    if (previousPosition.y == currentPosition.y - 1)
                        nextPosition = (y: currentPosition.y, x: currentPosition.x + 1);
                    else
                        nextPosition = (y: currentPosition.y - 1, x: currentPosition.x);
                    break;

                case '7':
                    if (previousPosition.x == currentPosition.x - 1)
                        nextPosition = (y: currentPosition.y + 1, x: currentPosition.x);
                    else
                        nextPosition = (y: currentPosition.y, x: currentPosition.x - 1);
                    break;

                case 'F':
                    if (previousPosition.x == currentPosition.x + 1)
                        nextPosition = (y: currentPosition.y + 1, x: currentPosition.x);
                    else
                        nextPosition = (y: currentPosition.y, x: currentPosition.x + 1);
                    break;

                default:
                    break;
            }

            previousPosition = currentPosition;
            currentPosition = nextPosition;
            step++;
        } while (currentPosition != startingPosition);

        // Agrandit la grille poiur gerer plus facilement le fait que || laisse passer
        bigGrid = new int[fillingGrid.GetLength(0) * 3, fillingGrid.GetLength(1) * 3];
        for (var y = 0; y < fillingGrid.GetLength(0); y++)
            for (var x = 0; x < fillingGrid.GetLength(1); x++)
            {
                var shape = fillingGrid[y, x] == 0 ? '.' : grid[y][x];
                //var shape = grid[y][x];
                switch (shape)
                {
                    case '|':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 1; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 0; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 1; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    case '-':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 0; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 1; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 1;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 0; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    case 'J':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 1; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 1; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 0; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    case 'L':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 1; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 0; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 1;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 0; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    case '7':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 0; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 1; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 1; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    case 'F':
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 0; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 0; bigGrid[y * 3 + 1, x * 3 + 1] = 1; bigGrid[y * 3 + 1, x * 3 + 2] = 1;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 1; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;

                    default:
                        bigGrid[y * 3, x * 3] = 0; bigGrid[y * 3, x * 3 + 1] = 0; bigGrid[y * 3, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 1, x * 3] = 0; bigGrid[y * 3 + 1, x * 3 + 1] = 0; bigGrid[y * 3 + 1, x * 3 + 2] = 0;
                        bigGrid[y * 3 + 2, x * 3] = 0; bigGrid[y * 3 + 2, x * 3 + 1] = 0; bigGrid[y * 3 + 2, x * 3 + 2] = 0;
                        break;
                }
            }

        ShowGrid(fillingGrid);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        ShowGrid(bigGrid);

        // spread the virus in the big grid from the top
        var shouldBeTest = new Queue<(int Y, int X)>();
        shouldBeTest.Enqueue(new (0,0));
        do
        {
            var infectedPoint = shouldBeTest.Dequeue();
            
            bigGrid[infectedPoint.Y, infectedPoint.X] = 2;

            if (infectedPoint.Y + 1 < bigGrid.GetLength(0) && bigGrid[infectedPoint.Y + 1, infectedPoint.X] == 0
                && !shouldBeTest.Any(p => p.Y == infectedPoint.Y + 1 && p.X == infectedPoint.X))
                shouldBeTest.Enqueue(new (infectedPoint.Y + 1, infectedPoint.X));

            if (infectedPoint.Y > 0 && bigGrid[infectedPoint.Y - 1, infectedPoint.X] == 0
                && !shouldBeTest.Any(p => p.Y == infectedPoint.Y - 1 && p.X == infectedPoint.X))
                shouldBeTest.Enqueue(new (infectedPoint.Y - 1, infectedPoint.X));

            if (infectedPoint.X + 1 < bigGrid.GetLength(1) && bigGrid[infectedPoint.Y, infectedPoint.X + 1] == 0
                && !shouldBeTest.Any(p => p.Y == infectedPoint.Y && p.X == infectedPoint.X + 1))
                shouldBeTest.Enqueue(new (infectedPoint.Y, infectedPoint.X + 1));

            if (infectedPoint.X > 0 && bigGrid[infectedPoint.Y, infectedPoint.X - 1] == 0
                && !shouldBeTest.Any(p => p.Y == infectedPoint.Y + 1 && p.X == infectedPoint.X - 1))
                shouldBeTest.Enqueue(new (infectedPoint.Y, infectedPoint.X - 1));

        } while (shouldBeTest.Count > 0); 

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        ShowGrid(bigGrid);

        // reduce the grid
        var result = 0;
        for (var y = 0; y < fillingGrid.GetLength(0); y++)
            for (var x = 0; x < fillingGrid.GetLength(1); x++)
            {
                if (bigGrid[y * 3, x * 3] == 0 && bigGrid[y * 3, x * 3 + 1] == 0 && bigGrid[y * 3, x * 3 + 2] == 0
                && bigGrid[y * 3 + 1, x * 3] == 0 && bigGrid[y * 3 + 1, x * 3 + 1] == 0 && bigGrid[y * 3 + 1, x * 3 + 2] == 0
                && bigGrid[y * 3 + 2, x * 3] == 0 && bigGrid[y * 3 + 2, x * 3 + 1] == 0 && bigGrid[y * 3 + 2, x * 3 + 2] == 0)
                {
                    fillingGrid[y, x] = 2;
                    result++;
                }
            }

        ShowGrid(fillingGrid);

        Console.WriteLine(result);
    }

    private void Infect(int y, int x)
    {
        bigGrid[y, x] = 2;

        if (y + 1 < bigGrid.GetLength(0) && bigGrid[y + 1, x] == 0)
            Infect(y + 1, x);

        if (y > 0 && bigGrid[y - 1, x] == 0)
            Infect(y - 1, x);

        if (x + 1 < bigGrid.GetLength(1) && bigGrid[y, x + 1] == 0)
            Infect(y, x + 1);

        if (x > 0 && bigGrid[y, x - 1] == 0)
            Infect(y, x - 1);
    }

    private void ShowGrid(int[,] fillingGrid)
    {

        for (var y = 0; y < fillingGrid.GetLength(0); y++)
        {
            for (var x = 0; x < fillingGrid.GetLength(1); x++)
            {
                if(fillingGrid[y, x] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('X');
                }
                else if (fillingGrid[y, x] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write('X');
                }
                else if (fillingGrid[y, x] == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('X');
                }
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
