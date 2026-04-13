namespace Minesweeper.Test;

public class RevealBombs
{
	[Fact]
	public void RevealBombs_Move1_Seed_67()
	{
		var board = new Minesweeper.Core.Board(67, 8);
		board.GenerateBoard();
		board.RevealTile(new Minesweeper.Core.Pair(1, 7));
		string result = board.PrintBoard();

		string trueBoard = "  0 1 2 3 4 5 6 7 \n0 # # b # b # # # \n1 # # # # b # # # \n2 # # # b # # # b \n3 # # b # # b # # \n4 # # # # # # # b \n5 b # # # # # # # \n6 # # # # # # # # \n7 # b # # # # # # \n";

		Assert.Equal(result, trueBoard);
	}
}



