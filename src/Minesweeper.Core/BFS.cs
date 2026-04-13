namespace Minesweeper.Core;

//I got this breadth first search algorithm from GeeksForGeeks
//Link:
//https://www.geeksforgeeks.org/dsa/breadth-first-traversal-bfs-on-a-2d-array/
//Modified it for my use
public static class BFS
{
	//These directions are used for checking adjacent tiles
	//First 4 are for up, right, down, and left
	//Last 4 are for diagonals
	//Allows progam to use a loop when queueing tiles
    public static int []yDirection = { -1, 0, 1, 0, -1, -1, 1, 1};
    public static int []xDirection = { 0, 1, 0, -1, -1, 1, -1, 1};

	//Checks if the tile is valid
	//(Either off the board or has been visited)
	//Avoids using a long if statment for edgecases
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

	//Same as previous but also makes any tiles not adjaced to original tile invalid
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

	//Basic breadth first search algorithm
	//takes in Row and Col as row and collum of the array for Validation check
	//Pair is just a coordiant pair (Better than two seprate arguments
	//Passes in a function pointer so it can do stuff with the valid tiles
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

	//Same as last but only for tiles adjacent to the original tile
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
