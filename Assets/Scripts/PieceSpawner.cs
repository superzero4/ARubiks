using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
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
    public Piece SpawnPiece(Piece p, Material mat)
    {
        var spawned = SpawnPiece(p, mat, Vector3.zero);
        StartCoroutine(ActivateLightBeam(spawned));
        return spawned;
    }
    //Spawn the piece and set her color
    public Piece SpawnPiece(Piece p, Material mat, Vector3 offset, Transform parent = null)
    {
        var go = Instantiate(p, transform.position + offset, Quaternion.identity, parent);
        go.GetComponent<MeshRenderer>().material = mat;
        return go;
    }
    public Piece SpawnPiece(Piece prefab, Material mat, NMinos.NMino nmino, Quaternion rotation)
    {
        var parent = new GameObject("piece").transform;
        parent.position = transform.position;
        parent.rotation = rotation;
        Piece subPiece = null;
        foreach (var ind in nmino)
        {
            if (nmino[ind])
            {
                subPiece = SpawnPiece(prefab, mat, new Vector3(ind.Item1 - 1, 0, ind.Item2 - 1), parent.transform);
                subPiece.name = "subPiece" + ind;
            }
        }
        StartCoroutine(ActivateLightBeam(subPiece));
        return subPiece;
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
