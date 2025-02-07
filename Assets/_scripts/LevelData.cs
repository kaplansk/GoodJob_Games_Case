using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Board Settings")]
    public int rows = 10;
    public int columns = 12;
    public int totalColors = 6;

    [Header("Block Spawn & Drop")]
    public float spawnHeight = 2.0f;          
    public float dropDuration = 0.3f;  // Drop Time
    [Range(0f, 1f)]
    public float neighborMatchProbability = 0.8f; // Eslesme olasiligi

    [Header("Icon Thresholds")]
    public int conditionA = 4;  
    public int conditionB = 7;  
    public int conditionC = 9;  
}
