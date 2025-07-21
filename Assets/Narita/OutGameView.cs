using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(OutGameManager))]
public class OutGameView : MonoBehaviour
{
    [SerializeField] UIButton _healButton;
    [SerializeField] TextMeshProUGUI _healText;
    [SerializeField] UIButton _liveButton;
    [SerializeField] TextMeshProUGUI _liveText;
    [SerializeField] UIButton _snackButton;
    [SerializeField] TextMeshProUGUI _snackText;
    [SerializeField] SerifCsvDataSet[] _serifCsvDataSets;
    OutGameManager _outGameManager;
    void Awake()
    {
        _outGameManager = GetComponent<OutGameManager>();

        _healButton.onClick.AddListener(_outGameManager.Rest);
        _healText.text = $"一日主に寝る、精神+{_outGameManager.RestPoint}";

        _liveButton.onClick.AddListener(_outGameManager.GoStream);
        _liveText.text = $"配信に行く、精神{_outGameManager.StreamPoint}";

        _snackButton.onClick.AddListener(_outGameManager.Eat);
        _snackText.text = DataManager.Instance.MoneyData.CurrentMoney > _outGameManager.EatingMoney ? $"{_outGameManager.EatingMoney}払って精神+{_outGameManager.EatingPoint}" : "お金が足りません";

        if (DataManager.Instance.MoneyData.CurrentMoney > _outGameManager.EatingMoney)_snackButton.onClick.AddListener(_outGameManager.Eat);
        if (_serifCsvDataSets.Length == 0) return;
        // SerifCsvDataSet todaySerif = Array.Find(_serifCsvDataSets, x => x.Day == DataManager.Instance.DayData.CurrentDay);
        // _outGameManager._data.PrepairFaseDataPath = todaySerif.CsvPath;
        // CharacterTextManager.CharacterTextManagerInstance.TextUpdate(_outGameManager.GetSerif());
    }
}
[System.Serializable]
public class SerifCsvDataSet
{
    [SerializeField, Header("CSVファイルのパス")]
    public string CsvPath = "SerifData.csv"; // CSVファイルのパス
    [SerializeField, Header("このCSVは何日目のデータか指定してください")]
    public int Day = 1; // このCSVは何日目のデータか指定
}
