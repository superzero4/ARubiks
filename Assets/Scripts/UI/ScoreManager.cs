using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ScoreManager;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private DynamicTextManager _textSpawner;
    [Header("Displayers")]
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _percentText;
    [SerializeField, Header("Updaters")]
    private Possibilites _scoreForCorrect;
    [SerializeField]
    private Possibilites _bonusForPerfectFace;
    [SerializeField]
    private Possibilites _bonusForPerfectCube;
    [System.Serializable]
    public struct Text
    {
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private string _prefix, _postfix;
        [SerializeField]
        public MMF_Player _feedback;
        public void Update(object val)
        {
            _text.text = _prefix + val.ToString() + _postfix;
            _feedback.PlayFeedbacks();
        }
    }
    [System.Serializable]
    public struct Possibilites
    {
        [Range(10, 2000)]
        public int score;
        public string prefix;
        public DynamicTextData data;
        public override string ToString()
        {
            return prefix + "" + score.ToString();
        }
    }
    public void SpawnScore(SubPiece origin) => SpawnScore(origin.transform.position, _scoreForCorrect);
    public void SpawnScore(Face origin) => SpawnScore(origin.transform.position + origin.transform.up * .25f, _bonusForPerfectFace);
    public void SpawnScore(GameManager origin)
    {
        var tr = origin.VirtualRubiksCube.transform;
        SpawnScore(tr.position + Vector3.up * 1f, _bonusForPerfectCube);
    }

    private void SpawnScore(Vector3 origin, Possibilites _case)
    {
        var text = DynamicTextManager.CreateText(origin, _case.ToString(), _case.data);
        text.StartAnimation(() => UpdateScore(_case.score));
    }
    private void UpdateText(Text text, object value) => text.Update(value);
    public void UpdateScore(object value) => _scoreText.Update(value);
    public void UpdatePercent(object value) => _percentText.Update(value);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
