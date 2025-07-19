using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
    public string ReplyComment { get; private set; }
    public string Name { get; private set; }
    [SerializeField] Text _text;

    public void OnReply()
    {
        Debug.Log("Reply");
    }

    public void SetText(string name, string reply)
    {
        Name = name;
        ReplyComment = reply;
        _text.text = $"{Name}\n{ReplyComment}";
    }
}
