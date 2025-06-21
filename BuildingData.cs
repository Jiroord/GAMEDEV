using UnityEngine;

[CreateAssetMenu(menuName = "MMO/Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public GameObject prefab;
    public int cost;
    public float buildTime;
}
