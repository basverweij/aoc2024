var lines = await File.ReadAllLinesAsync("input.txt");

var reports = lines
    .Select(ParseLine)
    .ToArray();

var puzzle1 = reports.Count(IsSafe);

Console.WriteLine($"Day 2 - Puzzle 1: {puzzle1}");

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
