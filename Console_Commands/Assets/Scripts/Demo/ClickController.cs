using UnityEngine;
using TMPro;

public class ClickController : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    [SerializeField] private int _clickPoint = 1;
    [SerializeField] private GameObject _clickEffect;
    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private TextMeshProUGUI _clickPointTxt;


    public int Score { set { _score = value; } get { return _score; } }
    public int ClickPoint { set { _clickPoint = value; } get { return _clickPoint; } }

    private void Update()
    {
        _scoreTxt.text = $"Score: {_score.ToString()}";
        _clickPointTxt.text = $"Click Point: {_clickPoint.ToString()}";
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector2(.48f, .48f);

        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject o = Instantiate(_clickEffect, cursorPos, Quaternion.identity);
        o.GetComponent<ParticleSystem>().Play();

        Score += ClickPoint;

    }

    private void OnMouseUp()
    {
        transform.localScale = new Vector2(.5f, .5f);
    }



}
