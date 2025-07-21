using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentGenerator : MonoBehaviour
{
    [SerializeField, Header("コメントのプレハブ")] private Comment _commentData;

    [SerializeField, Header("Canvas下のオブジェクト")] private Transform _uiParent;

    [SerializeField, Header("話題に対するコメントの出現数")] private int _topicCount = 5;

    private int _currentCount = 0;

    private CommentDataManager _commentDataManager;

    private TimeManager _timeManger;

    [SerializeField, Header("アンチコメントの生成確率  1 / value")] private float _value = 5;
    private void Awake()
    {
        _commentDataManager = new CommentDataManager();

        _timeManger = ServiceLocater.Get<TimeManager>();

        _timeManger.CommentAction += SetComment;
        DataManager.Instance.TopicData.OnStateChange += Initialize;
        DataManager.Instance.ViewerLikedPointData.OnAddPoint += SetSuperChat;
    }

    private void OnDisable()
    {
        _timeManger.CommentAction -= SetComment;
        DataManager.Instance.TopicData.OnStateChange -= Initialize;
        DataManager.Instance.ViewerLikedPointData.OnAddPoint -= SetSuperChat;

    }

    private void Initialize()
    {
        _currentCount = _topicCount;
    }
    /// <summary>
    /// コメント生成
    /// </summary>
    /// <param name="topic"></param>
    public void SetComment(string topic)
    {
        Comment com = Instantiate(_commentData, _uiParent);

        float value = 1 / _value;
        float rand = Random.Range(0f, 1f);

        CommentAndResponseData data;

        if (value < rand)
        {
            if (!string.IsNullOrEmpty(topic) && _currentCount != 0)
            {
                data = new CommentAndResponseData();
                _commentDataManager.GetCommentData(topic, ref data);
                _currentCount--;
            }
            //話題がなかったら、または、話題生成の上限に達していたら
            else
            {
                data = _commentDataManager.GetCommentData();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(topic) && _currentCount != 0)
            {
                data = new CommentAndResponseData();
                _commentDataManager.GetHateCommentData(topic, ref data);
                _currentCount--;
            }
            //話題がなかったら、または、話題生成の上限に達していたら
            else
            {
                data = _commentDataManager.GetHateCommentData();
            }
        }
        com.SetData(new CommentData(data));
    }
    public void SetSuperChat()
    {
        string topic = DataManager.Instance.TopicData.Topic;
        int currentLikedPoint = DataManager.Instance.ViewerLikedPointData.CurrentLikedPoint;
        int maxLikedPoint = DataManager.Instance.ViewerLikedPointData.MaxLikedPoint;

        // (Current/2) / Max の確率でGetSuperChatDataを呼ぶ
        float superChatProbability = (currentLikedPoint / 2f) / maxLikedPoint;
        float rand = Random.Range(0f, 1f);


        if (rand < superChatProbability)
        {
            Comment com = Instantiate(_commentData, _uiParent);
            CommentAndResponseData data;
            // SuperChatデータを取得
            if (!string.IsNullOrEmpty(topic) && _currentCount != 0)
            {
                data = new CommentAndResponseData();
                _commentDataManager.GetSuperChatData(topic, ref data);
                _currentCount--;
                Debug.LogError(data.Comment + data.Money);
            }
            else
            {
                data = _commentDataManager.GetSuperChatData();
            }
            com.SetData(new CommentData(data));
        }
        else
        {
            // 通常のコメントデータを取得
            SetComment(topic);
            return;
        }
    }
}
