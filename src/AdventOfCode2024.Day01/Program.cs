var lines = await File.ReadAllLinesAsync("input.txt");

var locationIds = lines
    .Select(ParseLine)
    .ToArray();

var left = locationIds
    .Select(l => l.left)
    .Order()
    .ToArray();

var right = locationIds
    .Select(l => l.right)
    .Order()
    .ToArray();

var puzzle1 = 0;

for ( var i = 0; i < locationIds.Length; i++ )
{
    puzzle1 += Math.Abs(left[i] - right[i]);
}

Console.WriteLine($"Day 1 - Puzzle 1: {puzzle1}");

(int left, int right) ParseLine(
    string line)
{
    var parts = line
        .Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    return ( parts[0], parts[1] );
}
