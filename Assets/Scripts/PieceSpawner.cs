using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] GameObject piecePrefab;
    [SerializeField] Material[] pieceMaterialColor;
    float[] colorProbability = { .16f, .16f, .16f, .16f, .16f, .16f, .04f };
    GameObject lightBeam;

    int isActive = 0;

    private void Awake()
    {
        lightBeam = transform.GetChild(0).gameObject;
    }

    //Spawn the piece and set her color
    public void SpawnPiece()
    {
        GameObject go = Instantiate(piecePrefab, transform.position, Quaternion.identity);
        int r = PickOne(colorProbability);
        go.GetComponent<MeshRenderer>().material = pieceMaterialColor[r];
        StartCoroutine(ActivateLightBeam(go.GetComponent<Piece>()));
    }

    //Activate light beam effect during the falling of the piece
    IEnumerator ActivateLightBeam(Piece piece)
    {
        GameObject pieceObject = piece.gameObject;
        lightBeam.SetActive(true);
        isActive++;

        while(pieceObject != null && piece.GetIsFalling())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        isActive--;

        if(isActive == 0)
            lightBeam.SetActive(false);
    }

    //Choose randomly a color for a piece using their probability
    public int PickOne(float[] prob)
    {
        int index = 0;
        float r = UnityEngine.Random.value;

        while (r > 0)
        {
            r -= prob[index];
            index++;
        }
        index--;

        if(index == prob.Length-1)
            index = Random.Range(0, pieceMaterialColor.Length);

        return index;
    }

    //Redistribute probability after a face is completed to avoid getting to much of that side color appearing
    public void RedistributeProbability(int color)
    {
        float d = colorProbability[color] - .06f;
        colorProbability[color] = .06f;
        int p = 0;
        float redistribValue = 0;

        foreach (float prob in colorProbability)
        {
            if (prob > .06f)
                p++;
        }

        Debug.Log("division : " + d + "/" + p);

        if (p > 0)
            redistribValue = d / p;

        Debug.Log("resultat : " + redistribValue);

        float debug = 0;

        for (int i = 0; i < colorProbability.Length; i++)
        {
            if (colorProbability[i] > .06f)
                colorProbability[i] += redistribValue;

            Debug.Log(colorProbability[i]);
            debug += colorProbability[i];
        }
        Debug.Log(debug);
    }
}
