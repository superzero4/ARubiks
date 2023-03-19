using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using Sirenix.OdinInspector;

public class ARTrackableImagesSubscriber : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager _tracker;
    [SerializeField,InfoBox("Add a PlanesController component inside the prefab hierarchy to recolor prefab based on image tracked index")]
    private GameObject _prefabToSpawnOnTracked;
    [SerializeField, InfoBox("To iterate and find correct scaling of prefab to spawn, you might want to scale your prefab and/or spawn multiple time the prefab on the tracked image with different scale", InfoMessageType = InfoMessageType.Info)]
    [InfoBox("Might color your prefab differently based on their size, to differentiate them in game, check corresponding function isnide code", InfoMessageType = InfoMessageType.Info)]
    [InfoBox("Only one value for default behaviour", InfoMessageType = InfoMessageType.Warning, VisibleIf = "_scales.Length!=1")]
    private float[] _scales;
    private Dictionary<ARTrackedImage, int> _reindexedList;
    private void OnEnable()
    {
        _reindexedList = new Dictionary<ARTrackedImage, int>();
        _tracker.trackedImagesChanged += OnImageChanged;
    }
    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        ;
        foreach (var im in args.added)
        {
            foreach (var scale in _scales)
            {
                var instantiated = Instantiate(_prefabToSpawnOnTracked, im.transform).transform;
                instantiated.localScale = scale * Vector3.one;
                instantiated.localPosition = Vector3.zero;
                int customIndex = _reindexedList.Count;
                _reindexedList.TryAdd(im, customIndex);
                foreach (var plane in instantiated.GetComponentsInChildren<PlanesController>())
                {
                    //You can also use scale for specific coloration basedOnIndex
                    plane.SetColor(GetColorBasedOnIndex(Index));
                }
            }
        }
        LogState(args.added, "Begin tracking : ");
        foreach (var added in args.added) { }
        LogState(args.updated, "Keep tracking : ");
        LogState(args.removed, "Loose/Stop tracking : ");
    }
    private Color GetColorBasedOnIndex(float customIndex)
    {
        if (customIndex < 1.25f)
            return Color.red;
        else if (customIndex < 1.75f)
            return Color.green;
        else if (customIndex < 2.75f)
            return Color.yellow;
        else if (customIndex < 3.75f)
            return Color.blue;
        return UnityEngine.Random.ColorHSV();
    }
    private Color GetColorBasedOnIndex(int customIndex)
    {
        switch (customIndex)
        {
            case 0: return Color.red;
            case 1: return Color.green;
            case 2: return Color.yellow;
        }
        return UnityEngine.Random.ColorHSV();
    }

    private void LogState(List<ARTrackedImage> list, string prefix = "")
    {
        foreach (var image in list)
        {
            var child = image.transform.GetChild(0);
            Debug.Log(prefix + " " + image.trackingState + " : " + image.transform.position + " : " + child.name + " : " + child.GetChild(0).name + "," + child.GetChild(1).name + " " + image.referenceImage.name + " " + image.transform.GetChild(0)?.name + " " + _reindexedList[image] + ",," + image.name + ",,," /*+ string.Join('\\', image.GetComponents<Component>())*/);
            var planes = GetComponentInChildren<PlanesController>();
            if (planes != null)
                Debug.Log(planes.transform.lossyScale);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var image in _tracker.trackables)
        {

        }
    }
}
