namespace Minesweeper.Test;

public class MineCountSize8
{
	[Fact]
	public void Count_Equal_10()
	{
		var board = new Minesweeper.Core.Board(67, 8);

		board.GetMineCount();
		int result = board.MineCount;

		Assert.Equal(10, result);
	}
}
