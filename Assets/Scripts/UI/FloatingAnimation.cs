using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using System;

public class FloatingAnimation : MonoBehaviour
{
    [SerializeField, Range(.1f, 10f)]
    private float _time;
    [SerializeField]
    private Ease _ease;
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
        (transform as RectTransform).DOMoveY(Screen.height, _time).SetEase(_ease).SetAutoKill().OnComplete(onComplete);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
