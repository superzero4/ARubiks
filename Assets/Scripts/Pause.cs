using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Pause : MonoBehaviour
{
    [SerializeField] DefaultObserverEventHandler defaultObserver;
    bool isStarted = false;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
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
        isStarted = true;
        StartCoroutine(gameManager.RandomSpawnPiece());
    }

    void UnpauseGame()
    {
        if (!gameManager.isEnded)
        {
            Debug.Log("Unpause game");
            StartCoroutine(gameManager.RandomSpawnPiece());
        }
    }

    void PauseGame()
    {
        if (!gameManager.isEnded)
        {
            Debug.Log("Pause game");
            gameManager.StopAllCoroutines();
        }
    }
}
