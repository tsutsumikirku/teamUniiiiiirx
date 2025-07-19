using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentGenerator : MonoBehaviour
{
    [SerializeField] Comment _commentData;
    [SerializeField] Transform _uiParent;
    private Viewer _viewer;

    [ContextMenu("SetText")]
    public void SetComment(string name)
    {
        Comment com = Instantiate(_commentData, transform.position, Quaternion.identity, _uiParent);
        var pair = ViewerManager.CSVReader.GetRandomCommentAndResponse("Common");
        com.SetText(name, pair.Key);
    }

    public void SetInstance(Viewer viewer)
    {
        _viewer = viewer;
    }

    public void OnReply()
    {
        Debug.Log("返信されました");
    }

}
