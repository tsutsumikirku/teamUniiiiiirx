using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ƒeƒXƒg‚æ‚¤
public class LoadData : MonoBehaviour
{
    [SerializeField] string type = "";
    CommentDataManager commentDataManager;
    void Start()
    {
        commentDataManager = new CommentDataManager();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComment();
        }
        //CommentAndResponseData data = commentDataManager.GetCommentData();
        //Debug.Log($"Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Comment Type: {data.CommentType}, Motion Type: {data.MotionType}, Money: {data.Money}");
        //commentDataManager.GetCommentData("EarlyStage", ref data);
        //Debug.Log($"EarlyStage Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Comment Type: {data.CommentType}, Motion Type: {data.MotionType}, Money: {data.Money}");
        //if (commentDataManager.GetCommentData(data.CommentType, ref data))
        //{
        //    Debug.Log($"Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Motion Type: {data.MotionType}, Money: {data.Money}");
        //}
    }
    [ContextMenu("Comment")]
    void GetComment()
    {
        CommentAndResponseData data = new CommentAndResponseData();
        if (type == "")
            data = commentDataManager.GetCommentData();
        else
            commentDataManager.GetCommentData(type, ref data);
        Debug.Log($"Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Comment Type: {data.CommentType}, Motion Type: {data.MotionType}, Money: {data.Money}");
    }
}
