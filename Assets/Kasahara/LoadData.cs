using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ƒeƒXƒg‚æ‚¤
public class LoadData : MonoBehaviour
{
    CommentDataManager commentDataManager;
    void Start()
    {
        commentDataManager = new CommentDataManager();
    }

    // Update is called once per frame
    void Update()
    {
        CommentAndResponseData data = commentDataManager.GetCommentData();
        Debug.Log($"Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Comment Type: {data.CommentType}, Motion Type: {data.MotionType}, Money: {data.Money}");
        commentDataManager.GetCommentData("EarlyStage", ref data);
        Debug.Log($"EarlyStage Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Comment Type: {data.CommentType}, Motion Type: {data.MotionType}, Money: {data.Money}");
        if (commentDataManager.GetCommentData(data.CommentType, ref data))
        {
            Debug.Log($"CommentType: {data.CommentType}, Comment: {data.Comment}, Response: {data.Response}, Mental Damage: {data.MentalDamage}, Like Point: {data.LikePoint}, Motion Type: {data.MotionType}, Money: {data.Money}");
        }
    }
}
