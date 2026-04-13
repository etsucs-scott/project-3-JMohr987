namespace Minesweeper.Core;

//Simple class to hold coordinates
//Easier than passing in row and col for each method
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
