namespace aoc24.Solutions.Day04;

public class Day04 : IDay
{
    /*
     * Part 1
     */
    public int Part1()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day04", "day04_input.txt"); 
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

            // Look for 'X's 
            for (int i = 0; i < mtx.GetLength(0); i++)
            {
                for (int j = 0; j < mtx.GetLength(0); j++)
                {
                    // Pivot over the 'X'
                    if (mtx[i, j] == 'X')
                    {
                        result += evaluateElementPart1(mtx, i, j);
                    }
                }
            }

            /*
             ------------------------------------------------------------------------------------------------
             My initial approach was to create a small 4x4 matrix that took values from the original matrix,
             which ended up being an incorrect solution as I was re-evaluating (and re-counting) words in
             vertical positions that had already been evaluated :)
             ------------------------------------------------------------------------------------------------
            // Assign values from the original matrix to a small 4x4 matrix to check the number of XMAS on it
            for (int i = 0; i <= mtx.GetLength(0) - WINMTXSIZE; i++)
            {
                for (int j = 0; j <= mtx.GetLength(1) - WINMTXSIZE; j++)
                {
                    for (int k = 0; k < WINMTXSIZE; k++)
                    {
                        for (int l = 0; l < WINMTXSIZE; l++)
                        {
                            winMtx[k, l] = mtx[i + k, j + l];
                        }
                    }
                    result += checkMatrix(winMtx);
                    printMatrix(winMtx);
                    Console.WriteLine("\n" + result + "\n");
                }
            }
            */
        }

        return result;
    }

    int evaluateElementPart1(char[,] mtx, int row, int col)
    {
        const int WORD_LENGTH = 4;
        int nMatches = 0;
        int state = 0;

        // Check straight horizontal [ ⟶ ] 
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (col + 1 < mtx.GetLength(1) && state == 0 && mtx[row, col + 1] == 'M')
            {
                state = 1;
            }
            else if (col + 2 < mtx.GetLength(1) && state == 1 && mtx[row, col + 2] == 'A')
            {
                state = 2;
            }
            else if (col + 3 < mtx.GetLength(1) && state == 2 && mtx[row, col + 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check backwards horizontal [ ← ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (col - 1 >= 0 && state == 0 && mtx[row, col - 1] == 'M')
            {
                state = 1;
            }
            else if (col - 2 >= 0 && state == 1 && mtx[row, col - 2] == 'A')
            {
                state = 2;
            }
            else if (col - 3 >= 0 && state == 2 && mtx[row, col - 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check falling vertical [ ↓ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row + 1 < mtx.GetLength(0) && state == 0 && mtx[row + 1, col] == 'M')
            {
                state = 1;
            }
            else if (row + 2 < mtx.GetLength(0) && state == 1 && mtx[row + 2, col] == 'A')
            {
                state = 2;
            }
            else if (row + 3 < mtx.GetLength(0) && state == 2 && mtx[row + 3, col] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check rising vertical [ ↑ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row - 1 >= 0 && state == 0 && mtx[row - 1, col] == 'M')
            {
                state = 1;
            }
            else if (row - 2 >= 0 && state == 1 && mtx[row - 2, col] == 'A')
            {
                state = 2;
            }
            else if (row - 3 >= 0 && state == 2 && mtx[row - 3, col] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check falling backslash diagonal [ ↘ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row + 1 < mtx.GetLength(0) && col + 1 < mtx.GetLength(1) &&
                state == 0 && mtx[row + 1, col + 1] == 'M')
            {
                state = 1;
            }
            else if (row + 2 < mtx.GetLength(0) && col + 2 < mtx.GetLength(1) && state == 1 &&
                     mtx[row + 2, col + 2] == 'A')
            {
                state = 2;
            }
            else if (row + 3 < mtx.GetLength(0) && col + 3 < mtx.GetLength(1) && state == 2 &&
                     mtx[row + 3, col + 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check rising backslash diagonal [ ↖ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row - 1 >= 0 && col - 1 >= 0 && state == 0 && mtx[row - 1, col - 1] == 'M')
            {
                state = 1;
            }
            else if (row - 2 >= 0 && col - 2 >= 0 && state == 1 && mtx[row - 2, col - 2] == 'A')
            {
                state = 2;
            }
            else if (row - 3 >= 0 && col - 3 >= 0 && state == 2 && mtx[row - 3, col - 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check rising slash diagonal [ ↙ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row + 1 < mtx.GetLength(0) && col - 1 >= 0 && state == 0 && mtx[row + 1, col - 1] == 'M')
            {
                state = 1;
            }
            else if (row + 2 < mtx.GetLength(0) && col - 2 >= 0 && state == 1 && mtx[row + 2, col - 2] == 'A')
            {
                state = 2;
            }
            else if (row + 3 < mtx.GetLength(0) && col - 3 >= 0 && state == 2 && mtx[row + 3, col - 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        state = 0;
        // Check rising slash diagonal [ ↗ ]
        for (int i = 0; i < WORD_LENGTH; i++)
        {
            if (row - 1 >= 0 && col + 1 < mtx.GetLength(1) && state == 0 && mtx[row - 1, col + 1] == 'M')
            {
                state = 1;
            }
            else if (row - 2 >= 0 && col + 2 < mtx.GetLength(1) && state == 1 && mtx[row - 2, col + 2] == 'A')
            {
                state = 2;
            }
            else if (row - 3 >= 0 && col + 3 < mtx.GetLength(1) && state == 2 && mtx[row - 3, col + 3] == 'S')
            {
                nMatches++;
                state = 0;
            }
            else
            {
                break;
            }
        }

        return nMatches;
    }

    /*
     * Part 2
     */

    public int Part2()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day04", "day04_input.txt"); 
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

            // Look for 'X's 
            for (int i = 0; i < mtx.GetLength(0); i++)
            {
                for (int j = 0; j < mtx.GetLength(0); j++)
                {
                    // Pivot over the 'A'
                    if (mtx[i, j] == 'A')
                    {
                        result += evaluateElementPart2(mtx, i, j);
                    }
                }
            }
        }

        return result;
    }

    private int evaluateElementPart2(char[,] mtx, int row, int col)
    {
        int nMatches = 0;
        bool backlash = false;
        bool slash = false;

        // Check falling backslash diagonal [ ↘ ]
        if (row - 1 >= 0 && col - 1 >= 0 &&
            mtx[row - 1, col - 1] == 'M' &&
            row + 1 < mtx.GetLength(0) &&
            col + 1 < mtx.GetLength(1) &&
            mtx[row + 1, col + 1] == 'S')
        {
            backlash = true;
        }

        // Check rising backslash diagonal [ ↖ ]
        else if (row - 1 >= 0 && col - 1 >= 0 &&
                 mtx[row - 1, col - 1] == 'S' &&
                 row + 1 < mtx.GetLength(0) &&
                 col + 1 < mtx.GetLength(1) &&
                 mtx[row + 1, col + 1] == 'M')
        {
            backlash = true;
        }

        // Check rising slash diagonal [ ↙ ]
        if (row - 1 >= 0 && col + 1 < mtx.GetLength(1) &&
            mtx[row - 1, col + 1] == 'M' &&
            row + 1 < mtx.GetLength(0) &&
            col - 1 >= 0 &&
            mtx[row + 1, col - 1] == 'S')
        {
            slash = true;
        }

        // Check rising slash diagonal [ ↗ ]
        else if (row - 1 >= 0 && col + 1 < mtx.GetLength(1) &&
                 mtx[row - 1, col + 1] == 'S' &&
                 row + 1 < mtx.GetLength(0) &&
                 col - 1 >= 0 &&
                 mtx[row + 1, col - 1] == 'M')
        {
            slash = true;
        }

        if (backlash && slash)
        {
            nMatches++;
        }

        return nMatches;
    }
}