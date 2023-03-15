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

    public void UpdateFaceColor(int squareIndex, Color pieceColor)
    {
        if (isFull)
            return;

        placedColors[squareIndex] = pieceColor;
        isFull = IsFaceFull();
    }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
