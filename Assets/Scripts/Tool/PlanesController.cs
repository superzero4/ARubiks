using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one*Mathf.Sin(Time.time);
    }
     
    internal void SetColor(Color color)
    {
        foreach(var render in GetComponentsInChildren<Renderer>())
            render.material.color = color;
    }
}
