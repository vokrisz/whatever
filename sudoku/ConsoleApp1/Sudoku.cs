namespace Sudoku;
public class Sudoku
{
    //0,0 is top left
    private int[,] _grid = new int[9, 9];
    public int[,] Grid = new int[9, 9];

    public Sudoku()
    {
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                _grid[i, j] = j + 1;

        Scramble();
        Del();
    }

    public bool Check()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (_grid[i, j] != 0
                 && _grid[i, j] != Grid[i, j])
                    throw new Exception();

        return Check(Grid);
    }

    private bool Check(int[,] grid)
    {
        if (grid.GetLength(0) != 9) return false;
        if (grid.GetLength(1) != 9) return false;

        for (int i = 0; i < 9; i++)
        {
            HashSet<int> row = new HashSet<int>();
            HashSet<int> col = new HashSet<int>();
            HashSet<int> box = new HashSet<int>();

            for (int j = 0; j < 9; j++)
            {
                row.Add(grid[i, j]);
                col.Add(grid[j, i]);
                box.Add(grid[(i / 3) * 3, j / 3]);
            }

            if (row.Count() != 9) return false;
            if (col.Count() != 9) return false;
            if (box.Count() != 9) return false;
        }

        return true;
    }

    private void Scramble()
    {
        Random rnd = new Random();
        int col;
        int num;

        while (!Check(_grid))
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    col = rnd.Next(0, 8);

                    num = _grid[i, col];
                    _grid[i, col] = _grid[i, j];
                    _grid[i, j] = num;
                }
        }
    }

    private void Del()
    {
        Random rnd = new Random();

        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (rnd.Next(100) < 80)
                    _grid[i, j] = -1;
    }

    public void Draw()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i % 3 != 0)
            {
                Console.Write("|");
            }

            for (int j = 0; j < 9; j++)
            {

                if (i != j)
                {
                    if (j % 3 == 0)
                    {
                        Console.Write("-");
                    }

                }

                if (_grid[i, j] != -1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(_grid[i, j]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    if (Grid[i, j] != 0)
                    {
                        Console.Write(Grid[i, j]);
                    }
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(Grid[i, j]);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }

            Console.WriteLine();
        }

    }
}
