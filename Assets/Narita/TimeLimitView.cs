using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dayText;
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Start()
    {
        DataManager.Instance.MoneyData.CoinViewUpdate += (a, money) => SetMoneyText();
        DataManager.Instance.DayData.OnStepDay += SetDayText;

        int money = DataManager.Instance.MoneyData.AmountPaid - DataManager.Instance.MoneyData.TotalMoney;
        int day = DataManager.Instance.DayData.CurrentDay;

        _moneyText.text = $"{money}円";
        _dayText.text = $"{day}日";
    }

    private void SetMoneyText()
    {
        int money = DataManager.Instance.MoneyData.AmountPaid - DataManager.Instance.MoneyData.TotalMoney;
        _moneyText.text = $"{money}円";
    }
    private void SetDayText(string day)
    {
        _dayText.text = $"{day}日";
    }
}
