using System.Text.RegularExpressions;

var program = await File.ReadAllTextAsync("input.txt");

var puzzle1 = ParseProgram(program);

Console.WriteLine($"Day 3 - Puzzle 1: {puzzle1}");

var puzzle2 = ParseProgramWithConditions(program);

Console.WriteLine($"Day 3 - Puzzle 2: {puzzle2}");

static int ParseProgram(
    string program)
{
    var span = program.AsSpan();

    var matches = Helper.Mul.EnumerateMatches(span);

    var sum = 0;

    foreach ( var match in matches )
    {
        var mul = span[( match.Index + 4 )..( match.Index + match.Length - 1 )];

        var comma = mul.IndexOf(',');

        var x = int.Parse(mul[..comma]);

        var y = int.Parse(mul[( comma + 1 )..]);

        sum += x * y;
    }

    return sum;
}

static int ParseProgramWithConditions(
    string program)
{
    var span = program.AsSpan();

    var matches = Helper.MulWithConditions.EnumerateMatches(span);

    var sum = 0;

    var enabled = true;

    foreach ( var match in matches )
    {
        if ( match.Length == 4 ) // `do()`
        {
            enabled = true;

            continue;
        }

        if ( match.Length == 7 ) // `don't()`
        {
            enabled = false;

            continue;
        }

        if ( !enabled )
        {
            continue;
        }

        var mul = span[( match.Index + 4 )..( match.Index + match.Length - 1 )];

        var comma = mul.IndexOf(',');

        var x = int.Parse(mul[..comma]);

        var y = int.Parse(mul[( comma + 1 )..]);

        sum += x * y;
    }

    return sum;
}

internal static partial class Helper
{
    [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)")]
    internal static partial Regex Mul { get; }

    [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)")]
    internal static partial Regex MulWithConditions { get; }
}
