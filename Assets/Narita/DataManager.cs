using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9999)]
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public MentalData MentalData { get; private set; }
    public MoneyData MoneyData { get; private set; }
    public DayCounter DayData { get; private set; }

    public CurrentTopicData TopicData { get; private set; }

    public ViewerLikedPointData ViewerLikedPointData { get; private set; }

    [SerializeField] private int _maxDayCount = 7;

    [SerializeField] private int _initialMental = 100;
    [SerializeField] private int _maxMental = 100;
    [SerializeField] private int _maxLikedPoint = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MentalData = new MentalData(_initialMental, _maxMental);

        MoneyData = new MoneyData();

        DayData = new DayCounter(_maxDayCount);

        TopicData = new CurrentTopicData();

        ViewerLikedPointData = new ViewerLikedPointData(_maxLikedPoint);
    }
    /// <summary>
    /// すべてを一括で初期化
    /// </summary>
    public void Initialize()
    {
        MentalData.Initialize(_initialMental, _maxMental);

        MoneyData.Initialize();

        DayData.Initialize(_maxDayCount);

        TopicData.Initialize();

        ViewerLikedPointData?.Initialize();
    }
}
[System.Serializable]
public class CurrentTopicData
{
    public string Topic { get; private set; } = "";

    public event System.Action OnStateChange;

    public void ChangeState(string topic)
    {
        Topic = topic;
        OnStateChange?.Invoke();
    }
    public void Initialize()
    {
        Topic = "";
        OnStateChange = null;
    }
}
[System.Serializable]
public class ViewerLikedPointData
{
    public int BeforeLikedPoint { get; private set; }
    public int CurrentLikedPoint { get; private set; }
    public int MaxLikedPoint { get; private set; }

    public event Action<string, string> LikeabilityUpdate;
    public event Action OnAddPoint;

    public ViewerLikedPointData(int viewerLikedPoint)
    {
        MaxLikedPoint = viewerLikedPoint;
        BeforeLikedPoint = CurrentLikedPoint;
    }

    public void ChangeViewerLikedPoint(int viewerLikedPoint)
    {
        if (CurrentLikedPoint > 0)
        {
            OnAddPoint?.Invoke();
        }

        CurrentLikedPoint = Mathf.Min(CurrentLikedPoint + viewerLikedPoint, MaxLikedPoint);
        LikeabilityUpdate?.Invoke(viewerLikedPoint.ToString(), CurrentLikedPoint.ToString());
        Debug.Log($"増減{viewerLikedPoint}----値{CurrentLikedPoint}");
    }

    public void Initialize()
    {
        BeforeLikedPoint = CurrentLikedPoint;
        CurrentLikedPoint = 0;
        LikeabilityUpdate = null;
        OnAddPoint = null;
    }
}