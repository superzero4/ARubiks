using UnityEngine;

public class MaterialPicker : MonoBehaviour
{
    [SerializeField] Material[] pieceMaterialColor;
    /// <summary>
    /// Last value is probability of black to be spawned
    /// </summary>
    //float[] colorProbability = { .16f, .16f, .16f, .16f, .16f, .16f, .04f };
    float[] colorProbability = { 0, 1f, 0, 0, 0, 0, 0 };
    public Material RandomMat => pieceMaterialColor[PickOne()];
    //Choose randomly a color for a piece using their probability
    private int PickOne()
    {
        int index = 0;
        float r = UnityEngine.Random.value;

        while (r > 0)
        {
            r -= colorProbability[index];
            index++;
        }
        index--;
        /*
                //If we want to always select a "non black" color
                if (index == colorProbability.Length - 1)
                    index = Random.Range(0, pieceMaterialColor.Length);
        */
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