using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveTimeScaler : MonoBehaviour
{
    [SerializeField,Range(0,20f)]
    private float _timeScale = 1f;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _timeScale;
    }
}
