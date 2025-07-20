using System.Collections;
using System.Collections.Generic;
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
    [Header("好感度のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _likePointText;
    [Header("お金のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _moneyText;


    public void HPSliderUpdate(float value)
    {
        if (!_hpSlider) return;
        float before = _hpSlider.value;
        DOTween.To(() => before, x => before = x, value, 0.2f)
            .OnUpdate(() => _hpSlider.value = before);
    }
    public void TimeSliderUpdate(float value)
    {
        if (!_timeSlider) return;
        float before = _timeSlider.value;
        DOTween.To(() => before, x => before = x, value, 0.1f)
            .OnUpdate(() => _timeSlider.value = before);
    }
    public void DayTextUpdate(string value)
    {
        if (!_dayText) return;
        _dayText.text = value + "日目";
    }
    public void LikePointUpdate(string value)
    {
        if (!_likePointText) return;
        _likePointText.text = "好感度:" + value;
    }
    public void MoneyUpdate(string value)
    {
        if (!_moneyText) return;
        _moneyText.text = "お金:" + value;
    }
}
