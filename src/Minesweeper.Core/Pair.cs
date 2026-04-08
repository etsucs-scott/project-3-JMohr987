namespace Minesweeper.Core;

public class Pair
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Pair(int x, int y)
    {
        X = x;
        Y = y;
    }
}
