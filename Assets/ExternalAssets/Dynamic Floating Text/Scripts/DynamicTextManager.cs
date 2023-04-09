using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class DynamicTextManager : MonoBehaviour
{

    public static DynamicTextData defaultData;
    public static FloatingAnimation canvasPrefab;
    public static Transform mainCamera;

    [SerializeField] private DynamicTextData _defaultData;
    [SerializeField] private FloatingAnimation _canvasPrefab;
    [SerializeField] private Transform _mainCamera;

    private void Awake()
    {
        defaultData = _defaultData;
        mainCamera = _mainCamera;
        canvasPrefab = _canvasPrefab;
    }

    public static FloatingAnimation CreateText2D(Vector2 position, string text, DynamicTextData data)
    {
        var newText = Instantiate(canvasPrefab, position, Quaternion.identity);
        newText.transform.GetComponent<DynamicText2D>().Initialise(text, data);
        newText.cam = mainCamera.GetComponent<Camera>();
        return newText;
    }

    public static FloatingAnimation CreateText(Vector3 position, string text, DynamicTextData data)
    {
        var newText = Instantiate(canvasPrefab, position, Quaternion.identity);
        newText.transform.GetComponent<DynamicText>().Initialise(text, data);
        newText.cam = mainCamera.GetComponent<Camera>();
        return newText;
    }
    [Button]
    public void SampleText()
    {
        canvasPrefab = _canvasPrefab;
        mainCamera=_mainCamera;
        CreateText(FindObjectOfType<Face>().transform.position, "100", _defaultData);
    }

}
