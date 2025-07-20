using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class ChatMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float _speed;
    [SerializeField] private float _scale;
    [SerializeField] private float _xMaxDistance = -1000f;
    [SerializeField] private float _yMinDistance = -1000f;
    [SerializeField] private float _yMaxDistance = 0;
    private bool _isEnd = false;
    private bool _isMoving;
    private bool _isDragging;
    private RectTransform _rectTransform;
    private Vector2 _initialScale;
    private Vector2 _dragPosition;
    public CommentAndResponseData _data;

    void Awake()
    {
        _initialScale = transform.localScale;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, Random.Range(_yMinDistance, _yMaxDistance));
        var frameObj = GameObject.FindWithTag("Frame");
        _isMoving = true;
        UniTaskMove().Forget();
    }
    async UniTask UniTaskMove()
    {
        CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
        while (_isMoving)
        {
            if (_isEnd) return;
            while (_isDragging)
            {
                _rectTransform.anchoredPosition = (Vector2)Input.mousePosition - _dragPosition;
                await UniTask.Yield(cancellationToken);
            }
            _rectTransform.anchoredPosition += new Vector2(_speed * Time.deltaTime, 0);
            if (_xMaxDistance > _rectTransform.anchoredPosition.x + _rectTransform.rect.width / 2)
            {
                _isMoving = false;
            }
            await UniTask.Delay(1, cancellationToken: cancellationToken);
        }
        Destroy(gameObject);
    }
    async UniTask End()
    {
        _isEnd = true;
        transform.DOScale(0f,0.2f).OnComplete(() => Destroy(gameObject));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _dragPosition = (Vector2)Input.mousePosition - _rectTransform.anchoredPosition;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.tag == "Player")
            {
                if (!result.gameObject.TryGetComponent<Reply>(out var reply)) return;
                reply.SaveState(_data);
                if (!result.gameObject.TryGetComponent<CharacterTextManager>(out var characterText)) return;
                characterText.TextUpdate(_data.Response);
                End().Forget();
            }
        }
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isEnd) return;
        _rectTransform.DOScale(_rectTransform.localScale * _scale, 0.2f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isEnd) return;
        _rectTransform.DOScale(_initialScale, 0.2f);
    }
}
