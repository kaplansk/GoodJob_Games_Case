using UnityEngine;
using System.Collections.Generic;

public class BlockPool : MonoBehaviour
{
    public GameObject blockPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject GetBlock()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(blockPrefab);
        }
    }

    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        pool.Enqueue(block);
    }
}
