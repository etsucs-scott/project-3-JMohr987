namespace Minesweeper.Test;

public class RevealFlag
{
	[Fact]
	public void RevealMineFromHidden()
	{
		var board = new Minesweeper.Core.Board(67, 8);
		board.GenerateBoard();
		board.PlaceFlag(new Minesweeper.Core.Pair(1, 7));

		bool result = board.RevealTile(new Minesweeper.Core.Pair(1, 7));

		Assert.True(result);
	}
}
