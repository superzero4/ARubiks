using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField,InfoBox("Played randomly one after another")]
    private AudioClip[] _music;
    private IEnumerator Start()
    {
        _audioSource.loop = false;
        while (true)
        {
            _audioSource.clip = _music[UnityEngine.Random.Range(0,_music.Length)];
            _audioSource.Play();
            yield return new WaitWhile(()=>_audioSource.isPlaying);
        }
    }
}
