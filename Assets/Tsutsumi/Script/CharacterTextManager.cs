using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CharacterTextManager : MonoBehaviour
{
    [SerializeField] int _textUpdateDelay = 50;
    [SerializeField] float _animationDuration = 0.25f;
    [SerializeField] GameObject _textObject;
    [SerializeField] Animator _animator;
    public static CharacterTextManager CharacterTextManagerInstance;
    [SerializeField]TextMeshProUGUI _charaTalkText;
    Vector2 _beforeScale;
    bool _isTextUpdate = false;
    CancellationTokenSource _cancellationTokenSource;

    void Awake()
    {
        _beforeScale = _textObject.transform.localScale;
        CharacterTextManagerInstance = this;
        _textObject.SetActive(false);
    }
    public void TextUpdate(string text, string motionData)
    {
        _textObject.SetActive(true);
        _textObject.transform.localScale = Vector2.zero;
        _charaTalkText.text = "";
        _textObject.transform.DOScale(_beforeScale, 0.25f);
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        _charaTalkText.text = "";
        TextUpdateAsync(text).Forget();
        // 怒り
        // 泣き
        // 流し目
        // 焦り
        // 笑顔
        // 絶望
        // 通常
        // 驚き
    }
    async UniTask TextUpdateAsync(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (_isTextUpdate) return;
            _charaTalkText.text += text[i];
            await UniTask.Delay(_textUpdateDelay, cancellationToken: _cancellationTokenSource?.Token ?? CancellationToken.None);
        }
    }
    void TextHidden()
    {
        _beforeScale = _textObject.transform.localScale;
        _textObject.transform.DOScale(0, 0.25f).onComplete();
        _textObject.SetActive(false);
    }
}
