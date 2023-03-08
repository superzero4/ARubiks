using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Sirenix.OdinInspector;
using System;

public class ARCursor : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _raycast;
    [SerializeField]
    private Camera _camera;
    [SerializeField, EnumToggleButtons]
    private TrackableType _tracked;
    private void Awake()
    {
        if (_raycast == null)
            _raycast = GameObject.FindObjectOfType<ARRaycastManager>();
        if (_camera == null)
            _camera = Camera.main;
    }
    private void Update()
    {
        var hits = new List<ARRaycastHit>();
        if (_raycast.Raycast(_camera.ViewportToScreenPoint(ViewPortpoint()), hits, _tracked))
        {
            Pose firstPose = hits[0].pose;
            transform.position = firstPose.position;
            transform.rotation = firstPose.rotation;
            foreach (var hit in hits)
            {
                Material mat;
                try
                {
                    mat = hit.trackable?.gameObject?.GetComponentInChildren<Renderer>()?.material;
                }
                catch (Exception e)
                {
                    mat = null;
                }
                if (mat != null)
                    mat.color = Color.red;
                else
                    Debug.LogWarning("mat is null");
            }
        }

    }

    private static Vector3 ViewPortpoint()
    {
        //return Input.GetTouch(0).position;
        return new Vector3(.5f, .5f, 0);
    }
}
