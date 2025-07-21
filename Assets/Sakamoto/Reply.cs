using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Reply : MonoBehaviour
{
    public CommentAndResponseData Data { get; private set; }

    public void SaveState(CommentAndResponseData data)
    {
        Data = data;

        DataManager.Instance.ViewerLikedPointData.ChangeViewerLikedPoint(Data.LikePoint);

        DataManager.Instance.MentalData.ChangeMental(-Data.MentalDamage);

        DataManager.Instance.TopicData.ChangeState(Data.CommentType);
    }
}
