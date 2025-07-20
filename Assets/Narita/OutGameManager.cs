using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutGameManager : MonoBehaviour
{
    [SerializeField] PrepairFaseDataManager _data;

    [SerializeField, Header("寝た時のメンタル回復量")] int _restPoint = 50;

    [SerializeField, Header("お菓子を食べた際のメンタル回復量")] int _eatingPoint;
    [SerializeField, Header("お菓子を食べた際のお金の消費量")] int _eatingMoney;

    /// <summary>
    /// 休む
    /// </summary>
    public void Rest()
    {
        DataManager.Instance.MentalData.ChangeMental(_restPoint);
    }

    public void GoStream()
    {
        //中は書かない
    }
    /// <summary>
    /// お菓子を食べる
    /// </summary>
    public void Eat()
    {
        MoneyData moneyData = DataManager.Instance.MoneyData;
        if (moneyData.IsUseMoney(_eatingMoney))
        {
            DataManager.Instance.MentalData.ChangeMental(-_eatingPoint);
            moneyData.ChangeMoney(-_eatingMoney);
        }
    }
    /// <summary>
    /// オモニネルちゃんが言うセリフを取得する
    /// </summary>
    public string GetSerif()
    {
        int likedPoint = DataManager.Instance.ViewerLikedPointData.CurrentLikedPoint;
        int money = DataManager.Instance.MoneyData.CurrentMoney;
        return _data.GetSerif(likedPoint, money);
    }
}
