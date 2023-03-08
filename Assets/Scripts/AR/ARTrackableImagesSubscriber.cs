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
    private void OnEnable()
    {
        _tracker.trackedImagesChanged += OnImageChanged;
    }
    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var im in args.added)
        {
            Instantiate(_prefabToSpawnOnTracked, im.transform);
            _prefabToSpawnOnTracked.transform.localScale = .08f * Vector3.one;
        }
        LogState(args.added, "Begin tracking : ");
        LogState(args.updated, "Keep tracking : ");
        LogState(args.removed, "Loose/Stop tracking : ");
    }

    private static void LogState(List<ARTrackedImage> list, string prefix = "")
    {
        foreach (var image in list)
            Debug.Log(prefix + " " + image.trackableId + " : " + image.transform.position);
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
