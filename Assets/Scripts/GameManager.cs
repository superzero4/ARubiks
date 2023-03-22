using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        if(completionPercents.Count >= 6)
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
            int r = Random.Range(0, pieceSpawners.Length);
            pieceSpawners[r].SpawnPiece();
            yield return new WaitForSeconds(spawnTime);
            spawnTime -= Random.Range(Time.deltaTime, .2f);
            spawnTime = Mathf.Clamp(spawnTime, 1.5f, 10);
        }
    }

    //Get the percent completion of a complete face
    public void CompleteFace(float completionPercent, int faceId)
    {
        completionPercents.Add(completionPercent);
        foreach (PieceSpawner pieceSpawner in pieceSpawners)
        {
            pieceSpawner.RedistributeProbability(faceId);
        }
    }
}
