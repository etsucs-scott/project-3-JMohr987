namespace Minesweeper.Test;

public class MineCountSize12
{
	[Fact]
	public void Count_Equal_25()
	{
		var board = new Minesweeper.Core.Board(67, 12);

		board.GetMineCount();
		int result = board.MineCount;

		Assert.Equal(25, result);
	}
}
