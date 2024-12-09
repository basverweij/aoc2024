var lines = await File.ReadAllLinesAsync("input.txt");

var search = string.Join(
    "@", // including a separator prevents false positives
    lines);

var puzzle1 = SearchWords(
    search,
    lines[0].Length);

Console.WriteLine($"Day 4 - Puzzle 1: {puzzle1}");

int SearchWords(
    string search,
    int    rowLength)
{
    var directions = Helper.BuildDirections(rowLength);

    var count = 0;

    var span = search.AsSpan();

    var remaining = span;

    var offset = 0;

    while ( true )
    {
        var start = remaining.IndexOf(Helper.Letters[0]);

        if ( start == -1 )
        {
            break;
        }

        foreach ( var direction in directions )
        {
            for ( var i = 1; i < Helper.Letters.Length; i++ )
            {
                var j = offset + start + ( i * direction );

                if ( j < 0 ||
                    j >= span.Length ||
                    span[j] != Helper.Letters[i] )
                {
                    goto NextDirection;
                }
            }

            count++;

            NextDirection: ;
        }

        offset += start + 1;

        remaining = span[offset..];
    }

    return count;
}

internal static class Helper
{
    internal static readonly char[] Letters = ['X', 'M', 'A', 'S',];

    internal static int[] BuildDirections(
        int rowLength) =>
        [
            1, // Right
            -1, // Left
            -( rowLength + 1 ), // Up
            rowLength + 1, // Down
            -( rowLength + 2 ), // UpLeft
            -rowLength, // UpRight
            rowLength, // DownLeft
            rowLength + 2, // DownRight
        ];
}
