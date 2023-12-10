internal class Day10_1 : Day
{
    protected override int DayCount => 10;

    List<string> grid;

    protected override void Run(string input)
    {
        grid = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        var startingPosition = (y: -1, x: -1);
        for(var y = 0; y < grid.Count; y++)
            for(var x = 0; x < grid[y].Length; x++)
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
            var currentShape = grid[currentPosition.y][currentPosition.x];
            var nextPosition = (y: -1, x: -1);
            switch (currentShape)
            {
                case '|':
                    if(previousPosition.y > currentPosition.y)
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


        Console.WriteLine(step/2);
        
    }
}
