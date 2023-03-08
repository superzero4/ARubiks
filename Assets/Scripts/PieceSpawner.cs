using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] GameObject piecePrefab;
    [SerializeField] Material[] pieceMaterialColor;

    public void SpawnPiece()
    {
        GameObject go = Instantiate(piecePrefab, transform.position, Quaternion.identity);
        int r = Random.Range(0, pieceMaterialColor.Length);
        go.GetComponent<MeshRenderer>().material = pieceMaterialColor[r];
    }
}
