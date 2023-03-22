using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] Color faceColor;
    [SerializeField] int faceId;
    public bool isFull;
    [SerializeField] float completionPercent;

    public Color[] placedColors = 
    {
        Color.black, Color.black, Color.black, 
        Color.black, Color.black, Color.black, 
        Color.black, Color.black, Color.black
    };

    //Register color of a piece that falled on a square of the face
    public void UpdateFaceColor(int squareIndex, Color pieceColor)
    {
        if (isFull)
            return;

        placedColors[squareIndex] = pieceColor;
        isFull = IsFaceFull();
    }

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
