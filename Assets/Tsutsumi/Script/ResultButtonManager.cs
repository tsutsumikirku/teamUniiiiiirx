using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButtonManager : MonoBehaviour
{
    public void ResultButton()
    {
        DataManager.Instance.DayData.AdvanceOneDay();
    }
}
