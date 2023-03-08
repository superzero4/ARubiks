using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] Transform pieceTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Piece>())
        {
            other.gameObject.GetComponent<Piece>().SetOnFace(pieceTransform);
        }
    }
}
