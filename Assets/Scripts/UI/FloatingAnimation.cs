using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System;

public class FloatingAnimation : MonoBehaviour
{
    [HideInInspector] public Camera cam;
    [SerializeField, Range(.1f, 10f)]
    private float _time;
    [SerializeField]
    private Ease _ease;
    [SerializeField]
    private AnimationCurve _curve;
    ScoreManager _score;
    // Start is called before the first frame update
    private void Awake()
    {
        _score = FindObjectOfType<ScoreManager>();
    }
    void Start()
    {
        //StartAnimation();
    }

    public void StartAnimation(TweenCallback onComplete)
    {
        var rt = transform as RectTransform;
        // Start the floating animation        
        float canvasDistance = Vector3.Distance(rt.position, cam.transform.position);
        float canvasHeight = 2.0f * canvasDistance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Vector3 startPos = rt.position;
        Vector3 endPos = startPos + Vector3.up * (canvasHeight);
        rt.DOMove(endPos, _time).SetEase(_curve)/*.SetAutoKill()*/.OnComplete(onComplete);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
