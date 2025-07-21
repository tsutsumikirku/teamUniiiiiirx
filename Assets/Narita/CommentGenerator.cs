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

    [SerializeField, Header("タイムマネージャー")] private TimeManager _timeManger;

    private void Awake()
    {
        _commentDataManager = new CommentDataManager();
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

        CommentAndResponseData data;

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

        Comment com = Instantiate(_commentData, _uiParent);
        CommentAndResponseData data;

        if (rand < superChatProbability)
        {
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
        }
        else
        {
            // 通常のコメントデータを取得
            if (!string.IsNullOrEmpty(topic) && _currentCount != 0)
            {
                data = new CommentAndResponseData();
                _commentDataManager.GetCommentData(topic, ref data);
                _currentCount--;
            }
            else
            {
                data = _commentDataManager.GetCommentData();
            }
        }

        com.SetData(new CommentData(data));
    }
}
