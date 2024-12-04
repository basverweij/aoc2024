using System.Text.RegularExpressions;

var program = await File.ReadAllTextAsync("input.txt");

var puzzle1 = ParseProgram(program);

Console.WriteLine($"Day 3 - Puzzle 1: {puzzle1}");

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

internal static partial class Helper
{
    [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)")]
    internal static partial Regex Mul { get; }
}
