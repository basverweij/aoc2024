var lines = await File.ReadAllLinesAsync("input.txt");

var reports = lines
    .Select(ParseLine)
    .ToArray();

var puzzle1 = reports.Count(IsSafe);

Console.WriteLine($"Day 2 - Puzzle 1: {puzzle1}");

var puzzle2 = reports.Count(IsSafeWithProblemDampener);

Console.WriteLine($"Day 2 - Puzzle 2: {puzzle2}");

static int[] ParseLine(
    string line) =>
    line.Split(' ')
        .Select(int.Parse)
        .ToArray();

static bool IsSafe(
    int[] report)
{
    var deltas = Enumerable
        .Range(
            0,
            report.Length - 1)
        .Select(i => report[i + 1] - report[i])
        .ToArray();

    if ( deltas.Any(d => d is 0 or < -3 or > 3) )
    {
        return false;
    }

    return deltas.All(d => d < 0) || deltas.All(d => d > 0);
}

static bool IsSafeWithProblemDampener(
    int[] report)
{
    if ( IsSafe(report) )
    {
        return true;
    }

    for ( var i = 0; i < report.Length; i++ )
    {
        var left =
            i > 0 ?
                report[..i] :
                    [];

        var right =
            i < report.Length - 1 ?
                report[( i + 1 )..] :
                    [];

        if ( IsSafe([..left, ..right,]) )
        {
            return true;
        }
    }

    return false;
}
