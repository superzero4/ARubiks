using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] Transform pieceTransform;
    [SerializeField] Face face;
    [SerializeField] int index;

    public Face Face { get => face; }
    public Transform PieceTransform { get => pieceTransform; }
    public int Index { get => index; }

    public void SnapPiece(Piece piece)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        //Detect a piece
        if (other.attachedRigidbody.TryGetComponent<Piece>(out Piece piece) && piece.GetIsFalling())
        {
            Debug.Log(gameObject.name + " from " + face.name);
            Debug.LogWarning("To do, ensure that connected cubes also update their corresponding squares");
            var or = piece.transform.position;
            var down = face.transform.parent.position - or;
            Debug.DrawRay(or, down, Color.red, 1f);
            if (Physics.Raycast(or, down, out RaycastHit info, down.magnitude, 0b1 << gameObject.layer))
            {
                Square square = info.collider.gameObject.GetComponent<Square>();
                if (square.Face != face)
                    Debug.Log("Face collided different from raycasted?");
                piece.SetOnSquare(square);
            }
            else
            {
                Debug.Log("Sub piece collided but couldn't find where to snap");
            }
            face.UpdateFaceColor(index, other.gameObject.GetComponent<MeshRenderer>().material.color);
        }
    }
}
