using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentData
{
    public CommentAndResponseData Data { get; private set; }

    public CommentData(CommentAndResponseData data)
    {
        Data = data;
    }
}
