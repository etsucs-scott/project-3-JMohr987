namespace Minesweeper.Core;

public class Gameplay
{
    public Board MineField { get; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public TimeSpan TotalTime { get; private set; }
    public int Seconds { get; private set; }
    public int MoveCount { get; private set; }
    public int Size { get; private set; }
    public int Seed { get; private set; }

    public bool Running  {get; private set; }

    public Gameplay(Board b)
    {
        MineField = b;
        StartTime = DateTime.Now;
        Size = b.Size;
        Seed = b.Seed;
        Running = true;
    }

    public void OptionSelect(Command cmd, Pair pr)
    {
        switch (cmd)
        {
            case Command.Flag:
                MineField.PlaceFlag(pr);
                MoveCount++;
                break;
            case Command.Reveal:
                Running = MineField.RevealTile(pr);
                MoveCount++;
                break;
            default:
                break;
        }
    }

    public string EndGame()
    {
        string returnString = "";

        if (!(MineField.EmptyTileCheck()))
        {
            returnString += "You lost...\n";
            return returnString;
        }
            returnString += "You win!\n";

        EndTime = DateTime.Now;
        TotalTime = EndTime - StartTime;
        Seconds = TotalTime.Seconds;

        returnString += $"Total Time: {Seconds}\n";
        returnString += $"Total Moves: {MoveCount}\n";
        
        return returnString;
        
    }
}
