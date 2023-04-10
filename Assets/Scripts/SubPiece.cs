using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPiece : MonoBehaviour
{

    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private MMF_Player _snapFeedback;
    [SerializeField]
    private MMF_Player _destroyFeedback;
    [SerializeField]
    private AnimationCurve _curve;
    private Piece _mother;
    [HideInInspector]
    public bool _isRegistered;
    public Color Color => _renderer.material.color;

    public Piece Mother { get => _mother; }
    public bool isFalling => _mother.GetIsFalling();

    public Renderer Renderer { get => _renderer; set => _renderer = value; }

    //Destroy piece if colliding with an already placed piece
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SubPiece>() && isFalling)
        {
            //In case we collide with other placed piece current feedback isn't fine so we just instant destroy
            Destroy(gameObject);
            //DestroySubPiece();
        }
    }
    public void SnapFeedback() => _snapFeedback.PlayFeedbacks();
    public void DestroySubPiece()
    {
        if (!_isRegistered)
        {
            //We add delay even after feedbacks completed because particle plays isn't accounted in this
            _destroyFeedback.Events.OnComplete.AddListener(() => Destroy(gameObject,5f));
            GetComponent<Collider>().enabled = false;
            DOTween.To(() => Renderer.material.color.a, (float a) =>
            {
                var color = Renderer.material.color;
                color.a = a;
                Renderer.material.color = color;
                Debug.Log(Renderer.material.color.a+" in "+Renderer.name+","+Renderer.transform.parent.name);
            }, 0f, 1f).SetEase(_curve).OnComplete(()=>Destroy(Renderer.gameObject));
            _destroyFeedback.PlayFeedbacks();
        }
    }

    public void Parent(Piece mother)
    {
        _mother = mother;
        transform.parent = mother.transform;
    }
}
