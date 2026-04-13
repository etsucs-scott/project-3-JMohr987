namespace Minesweeper.Core;

public static class Menu
{
	//Max and min ranges for menus for validation stuff
    private static readonly int min = 1;
    private static readonly int max = 3;

	//Menu for board sizes
    public static string PrintBoardMenu()
    {
        string returnString = "";
        returnString += "1) 8x8\n";
        returnString += "2) 12x12\n";
        returnString += "3) 16x16\n";

        return returnString;
    }
	
	//Menu for options
    public static string PrintCommandMenu()
    {
        string returnString = "";
        returnString += "r row col\n";
        returnString += "f row col\n";
        returnString += "q\n";

        return returnString;
    }

	//Menu logic to handle input for board size
    public static bool BoardSize(string input, out int size)
    {
        int selection;
        if (!Validation.ValidIntRange(input, out selection, min, max))
        {
            size = 0;
            return false;
        }

        switch (selection)
        {
            case 1:
                size = 8;
                return true;
            case 2:
                size = 12;
                return true;
            case 3:
                size = 16;
                return true;
            default:
                size = 0;
                return false;
        }
    }

	//Takes in input for revealing, flagging, and quitting
	//Assigns (0,0) and Quit for errors so no null  values
	//Boolean so the program can tell when the player enters a valid command
    public static bool PlayerCommand(string str, int size, out Command cmd, out Pair pair)
    {
        string[] inputs = str.Split(' ');
        int row;
        int col;
        if ((inputs.Length != 3) && (inputs.Length != 1))
        {
            cmd = Command.Quit;
            pair = new Pair(0, 0);
            return false;
        }

        switch (inputs[0].ToLower())
        {
            case "q":
                cmd = Command.Quit;
                pair = new Pair(0, 0);
                return true;
            case "r":
                cmd = Command.Reveal;
                break;
            case "f":
                cmd = Command.Flag;
                break;
            default:
                cmd = Command.Quit;
                pair = new Pair(0, 0);
                return false;
        }

        if (!Validation.ValidIntRange(inputs[1], out row, 0, size - 1))
        {
            cmd = Command.Quit;
            pair = new Pair(0, 0);
            return false;
        }
        if (!Validation.ValidIntRange(inputs[2], out col, 0, size - 1))
        {
            cmd = Command.Quit;
            pair = new Pair(0, 0);
            return false;
        }

        pair = new Pair(col, row);

        return true;
    }
}
