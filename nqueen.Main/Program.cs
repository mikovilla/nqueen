using nqueen.Application;

int queenSize = 4;
int knightSize = 5;
if (args.Length > 0)
{
    if (!string.IsNullOrEmpty(args[0]))
    {
        queenSize = int.Parse(args[0]);
    }
    if (!string.IsNullOrEmpty(args[1]))
    {
        knightSize = int.Parse(args[1]);
    }
}

Console.WriteLine($"NQUEEN for size ({queenSize}):");
NQueen nqueen = new NQueen(queenSize);
nqueen.Solve();

Console.WriteLine($"NKNIGHT for size ({knightSize}):");
NKnight nknight = new NKnight(knightSize);
nknight.Solve();