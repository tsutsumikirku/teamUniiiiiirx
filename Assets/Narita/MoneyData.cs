using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyData
{
    public int CurrentMoney { get; private set; }

    /// <summary>
    /// 所持金を変更する
    /// </summary>
    /// <param name="money">正か負かで加算減算</param>
    public void ChangeMoney(int money)
    {
        CurrentMoney = Mathf.Max(CurrentMoney + money, 0);
    }
}
