using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessage : MonoBehaviour
{
    public static UIMessage Instance;
    public GameObject messagePrefab;
    public float displayTime = 1.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Llama a UIMessage.Show("Tu texto") desde cualquier parte.
    /// </summary>
    public static void Show(string message)
    {
        if (Instance == null)
        {
            Debug.LogWarning("UIMessage no está inicializado en la escena.");
            return;
        }
        Instance.StopAllCoroutines();
        Instance.StartCoroutine(Instance.Display(message));
    }

    IEnumerator Display(string message)
    {
        // Instanciar el prefab y configurar el texto
        GameObject go = Instantiate(messagePrefab, Instance.transform);
        var txt = go.GetComponentInChildren<Text>();
        if (txt != null) txt.text = message;
        // Esperar displayTime segundos
        yield return new WaitForSeconds(displayTime);
        Destroy(go);
    }
}
