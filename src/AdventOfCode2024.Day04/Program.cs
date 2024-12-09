var lines = await File.ReadAllLinesAsync("input.txt");

var search = string.Join(
    "@", // including a separator prevents false positives
    lines);

var puzzle1 = SearchWords(
    search,
    lines[0].Length);

Console.WriteLine($"Day 4 - Puzzle 1: {puzzle1}");

var puzzle2 = SearchCrossWords(
    search,
    lines[0].Length);

Console.WriteLine($"Day 4 - Puzzle 2: {puzzle2}");

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

int SearchCrossWords(
    string search,
    int    rowLength)
{
    var directions = Helper.BuildCrossDirections(rowLength);

    var count = 0;

    var span = search.AsSpan();

    var remaining = span;

    var offset = 0;

    while ( true )
    {
        var start = remaining.IndexOf('A');

        if ( start == -1 )
        {
            break;
        }

        foreach ( var crossLetters in Helper.CrossLettersPermutations )
        {
            for ( var i = 0; i < crossLetters.Length; i++ )
            {
                var j = offset + start + directions[i];

                if ( j < 0 ||
                    j >= span.Length ||
                    span[j] != crossLetters[i] )
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

    internal static readonly char[][] CrossLettersPermutations =
        [
                ['M', 'S', 'M', 'S',],
                ['M', 'S', 'S', 'M',],
                ['S', 'M', 'M', 'S',],
                ['S', 'M', 'S', 'M',],
        ];

    internal static int[] BuildCrossDirections(
        int rowLength) =>
        [
            -( rowLength + 2 ), // UpLeft
            rowLength + 2, // DownRight
            -rowLength, // UpRight
            rowLength, // DownLeft
        ];
}
