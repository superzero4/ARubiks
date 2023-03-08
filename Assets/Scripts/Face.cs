using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] Color faceColor;

    [SerializeField] Square[] squares;

    public Color[] placedColors = 
    {
        Color.black, Color.black, Color.black, 
        Color.black, Color.black, Color.black, 
        Color.black, Color.black, Color.black
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
