using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPiece : MonoBehaviour
{

    [SerializeField]
    private Renderer _renderer;
    private Piece _mother;
    public bool _isRegistered;
    public Color Color => _renderer.material.color;

    public Piece Mother { get => _mother; set => _mother = value; }
    public bool isFalling => _mother.GetIsFalling();

    //Destroy piece if colliding with an already placed piece
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SubPiece>() && isFalling)
        {
            DestroySubPiece();
        }
    }

    public void DestroySubPiece()
    {
        if (!_isRegistered)
            Destroy(gameObject);
    }

    public void Parent(Piece mother)
    {
        _mother = mother;
        transform.parent = mother.transform;
    }
}
