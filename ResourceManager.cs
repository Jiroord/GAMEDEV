using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public static event Action<int> OnResourcesChanged;

    public int currentResources = 0;
    public Text resourcesText;
    public float gatherInterval = 1f; // cada segundo
    public int gatherAmount = 5;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(GatherResources());
        OnResourcesChanged?.Invoke(currentResources);
    }

    IEnumerator GatherResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(gatherInterval);
            currentResources += gatherAmount;
            OnResourcesChanged?.Invoke(currentResources);
        }
    }

    public bool SpendResources(int amount)
    {
        if (currentResources < amount) return false;
        currentResources -= amount;
        OnResourcesChanged?.Invoke(currentResources);
        return true;
    }
}
