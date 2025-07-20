using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    private CommentData _data;
    [SerializeField] Text _text;
    public void OnReply()
    {
        Debug.Log("Reply");
    }

    public void SetData(CommentData data)
    {
        _data = data;
        _text.text = _data.Data.Comment;
    }
}
