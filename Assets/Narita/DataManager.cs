﻿using System;
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
    [SerializeField] private int _minLikedPoint = -100;
    [SerializeField] private int _maxDayLikedPoint = 30;
    [SerializeField] private int _amountPaid = 100000;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MentalData = new MentalData(_initialMental, _maxMental);

        MoneyData = new MoneyData(_amountPaid);

        DayData = new DayCounter(_maxDayCount);

        TopicData = new CurrentTopicData();

        ViewerLikedPointData = new ViewerLikedPointData(_maxLikedPoint, _minLikedPoint, _maxDayLikedPoint);
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

        ViewerLikedPointData.Initialize();
    }
    /// <summary>
    /// 次の配信に移る際のデータの初期化
    /// </summary>
    public void NextScene()
    {
        MentalData.Next();

        MoneyData.Next();

        TopicData.Initialize();

        ViewerLikedPointData.Next();
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
    public int MinLikedPoint { get; private set; }

    public int TotalPoint { get; private set; }

    private int _maxDayPoint;
    private int _currentDayPoint;

    public event Action<string, string> LikeabilityUpdate;
    public event Action OnAddPoint;

    public ViewerLikedPointData(int maxLikedPoint, int minLikedPoint, int maxDayPoint)
    {
        MaxLikedPoint = maxLikedPoint;
        MinLikedPoint = minLikedPoint;
        BeforeLikedPoint = CurrentLikedPoint;
        _maxDayPoint = maxDayPoint;
        _currentDayPoint = 0;
        TotalPoint = 0;
    }

    public void ChangeViewerLikedPoint(int viewerLikedPoint)
    {
        if (CurrentLikedPoint > 0)
        {
            OnAddPoint?.Invoke();
        }
        if (_maxDayPoint <= _currentDayPoint) { return; }

        _currentDayPoint = Mathf.Min(_currentDayPoint + viewerLikedPoint, _maxDayPoint);

        CurrentLikedPoint = Mathf.Clamp(BeforeLikedPoint + _currentDayPoint, MinLikedPoint, MaxLikedPoint);

        TotalPoint = Mathf.Max(TotalPoint, CurrentLikedPoint);

        LikeabilityUpdate?.Invoke(viewerLikedPoint.ToString(), CurrentLikedPoint.ToString());
    }

    public void Next()
    {
        BeforeLikedPoint = CurrentLikedPoint;
        LikeabilityUpdate = null;
        OnAddPoint = null;
        _currentDayPoint = 0;
        TotalPoint = 0;
    }

    public void Initialize()
    {
        BeforeLikedPoint = CurrentLikedPoint;
        CurrentLikedPoint = 0;
        LikeabilityUpdate = null;
        OnAddPoint = null;
        _currentDayPoint = 0;
        TotalPoint = 0;
    }
}