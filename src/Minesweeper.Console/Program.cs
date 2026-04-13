int size;
int seed;
string input;
Minesweeper.Core.Command cmd;
Minesweeper.Core.Pair pair;

while (true)
{
    Console.Write(Minesweeper.Core.Menu.PrintBoardMenu());
    Console.Write("Enter selection: ");
    input = Console.ReadLine();

    if (Minesweeper.Core.Menu.BoardSize(input, out size)){ 
            break;
    }

    Console.WriteLine("Enter valid option");
}

while (true)
{
    Console.Write("Seed (blank = time): ");
    input = Console.ReadLine();

    if (Minesweeper.Core.Validation.ValidSeed(input, out seed))
    {
        break;
    }
    Console.WriteLine("Enter valid seed");
}

Minesweeper.Core.Board b = new Minesweeper.Core.Board(seed, size);
b.GenerateBoard();

Minesweeper.Core.Gameplay g = new Minesweeper.Core.Gameplay(b);

Minesweeper.Core.Scores scores = new Minesweeper.Core.Scores();

Console.WriteLine(scores.OpenFile());

if(!scores.Opened)
{
	return;
}

Console.Write(b.PrintBoard());
while (g.Running){

    while (true)
    {
        Console.Write(Minesweeper.Core.Menu.PrintCommandMenu());
        Console.Write("Enter selection: ");
        input = Console.ReadLine();

        if (Minesweeper.Core.Menu.PlayerCommand(input, size, out cmd, out pair)){ 
                break;
        }

        Console.WriteLine("Enter valid option");
    }
    if (cmd == Minesweeper.Core.Command.Quit)
    {
        break;
    }

    g.OptionSelect(cmd, pair);
	Console.Clear();
    Console.Write(b.PrintBoard());
}

Console.Write(g.EndGame(out bool win));

if (!win)
{
	return;
}

if (scores.SaveScore(g))
{
	Console.WriteLine("New Score on Leaderboard!");
	Console.WriteLine(":)");
}
else
{
	Console.WriteLine("Did not make a top 5");
	Console.WriteLine(":(");
}

Console.WriteLine("High Scores");
foreach (string s in scores.Scores8)
{
	Console.WriteLine(s);
}
foreach (string s in scores.Scores12)
{
	Console.WriteLine(s);
}
foreach (string s in scores.Scores16)
{
	Console.WriteLine(s);
}

scores.SaveFile();
