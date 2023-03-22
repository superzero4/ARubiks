using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    [SerializeField, AssetsOnly] Piece _piecePrefab;
    [SerializeField] private MaterialPicker _material;
    [SerializeField] PieceSpawner[] pieceSpawners;
    [SerializeField] float spawnTime = 5f;
    public List<float> completionPercents = new List<float>();

    void Start()
    {
        //Start piece spawn
        StartCoroutine(RandomSpawnPiece());
    }

    void Update()
    {
        //Check cube completion
        if (completionPercents.Count >= 6)
        {
            StopAllCoroutines();

            //Destroy all piece still falling
            Piece[] leftPieces = FindObjectsOfType<Piece>();
            foreach (Piece piece in leftPieces)
            {
                if (piece.GetIsFalling())
                    Destroy(piece.gameObject);
            }

            //Calculate total rubiks completion
            float moy = 0;
            foreach (float percent in completionPercents)
            {
                moy += percent;
            }
            moy = moy / completionPercents.Count;
            Debug.Log("Rubiks completion : " + moy + "%");
        }
    }

    //Spawn piece on a random spawner
    IEnumerator RandomSpawnPiece()
    {
        while (true)
        {
            //LegacySpawn();
            var piece = NMinos.NMino.NMinoFactory.RandomNMino();
            SpawnPiece(piece, pieceSpawners[4]);
            yield return new WaitForSeconds(spawnTime);
            spawnTime -= Random.Range(Time.deltaTime, .2f);
            spawnTime = Mathf.Clamp(spawnTime, 1.5f, 10);
        }
    }

    private void LegacySpawn()
    {
        int r = Random.Range(0, pieceSpawners.Length);
        pieceSpawners[r].SpawnPiece(_piecePrefab, _material.RandomMat);
    }

    public void SpawnPiece(NMinos.NMino piece, PieceSpawner s)
    {
        s.SpawnPiece(_piecePrefab, _material.RandomMat, piece, Quaternion.identity);
    }

    //Get the percent completion of a complete face
    public void CompleteFace(float completionPercent, int faceId)
    {
        completionPercents.Add(completionPercent);
        _material.RedistributeProbability(faceId);
    }
}
