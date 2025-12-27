using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level")]

public class LevelDataSO : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private int levelIndex;
    [SerializeField] private float levelDuration;
}
