using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using UnityEngine;

[RequireComponent(typeof(Texture))]
public class ChatDetail : MonoBehaviour
{
    [SerializeField] CommentDetail _commentDetail;
    [SerializeField] SuperChatDetail _superChatDetail;
    
    public void SetChatDetail(CommentType type, string message, CommentAndResponseData data)
    {
        switch (type)
        {
            case CommentType.None:
                _commentDetail.gameObject.SetActive(true);
                _commentDetail.SetString(message, data);
                break;
            case CommentType.Super:
                _superChatDetail.gameObject.SetActive(true);
                _superChatDetail.SetText(message, data);
                break;
        }
    }
}
