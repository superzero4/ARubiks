using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPiece : MonoBehaviour
{

    private Piece _mother;

    public Piece Mother { get => _mother; set => _mother = value; }
    private bool isFalling => _mother.GetIsFalling();
    //Destroy piece if colliding with an already placed piece
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SubPiece>() && isFalling)
        {
            Destroy(gameObject);
        }
    }
    public void Parent(Piece mother)
    {
        _mother = mother;
        transform.parent = mother.transform;
    }
}
