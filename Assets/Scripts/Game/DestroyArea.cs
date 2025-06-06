using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyArea : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color defaultColor;
    [SerializeField, Range(0f, 4f)] private float _timeToChange;
    [SerializeField, Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _destroyClip;
    private void Start()
    {
        SetColor(defaultColor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent<Piece>(out Piece piece))
        {
            SetColor(piece.Color ?? defaultColor);
            _audioSource.PlayOneShot(_destroyClip);
            piece.Destroy();
        }
    }

    private void SetColor(Color color)
    {
        DOTween.To(() => _renderer.material.GetColor("_Color"),
            (c) => _renderer.material.SetColor("_Color", c), color, _timeToChange);
    }
}
