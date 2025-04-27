namespace nqueen.Application
{
    public class NQueen
    {
        private int size; // The size of the chessboard (N x N)
        private List<int[]> solutions = new List<int[]>(); // Stores all valid board configurations

        public NQueen(int n)
        {
            size = n; // Initialize board size
        }

        public void Solve()
        {
            int[] board = new int[size]; // Represents column placement for each row
            PlaceQueen(board, 0); // Start placing queens from row 0
            Console.WriteLine($"Total solutions for Queen: {solutions.Count}");
            PrintSolutions(); // Print all found solutions
        }

        private void PlaceQueen(int[] board, int row)
        {
            // Base case: If all rows are filled, a valid solution is found
            if (row == size)
            {
                solutions.Add((int[])board.Clone()); // Store the completed board configuration
                return;
            }

            // Track available positions using bitmasking
            int availablePositions = ((1 << size) - 1);
            for (int prevRow = 0; prevRow < row; prevRow++)
            {
                int col = board[prevRow];

                // Prune: Remove occupied columns and diagonals
                availablePositions &= ~(1 << col);
                availablePositions &= ~(1 << (col + (row - prevRow)));
                availablePositions &= ~(1 << (col - (row - prevRow)));
            }

            // Loop through remaining safe positions
            for (int col = 0; col < size; col++)
            {
                if ((availablePositions & (1 << col)) != 0) // Ensure position is still safe
                {
                    board[row] = col; // Place queen
                    PlaceQueen(board, row + 1); // Recur for next row
                }
            }
        }

        private void PrintSolutions()
        {
            foreach (var board in solutions)
            {
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        Console.Write(board[row] == col ? "Q " : ". "); // Display queen or empty space
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
