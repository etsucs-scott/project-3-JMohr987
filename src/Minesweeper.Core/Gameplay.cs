namespace Minesweeper.Core;

public class Gameplay
{
    public Board MineField { get; private set; }

    public Gameplay(Board b)
    {
        MineField = b;
    }

    public void OptionSelect(Command cmd, Pair pr)
    {
        switch (cmd)
        {
            case Command.Flag:
                MineField.PlaceFlag(pr);
                break;
            case Command.Reveal:
                MineField.RevealTile(pr);
                break;
            default:
                break;
        }
    }
    
}
