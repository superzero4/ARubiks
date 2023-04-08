using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotater : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Random.rotation;
    }
}
