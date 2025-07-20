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
        _timeManger.CommentAction += SetComment;
        DataManager.Instance.TopicData.OnStateChange += Initialize;
    }

    private void Start()
    {
    }
    private void OnDisable()
    {
        _timeManger.CommentAction -= SetComment;
        DataManager.Instance.TopicData.OnStateChange -= Initialize;
    }

    private void Initialize()
    {
        _currentCount = _topicCount;
    }

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
        else
        {
            data = _commentDataManager.GetCommentData();
        }

        com.SetData(new CommentData(data));
    }
    public void SetSuperChat()
    {

    }
}
