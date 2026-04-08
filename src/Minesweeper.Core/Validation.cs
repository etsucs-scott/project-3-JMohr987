namespace Minesweeper.Core;

public static class Validation
{
    public static bool ValidIntRange(string input, out int num, int min, int max)
    {
        if (!int.TryParse(input, out num))
        {
            return false;
        }

        if ((num < min) || (num > max))
        {
            return false;
        }

        return true;
    }

    public static bool ValidSeed(string input, out int seed)
    {
        if (input == "")
        {
            seed = Math.Abs((int)DateTime.Now.Ticks);
            return true;
        }

        if (!int.TryParse(input, out seed))
        {
            return false;
        }

        return true;
    }


    public static bool ValidPair(string str, int size, out Pair pair)
    {
        string[] input = str.Split(' ');

        int row;
        int col;

        pair = new Pair(0,0);

        if (input.Length != 2)
        {
            return false;
        }

        if (!ValidIntRange(input[0], out row, 0, size - 1)) {
            return false;
        }
        if (!ValidIntRange(input[1], out col, 0, size - 1))
        {
            return false;
        }

        pair = new Pair(col, row);

        return true;
    }

}
