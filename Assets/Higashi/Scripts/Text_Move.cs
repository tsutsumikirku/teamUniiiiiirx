using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Text_Move : MonoBehaviour
{
    [SerializeField] float _posX = 1f;
    [SerializeField] float _moveTime = 5f;
    float _canvasWidth;
    Text_gene _tg;

    private void Awake()
    {
        _tg = FindAnyObjectByType<Text_gene>();
       _canvasWidth =_tg.GiveCanvsWidth();
    }
    private void Start()
    {
        RectTransform _rect = GetComponent<RectTransform>();

        _rect.DOAnchorPosX(-_canvasWidth/2 + _posX, _moveTime).SetEase(Ease.Linear);

    }
}
