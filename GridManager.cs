using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public float cellSize = 1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Vector3 SnapPosition(Vector3 rawPos)
    {
        float x = Mathf.Round(rawPos.x / cellSize) * cellSize;
        float z = Mathf.Round(rawPos.z / cellSize) * cellSize;
        return new Vector3(x, rawPos.y, z);
    }
}
