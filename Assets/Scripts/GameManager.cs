using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PieceSpawner[] pieceSpawners;
    [SerializeField] float spawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnPiece());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomSpawnPiece()
    {
        while (true)
        {
            int r = Random.Range(0, pieceSpawners.Length);
            pieceSpawners[r].SpawnPiece();
            yield return new WaitForSeconds(spawnTime);
            spawnTime -= Random.Range(Time.deltaTime, .2f);
            spawnTime = Mathf.Clamp(spawnTime, .5f, 10);
        }
    }
}
