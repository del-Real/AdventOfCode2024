namespace aoc24.Solutions.Day06;

public class Day06 : IDay
{
    public int Part1()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day06",
            "day06_input.txt");
        int result = 0;

        using (StreamReader sr = new StreamReader(inputPath))
        {
            string line = sr.ReadLine();
            char[,] mtx = new char[line.Length, line.Length];

            // Store the input data as a matrix
            while (line != null)
            {
                for (int i = 0; i < mtx.GetLength(0); i++)
                {
                    for (int j = 0; j < mtx.GetLength(0); j++)
                    {
                        mtx[i, j] = line[j];
                    }

                    line = sr.ReadLine();
                }
            }

            // Find guard in matrix
            var guard = findGuard(mtx);

            // Movement loop 
            while (guard.Item1 - 1 >= 0 && guard.Item2 - 1 >= 0 &&
                   guard.Item1 + 1 < mtx.GetLength(0) &&
                   guard.Item2 + 1 < mtx.GetLength(1))
            {
                guard = findGuard(mtx);

                // Up movement (^)
                if (guard.Item3 == 'u')
                {
                    while (guard.Item1 - 1 >= 0 && mtx[guard.Item1 - 1, guard.Item2] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = 'X';
                        result++;

                        // Check if there is already an X in the position
                        if (mtx[guard.Item1 - 1, guard.Item2] == 'X')
                        {
                            result--;
                        }

                        // Move guard up
                        mtx[guard.Item1 - 1, guard.Item2] = '^';
                        guard.Item1--;
                    }

                    // Switch position to right
                    mtx[guard.Item1, guard.Item2] = '>';
                    guard.Item3 = '>';
                }

                // Right movement (>)
                else if (guard.Item3 == 'r')
                {
                    while (guard.Item2 + 1 < mtx.GetLength(1) && mtx[guard.Item1, guard.Item2 + 1] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = 'X';
                        result++;

                        // Check if there is already an X in the position
                        if (mtx[guard.Item1, guard.Item2 + 1] == 'X')
                        {
                            result--;
                        }

                        // Move guard to the right
                        mtx[guard.Item1, guard.Item2 + 1] = '>';
                        guard.Item2++;
                    }

                    // Switch position to down
                    mtx[guard.Item1, guard.Item2] = 'v';
                    guard.Item3 = 'v';
                }

                // Down movement (v)
                else if (guard.Item3 == 'd')
                {
                    while (guard.Item1 + 1 < mtx.GetLength(0) && mtx[guard.Item1 + 1, guard.Item2] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = 'X';
                        result++;

                        // Check if there is already an X in the position
                        if (mtx[guard.Item1 + 1, guard.Item2] == 'X')
                        {
                            result--;
                        }

                        // Move guard down
                        mtx[guard.Item1 + 1, guard.Item2] = 'v';
                        guard.Item1++;
                    }

                    // Switch position to left
                    mtx[guard.Item1, guard.Item2] = '<';
                    guard.Item3 = '<';
                }

                // Left movement (<)
                else if (guard.Item3 == 'l')
                {
                    while (guard.Item2 - 1 >= 0 && mtx[guard.Item1, guard.Item2 - 1] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = 'X';
                        result++;

                        // Check if there is already an X in the position
                        if (mtx[guard.Item1, guard.Item2 - 1] == 'X')
                        {
                            result--;
                        }

                        // Move guard to the left
                        mtx[guard.Item1, guard.Item2 - 1] = '<';
                        guard.Item2--;
                    }

                    // Switch position to up
                    mtx[guard.Item1, guard.Item2] = '^';
                    guard.Item3 = '^';
                }
            }

            // Add X to the last position
            mtx[guard.Item1, guard.Item2] = 'X';
            result++;
        }

        return result;
    }

    public (int, int, char) findGuard(char[,] mtx)
    {
        // coordX, coordY, orientation
        var coords = (-1, -1, '\0');

        // Find guard in matrix
        for (int i = 0; i < mtx.GetLength(0); i++)
        {
            for (int j = 0; j < mtx.GetLength(1); j++)
            {
                if (mtx[i, j] == '^')
                {
                    coords = (i, j, 'u');
                }

                else if (mtx[i, j] == '>')
                {
                    coords = (i, j, 'r');
                }

                else if (mtx[i, j] == 'v')
                {
                    coords = (i, j, 'd');
                }

                else if (mtx[i, j] == '<')
                {
                    coords = (i, j, 'l');
                }
            }
        }

        return coords;
    }

    public void printMtx(char[,] mtx)
    {
        // Print mtx (debug)
        for (int i = 0; i < mtx.GetLength(0); i++)
        {
            for (int j = 0; j < mtx.GetLength(1); j++)
            {
                Console.Write(mtx[i, j]);
            }

            Console.WriteLine();
        }
    }

    public int Part2()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day06",
            "day06_test.txt");
        int result = 0;

        using (StreamReader sr = new StreamReader(inputPath))
        {
            string line = sr.ReadLine();
            char[,] mtx = new char[line.Length, line.Length];

            // Store the input data as a matrix
            while (line != null)
            {
                for (int i = 0; i < mtx.GetLength(0); i++)
                {
                    for (int j = 0; j < mtx.GetLength(0); j++)
                    {
                        mtx[i, j] = line[j];
                    }

                    line = sr.ReadLine();
                }
            }

            // Find guard in matrix
            var guard = findGuard(mtx);

            // Movement loop 
            while (guard.Item1 - 1 >= 0 && guard.Item2 - 1 >= 0 &&
                   guard.Item1 + 1 < mtx.GetLength(0) &&
                   guard.Item2 + 1 < mtx.GetLength(1))
            {
                guard = findGuard(mtx);

                // Up movement (^)
                if (guard.Item3 == 'u')
                {
                    while (guard.Item1 - 1 >= 0 && mtx[guard.Item1 - 1, guard.Item2] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = '|';

                        // Move guard up
                        mtx[guard.Item1 - 1, guard.Item2] = '^';
                        guard.Item1--;
                    }

                    // Switch position to right
                    mtx[guard.Item1, guard.Item2] = '+';
                    mtx[guard.Item1, guard.Item2 + 1] = '>';
                    guard.Item3 = '>';
                }

                // Right movement (>)
                else if (guard.Item3 == 'r')
                {
                    while (guard.Item2 + 1 < mtx.GetLength(1) && mtx[guard.Item1, guard.Item2 + 1] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = '-';

                        // Move guard to the right
                        mtx[guard.Item1, guard.Item2 + 1] = '>';
                        guard.Item2++;
                    }

                    // Switch position to down
                    mtx[guard.Item1, guard.Item2] = '+';
                    mtx[guard.Item1 + 1, guard.Item2] = 'v';
                    guard.Item3 = 'v';
                }

                // Down movement (v)
                else if (guard.Item3 == 'd')
                {
                    while (guard.Item1 + 1 < mtx.GetLength(0) && mtx[guard.Item1 + 1, guard.Item2] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = '|';

                        // Move guard down
                        mtx[guard.Item1 + 1, guard.Item2] = 'v';
                        guard.Item1++;
                    }

                    // Switch position to left
                    mtx[guard.Item1, guard.Item2] = '+';
                    mtx[guard.Item1, guard.Item2 - 1] = '<';
                    guard.Item3 = '<';
                }

                // Left movement (<)
                else if (guard.Item3 == 'l')
                {
                    while (guard.Item2 - 1 >= 0 && mtx[guard.Item1, guard.Item2 - 1] != '#')
                    {
                        mtx[guard.Item1, guard.Item2] = '-';

                        // Move guard to the left
                        mtx[guard.Item1, guard.Item2 - 1] = '<';
                        guard.Item2--;
                    }

                    // Switch position to up
                    mtx[guard.Item1, guard.Item2] = '+';
                    mtx[guard.Item1 - 1, guard.Item2] = '^';
                    guard.Item3 = '^';
                }
            }

            printMtx(mtx); // print matrix debug
        }

        return result;
    }
}