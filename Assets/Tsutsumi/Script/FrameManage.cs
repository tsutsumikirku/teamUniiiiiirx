using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameManage : MonoBehaviour
{
    [Header("HPのスライダーを設定してください")]
    [SerializeField] private Slider _hpSlider;

    [Header("時間のスライダーを設定してください")]
    [SerializeField] private Slider _timeSlider;

    [Header("残り日数のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _dayText;

    [Header("残りの日数のアップデートのテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _dayInText;

    [Header("好感度のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _likePointText;

    [Header("好感度のアップデートのテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _likePointInText;

    [Header("お金のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _moneyText;

    [Header("お金のアップデートのテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _moneyInText;

    //Tweenをキルする必要がある
    private Tween _hpTween;
    private Tween _timeTween;
    async UniTask Awake()
    {
        await UniTask.WaitUntil(() => DataManager.Instance != null);
        await UniTask.WaitUntil(() => DataManager.Instance.MentalData != null);
        await UniTask.WaitUntil(() => DataManager.Instance.ViewerLikedPointData != null);
        DataManager.Instance.MentalData.MindHP += HPSliderUpdate;
        DataManager.Instance.ViewerLikedPointData.LikeabilityUpdate += LikePointUpdate;
        DataManager.Instance.MoneyData.CoinViewUpdate += MoneyUpdate;
        DayTextUpdate(DataManager.Instance.DayData.CurrentDay.ToString());
    }
    public void HPSliderUpdate(float value)
    {
        Debug.Log("HPスライダー更新: " + value);
        _hpTween?.Kill();
        if (!_hpSlider) return;
        _hpTween = _hpSlider.DOValue(value, 0.25f);
    }
    public void TimeSliderUpdate(float value)
    {
        _timeTween?.Kill();
        if (!_timeSlider) return;
        _timeTween = _timeSlider.DOValue(value, 0.25f);
    }
    private void DayTextUpdate(string value1)
    {
        if (!_dayText) return;
        _dayText.text = value1 + "日目";
    }
    private void LikePointUpdate(string value1, string value2)
    {
        if (!_likePointText) return;
        _likePointText.text = "好感度:" + value2;
        if(!_likePointInText) return;
        _likePointInText.text = "+" + value1;
        _likePointInText.gameObject.SetActive(true);
    }
    private void MoneyUpdate(string value1, string value2)
    {
        if (!_moneyText) return;
        _moneyText.text = "お金:" + value2;
        if (!_moneyInText) return;
        _moneyInText.text = "+" + value1;
        _moneyInText.gameObject.SetActive(true);
    }
}