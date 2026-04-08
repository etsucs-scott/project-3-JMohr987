namespace Minesweeper.Core;

public static class Menu
{
    private static readonly int min = 1;
    private static readonly int max = 3;

    public static string PrintBoardMenu()
    {
        string returnString = "";
        returnString += "1) 8x8\n";
        returnString += "2) 12x12\n";
        returnString += "3) 16x16\n";

        return returnString;
    }
    public static string PrintCommandMenu()
    {
        string returnString = "";
        returnString += "1) Reveal\n";
        returnString += "2) Flag\n";
        returnString += "3) Quit\n";

        return returnString;
    }

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

    public static bool PlayerCommand(string input, out Command cmd)
    {
        int selection;
        if (!Validation.ValidIntRange(input, out selection, min, max))
        {
            cmd = Command.Quit;
            return false;
        }

        switch (selection)
        {
            case 1:
                cmd = Command.Reveal;
                return true;
            case 2:
                cmd = Command.Flag;
                return true;
            case 3:
                cmd = Command.Quit;
                return true;
            default:
                cmd = Command.Quit;
                return false;
        }
    }
    
}
