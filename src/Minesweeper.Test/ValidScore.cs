namespace Minesweeper.Test;

public class ValidScore
{
	[Fact]
	public void Is_Score_Valid()
	{
		var scores = new Minesweeper.Core.Scores();
		string testLine = "h,6,12,67,8/18/2010 1:30:30 PM";

		bool result = scores.ValidScore(testLine);

		Assert.False(result);
	}
}



