using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField]
    private bool _enables;
    // Start is called before the first frame update
    void Awake()
    {
        if (_enables)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 3 && Input.touches.Any(t => t.phase == TouchPhase.Began))
        {
            Debug.LogWarning("Changing scene");
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }
}
