namespace nqueen.Application
{
    public class NKnight
    {
        private int size;
        private int[,] board;
        private bool[,] unavailable;
        private List<(int, int)> knightPositions; // List to store placed knight positions

        public NKnight(int n)
        {
            size = n;
            board = new int[n, n];
            unavailable = new bool[n, n];
            knightPositions = new List<(int, int)>();
        }

        public void Solve()
        {
            if (size != 1 && size < 4)
            {
                Console.WriteLine("No valid solution found.");
                return;
            }
            if (PlaceKnights(0))
                PrintBoard();
            else
                Console.WriteLine("No valid solution found.");
        }

        private bool PlaceKnights(int placedKnights)
        {
            if (placedKnights == size) return true;

            // Prioritize placing knights in positions that maximize board coverage
            foreach (var position in GetBestPlacementOrder())
            {
                int row = position.Item1;
                int col = position.Item2;

                if (IsSafe(row, col))
                {
                    board[row, col] = 1;
                    MarkThreats(row, col, true);
                    knightPositions.Add((row, col)); // Track placed knights

                    if (PlaceKnights(placedKnights + 1))
                        return true;

                    board[row, col] = 0;
                    MarkThreats(row, col, false);
                    knightPositions.Remove((row, col));
                }
            }
            return false;
        }

        private bool IsSafe(int row, int col)
        {
            return !unavailable[row, col];
        }

        private void MarkThreats(int row, int col, bool state)
        {
            int[][] moves = {
            new int[] { -2, -1 }, new int[] { -2, 1 },
            new int[] { -1, -2 }, new int[] { -1, 2 },
            new int[] { 1, -2 }, new int[] { 1, 2 },
            new int[] { 2, -1 }, new int[] { 2, 1 }
        };

            unavailable[row, col] = state;

            foreach (var move in moves)
            {
                int newRow = row + move[0];
                int newCol = col + move[1];

                if (newRow >= 0 && newRow < size && newCol >= 0 && newCol < size)
                    unavailable[newRow, newCol] = state;
            }
        }

        private List<(int, int)> GetBestPlacementOrder()
        {
            List<(int, int)> positions = new List<(int, int)>();

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    positions.Add((r, c));
                }
            }

            // Sort based on central positioning to maximize board coverage
            positions.Sort((a, b) =>
            {
                int centerA = Math.Abs(size / 2 - a.Item1) + Math.Abs(size / 2 - a.Item2);
                int centerB = Math.Abs(size / 2 - b.Item1) + Math.Abs(size / 2 - b.Item2);
                return centerA.CompareTo(centerB);
            });

            return positions;
        }

        private void PrintBoard()
        {
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                    Console.Write(board[r, c] == 1 ? "K " : ". ");
                Console.WriteLine();
            }
        }
    }
}
