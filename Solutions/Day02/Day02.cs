namespace aoc24.Solutions.Day02;

public class Day02 : IDay
{
    public int Part1()
    {
        // Read file
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day02", "day02_input.txt");
        StreamReader sr = File.OpenText(Path.GetFullPath(inputPath));
        
        List<int> reports = new List<int>();
        int numSafe = 0;

        // Process file line by line
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            string[] regLine = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert split string to int values
            foreach (var reg in regLine)
            {
                reports.Add(int.Parse(reg));
            }

            bool safe = false; // flag to check if the line is safe
            bool ascending = false; // flag to check if line is ascending
            bool descending = false; // flag to check if line is descending
            bool initial = true;

            // Check line by line if it is safe
            for (int i = 0; i < reports.Count() - 1; i++)
            {
                // Check if line is ascending
                if (initial && reports[i] - reports[i + 1] < 0)
                {
                    ascending = true;
                }
                // Check if line is descending
                else if (initial && reports[i] - reports[i + 1] > 0)
                {
                    descending = true;
                }

                // Check if the jump to the next number is higher than 3
                if (Math.Abs(reports[i] - reports[i + 1]) > 3)
                {
                    safe = false;
                    break;
                }

                // Check if the next number is equal to the current
                if (reports[i] == reports[i + 1])
                {
                    safe = false;
                    break;
                }

                // Check if the next number is higher to the current number and the line is descending
                if (reports[i] < reports[i + 1] && descending)
                {
                    safe = false;
                    break;
                }

                // Check if the next number is lower to the current number and the line is ascending
                if (reports[i] > reports[i + 1] && ascending)
                {
                    safe = false;
                    break;
                }

                safe = true; // the number is safe
                initial = false;
            }

            // If the line is safe increase the counter
            if (safe)
            {
                numSafe++;
            }

            // Reset list
            reports.Clear();
        }

        sr.Close();  
        sr.Dispose();
        
        return numSafe;
    }

    public int Part2()
    {
        // Read file
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day02", "day02_input.txt");
        StreamReader sr = File.OpenText(Path.GetFullPath(inputPath));
        
        List<int> reports = new List<int>();
        int numSafe = 0;

        // Process file line by line
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            string[] regLine = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert split string to int values
            foreach (var reg in regLine)
            {
                reports.Add(int.Parse(reg));
            }

            bool safe = CheckReports(reports, true);
            numSafe += safe ? 1 : 0; // If the line is safe increase the counter

            // Print line [DEBUG]
            // Console.WriteLine(s + "  [" + safe + "]");

            // Reset list
            reports.Clear();
        }

        sr.Close();  
        sr.Dispose();

        return numSafe;
    }

    public bool CheckReports(List<int> reports, bool shield)
    {
        // bool shield: flag to check if we can afford an invalid number in that line
        bool safe = false; // flag to check if the line is safe
        bool initial = true; // flag to check if it is the start of the loop
        bool ascending = false; // flag to check if line is ascending
        bool descending = false; // flag to check if line is descending

        // Check line by line if it is safe
        for (int i = 0; i < reports.Count() - 1; i++)
        {
            // Check if the line is ascending or descending
            if (initial)
            {
                int step = 0;
                int sign = 0;
                for (int j = 0; j < reports.Count() - 1; j++)
                {
                    step = reports[j] - reports[j + 1];
                    sign += step;
                }

                // The line is ascending
                if (sign <= 0)
                {
                    ascending = true;
                }
                // The line is descending
                else
                {
                    descending = true;
                }
            }

            // Check if the jump to the next number is higher than 3
            if (Math.Abs(reports[i] - reports[i + 1]) > 3)
            {
                if (shield)
                {
                    shield = false;
                    safe = false;
                    
                    List<int> reportCurr = reports.ToList();
                    List<int> reportNext = reports.ToList();
                    bool safeCurr = false;
                    bool safeNext = false;

                    reportCurr.RemoveAt(i);
                    safeCurr = CheckReports(reportCurr, false);

                    if (i + 1 < reportNext.Count)
                    {
                        reportNext.RemoveAt(i + 1);
                        safeNext = CheckReports(reportNext, false);
                    }
                    
                    if (safeCurr || safeNext)
                    {
                        safe = true;
                        break;
                    }
                    
                    i = -1;
                    continue;
                }
                else
                {
                    safe = false;
                    break;
                }
            }

            // Check if the current number is equal to the next one
            if (reports[i] == reports[i + 1])
            {
                if (shield)
                {
                    shield = false;
                    safe = false;
                    
                    List<int> reportCurr = reports.ToList();
                    List<int> reportNext = reports.ToList();
                    bool safeCurr = false;
                    bool safeNext = false;

                    reportCurr.RemoveAt(i);
                    safeCurr = CheckReports(reportCurr, false);

                    if (i + 1 < reportNext.Count)
                    {
                        reportNext.RemoveAt(i + 1);
                        safeNext = CheckReports(reportNext, false);
                    }

                    if (safeCurr || safeNext)
                    {
                        safe = true;
                        break;
                    }
                    
                    i = -1;
                    continue;
                }
                else
                {
                    safe = false;
                    break;
                }
            }

            // Check if the current number is lower than the next number and the line is descending
            if (reports[i] < reports[i + 1] && descending)
            {
                if (shield)
                {
                    shield = false;
                    safe = false;
                    
                    List<int> reportCurr = reports.ToList();
                    List<int> reportNext = reports.ToList();
                    bool safeCurr = false;
                    bool safeNext = false;

                    reportCurr.RemoveAt(i);
                    safeCurr = CheckReports(reportCurr, false);

                    if (i + 1 < reportNext.Count)
                    {
                        reportNext.RemoveAt(i + 1);
                        safeNext = CheckReports(reportNext, false);
                    }
                    
                    if (safeCurr || safeNext)
                    {
                        safe = true;
                        break;
                    }
                    i = -1;
                    continue;
                }
                else
                {
                    safe = false;
                    break;
                }
            }

            // Check if the current number is higher than the next number and the line is ascending
            if (reports[i] > reports[i + 1] && ascending)
            {
                if (shield)
                {
                    shield = false;
                    safe = false;
                    
                    List<int> reportCurr = reports.ToList();
                    List<int> reportNext = reports.ToList();
                    bool safeCurr = false;
                    bool safeNext = false;

                    reportCurr.RemoveAt(i);
                    safeCurr = CheckReports(reportCurr, false);

                    if (i + 1 < reportNext.Count)
                    {
                        reportNext.RemoveAt(i + 1);
                        safeNext = CheckReports(reportNext, false);
                    }
                    
                    if (safeCurr || safeNext)
                    {
                        safe = true;
                        break;
                    }
                    
                    i = -1;
                    continue;
                }
                else
                {
                    safe = false;
                    break;
                }
            }

            initial = false;
            safe = true; // the number is safe
        }

        return safe;
    }
}