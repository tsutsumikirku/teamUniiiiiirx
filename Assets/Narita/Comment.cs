using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    public CommentAndResponseData Data { get; private set; }
    public CommentType CommentType { get; private set; }
    public void SetData(CommentData data)
    {
        Data = data.Data;

        if (data.Data.MentalDamage != 0)
        {
            CommentType = CommentType.Anti;
        }

        else if (data.Data.Money != 0)
        {
            CommentType = CommentType.Super;
        }

        if (!TryGetComponent<TextMeshProUGUI>(out var tmp)) return;
        tmp.text = Data.Comment;
        
        if (!TryGetComponent<ChatMove>(out var chatMove)) return;
        chatMove.data = Data;
    }
}
public enum CommentType
{
    None,
    Super,
    Anti,
}