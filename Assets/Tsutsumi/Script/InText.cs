using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InText : MonoBehaviour
{
    [SerializeField] Vector2 _textMovePos;
    [SerializeField] float _animationTime = 0.25f;
    [SerializeField] TextMeshProUGUI _text;
    Tween _fadeTween;
    Tween _moveTween;
    Vector2 _beforePos;
    void OnEnable()
    {
        _beforePos = ((RectTransform)transform).anchoredPosition;
        _fadeTween = _text.DOFade(0, _animationTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }
        );
        _moveTween = ((RectTransform)transform).DOAnchorPos(_textMovePos, _animationTime);
    }
    void OnDisable()
    {
        _fadeTween?.Kill();
        _moveTween?.Kill();
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
        ((RectTransform)transform).anchoredPosition = _beforePos;
    }
}
