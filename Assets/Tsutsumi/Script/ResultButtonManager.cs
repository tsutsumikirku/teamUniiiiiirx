using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField, Header("リタイアエンドのシーン名")]
    private string retireEndSceneName = "RetireEnd";

    [SerializeField, Header("お金持ちエンドのシーン名")]
    private string richEndSceneName = "RichEnd";

    [SerializeField, Header("お金持ちエンドの残金条件")]
    int richEndMoneyCondition = 500000;

    [SerializeField, Header("ノーマルエンドのシーン名")]
    private string normalEndSceneName = "NormalEnd";

    [SerializeField, Header("ホームレスエンドのシーン名")]
    private string homelessEndSceneName = "HomelessEnd";
    public void ResultButton()
    {
        DataManager.Instance.DayData.AdvanceOneDay();
        if (DataManager.Instance.MentalData.CurrentMental <= 0)
        {
            SceneManager.LoadScene(retireEndSceneName);
        }
        else if (DataManager.Instance.MoneyData.CurrentMoney >= richEndMoneyCondition && DataManager.Instance.DayData.CurrentDay >= 0)
        {
            SceneManager.LoadScene(richEndSceneName);
        }
        else if (DataManager.Instance.MoneyData.TotalMoney >= DataManager.Instance.MoneyData.AmountPaid && DataManager.Instance.DayData.CurrentDay >= 0)
        {
            SceneManager.LoadScene(normalEndSceneName);
        }
        else if (DataManager.Instance.MoneyData.TotalMoney < DataManager.Instance.MoneyData.AmountPaid && DataManager.Instance.DayData.CurrentDay >= 0)
        {
            SceneManager.LoadScene(homelessEndSceneName);
        }
        else
        {
            SceneManager.LoadScene("OutGameScene");
        }
    }
}
