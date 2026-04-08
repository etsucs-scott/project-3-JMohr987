int size;
int seed;
string input;
Minesweeper.Core.Command cmd;
Minesweeper.Core.Pair pair;

Minesweeper.Core.FileManip file = new Minesweeper.Core.FileManip();

file.OpenFile();

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
    Console.Write(b.PrintBoard());
}

Console.Write(g.EndGame());
