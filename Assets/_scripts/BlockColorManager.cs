using UnityEngine;
using System.Collections.Generic;

public class BlockColorManager : MonoBehaviour
{
    public static BlockColorManager Instance;
    public List<BlockColorData> colorDataList;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public BlockColorData GetBlockColorData(int colorID)
    {
        return colorDataList.Find(data => data.colorID == colorID);
    }
}

