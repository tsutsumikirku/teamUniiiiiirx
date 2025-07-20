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
    [SerializeField] private int _initialLikedPoint = 0;

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

        ViewerLikedPointData = new ViewerLikedPointData(_initialLikedPoint);
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

        ViewerLikedPointData?.Initialize(_initialLikedPoint);
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
    public int CurrentLikedPoint { get; private set; }

    public event Action<string, string> LikeabilityUpdate;

    public ViewerLikedPointData(int viewerLikedPoint)
    {
        CurrentLikedPoint = viewerLikedPoint;
    }

    public void ChangeViewerLikedPoint(int viewerLikedPoint)
    {
        CurrentLikedPoint += viewerLikedPoint;
        LikeabilityUpdate?.Invoke(viewerLikedPoint.ToString(), CurrentLikedPoint.ToString());
    }

    public void Initialize(int viewerLikedPoint)
    {
        CurrentLikedPoint = viewerLikedPoint;
        LikeabilityUpdate = null;
    }
}