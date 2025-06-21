using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> OnPointerClick;
    public LayerMask groundLayer; // marca aquí la capa Ground

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                OnPointerClick?.Invoke(hit.point);
            }
        }
    }
}
