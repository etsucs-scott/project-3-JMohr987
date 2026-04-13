namespace Minesweeper.Core;

public class Board
{
	//Stores Seed and Size
    public int Seed { get; private set; }
    public int Size { get; private set; }

	//Two different boards
	//Gameboard is the board with all empty tiles and bombs
	//Playerboard is the one that the player sees
	//Makes things like placing flags and showing moves the player has done easy
    public TileType[,] GameBoard { get; private set; }
    public TileType[,] PlayerBoard { get; private set; }

    private Random rnd;
	
	//MineCount and Placed are for keeping track of mines when generating the board
	//So it knows when to stop
	//MinesRevealed is used to count the adjacent mines to a revealed tile
    public int MineCount { get; private set; }
    private int minesPlaced;
    public int MinesRevealed { get; private set; }

	//For breadth first search
	//CheckPairs keeps a queue of tiles that needs to be revealed for the cascading effect
	//PairsChecked keeps an array of tiles that have already been handled during cascading
	//to avoid infinite loops
    public Queue<Pair> CheckPairs { get; private set; }
    public bool[,] PairsChecked { get; private set; }

	//Initializes the variables
	//Seed is the absolute value of the paramenter because
	//getting current ticks returns a negative value
    public Board(int sd, int sz)
    {
        Size = sz;
        Seed = Math.Abs(sd);
        GameBoard = new TileType[Size,Size];
        PlayerBoard = new TileType[Size,Size];
        CheckPairs = new Queue<Pair>();
        PairsChecked = new bool[Size,Size];
        rnd = new Random(Seed);
    }

	//Simply assigns the mine count needed for generating the board
    public void GetMineCount()
    {
        switch (Size)
        {
            case 8:
                MineCount = 10;
                break;
            case 12:
                MineCount = 25;
                break;
            case 16:
                MineCount = 40;
                break;
            default:
                break;
        }
    }

	//10% chance of placing a mine and wont place one if the mine count has been met
    public bool PlaceMine()
    {
        if (((rnd.Next() % 10) == 0) && (minesPlaced < MineCount))
        {
            minesPlaced++;
            return true;
        }

        return false;
    }

	//Uses PlaceMine() to determine when to place a mine
	//Loops through the board until the mine count has been met
	//Doesnt break out of loop the moment the minecount has been met
	//because if all the mines are placed before going through the board
	//once there will undefined tiles if loop is broken from
    public void GenerateBoard()
    {
        minesPlaced = 0;
        bool looped = false;
        GetMineCount();


        while (true)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int k = 0; k < Size; k++)
                {
                    if (PlaceMine())
                    {
                        GameBoard[i, k] = TileType.Mine;
                    }
                    else if (looped == false)
                    {
                        GameBoard[i, k] = TileType.Empty;
                    }
                }
            }
			
            looped = true;

            if (minesPlaced == MineCount)
            {
                break;
            }
        }

        for (int i = 0; i < Size; i++)
        {
            for (int k = 0; k < Size; k++)
            {
                PlayerBoard[i, k] = TileType.Hidden;
            }
        }
    }

	//First does an early return if the flag is trying to be placed on an already revealed empty tile
	//Then places a flag if the tile is hidden or removes the flag if the tile is flagged
    public void PlaceFlag(Pair pair)
    {
        if ((PlayerBoard[pair.Y, pair.X] != TileType.Hidden) && (PlayerBoard[pair.Y, pair.X] != TileType.Flag))
        {
            return;
        }

        if ((PlayerBoard[pair.Y, pair.X] == TileType.Hidden))
        {
            PlayerBoard[pair.Y, pair.X] = TileType.Flag;
        }
        else
        {
            PlayerBoard[pair.Y, pair.X] = TileType.Hidden;
        }
    }

	//Goes through the board and checks if any empty tiles are flagged or hidden
	//If yes, this means it hasn't been revealed and the player has not won
	//Returns true if the player has won
    public bool EmptyTileCheck()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int k = 0; k < Size; k++)
            {
                if ((GameBoard[i, k] == TileType.Empty) && (((PlayerBoard[i, k] == TileType.Hidden) || (PlayerBoard[i, k] == TileType.Flag))))
                {
                    return false;
                }
            }
        }
        return true;
    }

	//Reveals tiles
	//Also handles if the game loop should end
	//returns false (tells the game to stop) if the player has won or revealed a mine
    public bool RevealTile(Pair pair)
    {
        if (PlayerBoard[pair.Y, pair.X] == TileType.Flag)
        {
            return true;
        }
        else if (GameBoard[pair.Y, pair.X] == TileType.Empty)
        {
            AdjMineSearch(pair);
            if (EmptyTileCheck())
            {
                return false;
            }else{
                return true;
            }
        }
        else if (GameBoard[pair.Y, pair.X] == TileType.Mine)
        {
            PlayerBoard[pair.Y, pair.X] = GameBoard[pair.Y, pair.X];
            AllMineSearch();
            return false;
        }
        else
        {
            return true;
        }
    }

	//Simple, but put this in to avoid to keep DRY
    public void RevealMine(Pair pair)
    {
        if (GameBoard[pair.Y, pair.X] == TileType.Mine)
        {
            PlayerBoard[pair.Y, pair.X] = TileType.Mine;
        }
    }

	//Looks at a tile to see if it is a mine
	//Adds to MinesRevealed as this is used only when searching for adjacent mines
	//Also to keep DRY
    public void PeekTile(Pair pair)
    {
        if (GameBoard[pair.Y, pair.X] == TileType.Mine)
        {
            MinesRevealed += 1;
        }
    }

	//Uses a breadth first search to search for mines through the whole board
	//Needs a new array so the program know which tiles have been visited
	//Uses RevealMine method so it will reveal all mines on Game Over
    public void AllMineSearch()
    {
        bool[,] visited = new bool[Size, Size];

        BFS.Search(GameBoard, visited, new Pair(0, 0), Size, Size, RevealMine);
    }

	//Searches for adjacent mines for a tile
	//Resets the checked tiles and queue
	//Starts at the tile specified by the argument
	//Goes until no more tiles need to be searched (This enables cascading)
	//Uses SearchAdj as the OG search goes through the entire board instead of just the adjacent tiles
	//if MinesRevealed > 0, that means no cascading for that tile and changes the tile on the player board
	//to the number of adjacent mines
	//If no adjacent mines, sets the tile on the playerboard to be empty and cascades
    public void AdjMineSearch(Pair pair)
    {

        CheckPairs = new Queue<Pair>();
        PairsChecked = new bool[Size,Size];

        CheckPairs.Enqueue(pair);
        PairsChecked[pair.Y, pair.X] = true;

        while (CheckPairs.Count != 0)
        {
            bool[,] visited = new bool[Size,Size];
            MinesRevealed = 0;
            Pair node = CheckPairs.Peek();

            BFS.SearchAdjacent(GameBoard, visited, node, node, Size, Size, PeekTile);
            PairsChecked[node.Y, node.X] = true;

            if (MinesRevealed > 0)
            {
                GameBoard[node.Y, node.X] = (TileType)MinesRevealed;
                PlayerBoard[node.Y, node.X] = GameBoard[node.Y, node.X];
            }
            else if (MinesRevealed == 0)
            {
                PlayerBoard[node.Y, node.X] = GameBoard[node.Y, node.X];
                for (int i = 0; i < 8; i++)
                {
                    Pair newPair = new Pair(
                            node.X + BFS.xDirection[i], 
                            node.Y + BFS.yDirection[i]
                    );
                    if (BFS.IsValid(PairsChecked, newPair, Size, Size))
                    {
                        CheckPairs.Enqueue(newPair);
                        PairsChecked[newPair.Y, newPair.X] = true;
                    }
                }
            }
            CheckPairs.Dequeue();
        }
    }


	//Giant switch statment to get the needed character for the tile type for printing
	//To keep DRY
    public string GetCharacter(int row, int col, TileType[,] board)
    {
        switch (board[row, col])
        {
            case TileType.Empty:
                return ".";
            case TileType.Mine:
                return "b";
            case TileType.Hidden:
                return "#";
            case TileType.Flag:
                return "f";
            case TileType.One:
                return "1";
            case TileType.Two:
                return "2";
            case TileType.Three:
                return "3";
            case TileType.Four:
                return "4";
            case TileType.Five:
                return "5";
            case TileType.Six:
                return "6";
            case TileType.Seven:
                return "7";
            case TileType.Eight:
                return "8";
            default:
                return "#";
        }
    }

	//Prints board for the player to see
	//Adds row and column numbers
    public string PrintBoard()
    {
        string returnString = "  ";

        for (int i = 0; i < Size; i++)
        {
            returnString += $"{i} ";
        }
        returnString += "\n";

        for (int i = 0; i < Size; i++)
        {
            returnString += $"{i} ";
            for (int k = 0; k < Size; k++)
            {
                returnString += GetCharacter(i, k, PlayerBoard);
                returnString += " ";
            }
            returnString += "\n";
        }

        return returnString;
    }

	//Used for testing the generated GameBoard
    public string PrintGameBoard()
    {
        string returnString = "  ";

        for (int i = 0; i < Size; i++)
        {
            returnString += $"{i} ";
        }
        returnString += "\n";

        for (int i = 0; i < Size; i++)
        {
            returnString += $"{i} ";
            for (int k = 0; k < Size; k++)
            {
                returnString += GetCharacter(i, k, GameBoard);
                returnString += " ";
            }
            returnString += "\n";
        }

        return returnString;
    }

}


