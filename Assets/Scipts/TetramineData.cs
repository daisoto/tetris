using UnityEngine;

[CreateAssetMenu(fileName = "New TetramineData", menuName = "Tetramine data")]
public class TetramineData : ScriptableObject
{
    public bool[,] shape = default; // TODO: custom editor
}
