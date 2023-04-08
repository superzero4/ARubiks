using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject fakeCube;
    MeshRenderer[] meshRenderers;

    //Temporary script to test cube rotation

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.enabled = false;
        }
    }

    public void ActivateMesh()
    {
        foreach (MeshRenderer mesh in meshRenderers)
        {
            mesh.enabled = true;
        }
    }

    private void Update()
    {
        if(fakeCube != null  && gameManager.CubeTracked)
        {
            transform.position = fakeCube.transform.position;
            transform.rotation = fakeCube.transform.rotation;
            transform.localScale = fakeCube.transform.localScale;
        }
    }
}
