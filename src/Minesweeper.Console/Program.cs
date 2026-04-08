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


bool running = true;
while (running){
    Console.Write(b.PrintBoard());

    while (true)
    {
        Console.Write(Minesweeper.Core.Menu.PrintCommandMenu());
        Console.Write("Enter selection: ");
        input = Console.ReadLine();

        if (Minesweeper.Core.Menu.PlayerCommand(input, out cmd)){ 
                break;
        }

        Console.WriteLine("Enter valid option");
    }
    if (cmd == Minesweeper.Core.Command.Quit)
    {
        running = false;
    }
    while (true)
    {
        Console.Write("Enter Coordinate (row col): ");
        input = Console.ReadLine();

        if (Minesweeper.Core.Validation.ValidPair(input, size, out pair))
        {
            break;
        }
        Console.WriteLine("Enter valid option");
    }

    g.OptionSelect(cmd, pair);
}
