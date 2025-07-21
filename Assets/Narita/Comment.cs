using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    [SerializeField] private int _minusLikedPoint = 15;
    public CommentAndResponseData Data { get; private set; }
    public CommentType CommentType { get; private set; }
    public void SetData(CommentData data)
    {
        Data = data.Data;

        if (Data.Money != 0)
        {
            CommentType = CommentType.Super;
            DataManager.Instance.MoneyData.ChangeMoney(Data.Money);
        }

        if (!TryGetComponent<ChatDetail>(out var chat)) return;
        chat.SetChatDetail(CommentType, Data.Comment, Data);
        if (!TryGetComponent<ChatMove>(out var chatMove)) return;
        chatMove.Data = Data;
    }

    public void OnThrowEvent()
    {
        if (CommentType == CommentType.Super)
        {
            DataManager.Instance.ViewerLikedPointData.ChangeViewerLikedPoint(-_minusLikedPoint);
        }
    }
}
public enum CommentType
{
    None,
    Super,
}