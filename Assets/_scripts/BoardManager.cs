using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData currentLevelData;

    [HideInInspector]
    public int rows, columns;

    public Block[,] boardBlocks;
    public BlockPool blockPool;

  
    [HideInInspector]
    public int totalColors;
    [HideInInspector]
    public float spawnHeight;
    [HideInInspector]
    public float dropDuration;
    [HideInInspector]
    public float neighborMatchProbability;

   
    private void Start()
    {
        ApplyLevelData();
        SetupBoard();
    }


    private void ApplyLevelData()
    {
        if (currentLevelData != null)
        {
            rows = currentLevelData.rows;
            columns = currentLevelData.columns;
            totalColors = currentLevelData.totalColors;
            spawnHeight = currentLevelData.spawnHeight;
            dropDuration = currentLevelData.dropDuration;
            neighborMatchProbability = currentLevelData.neighborMatchProbability;

           
            CollapseManager.conditionA = currentLevelData.conditionA;
            CollapseManager.conditionB = currentLevelData.conditionB;
            CollapseManager.conditionC = currentLevelData.conditionC;
        }
        else
        {
            Debug.LogError("LevelData atanmamýþ!");
        }
    }

    
    public void SetupBoard()
    {
        boardBlocks = new Block[rows, columns];
        InitializeBoard();
        RefreshBlockIcons();
    }

    void InitializeBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                SpawnBlock(row, col);
            }
        }
    }

    public void SpawnBlock(int row, int col)
    {
        GameObject blockObj = blockPool.GetBlock();
        blockObj.transform.SetParent(transform);

        Vector3 startPosition = new Vector3(col, spawnHeight, 0);
        blockObj.transform.localPosition = startPosition;

        Block block = blockObj.GetComponent<Block>();

        int chosenColor;
        if (row < rows - 1 && boardBlocks[row + 1, col] != null && Random.value < neighborMatchProbability)
        {
            chosenColor = boardBlocks[row + 1, col].colorID;
        }
        else
        {
            chosenColor = Random.Range(0, totalColors);
        }
        block.colorID = chosenColor;
        boardBlocks[row, col] = block;
        block.UpdateIcon(1);

        StartCoroutine(DropBlock(block, row, col));
    }

    private IEnumerator DropBlock(Block block, int row, int col)
    {
        Vector3 startPos = block.transform.localPosition;
        Vector3 targetPos = new Vector3(col, -row, 0);
        float elapsedTime = 0f;

        while (elapsedTime < dropDuration)
        {
            block.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / dropDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        block.transform.localPosition = targetPos;
    }

    public void UpdateBoard()
    {
        for (int col = 0; col < columns; col++)
        {
            int emptySlots = 0;
            for (int row = rows - 1; row >= 0; row--)
            {
                if (boardBlocks[row, col] == null)
                {
                    emptySlots++;
                }
                else if (emptySlots > 0)
                {
                    boardBlocks[row + emptySlots, col] = boardBlocks[row, col];
                    boardBlocks[row, col] = null;
                    StartCoroutine(MoveBlockDown(boardBlocks[row + emptySlots, col], row + emptySlots, col));
                }
            }
            for (int i = 0; i < emptySlots; i++)
            {
                SpawnBlock(i, col);
            }
        }
        StartCoroutine(DelayedRefreshIcons(dropDuration));
    }

    private IEnumerator MoveBlockDown(Block block, int newRow, int col)
    {
        Vector3 startPosition = block.transform.localPosition;
        Vector3 targetPosition = new Vector3(col, -newRow, 0);
        float elapsedTime = 0f;

        while (elapsedTime < dropDuration)
        {
            block.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dropDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        block.transform.localPosition = targetPosition;
    }

    private void RefreshBlockIcons()
    {
        bool[,] visited = new bool[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (visited[row, col] || boardBlocks[row, col] == null)
                    continue;
                List<Block> connectedGroup = new List<Block>();
                FindConnectedBlocks(row, col, boardBlocks[row, col].colorID, connectedGroup, visited);
                foreach (Block block in connectedGroup)
                {
                    block.UpdateIcon(connectedGroup.Count);
                }
            }
        }
    }

    private void FindConnectedBlocks(int row, int col, int colorID, List<Block> group, bool[,] visited)
    {
        if (row < 0 || row >= rows || col < 0 || col >= columns)
            return;
        if (visited[row, col])
            return;
        Block block = boardBlocks[row, col];
        if (block == null || block.colorID != colorID)
            return;
        visited[row, col] = true;
        group.Add(block);
        FindConnectedBlocks(row + 1, col, colorID, group, visited);
        FindConnectedBlocks(row - 1, col, colorID, group, visited);
        FindConnectedBlocks(row, col + 1, colorID, group, visited);
        FindConnectedBlocks(row, col - 1, colorID, group, visited);
    }

    private IEnumerator DelayedRefreshIcons(float delay)
    {
        yield return new WaitForSeconds(delay);
        RefreshBlockIcons();
    }
}
