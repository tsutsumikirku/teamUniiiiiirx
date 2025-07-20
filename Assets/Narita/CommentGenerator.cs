using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentGenerator : MonoBehaviour
{
    [SerializeField] Comment _commentData;
    [SerializeField] Transform _uiParent;

    [ContextMenu("SetText")]
    public void SetComment()
    {
        Comment com = Instantiate(_commentData, transform.position, Quaternion.identity, _uiParent);

        var data = new CommentAndResponseData();
        data.Comment = "こんめむ～";

        com.SetData(new CommentData(data));
        //var pair = ViewerManager.CSVReader.GetRandomCommentAndResponse("Common");
        //com.SetText(name, pair.Key);
    }
}
