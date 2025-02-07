using UnityEngine;
using System.Collections.Generic;

public class CollapseManager : MonoBehaviour
{
    public static int conditionA = 4, conditionB = 7, conditionC = 9;
    public BoardManager boardManager;

    public void HandleBlockClick(Block clickedBlock)
    {
        List<Block> connectedBlocks = GetConnectedBlocks(clickedBlock);

        if (connectedBlocks.Count >= 2)
        {
            foreach (Block block in connectedBlocks)
            {
                StartCoroutine(block.DestroyBlockSmoothly());
                RemoveBlockFromBoard(block);
            }

            boardManager.UpdateBoard();
        }
    }

    private void RemoveBlockFromBoard(Block block)
    {
        for (int row = 0; row < boardManager.rows; row++)
        {
            for (int col = 0; col < boardManager.columns; col++)
            {
                if (boardManager.boardBlocks[row, col] == block)
                {
                    boardManager.boardBlocks[row, col] = null;
                    return;
                }
            }
        }
    }

    private List<Block> GetConnectedBlocks(Block startBlock)
    {
        List<Block> connectedBlocks = new List<Block>();
        bool[,] visited = new bool[boardManager.rows, boardManager.columns];

        Vector2Int startPos = GetBlockPosition(startBlock);
        FindConnectedBlocksRecursive(startPos.x, startPos.y, startBlock.colorID, connectedBlocks, visited);

        return connectedBlocks;
    }

    private Vector2Int GetBlockPosition(Block block)
    {
        for (int row = 0; row < boardManager.rows; row++)
        {
            for (int col = 0; col < boardManager.columns; col++)
            {
                if (boardManager.boardBlocks[row, col] == block)
                    return new Vector2Int(row, col);
            }
        }
        return -Vector2Int.one;
    }

    private void FindConnectedBlocksRecursive(int row, int col, int colorID, List<Block> connectedBlocks, bool[,] visited)
    {
        if (row < 0 || row >= boardManager.rows || col < 0 || col >= boardManager.columns || visited[row, col]) return;

        Block currentBlock = boardManager.boardBlocks[row, col];
        if (currentBlock != null && currentBlock.colorID == colorID)
        {
            visited[row, col] = true;
            connectedBlocks.Add(currentBlock);

            FindConnectedBlocksRecursive(row + 1, col, colorID, connectedBlocks, visited);
            FindConnectedBlocksRecursive(row - 1, col, colorID, connectedBlocks, visited);
            FindConnectedBlocksRecursive(row, col + 1, colorID, connectedBlocks, visited);
            FindConnectedBlocksRecursive(row, col - 1, colorID, connectedBlocks, visited);
        }
    }
}
