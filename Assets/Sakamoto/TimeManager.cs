using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TopicData[] _topicDaters;
    [SerializeField] private float _streamTimeLimit = 60f;
    [SerializeField] private float _maxTime = 5, _minTime = 1;
    [SerializeField] private int _viewerLiked = 0;
    [SerializeField] private int _viewerCount = 5;
    [SerializeField] private FrameManage _frameManage;

    public static TimeManager Instance;

    public System.Action<string> CommentAction;

    private int _topicIndex = 0;
    public float StreamTime { get; private set; }
    public bool IsStream => StreamTime >= 0;
    public State State { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        State = new State(_viewerLiked, _frameManage.LikePointUpdate);
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize()
    {
        _topicIndex = 0;
        CancellationTokenSource cts = new CancellationTokenSource();
        for (int i = 0; i < _viewerCount; i++)
        {
            AsyncGenerate(cts.Token).Forget();
        }
        AsyncTimer(_streamTimeLimit, cts.Token).Forget();
    }

    private void Start()
    {
        Initialize();
    }

    public async UniTask AsyncTimer(float streamTime, CancellationToken token)
    {
        Debug.Log("配信開始");
        StreamTime = streamTime;
        while (StreamTime > 0)
        {
            StreamTime -= Time.deltaTime;
            _frameManage.TimeSliderUpdate((streamTime - StreamTime) / streamTime);
            if (_topicIndex < _topicDaters.Length)
            {
                if (StreamTime <= _topicDaters[_topicIndex].Time)
                {
                    State.ChangeState(_topicDaters[_topicIndex].Topic);
                    _frameManage.LikePointUpdate(State.ViewerLikedPoint.ToString());
                    _topicIndex++;
                }
            }
            await UniTask.Yield(cancellationToken: token);
        }

        Debug.Log("ストリーム終了");
    }

    private async UniTask AsyncGenerate(CancellationToken token)
    {
        while (true)
        {
            float randTime = UnityEngine.Random.Range(_minTime, _maxTime);
            await UniTask.Delay((int)(randTime * 1000), cancellationToken: token);

            Debug.Log("コメントジェネレーター発火");
            CommentAction?.Invoke(State.Topic);
        }
    }
}
public class State
{
    public int ViewerLikedPoint { get; private set; }

    string _topic;

    public string Topic { get; private set; } = "";

    private event Action<string> _action;
    public Action OnStateChange { get; set; }

    public State(int viewerLikedPoint, Action<string> action)
    {
        ViewerLikedPoint = viewerLikedPoint;
        _action = action;
    }

    public void ChangeState(string topic)
    {
        Topic = topic;
        OnStateChange?.Invoke();
    }

    public void ChangeViewerLikedPoint(int viewerLikedPoint)
    {
        ViewerLikedPoint += viewerLikedPoint;
        _action?.Invoke(ViewerLikedPoint.ToString());
    }
}

[System.Serializable]
public class TopicData
{
    [SerializeField, Header("話題を切り替えるタイミング(残り時間)")] int _time;

    [SerializeField, Header("話題")] string _topic;

    public int Time => _time;
    public string Topic => _topic;
}