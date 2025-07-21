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
        var type = BranchOutGameOver.GetGameOverType(DataManager.Instance.MentalData.CurrentMental,DataManager.Instance.MoneyData.CurrentMoney);
        if (type == BranchOutGameOver.BranchOutGameOverType.MentalBreakGameOver)
        {
            SceneManager.LoadScene(retireEndSceneName);
        }
        if(DataManager.Instance.DayData.CurrentDay != 0)
        {
            SoundManager.Instance.PlayBGM("配信準備中BGM");
            SceneManager.LoadScene("OutGame");
        }
        switch(type)
        {
            case BranchOutGameOver.BranchOutGameOverType.RichGameClear:
                SceneManager.LoadScene(richEndSceneName);
                break;
            case BranchOutGameOver.BranchOutGameOverType.NormalGameClear:
                SceneManager.LoadScene(normalEndSceneName);
                break;
            case BranchOutGameOver.BranchOutGameOverType.ForcedRemovalGameOver:
                SceneManager.LoadScene(homelessEndSceneName);
                break;
        }
    }
}
