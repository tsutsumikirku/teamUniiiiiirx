using TMPro;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dayCount;
    [SerializeField] TextMeshProUGUI _getMoney;
    [SerializeField] TextMeshProUGUI _likePoint;
    void Awake()
    {
        _dayCount.text = DataManager.Instance.DayData.CurrentDay.ToString() + "日目";
        _getMoney.text = "獲得：" + DataManager.Instance.MoneyData.CurrentMoney.ToString() + "円";
        _likePoint.text ="好感度：+" + DataManager.Instance.ViewerLikedPointData.BeforeLikedPoint.ToString();
    }
}
