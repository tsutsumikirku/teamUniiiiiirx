using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Texture))]
public class ChatDetail : MonoBehaviour
{
    [SerializeField] Sprite _noneSprite;
    [SerializeField] Sprite _superChatSprite;
    [SerializeField] Sprite _antiSprite;
    private Texture _texture;
    private void Awake()
    {
        _texture = GetComponent<Texture>();
    }
    public void SetChatDetail(CommentType type)
    {
        switch (type)
        {
            case CommentType.None:
                _texture = _noneSprite.texture;
                break;
            case CommentType.Super:
                _texture = _superChatSprite.texture;
                break;
            case CommentType.Anti:
                _texture = _antiSprite.texture;
                break;
        }
    }
}
