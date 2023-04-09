using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    static readonly Vector3 MinoScale = new Vector3(.018f, .018f, .018f);
    GameObject lightBeam;

    int isActive = 0;
    private void Awake()
    {
        lightBeam = transform.GetChild(0).gameObject;
    }

    /*//Spawn the piece and set her color
    public void SpawnPiece(Piece p)
    {
        var go = SpawnPiece(p, PickOne(colorProbability));
    }*/
    public Piece SpawnPiece(SubPiece p, Material mat)
    {
        var spawned = SpawnPiece(p, mat, Vector3.zero);
        return spawned.parent;
    }
    public Piece SpawnPiece(SubPiece prefab, Material mat, NMinos.NMino nmino, Quaternion rotation)
    {
        var parent = new GameObject("Piece").transform;
        var piece = parent.gameObject.AddComponent<Piece>();
        piece.PreparePiece((nmino.structure.GetLength(0),nmino.structure.GetLength(1)), prefab.gameObject.layer);
        parent.position = transform.position;
        parent.rotation = rotation;
        foreach (var ind in nmino)
        {
            if (nmino[ind])
            {
                var couple = SpawnPiece(prefab, mat, new Vector3(ind.Item1 * .018f - .018f, 0, ind.Item2 * .018f - .018f), parent.transform);
                couple.instantiated.name = "subPiece" + ind;
                piece[ind] = couple.instantiated;
            }
        }
        //StartCoroutine(ActivateLightBeam(piece));
        return piece;
    }
    //Spawn the piece and set her color
    public (Piece parent, SubPiece instantiated) SpawnPiece(SubPiece p, Material mat, Vector3 offset, Transform parent = null)
    {
        if (parent == null || !parent.TryGetComponent<Piece>(out Piece piece))
            piece = parent.gameObject.AddComponent<Piece>();
        var subPiece = Instantiate(p, transform.position + offset, Quaternion.identity, parent);
        subPiece.GetComponent<MeshRenderer>().material = mat;
        subPiece.Parent(piece);
        subPiece.transform.localScale = MinoScale;
        StartCoroutine(ActivateLightBeam(piece));
        return (piece, subPiece);
    }


    //Activate light beam effect during the falling of the piece
    IEnumerator ActivateLightBeam(Piece piece)
    {
        GameObject pieceObject = piece.gameObject;
        lightBeam.SetActive(true);
        isActive++;

        while (pieceObject != null && piece.GetIsFalling())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isActive--;

        if (isActive == 0)
            lightBeam.SetActive(false);
    }
}
