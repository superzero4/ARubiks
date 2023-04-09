using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color defaultColor;
    private void Start()
    {
        SetColor(defaultColor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent<Piece>(out Piece piece))
        {
            SetColor(piece.Color ?? defaultColor);
            piece.Destroy();
        }
    }

    private void SetColor(Color color)
    {
        _renderer.material.SetColor("_Color", color);
    }
}
