using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Pause : MonoBehaviour
{
    [SerializeField] DefaultObserverEventHandler defaultObserver;
    bool isStarted = false;
    GameManager gameManager;
    [SerializeField] CubeRotation cubeRotation;
    [SerializeField]
    private DestroyArea _destroyArea;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _start, _pause, _unpause;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        _destroyArea.gameObject.SetActive(false);

        defaultObserver.OnTargetFound.AddListener(delegate
        {
            if (!isStarted)
                StartGame();
            else
                UnpauseGame();
        });

        defaultObserver.OnTargetLost.AddListener(delegate
        {
            PauseGame();
        });
    }

    void StartGame()
    {
        _audioSource.PlayOneShot(_start);
        Debug.Log("Start game");
        cubeRotation.ActivateMesh();
        isStarted = true;
        gameManager.CubeTracked = true;
        gameManager.CenterOnCube();
        _destroyArea.gameObject.SetActive(true);
        StartCoroutine(gameManager.RandomSpawnPiece());
    }

    void UnpauseGame()
    {
        gameManager.CubeTracked = true;
        _audioSource.PlayOneShot(_unpause);
        if (!gameManager.isEnded)
        {
            Debug.Log("Unpause game");
            var leftPieces = FindObjectsOfType<Piece>();
            foreach (Piece piece in leftPieces)
            {
                if (piece.isPaused)
                    piece.isPaused = false;
            }
            //StartCoroutine(gameManager.RandomSpawnPiece());
        }
    }

    void PauseGame()
    {
        gameManager.CubeTracked = false;
        _audioSource.PlayOneShot(_pause);
        if (!gameManager.isEnded)
        {
            Debug.Log("Pause game");
            //gameManager.StopSpawning();
            var leftPieces = FindObjectsOfType<Piece>();
            foreach (Piece piece in leftPieces)
            {
                if (piece.GetIsFalling())
                    piece.isPaused = true;
            }
        }
    }
}
