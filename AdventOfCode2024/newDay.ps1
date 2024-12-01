$day=$args[0]

$fileName1 = "Day" + $day + "_1"
$fileName2 = "Day" + $day + "_2"

$inputName= "day" + $day + "_"

Add-Content -Path "Day$($day)_1.cs" -Value "internal class Day$($day)_1 : Day
{
    protected override int DayCount => $day;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();


        Console.WriteLine();
        
    }
}"

Add-Content -Path "Day$($day)_2.cs" -Value "internal class Day$($day)_2 : Day
{
    protected override int DayCount => $day;

    protected override void Run(string input)
    {
        var inputLines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();


        Console.WriteLine();
        
    }
}"

New-Item "./input/day$($day)_Run.txt"
New-Item "./input/day$($day)_Test.txt"