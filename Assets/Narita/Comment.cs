using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    public CommentAndResponseData Data { get; private set; }
    public void SetData(CommentData data)
    {
        Data = data.Data;
        if (!TryGetComponent<TextMeshProUGUI>(out var tmp)) return;
        tmp.text = Data.Comment;
        
        if (!TryGetComponent<ChatMove>(out var chatMove)) return;
        chatMove.data = Data;
    }
}
