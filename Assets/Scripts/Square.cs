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
        if (other.attachedRigidbody.TryGetComponent<Piece>(out Piece piece) && piece.GetIsFalling())
        {
            Debug.Log(gameObject.name + " from " + face.name);
            Debug.LogWarning("To do, ensure that connected cubes also update their corresponding squares");
            piece.SetOnFace(pieceTransform);
            face.UpdateFaceColor(index, other.gameObject.GetComponent<MeshRenderer>().material.color);
        }
    }
}
