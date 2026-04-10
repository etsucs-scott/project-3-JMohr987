namespace Minesweeper.Core;

public class Scores
{
    public List<string> Scores8 { get; private set; }
    public List<string> Scores12 { get; private set; }
    public List<string> Scores16 { get; private set; }

    private const string dataDir = "data";
    private const string dataFile = "HighScores.csv";
	private const int MaxNumber = 5;

    private string path;

	public bool Opened { get; private set; }

    public Scores() {
        Scores8 = new List<string>();
        Scores12 = new List<string>();
        Scores16 = new List<string>();
        path = Path.Combine(dataDir, dataFile);
		Opened = false;
	}

    public bool ValidScore(string line)
    {
        string[] elements = line.Split(',');
        int testInt;
        DateTime testTime;
        if (elements.Length != 5)
        {
            return false;
        }


        if (!(int.TryParse(elements[0], out testInt)))
        {
            return false;
        }
        if ((int.Parse(elements[0]) != 8) && (int.Parse(elements[0]) != 12) && (int.Parse(elements[0]) != 16))
        {
            return false;
        }
        if (!(int.TryParse(elements[1], out testInt)))
        {
            return false;
        }
        if (!(int.TryParse(elements[2], out testInt)))
        {
            return false;
        }
        if (!(int.TryParse(elements[3], out testInt)))
        {
            return false;
        }
        if (!(DateTime.TryParse(elements[4], out testTime)))
        {
            return false;
        }

        return true;

    }

    public bool AddScore(List<string>scoreList, string line)
    {
        string[] temp = line.Split(',');
        int lineTime = int.Parse(temp[1]);
        int lineMoves = int.Parse(temp[2]);

        if (scoreList.Count == 0)
        {
            scoreList.Add(line);
            return true;
        }

        for (int i = 0; i < scoreList.Count; i++)
        {
            string[] scoreArr = scoreList[i].Split(',');

            if (lineTime < int.Parse(scoreArr[1]))
            {
                scoreList.Insert(i, line);
				goto remove;
            }
            else if (lineTime > int.Parse(scoreArr[1]))
            {
                continue;
            }

            if (lineMoves <= int.Parse(scoreArr[2]))
            {
                scoreList.Insert(i, line);
				goto remove;
            }
        }

        if (scoreList.Count < MaxNumber)
        {
            scoreList.Add(line);
			return true;
        }

		remove:
		if (scoreList.Count > MaxNumber)
		{
			scoreList.RemoveAt(MaxNumber);
			return true;
		}

		return false;

    }

	public bool GetSize(string line)
	{
		if (line[0] == '8')
		{
			return AddScore(Scores8, line);
		}
		else if (line[1] == '2')
		{
			return AddScore(Scores12, line);
		}
		else if (line[1] == '6')
		{
			return AddScore(Scores16, line);
		}
		
		return false;

	}
    public string OpenFile()
    {
		string returnString = "";
        if (!File.Exists(path))
        {
            returnString += "Created HighScores file!";
            File.Create(path);
        }

		try
		{
			FileStream fs = File.Open(path, FileMode.Open);
			fs.Close();
			Opened = true;
		}
		catch (Exception)
		{
			return "Could not open file!";
		}



        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (!ValidScore(line))
            {
                continue;
            }

			GetSize(line);
        }

		returnString += "Scores Loaded Correctly!";

		return returnString;
	}

	public bool SaveScore(Gameplay game)
	{
		string scoreLine = "";
		scoreLine += game.Size.ToString();
		scoreLine += ",";
		scoreLine += game.Seconds.ToString();
		scoreLine += ",";
		scoreLine += game.MoveCount.ToString();
		scoreLine += ",";
		scoreLine += game.Seed.ToString();
		scoreLine += ",";
		scoreLine += game.EndTime.ToString();

		return GetSize(scoreLine);
	}



	public string SaveFile()
	{
		List<string> allScores = new List<string>(Scores8);

		foreach (string line in Scores12)
		{
			allScores.Add(line);
		}
		foreach (string line in Scores16)
		{
			allScores.Add(line);
		}

		File.WriteAllLines(path, allScores);

		return "Highscores successfully saved!";
	}
		

}
