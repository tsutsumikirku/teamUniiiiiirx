using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutGameManager : MonoBehaviour
{
    [SerializeField]public PrepairFaseDataManager _data;

    [SerializeField, Header("寝た時のメンタル回復量")] int _restPoint = 50;
    [SerializeField,Header("配信に行く際のメンタル消費量")] int _streamPoint = -20;
    [SerializeField, Header("お菓子を食べた際のメンタル回復量")] int _eatingPoint;
    [SerializeField, Header("お菓子を食べた際のお金の消費量")] int _eatingMoney;
    public int RestPoint => _restPoint;
    public int StreamPoint => _streamPoint;
    public int EatingPoint => _eatingPoint;
    public int EatingMoney => _eatingMoney;

    /// <summary>
    /// 休む
    /// </summary>
    public void Rest()
    {
        DataManager.Instance.MentalData.ChangeMental(_restPoint);
        DataManager.Instance.DayData.AdvanceOneDay();
    }

    public void GoStream()
    {
        SceneManager.LoadScene("InGame");
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
