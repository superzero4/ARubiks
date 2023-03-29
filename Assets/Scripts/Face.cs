using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] Color faceColor;
    [SerializeField] int faceId;
    [SerializeField]
    private Collider _area;
    public bool isFull;
    [SerializeField] float completionPercent;
    public List<(SubPiece, int)> subPiecesToFlush=new List<(SubPiece, int)>();
    const int pieceCorrectTreshold = 2;
    public Color[] placedColors =
    {
        Color.black, Color.black, Color.black,
        Color.black, Color.black, Color.black,
        Color.black, Color.black, Color.black
    };
    private void FixedUpdate()
    {
        Physics.SyncTransforms();
        for (int i = 0; i < subPiecesToFlush.Count; i++)
        {
            (SubPiece, int) piece = subPiecesToFlush[i];
            piece.Item2++;
            if (piece.Item2 > pieceCorrectTreshold)
            {
                subPiecesToFlush.RemoveAt(i);
            }
            if (piece.Item1!=null && !AreaContainsPoint(piece.Item1.transform.position))
            {
                piece.Item1.DestroySubPiece();
                subPiecesToFlush.RemoveAt(i);
            }
        }
    }
    //Register color of a piece that falled on a square of the face
    public void UpdateFaceColor(int squareIndex, Color pieceColor)
    {
        if (isFull)
            return;

        placedColors[squareIndex] = pieceColor;
        isFull = IsFaceFull();
    }
    internal bool AreaContainsPoint(Vector3 position) => _area.bounds.Contains(position);

    //Check if all the square of the face has been registred / if the face is completed
    public bool IsFaceFull()
    {
        foreach (Color color in placedColors)
        {
            if (color == Color.black)
                return false;
        }

        CalculateCompletion();

        return true;
    }

    //Calculate completion percentage of the face
    void CalculateCompletion()
    {
        float i = 0;
        foreach (Color color in placedColors)
        {
            if (color == faceColor)
            {
                i += 1;
            }
        }

        completionPercent = Mathf.Floor(i / placedColors.Length * 100);
        FindObjectOfType<GameManager>().CompleteFace(completionPercent, faceId);
    }


}
