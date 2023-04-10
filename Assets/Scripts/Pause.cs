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

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

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
        Debug.Log("Start game");
        cubeRotation.ActivateMesh();
        isStarted = true;
        gameManager.CubeTracked = true;
        gameManager.CenterOnCube();
        StartCoroutine(gameManager.RandomSpawnPiece());
    }

    void UnpauseGame()
    {
        gameManager.CubeTracked = true;
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
