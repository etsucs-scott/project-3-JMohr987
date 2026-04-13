namespace Minesweeper.Test;

public class Flag
{
	[Fact]
	public void PlaceFlagOnHidden()
	{
		var board = new Minesweeper.Core.Board(67, 8);
		board.GenerateBoard();

		board.PlaceFlag(new Minesweeper.Core.Pair(1, 7));
		var result = board.PlayerBoard[7, 1];

		Assert.Equal(Minesweeper.Core.TileType.Flag, result);
	}
}
