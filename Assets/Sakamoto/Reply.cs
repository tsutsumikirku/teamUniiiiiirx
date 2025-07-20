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

        TimeManager.Instance.State.ChangeViewerLikedPoint(Data.LikePoint);

        ServiceLocater.Get<MentalData>().ChangeMental(Data.MentalDamage);

        TimeManager.Instance.State.ChangeState(Data.CommentType);
    }
}
