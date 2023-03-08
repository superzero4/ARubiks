using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Sirenix.OdinInspector;
public class ARCursor : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _raycast;
    [SerializeField, EnumToggleButtons]
    private TrackableType _tracked;
    private void Update()
    {
        var hits = new List<ARRaycastHit>();
        if (Input.touchCount > 0)
            if (_raycast.Raycast(Camera.main.ViewportToScreenPoint(ViewPortpoint()), hits, TrackableType.Planes))
                hits.ForEach((hit) => hit.trackable.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red);
    }

    private static Vector3 ViewPortpoint()
    {
        //return Input.GetTouch(0).position;
        return new Vector3(.5f, .5f, 0);
    }
}
