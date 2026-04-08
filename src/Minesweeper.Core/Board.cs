namespace Minesweeper.Core;

public class Board
{
    public int Seed { get; private set; }
    public int Size { get; private set; }

    public TileType[,] GameBoard { get; private set; }
    public TileType[,] PlayerBoard { get; private set; }

    private Random rnd;

    public int MineCount { get; private set; }
    public int minesPlaced { get; private set; }
    public int MinesRevealed { get; private set; }

    public Queue<Pair> checkPairs { get; private set; }
    public bool[,] pairsChecked { get; private set; } 

    public Board(int sd, int sz)
    {
        Size = sz;
        Seed = Math.Abs(sd);
        GameBoard = new TileType[Size,Size];
        PlayerBoard = new TileType[Size,Size];
        checkPairs = new Queue<Pair>();
        pairsChecked = new bool[Size,Size];
        rnd = new Random(Seed);
    }

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

    public bool PlaceMine()
    {
        if (((rnd.Next() % 10) == 0) && (minesPlaced < MineCount))
        {
            minesPlaced++;
            return true;
        }

        return false;
    }

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

    public bool EmptyTileCheck()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int k = 0; k < Size; k++)
            {
                if ((GameBoard[i, k] == TileType.Empty) && ((PlayerBoard[i, k] == TileType.Hidden) || (PlayerBoard[i, k] == TileType.Flag)))
                {
                    return false;
                }
            }
        }
        return true;
    }

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

    public void RevealMine(Pair pair)
    {
        if (GameBoard[pair.Y, pair.X] == TileType.Mine)
        {
            PlayerBoard[pair.Y, pair.X] = TileType.Mine;
        }
    }

    public void PeekTile(Pair pair)
    {
        if (GameBoard[pair.Y, pair.X] == TileType.Mine)
        {
            MinesRevealed += 1;
        }
        //PlayerBoard[pair.Y, pair.X] = GameBoard[pair.Y, pair.X];
    }

    public void AllMineSearch()
    {
        bool[,] visited = new bool[Size, Size];

        BFS.Search(GameBoard, visited, new Pair(0, 0), Size, Size, RevealMine);
    }
    public void AdjMineSearch(Pair pair)
    {

        checkPairs = new Queue<Pair>();
        pairsChecked = new bool[Size,Size];

        checkPairs.Enqueue(pair);
        pairsChecked[pair.Y, pair.X] = true;

        while (checkPairs.Count != 0)
        {
            bool[,] visited = new bool[Size,Size];
            MinesRevealed = 0;
            Pair node = checkPairs.Peek();

            BFS.SearchAdjacent(GameBoard, visited, node, node, Size, Size, PeekTile);
            pairsChecked[node.Y, node.X] = true;

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
                    if (BFS.IsValid(pairsChecked, newPair, Size, Size))
                    {
                        checkPairs.Enqueue(newPair);
                        pairsChecked[newPair.Y, newPair.X] = true;
                    }
                }
            }
            checkPairs.Dequeue();
        }
    }


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

}


