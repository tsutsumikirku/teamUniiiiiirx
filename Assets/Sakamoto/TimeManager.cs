using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _streamTimeLimit = 60f;
    [SerializeField] private float _maxTime = 5, _minTime = 1;
    [SerializeField] private int _viewerLiked = 0;
    [SerializeField] private int _viewerCount = 0;
    [SerializeField] private int _maxViewerCount = 50;
    public static TimeManager Instance;
    public System.Action CommentAction;
    public float StreamTime { get; private set; }
    public bool IsStream => StreamTime >= 0;
    public State State { get; private set; }

    //public TimeManager(float streamTime)
    //{
    //    //StreamTime = streamTime;
    //    CancellationTokenSource cts = new CancellationTokenSource();
    //    LoopAsync(cts.Token).Forget();
    //    StartStream(streamTime,cts.Token).Forget();
    //}

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        State = new State(_viewerLiked);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        for (int i = _viewerCount; i < _viewerLiked; i++)
        {
            AsyncGenerate(cts.Token).Forget();
        }
        AsyncTimer(_streamTimeLimit, cts.Token).Forget();
    }

    public async UniTask AsyncTimer(float streamTime, CancellationToken token)
    {
        Debug.Log("配信開始");
        StreamTime = streamTime;

        while (StreamTime > 0)
        {
            StreamTime -= Time.deltaTime;
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
            CommentAction?.Invoke();
        }
    }
}
public class State
{
    public int ViewerLikedPoint { get; private set; }
    public string Topic { get; private set; }

    public State(int viewerLikedPoint)
    {
        ViewerLikedPoint = viewerLikedPoint;
    }

    public void ChangeState(string topic)
    {
        Topic = topic;
    }

    public void ChangeViewerLikedPoint(int viewerLikedPoint)
    {
        ViewerLikedPoint = viewerLikedPoint;
    }
}
