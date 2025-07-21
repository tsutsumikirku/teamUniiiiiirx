using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrameManage : MonoBehaviour
{
    [Header("HPのスライダーを設定してください")]
    [SerializeField] private Slider _hpSlider;

    [Header("時間のスライダーを設定してください")]
    [SerializeField] private Slider _timeSlider;

    [Header("時間のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _timeText;

    [Header("残り日数のテキストを設定してください")]
    [SerializeField] private TextMeshProUGUI _dayText;
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
        FindObjectOfType<TimeManager>().OnTimer += TimeSliderUpdate;
        DayTextUpdate(DataManager.Instance.DayData.CurrentDay.ToString());
        HPSliderUpdate(DataManager.Instance.MentalData.CurrentMental / 100);
        _likePointText.text = "好感度:" + 0;
        _moneyText.text = "お金:" + 0;
    }
    private void HPSliderUpdate(float value)
    {
        Debug.Log("HPスライダー更新: " + value);
        _hpTween?.Kill();
        if (!_hpSlider) return;
        _hpTween = _hpSlider.DOValue(value, 0.25f);
    }
    private void TimeSliderUpdate(float value1, float value2)
    {
        value1 = Mathf.Abs(value1 - 1f);
        _timeTween?.Kill();
        if (!_timeSlider) return;
        _timeTween = _timeSlider.DOValue(value1, 0.25f);
        _timeText.text = Mathf.Abs(value2 - 60).ToString("00:00");

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
        if (!_likePointInText) return;
        _likePointInText.gameObject.SetActive(false);
        _likePointInText.text = "+" + value1;
        _likePointInText.gameObject.SetActive(true);
    }
    private void MoneyUpdate(string value1, string value2)
    {
        if (!_moneyText) return;
        _moneyText.text = "お金:" + value2;
        if (!_moneyInText) return;
        _moneyInText.gameObject.SetActive(false);
        _moneyInText.text = "+" + value1;
        _moneyInText.gameObject.SetActive(true);
    }
    public void LayerUpdate()
    {
        transform.SetAsLastSibling();
    }
}