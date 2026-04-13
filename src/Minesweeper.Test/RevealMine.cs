namespace Minesweeper.Test;

public class RevealMine
{
	[Fact]
	public void RevealMineFromHidden()
	{
		var board = new Minesweeper.Core.Board(67, 8);
		board.GenerateBoard();

		bool result = board.RevealTile(new Minesweeper.Core.Pair(1, 7));

		Assert.False(result);
	}
}
