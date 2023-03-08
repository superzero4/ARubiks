using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PieceSpawner[] pieceSpawners;
    [SerializeField] float spawnTime = 5f;
    public List<float> completionPercents = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnPiece());
    }

    // Update is called once per frame
    void Update()
    {
        if(completionPercents.Count >= 6)
        {
            StopAllCoroutines();

            Piece[] leftPieces = FindObjectsOfType<Piece>();
            foreach (Piece piece in leftPieces)
            {
                if (piece.GetIsFalling())
                    Destroy(piece.gameObject);
            }

            float moy = 0;
            foreach (float percent in completionPercents)
            {
                moy += percent;
            }
            moy = moy / completionPercents.Count;
            Debug.Log("Rubiks completion : " + moy + "%");
        }
    }

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
}
