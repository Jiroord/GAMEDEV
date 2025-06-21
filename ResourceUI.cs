using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    public Text resourcesText;

    void OnEnable()
    {
        ResourceManager.OnResourcesChanged += UpdateText;
    }

    void OnDisable()
    {
        ResourceManager.OnResourcesChanged -= UpdateText;
    }

    void UpdateText(int newAmount)
    {
        resourcesText.text = $"Recursos: {newAmount}";
    }
}
