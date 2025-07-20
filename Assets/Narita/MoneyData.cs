using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyData
{
    public int CurrentMoney { get; private set; }
    public event Action<string, string> CoinViewUpdate;

    public void Initialize()
    {
        int beforeMoney = CurrentMoney;
        CurrentMoney = 0;
        CoinViewUpdate = null;
    }

    /// <summary>
    /// 所持金を変更する
    /// </summary>
    /// <param name="money">正か負かで加算減算</param>
    public void ChangeMoney(int money)
    {
        CurrentMoney = Mathf.Max(CurrentMoney + money, 0);
        CoinViewUpdate?.Invoke(money.ToString(), CurrentMoney.ToString());
    }

    public bool IsUseMoney(int money)
    {
        return CurrentMoney - money >= 0;
    }
}
