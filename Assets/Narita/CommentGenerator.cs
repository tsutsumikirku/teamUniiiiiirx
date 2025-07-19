using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentGenerator : MonoBehaviour
{
    [SerializeField] Comment _commentData;
    private string _comment;
    private IViewer _viewer;

    [ContextMenu("SetText")]
    public void GetComment()
    {
        _comment = "かわいいね";
        Comment com = Instantiate(_commentData,FindAnyObjectByType<Canvas>().transform);
        com.SetText("OMAE", _comment);
    }

    public void SetInstance(IViewer viewer)
    {
        _viewer = viewer;
    }

    public void OnReply()
    {
        Debug.Log("返信されました");
    }

}
