using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NMinos;
using static NMinos.Indexes;
using System;
using System.Linq;

public class Piece : MonoBehaviour
{
    /// <summary>
    /// Not that much need for now, we could aswell use the transform childs instead
    /// </summary>
    private Matrix<SubPiece> _subPieces;
    public SubPiece this[(int, int) index] { set => _subPieces[index] = value; }
    float speed = 3;
    bool isFalling = true;
    public bool isPaused = false;
    public float Speed { get => speed; set => speed = value; }
    private static int pieceCount = 0;
    public void PreparePiece((int, int) sizeOfSubpieces, int layer)
    {
        _subPieces = new Matrix<SubPiece>(sizeOfSubpieces.Item1, sizeOfSubpieces.Item2);
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        gameObject.layer = layer;
        gameObject.name = "Piece " + ++pieceCount;
    }
    private void Update()
    {
        if (!isFalling || isPaused)
            return;

        //Move the piece
        transform.position = transform.position - new Vector3(0, 1, 0) * speed * Time.deltaTime;

        /*if (transform.position.y <= -5)
            Destroy(gameObject);*/
    }
    //Attach the piece to the square it fallen on
    public void SetOnSquare(Square square)
    {
        //We parent the piece to the square
        gameObject.transform.parent = square.PieceTransform;
        //We center it
        gameObject.transform.localPosition = Vector3.zero;
        //And (because parenting kept rotation) we snap LOCAL rotation Y AXIS, to the closest multiple of 90 degree so pieces does the minimal movement to snap correctly
        gameObject.transform.localEulerAngles = new Vector3(0, ComputeClosestRotation(gameObject.transform.localEulerAngles.y), 0);
        //gameObject.transform.localScale = new Vector3(1.34f, 6.67f, 1.34f);
        //We assume squares are laid out the same way as _subPieces are while we don't have an equivalent structure for squares
        var center = Indexes.IndexToCoord(square.Index, _subPieces.structure.GetLength(0));
        //Debug.Log("Snapping to : " + square.Index + " became " + center);
        Face face = square.Face;
        foreach (var ind in _subPieces)
        {
            var sb = _subPieces[ind];
            if (sb != null)
            {
                face.subPiecesToFlush.Add((sb, 0));
            }
        }
        // Disable BCC2008
        // Disable BCC4005

        /*StartCoroutine(AfterSpecificUpdate<WaitForEndOfFrame>(() =>
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                var sb = transform.GetChild(i);
                if (sb != null)
                {
                    if (!face.AreaContainsPoint(sb.transform.position))
                        sb.GetComponent<SubPiece>().DestroySubPiece();
                    else
                    {
                        Debug.Log(sb.name + " face kept " + face);
                        //Debug.Break();
                    }
                }
            }
        }
        , 3));*/
        //isFalling = false;
        StartCoroutine(AfterSpecificUpdate<WaitForFixedUpdate>(() => isFalling = false, 2));
    }
    private IEnumerator AfterSpecificUpdate<T>(Action OnNextFixedUpdate, int nbOfFixedUpdate = 1) where T : YieldInstruction, new()
    {
        while (nbOfFixedUpdate > 0)
        {
            nbOfFixedUpdate--;
            yield return new T();
        }
        OnNextFixedUpdate?.Invoke();

    }
    private float ComputeClosestRotation(float y)
    {
        const int NbOfPossibility = 4;
        const float step = 360f / NbOfPossibility;
        var result = Mathf.RoundToInt((y / step)) * step;
        //Debug.Log("result : " + result);
        return result;
    }

    //Return isFalling
    public bool GetIsFalling()
    {
        return isFalling;
    }
    public void SetIsFalling(bool value)
    {
        isFalling = value;
    }
    public void Destroy()
    {
        foreach (var s in _subPieces)
            _subPieces[s]?.DestroySubPiece();
        Destroy(gameObject, 10f);
    }
    public Color? Color => _subPieces.Select(i => _subPieces[i]).FirstOrDefault(s => s != null)?.Color;
}
