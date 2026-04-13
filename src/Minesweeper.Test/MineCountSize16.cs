
namespace Minesweeper.Test;

public class MineCountSize16
{
	[Fact]
	public void Count_Equal_40()
	{
		var board = new Minesweeper.Core.Board(67, 16);

		board.GetMineCount();
		int result = board.MineCount;

		Assert.Equal(40, result);
	}
}
