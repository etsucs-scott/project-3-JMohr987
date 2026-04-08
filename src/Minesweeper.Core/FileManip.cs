namespace Minesweeper.Core;

public class FileManip
{
    public Gameplay CurrentGame { get; private set; }
    public List<string> Scores8 { get; private set; }
    public List<string> Scores12 { get; private set; }
    public List<string> Scores16 { get; private set; }

    private const string path = "./data/HighScores.csv";

    public FileManip(Gameplay g)
    {
        CurrentGame = g;
        Scores8 = new List<string>();
        Scores12 = new List<string>();
        Scores16 = new List<string>();
    }

    public FileManip() {;}

    public void OpenFile()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("High score File found");
        }
        else
        {
            Console.WriteLine("Creating HighScores file!");
            File.Create(path);
        }
    }
        
}
