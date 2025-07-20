using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentGenerator : MonoBehaviour
{
    [SerializeField] private Comment _commentData;
    [SerializeField] private Transform _uiParent;
    [SerializeField] private int _topicCount = 5;
    private int _currentCount = 0;
    private CommentDataManager _commentDataManager;
    [SerializeField] private TimeManager _timeManger;

    private void Awake()
    {
        _commentDataManager = new CommentDataManager();
    }

    private void Start()
    {
        _timeManger.CommentAction += (topic) => SetComment(topic);
        DataManager.Instance.TopicData.OnStateChange += () => _currentCount = _topicCount;
    }
    private void OnDisable()
    {
        _timeManger.CommentAction -= (topic) => SetComment(topic);
        DataManager.Instance.TopicData.OnStateChange -= () => _currentCount = _topicCount;
    }

    [ContextMenu("SetText")]
    public void SetComment(string topic)
    {
        Comment com = Instantiate(_commentData, _uiParent);


        CommentAndResponseData data;
        if (topic != "" && _currentCount != 0)
        {
            data = new CommentAndResponseData();
            _commentDataManager.GetCommentData(topic, ref data);
            _currentCount--;
        }
        else
        {
            data = _commentDataManager.GetCommentData();
        }

        com.SetData(new CommentData(data));
    }
}
