namespace aoc24.Solutions.Day03;

public class Day03 : IDay
{
    public int Part1()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day03", "day03_input.txt"); 
        int result = 0;

        // keep track of the states
        int state = 0;

        // 'm'  ->  1
        // 'u'  ->  2
        // 'l'  ->  3
        // '('  ->  4
        // 'X'  ->  
        // ','  ->  5
        // 'Y'  ->  
        // ')'  ->  0

        using (StreamReader reader = new StreamReader(inputPath))
        {
            int currentChar;
            string sA = "", sB = "";
            int numA = 0, numB = 0;

            // Read character by character
            while ((currentChar = reader.Read()) != -1)
            {
                // 'm'
                if (state == 0 && (char)currentChar == 'm')
                {
                    state = 1;
                }
                // 'u'
                else if (state == 1 && (char)currentChar == 'u')
                {
                    state = 2;
                }
                // 'l'
                else if (state == 2 && (char)currentChar == 'l')
                {
                    state = 3;
                }
                // '('
                else if (state == 3 && (char)currentChar == '(')
                {
                    state = 4;
                }
                // 'X'
                else if (state == 4 && Char.IsDigit((char)currentChar))
                {
                    sA += (char)currentChar;
                }

                // end of digit
                else if (state == 4 && !Char.IsDigit((char)currentChar))
                {
                    // ','
                    if ((char)currentChar == ',')
                    {
                        numA = Convert.ToInt32(sA);
                        state = 5;
                    }
                    else
                    {
                        // reset
                        sA = sB = "";
                        numA = numB = 0;
                        state = 0;
                    }
                }

                // 'Y'
                else if (state == 5 && Char.IsDigit((char)currentChar))
                {
                    sB += (char)currentChar;
                }

                // end of digit
                else if (state == 5 && !Char.IsDigit((char)currentChar))
                {
                    // ')'
                    if ((char)currentChar == ')')
                    {
                        numB = Convert.ToInt32(sB);
                        result += numA * numB;
                    }

                    // reset
                    sA = sB = "";
                    numA = numB = 0;
                    state = 0;
                }

                // None of above
                else
                {
                    // reset
                    sA = sB = "";
                    numA = numB = 0;
                    state = 0;
                }
            }
        }

        return result;
    }

    public int Part2()
    {
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day03", "day03_input.txt"); 
        int result = 0;

        // keep track of the states
        int state = 0;

        // m  ->  1
        // u  ->  2
        // l  ->  3
        // (  ->  4
        // X  ->  
        // ,  ->  5
        // Y  ->  
        // )  ->  0

        int scanState = 0;
        bool scan = true;

        // d  ->  1
        // o  ->  2
        // n  ->  3
        // '  ->  4
        // t  ->  5
        // (  ->  6
        // )  ->  0
        // (  ->  7
        // )  ->  0

        using (StreamReader reader = new StreamReader(inputPath))
        {
            int currentChar;
            string sA = "", sB = "";
            int numA = 0, numB = 0;

            // Read character by character
            while ((currentChar = reader.Read()) != -1)
            {
                // Check do() or don't()
                // 'd'
                if (scanState == 0 && (char)currentChar == 'd')
                {
                    scanState = 1;
                }
                // 'o'
                else if (scanState == 1 && (char)currentChar == 'o')
                {
                    scanState = 2;
                }
                else if (scanState == 2)
                {
                    if ((char)currentChar == 'n')
                    {
                        scanState = 3;
                    }
                    else if ((char)currentChar == '(')
                    {
                        scanState = 7;
                    }
                    else
                    {
                        scanState = 0;
                    }
                }
                
                // '
                else if (scanState == 3 && (char)currentChar == '\'')
                {
                    scanState = 4;
                }
                // 't'
                else if (scanState == 4 && (char)currentChar == 't')
                {
                    scanState = 5;
                }
                // don't + '(' 
                else if (scanState == 5 && (char)currentChar == '(')
                {
                    scanState = 6;
                }
                // ')'
                else if (scanState == 6 && (char)currentChar == ')')
                {
                    scan = false;
                    scanState = 0;
                }
                // do + '('
                else if (scanState == 7)
                {
                    // ')'
                    if ((char)currentChar == ')')
                    {
                        scan = true;
                    }
                    scanState = 0;
                }
                else
                {
                    scanState = 0;
                }

                // Check mul(X,Y)
                if (scan)
                {
                    // 'm'
                    if (state == 0 && (char)currentChar == 'm')
                    {
                        state = 1;
                    }
                    // 'u'
                    else if (state == 1 && (char)currentChar == 'u')
                    {
                        state = 2;
                    }
                    // 'l'
                    else if (state == 2 && (char)currentChar == 'l')
                    {
                        state = 3;
                    }
                    // '('
                    else if (state == 3 && (char)currentChar == '(')
                    {
                        state = 4;
                    }
                    // 'X'
                    else if (state == 4 && Char.IsDigit((char)currentChar))
                    {
                        sA += (char)currentChar;
                    }

                    // end of digit
                    else if (state == 4 && !Char.IsDigit((char)currentChar))
                    {
                        // ','
                        if ((char)currentChar == ',')
                        {
                            numA = Convert.ToInt32(sA);
                            state = 5;
                        }
                        else
                        {
                            // reset
                            sA = sB = "";
                            numA = numB = 0;
                            state = 0;
                        }
                    }

                    // 'Y'
                    else if (state == 5 && Char.IsDigit((char)currentChar))
                    {
                        sB += (char)currentChar;
                    }

                    // end of digit
                    else if (state == 5 && !Char.IsDigit((char)currentChar))
                    {
                        // ')'
                        if ((char)currentChar == ')')
                        {
                            numB = Convert.ToInt32(sB);
                            result += numA * numB;
                        }

                        // reset
                        sA = sB = "";
                        numA = numB = 0;
                        state = 0;
                    }

                    // None of above
                    else
                    {
                        // reset
                        sA = sB = "";
                        numA = numB = 0;
                        state = 0;
                    }
                }
            }
        }

        return result;
    }
}