namespace Minesweeper.Core;

public class Gameplay
{
	//Has a reference to the main board so it can get seed and size
	//Also allows this class to check if the player has won
    public Board MineField { get; private set; }

	//To keep track of seconds and timestamp
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

	//All parts of the score save to the file (Aside from timestamp)
    public int Seconds { get; private set; }
    public int MoveCount { get; private set; }
    public int Size { get; private set; }
    public int Seed { get; private set; }

	//So it knows when to stop
    public bool Running  {get; private set; }

    public Gameplay(Board b)
    {
        MineField = b;
        StartTime = DateTime.Now;
        Size = b.Size;
        Seed = b.Seed;
        Running = true;
    }

	//Does the proper method for the option.
	//Also increments the movecount
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

	//Handles game ending stuff
    public string EndGame(out bool win)
    {
        string returnString = "";

        if (!(MineField.EmptyTileCheck()))
        {
            returnString += "You lost...\n";
			win = false;
            return returnString;
        }
			win = true;
            returnString += "You win!\n";

        EndTime = DateTime.Now;
        Seconds = (EndTime - StartTime).Seconds;

        returnString += $"Total Time: {Seconds}\n";
        returnString += $"Total Moves: {MoveCount}\n";

        return returnString;
    }
}
