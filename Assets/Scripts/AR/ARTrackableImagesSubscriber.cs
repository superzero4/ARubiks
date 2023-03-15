using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrackableImagesSubscriber : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager _tracker;
    [SerializeField]
    private GameObject _prefabToSpawnOnTracked;
    private Dictionary<ARTrackedImage, int> _reindexedList;
    private void OnEnable()
    {
        _reindexedList = new Dictionary<ARTrackedImage, int>();
        _tracker.trackedImagesChanged += OnImageChanged;
    }
    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var im in args.added)
        {
            var instantiated = Instantiate(_prefabToSpawnOnTracked, im.transform).transform;
            instantiated.localScale = .08f * Vector3.one;
            int customIndex = _reindexedList.Count;
            _reindexedList.Add(im, customIndex);
            instantiated.GetChild(instantiated.childCount - 1).GetComponent<Renderer>().material.color = GetColorBasedOnIndex(customIndex);
        }
        LogState(args.added, "Begin tracking : ");
        foreach (var added in args.added) { }
        LogState(args.updated, "Keep tracking : ");
        LogState(args.removed, "Loose/Stop tracking : ");
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
            Debug.Log(prefix + " " + image.referenceImage.name + " : " + image.transform.position + " " + image.trackingState + " " + image.transform.GetChild(0)?.name + " " + _reindexedList[image] + ",," + image.name + ",,," /*+ string.Join('\\', image.GetComponents<Component>())*/);
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
