using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    [SerializeField, AssetsOnly] SubPiece _piecePrefab;
    [SerializeField] private MaterialPicker _material;
    [SerializeField] PieceSpawner[] pieceSpawners;
    [SerializeField] float spawnTime = 5f;
    [SerializeField, Range(1, 7f)] float _speed = 3f;
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
            var leftPieces = FindObjectsOfType<Piece>();
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
            //var piece = NMinos.NMino.NMinoFactory.L4Mino();
            var spawned = SpawnPiece(piece,
                //In unity editor we alwasy spawn in the center cause we can't really move the cube, but not in build/real life so we can offset the pieces
#if UNITY_EDITOR
                pieceSpawners[4]
#endif
                );
            spawned.Speed = _speed;
            yield return new WaitForSeconds(spawnTime);
            spawnTime -= Random.Range(Time.deltaTime, .2f);
            spawnTime = Mathf.Clamp(spawnTime, 1.5f, 10);
        }
    }
    private float CalculateFallSpeed()
    {
        return _speed * 1f;
        //Could scale on time elapsed;
    }
    private Piece LegacySpawn()
    {
        return RandomSpawner().SpawnPiece(_piecePrefab, _material.RandomMat);
    }

    private PieceSpawner RandomSpawner()
    {
        return pieceSpawners[Random.Range(0, pieceSpawners.Length)];
    }

    public Piece SpawnPiece(NMinos.NMino piece, PieceSpawner spawner = null)
    {
        spawner ??= RandomSpawner();
        return spawner.SpawnPiece(_piecePrefab, _material.RandomMat, piece, Quaternion.identity);
    }

    //Get the percent completion of a complete face
    public void CompleteFace(float completionPercent, int faceId)
    {
        completionPercents.Add(completionPercent);
        _material.RedistributeProbability(faceId);
    }
}
