using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] Transform pieceTransform;
    [SerializeField] Face face;
    [SerializeField] int index;
    private bool _isOccupied;

    public Face Face { get => face; }
    public Transform PieceTransform { get => pieceTransform; }
    public int Index { get => index; }

    public void SnapPiece(Piece piece)
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (_isOccupied)
            return;
        if (other.TryGetComponent<SubPiece>(out SubPiece subPiece))
        {
            if (face.AreaContainsPoint(subPiece.transform.position))
            {
                if (!subPiece.isFalling)
                    _isOccupied = face.UpdateFaceColor(index, subPiece, subPiece.Mother.name + "piece--square" + gameObject.name);
            }
            else
                subPiece.DestroySubPiece();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Check if we correctly collided with a subpiece
        if (other.TryGetComponent<SubPiece>(out SubPiece subPiece))
        {
            //Find it's mother (which appears to also be the attached rigidbody but we have this link usable
            var piece = subPiece.Mother;
            //Piece snapping when still falling
            if (piece != null && piece.GetIsFalling())
            {
                //Debug.Log(gameObject.name + " from " + face.name);
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
            }
            //Position registering (on following collision when no more falling)

        }
    }
    private bool AreaContainsPoint(Vector3 position) => face.AreaContainsPoint(position);
}
