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
    public List<(SubPiece, int)> subPiecesToFlush = new List<(SubPiece, int)>();
    const int pieceCorrectTreshold = 2;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;
    public Color?[] placedColors =
    {
        null, null, null,
        null, null, null,
        null, null, null
    };
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _scoreManager = _gameManager.Score;
    }
    private void FixedUpdate()
    {
        Physics.SyncTransforms();
        for (int i = 0; i < subPiecesToFlush.Count; i++)
        {
            (SubPiece, int) piece = subPiecesToFlush[i];
            piece.Item2++;
            if (piece.Item2 > pieceCorrectTreshold)
            {
                piece.Item1.SnapFeedback();
                subPiecesToFlush.RemoveAt(i);
            }
            if (piece.Item1 != null && !AreaContainsPoint(piece.Item1.transform.position))
            {
                piece.Item1.DestroySubPiece();
                subPiecesToFlush.RemoveAt(i);
            }
        }
    }
    //Register color of a piece that falled on a square of the face
    public bool UpdateFaceColor(int squareIndex, SubPiece subPiece, string additionnalInfo = "")
    {
        //If face is complete we shouldn't be here
        //If the square is already filled we shouldn't be here, but it can happen so we check
        if (isFull || placedColors[squareIndex].HasValue)
            return false;
        Color color = subPiece.Color;
        placedColors[squareIndex] = color;
        isFull = IsFaceFull();
        //Debug.Log(gameObject.name + " face--Registering " + subPiece.name + " on " + additionnalInfo);
        //Debug.Break();
        subPiece._isRegistered = true;
        if (IsColorMatchFace(color))
        {
            _gameManager.UpdatePercentage(faceId);
            _scoreManager.SpawnScore(subPiece);
        }
        return true;
    }
    internal bool AreaContainsPoint(Vector3 position) => _area.bounds.Contains(position);

    //Check if all the square of the face has been registred / if the face is completed
    public bool IsFaceFull()
    {
        foreach (Color? color in placedColors)
        {
            if (!color.HasValue)
                return false;
        }
        if (_gameManager.CompleteFace(faceId))
            _scoreManager.SpawnScore(this);
        //Debug.Log(string.Join(',', placedColors) + " Face : " + gameObject.name + " is full");
        if (GetComponentsInChildren<SubPiece>().Length != 9)
            Debug.LogError(GetComponentsInChildren<SubPiece>().Length + " subpieces under face " + gameObject.name + " probably due to one on edge that will be suppressed soon");
        return true;
    }
    private bool IsColorMatchFace(Color color)
    {
        return color == faceColor;
    }
}
