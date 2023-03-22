using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] Transform pieceTransform;
    [SerializeField] Face face;
    [SerializeField] int index;

    private void OnTriggerEnter(Collider other)
    {
        //Detect a piece
        if (other.gameObject.GetComponent<Piece>())
        {
            other.gameObject.GetComponent<Piece>().SetOnFace(pieceTransform);
            face.UpdateFaceColor(index, other.gameObject.GetComponent<MeshRenderer>().material.color);
        }
    }
}
