using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reply : MonoBehaviour
{
    private MentalData _mentalData;
    private int _viewerLikedPoint;
    private int _playerMental;
    private string _topic;

    private void Start()
    {
        if (_mentalData == null)
        {
            _mentalData = GetComponent<MentalData>();
        }
    }

    public void SaveState(CommentAndResponseData data)
    {
        _viewerLikedPoint = data.LikePoint;
        TimeManager.Instance.State.ChangeViewerLikedPoint(_viewerLikedPoint);
        _playerMental = data.MentalDamage;
        _mentalData.ChangeMental(_playerMental);
        _topic = data.CommentType;
        TimeManager.Instance.State.ChangeState(_topic);
    }
}
