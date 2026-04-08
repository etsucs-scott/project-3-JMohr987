namespace Minesweeper.Core;

public static class BFS
{
    public static int []yDirection = { -1, 0, 1, 0, -1, -1, 1, 1};
    public static int []xDirection = { 0, 1, 0, -1, -1, 1, -1, 1};

    public static bool IsValid(bool [,]visited, Pair pair, int Row, int Col)
    {
        if ((pair.Y < 0) || (pair.Y >= Row) || (pair.X < 0) || (pair.X >= Col))
        {
            return false;
        }

        if (visited[pair.Y, pair.X])
        {
            return false;
        }

        return true;
    }

    public static bool IsValidAdjacent(bool [,]visited, Pair currentPair, Pair ogPair, int Row, int Col)
    {
        if ((currentPair.Y < 0) || (currentPair.Y >= Row) || (currentPair.X < 0) || (currentPair.X >= Col))
        {
            return false;
        }

        if ((currentPair.Y > (ogPair.Y + 1)) || (currentPair.Y < (ogPair.Y - 1)) || (currentPair.X > (ogPair.X + 1)) || (currentPair.X < (ogPair.X - 1)))
        {
            return false;
        }

        if (visited[currentPair.Y, currentPair.X])
        {
            return false;
        }

        return true;
    }

    public static void Search(TileType [,]array, bool [,]visited, 
            Pair pair, int Row, int Col, Action<Pair> pairManip)
    {
        Queue<Pair> pairQueue = new Queue<Pair>();

        pairQueue.Enqueue(pair);
        visited[pair.Y, pair.X] = true;

        while (pairQueue.Count != 0)
        {
            Pair node = pairQueue.Peek();
            pairManip(node);
            pairQueue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                Pair newPair = new Pair(node.X + xDirection[i], node.Y + yDirection[i]);
                if (IsValid(visited, newPair, Row, Col))
                {
                    pairQueue.Enqueue(newPair);
                    visited[newPair.Y, newPair.X] = true;
                }
            }
        }
    }

    public static void SearchAdjacent(TileType [,]array, bool [,]visited, 
            Pair currentPair, Pair ogPair, int Row, int Col, Action<Pair> pairManip)
    {
        Queue<Pair> pairQueue = new Queue<Pair>();

        pairQueue.Enqueue(currentPair);
        visited[currentPair.Y, currentPair.X] = true;

        while (pairQueue.Count != 0)
        {
            Pair node = pairQueue.Peek();
            pairManip(node);
            pairQueue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                Pair newPair = new Pair(node.X + xDirection[i], node.Y + yDirection[i]);
                if (IsValidAdjacent(visited, newPair, ogPair, Row, Col))
                {
                    pairQueue.Enqueue(newPair);
                    visited[newPair.Y, newPair.X] = true;
                }
            }
        }
    }
}
