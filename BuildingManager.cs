using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    public BuildingData[] availableBuildings;
    public BuildingData selectedBuilding;
    public LayerMask buildingLayer;
    public Vector3 buildingSize = new Vector3(1,2,1);
    public Material ghostMaterial;
    public float ghostHeightOffset = 1f;

    private GameObject currentGhost;

    void OnEnable() => InputManager.OnPointerClick += HandleClick;
    void OnDisable() => InputManager.OnPointerClick -= HandleClick;
    void HandleClick(Vector3 groundPos)
    {
        // Ajusta al grid
        Vector3 spawnPos = GridManager.Instance.SnapPosition(groundPos);
        if (!CanPlaceAt(spawnPos))
        {
            Debug.Log("Espacio ocupado");
            return;
        }
        if (!ResourceManager.Instance.SpendResources(selectedBuilding.cost))
        {
            Debug.Log("No hay recursos suficientes");
            return;
        }
        StartCoroutine(ConstructBuilding(spawnPos, selectedBuilding));
    }
    void Update()
    {
        UpdateGhost();
    }

    void UpdateGhost()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.name == "Ground")
        {
            Vector3 pos = GridManager.Instance.SnapPosition(hit.point);
            if (currentGhost == null)
                currentGhost = Instantiate(selectedBuilding.prefab);
            currentGhost.transform.position = pos + Vector3.up * (selectedBuilding.prefab.transform.localScale.y/2f + ghostHeightOffset);
            currentGhost.GetComponent<Renderer>().sharedMaterial = ghostMaterial;
            bool ok = CanPlaceAt(pos);
            Color c = ok ? new Color(0,1,0,0.5f) : new Color(1,0,0,0.5f);
            currentGhost.GetComponent<Renderer>().material.color = c;
        }
        else if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
    }

    /*public void HandleBuild(Vector3 rawPos)
    {
        Vector3 spawnPos = GridManager.Instance.SnapPosition(rawPos);
        if (!CanPlaceAt(spawnPos))
        {
            UIMessage.Show("Espacio ocupado");
            return;
        }
        if (!ResourceManager.Instance.SpendResources(selectedBuilding.cost))
        {
            UIMessage.Show("No hay recursos suficientes");
            return;
        }
        StartCoroutine(ConstructBuilding(spawnPos, selectedBuilding));
    }*/

    bool CanPlaceAt(Vector3 pos)
    {
        Collider[] hits = Physics.OverlapBox(
            pos + Vector3.up * (buildingSize.y/2),
            new Vector3(buildingSize.x/2, buildingSize.y/2, buildingSize.z/2),
            Quaternion.identity,
            buildingLayer);
        return hits.Length == 0;
    }

    IEnumerator ConstructBuilding(Vector3 pos, BuildingData data)
    {
        GameObject site = Instantiate(data.prefab, pos, Quaternion.identity);
        Material originalMat = site.GetComponent<Renderer>().material;
        site.GetComponent<Renderer>().material = ghostMaterial;
        float timer = 0f;
        // TODO: add progress bar UI here
        while (timer < data.buildTime)
        {
            timer += Time.deltaTime;
            // update progress bar
            yield return null;
        }
        site.GetComponent<Renderer>().material = originalMat;
    }
}
