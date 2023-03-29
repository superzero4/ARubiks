using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NMinos;
using static NMinos.Indexes;
using System;

public class Piece : MonoBehaviour
{
    private Matrix<SubPiece> _subPieces;
    public SubPiece this[(int, int) index] { set => _subPieces[index] = value; }
    float speed = 3;
    bool isFalling = true;
    public float Speed { get => speed; set => speed = value; }
    public void PreparePiece((int, int) sizeOfSubpieces, int layer)
    {
        _subPieces = new Matrix<SubPiece>(sizeOfSubpieces.Item1, sizeOfSubpieces.Item2);
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        gameObject.layer = layer;
    }
    private void Update()
    {
        if (!isFalling)
            return;

        //Move the piece
        transform.position = transform.position - new Vector3(0, 1, 0) * speed * Time.deltaTime;

        if (transform.position.y <= -5)
            Destroy(gameObject);
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
        Debug.Log("Snapping to : " + square.Index + " became " + center);
        foreach (var ind in _subPieces)
        {
            var sub = _subPieces[ind];
            if (sub != null)
            {
                (int i, int j) coord = Sum((-1, -1), Sum(ind, center));
                if (_subPieces.OutOfBound(coord))
                {
                    Debug.Log(ind + " Removing coord : " + coord);
                    sub.DestroySubPiece();
                }
            }
        }
        StartCoroutine(DisableIsFalling());
    }
    private IEnumerator DisableIsFalling()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        isFalling = false;
    }
    private float ComputeClosestRotation(float y)
    {
        const int NbOfPossibility = 4;
        const float step = 360f / NbOfPossibility;
        var result = Mathf.RoundToInt((y / step)) * step;
        Debug.Log("result : " + result);
        return result;
    }

    //Return isFalling
    public bool GetIsFalling()
    {
        return isFalling;
    }
}
