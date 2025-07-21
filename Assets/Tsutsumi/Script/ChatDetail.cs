using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Texture))]
public class ChatDetail : MonoBehaviour
{
    [SerializeField] CommentDetail _commentDetail;
    [SerializeField] SuperChatDetail _superChatDetail;
    
    public void SetChatDetail(CommentType type, string message)
    {
        switch (type)
        {
            case CommentType.None:
                _commentDetail.gameObject.SetActive(true);
                _commentDetail.SetString(message);
                break;
            case CommentType.Super:
                _superChatDetail.gameObject.SetActive(true);
                _superChatDetail.SetText(message);
                break;
        }
    }
}
