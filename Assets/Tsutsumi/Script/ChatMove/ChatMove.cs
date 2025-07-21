using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine.Events;

public class ChatMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float _speed;
    [SerializeField] private float _scale;
    [SerializeField] private float _xMaxDistance = -1000f;
    [SerializeField] private float _yMinDistance = -1000f;
    [SerializeField] private float _yMaxDistance = 0;
    [SerializeField] private float _collision = 50f;
    [SerializeField, Header("クリックしたときのサウンドの名前")] private string _onClickSoundTag;
    [SerializeField, Header("リリースしたときのサウンドの名前")] private string _onReleaseSoundTag;
    [SerializeField, Header("ネルさんに食べさせた時のサウンドの名前")] private string _onCharacterCatchTag; 
    private bool _isEnd = false;
    private bool _isMoving;
    private bool _isDragging;
    private RectTransform _rectTransform;
    private Vector2 _initialScale;
    private Vector2 _dragPosition;
    private Vector2 _characterPosition;
    private CommentAndResponseData _data;
    private GameObject _player;
    public CommentAndResponseData Data
    {
        get => _data;
        set => UniTaskMove(value).Forget();
    }

    private void Awake()
    {
        _initialScale = transform.localScale;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, Random.Range(_yMinDistance, _yMaxDistance));
        var frameObj = GameObject.FindWithTag("Frame");
        FindObjectOfType<FrameManage>().LayerUpdate();
        _isMoving = true;
        _player = GameObject.FindWithTag("Player");
        _characterPosition = _player.GetComponent<RectTransform>().anchoredPosition;
    }
    public async UniTask UniTaskMove(CommentAndResponseData data)
    {
        _data = data;
        CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
        while (_isMoving)
        {
            if (_data.Money == 0 && _data.MentalDamage > 0)
            {
                if (ServiceLocater.Get<TimeManager>().StreamTime <= 0)
                {
                    Destroy(gameObject);
                } 
                if (_isEnd) return;
                while (_isDragging)
                {
                    _rectTransform.anchoredPosition = (Vector2)Input.mousePosition - _dragPosition;
                    await UniTask.Yield(cancellationToken);
                }
                _rectTransform.anchoredPosition += (_rectTransform.anchoredPosition - _characterPosition).normalized * _speed * Time.deltaTime;
                if (Vector2.Distance(_rectTransform.anchoredPosition, _characterPosition) < _collision)
                {
                    if (!_player.TryGetComponent<Reply>(out var reply)) return;
                    reply.SaveState(_data);
                    if (!_player.TryGetComponent<CharacterTextManager>(out var characterText)) return;
                    characterText.TextUpdate(_data.Response);
                    End();
                }
                if (_xMaxDistance > _rectTransform.anchoredPosition.x + _rectTransform.rect.width / 2)
                {
                    _isMoving = false;
                }
                await UniTask.Delay(1, cancellationToken: cancellationToken);
            }
            else
            {
                if (ServiceLocater.Get<TimeManager>().StreamTime <= 0)
                {
                    Destroy(gameObject);
                } 
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
        }
        if (!TryGetComponent<Comment>(out var comment))
        {
            Destroy(gameObject);
            return;
        }
        comment.OnThrowEvent();
        Destroy(gameObject);
    }
    
    void End()
    {
        _isEnd = true;
        transform.DOScale(0f, 0.2f).OnComplete(() => Destroy(gameObject));
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _dragPosition = (Vector2)Input.mousePosition - _rectTransform.anchoredPosition;
        SoundManager.Instance.PlaySE(_onClickSoundTag);
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
                End();
                SoundManager.Instance.PlaySE(_onCharacterCatchTag);
                return;
            }
            else if (result.gameObject.tag == "Trash")
            {
                if (!TryGetComponent<Comment>(out var comment))
                {
                    End();
                    return;
                }
                comment.OnThrowEvent();
                End();
            }
            SoundManager.Instance.PlaySE(_onReleaseSoundTag);
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
