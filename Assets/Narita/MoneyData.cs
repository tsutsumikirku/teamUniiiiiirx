using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyData : MonoBehaviour
{
    [SerializeField] private int _initialMoney = 0;
    public int CurrentMoney { get; private set; }
    [SerializeField] private int _targetMoney = 100000;
    /// <summary>
    /// 目標金額まで稼いだらtrue
    /// </summary>
    public bool IsGameClear => _initialMoney >= _targetMoney;
    private void Awake()
    {
        ServiceLocater.Set(this);
        CurrentMoney = _initialMoney;
    }
    /// <summary>
    /// 所持金を変更する
    /// </summary>
    /// <param name="money">正か負かで加算減算</param>
    public void ChangeMoney(int money)
    {
        CurrentMoney += money;
    }
}
