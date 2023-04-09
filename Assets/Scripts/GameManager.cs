using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private const int NbOfFaces = 6;
    private const float OneSquareCompletionPercentValue = (1f / NbOfFaces) * PerfectPercentage;
    private const int PerfectPercentage = 100;
    [SerializeField] private bool _allowCubeUntracked = true;
    [SerializeField] private bool _permanentFollow = false;
    [SerializeField, AssetsOnly] SubPiece _piecePrefab;
    [SerializeField] private MaterialPicker _material;
    [SerializeField]
    private GameObject lightbeam;
    [SerializeField] PieceSpawner[] pieceSpawners;
    [SerializeField] float spawnTime = 5f;
    [SerializeField, Range(.01f, .7f)] float _speed = .3f;
    private List<float> completionPercents = new List<float>();
    private int _nbOfFacesCompleted;
    public bool isEnded = false;
    [SerializeField] GameObject virtualRubiksCube;
    [SerializeField] ScoreManager _score;
    private bool _cubeTracked;
    public bool CubeTracked
    {
        get => _allowCubeUntracked || _cubeTracked;
        set => _cubeTracked = value;
    }

    public ScoreManager Score { get => _score; }
    public GameObject VirtualRubiksCube { get => virtualRubiksCube; set => virtualRubiksCube = value; }
    private void Awake()
    {
#if !UNITY_EDITOR
        _allowCubeUntracked = false;
#else

#endif
        foreach(var spawn in pieceSpawners)
        {
            spawn.lightBeam = lightbeam;
        }
    }
    void Start()
    {
        if (_allowCubeUntracked)
        {
            FindObjectOfType<DefaultObserverEventHandler>().OnTargetFound.Invoke();
        }
        CenterOnCube();
        completionPercents =new List<float>(Enumerable.Repeat(0f, NbOfFaces));    
    }

    void Update()
    {
        //Set the game manager on top of the cube
        if (virtualRubiksCube != null && CubeTracked && _permanentFollow)
        {
            CenterOnCube();
        }

        //Check cube completion
        if (_nbOfFacesCompleted >= NbOfFaces)
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
            float moy = CalculateMoy();
            if (moy >= PerfectPercentage)
                _score.SpawnScore(this);

            isEnded = true;
        }
    }
    //Called from UI
    [Button]
    public void CenterOnCube()
    {
        transform.position = new Vector3(virtualRubiksCube.transform.position.x, virtualRubiksCube.transform.position.y + .5f, virtualRubiksCube.transform.position.z);
    }

    private float CalculateMoy()
    {
        float moy = 0;
        foreach (float percent in completionPercents)
        {
            moy += percent;
        }
        moy /= completionPercents.Count;
        Debug.Log("Rubiks completion : " + moy + "%");
        return moy;
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    //Spawn piece on a random spawner
    public IEnumerator RandomSpawnPiece()
    {
        while (true)
        {
            if (CubeTracked)
            {
                //LegacySpawn();
                var piece = NMinos.NMino.NMinoFactory.RandomNMino();
                //var piece = NMinos.NMino.NMinoFactory.L4Mino();
                var spawned = SpawnPiece(piece
                //In unity editor we alwasy spawn in the center cause we can't really move the cube, but not in build/real life so we can offset the pieces
#if UNITY_EDITOR && false
                , pieceSpawners[4]
#endif
                );
                spawned.Speed = _speed;
                yield return new WaitForSeconds(spawnTime);
                spawnTime -= Random.Range(Time.deltaTime, .2f);
                spawnTime = Mathf.Clamp(spawnTime, 2, 10);
            }
            else
                yield return new WaitForSeconds(spawnTime);
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
    public void UpdatePercentage(int id)
    {
        completionPercents[id] += OneSquareCompletionPercentValue;
        _score.UpdatePercent(CalculateMoy());
    }
    //Get the percent completion of a complete face
    public bool CompleteFace(int faceId)
    {
        _nbOfFacesCompleted++;
        _material.RedistributeProbability(faceId);
        bool isPerfect = completionPercents[faceId] >= 100f;
        return isPerfect;
    }
}
