using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField,Header("クリック時のイベントを設定してください")] UnityEvent onClick;
    [SerializeField,Header("マウスカーソルが押されたときか離されたときか設定してください")] ButtonType _buttonType = ButtonType.Down;
    [SerializeField, Header("マウスカーソルがUIの上に載っているときのカラーを指定してください")] Color _pointerEnterColor;
    [SerializeField, Header("UIが押されたときのカラーを指定してください")] Color _pointerDownColor;
    [SerializeField, Header("マウスカーソルがUIの上に載っているときのスケールを指定してください")] Vector2 _pointerEnterScale;
    [SerializeField, Header("UIが押されたときのスケールを指定してください")] Vector2 _pointerDownScale;
    [SerializeField, Header("UIのImageを選択してください設定されていない場合はこのコンポーネントがアタッチされているクラスから取得されます")] Image _image;
    [SerializeField, Header("UIのRecttransformを選択してください設定されてない場合はこのオブジェクトから取得されます")] RectTransform _rect;
    [SerializeField, Header("アニメーションの時間")] float _animTime;
    Color _defaultColor;
    Vector2 _defaultScale;
    bool _click;
    bool _entry;
    
    void Start()
    {
        if (!_image)
        {
            TryGetComponent<Image>(out _image);
        }
        if (!_rect)
        {
            _rect = GetComponent<RectTransform>();
        }
        _defaultScale = _rect.localScale;
        if (!_image) return;
        _defaultColor = _image.color;
        _image.color = _defaultColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _click = true;
        if (_buttonType == ButtonType.Down)
        {
            onClick?.Invoke();
        }
        _rect.DOScale(_defaultScale + _pointerDownScale, _animTime);
        if (!_image) return;
        _image.DOColor(_pointerDownColor, _animTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _click = false;
        if (_buttonType == ButtonType.Up)
        {
            if (_entry)
            {
                onClick?.Invoke();
            }
        }
        if (_entry)
        {
            _rect.DOScale(_defaultScale + _pointerEnterScale, _animTime);
            if (!_image) return;
            _image.DOColor(_defaultColor, _animTime);
            #if windows || UNITY_STANDALONE
            #endif
        }
        else
        {
            _rect.DOScale(_defaultScale, _animTime);
            if (!_image) return;
            _image.DOColor(_defaultColor, _animTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _entry = true;
        if (!_click)
        {
            _rect.DOScale(_defaultScale + _pointerEnterScale, _animTime);
            if (!_image) return;
            _image.DOColor(_pointerEnterColor, _animTime);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _entry = false;
        if (!_click)
        {
            _rect.DOScale(_defaultScale, _animTime);
            if (!_image) return;
            _image.DOColor(_defaultColor, _animTime);
        }
    }
}
public enum ButtonType
{
    Down,
    Up
}
