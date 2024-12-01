// See https://aka.ms/new-console-template for more information
public abstract class Day
{
    protected abstract int DayCount { get; }

    private string GetInput(Env step)
    {
        using var fileStream = File.OpenRead($"input/day{DayCount}_{step}.txt");
        using var reader = new StreamReader(fileStream);
        return reader.ReadToEnd();
    }

    public void Solve()
    {
        Run(GetInput(Env.Run));
    }

    public void Test()
    {
        Run(GetInput(Env.Test));
    }

    protected abstract void Run(string input);
}






