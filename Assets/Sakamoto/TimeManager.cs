using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TopicData[] _topicDaters;
    [SerializeField] private float _streamTimeLimit = 60f;
    [SerializeField] private float _maxTime = 5, _minTime = 1;

    [SerializeField] private int _viewerCount = 5;

    public System.Action<string> CommentAction;

    public event Action<float> OnTimer;

    private int _topicIndex = 0;
    public float StreamTime { get; private set; }
    public bool IsStream => StreamTime >= 0;

    private void Start()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        for (int i = 0; i < _viewerCount; i++)
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

            if (_topicIndex < _topicDaters.Length)
            {
                if (StreamTime <= _topicDaters[_topicIndex].Time)
                {
                    DataManager.Instance.TopicData.ChangeState((string)_topicDaters[(int)_topicIndex].Topic);
                    _topicIndex++;
                }
            }
            await UniTask.Yield(cancellationToken: token);
            OnTimer?.Invoke(StreamTime / streamTime);
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
            CommentAction?.Invoke(DataManager.Instance.TopicData.Topic);
        }
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