using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CommentAndResponseData
{
    public int Id;
    public string Comment;
    public string Response;
    public int MentalDamage;
    public int LikePoint;
    public string CommentType;
    public string MotionType;
}
public class CommentDataManager : MonoBehaviour
{
    readonly Dictionary<string, List<CommentAndResponseData>> commentData = new Dictionary<string, List<CommentAndResponseData>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
