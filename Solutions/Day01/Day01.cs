namespace aoc24.Solutions.Day01;

public class Day01 : IDay
{
    public int Part1()
    {
        // Read file
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day01", "day01_input.txt");
        StreamReader sr = File.OpenText(Path.GetFullPath(inputPath));
        
        List<int> colA = new List<int>();
        List<int> colB = new List<int>();

        int numTotal = 0;

        // Process file line by line
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            string[] numLine = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            colA.Add(Convert.ToInt32(numLine[0]));
            colB.Add(Convert.ToInt32(numLine[1]));
        }

        // Sort lists
        colA.Sort();
        colB.Sort();

        int numResult = 0;

        // Calculate result (valColA - valColB)
        for (int i = 0; i < colA.Count; i++)
        {
            numResult = Math.Abs(colA[i] - colB[i]);
            numTotal += numResult; // add the result to the total
        }

        sr.Close();   // Closes the stream
        sr.Dispose(); // Releases resources
        
        return numTotal;
    }

    public int Part2()
    {
        // Read file
        string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Solutions", "Day01", "day01_input.txt");
        StreamReader sr = File.OpenText(Path.GetFullPath(inputPath));
        
        List<int> colA = new List<int>();
        List<int> colB = new List<int>();

        int numMatches = 0;
        int numTotal = 0;

        // Process file line by line
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            string[] numLine = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            colA.Add(Convert.ToInt32(numLine[0]));
            colB.Add(Convert.ToInt32(numLine[1]));
        }

        // Search matches
        for (int i = 0; i < colA.Count; i++)
        {
            numMatches = 0;
            for (int j = 0; j < colB.Count; j++)
            {
                if (colA[i] == colB[j])
                {
                    numMatches++;
                }
            }

            numTotal += colA[i] * numMatches;
        }

        sr.Close();
        sr.Dispose();
        
        return numTotal;
    }
}