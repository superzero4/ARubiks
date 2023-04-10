using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotater : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
    this.enabled = false;
#endif
        if(enabled)
            GetComponent<CubeRotation>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Random.rotation;
    }
}
