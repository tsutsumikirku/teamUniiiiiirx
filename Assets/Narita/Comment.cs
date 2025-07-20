using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    public CommentData Data { get; private set; }
    [SerializeField] Text _text;
    public void OnReply()
    {
        Debug.Log("Reply");
    }

    public void SetData(CommentData data)
    {
        Data = data;
        _text.text = Data.Data.Comment;
    }
}
