using System;
using UnityEngine;

public class DayCounter
{
    public int CurrentDay { get; private set; }
    public Action<string> OnStepDay;
    public DayCounter(int dayCount)
    {
        CurrentDay = dayCount;
    }

    public void Initialize(int dayCount)
    {
        CurrentDay = dayCount;
    }

    public void AdvanceOneDay()
    {
        CurrentDay--;
        OnStepDay?.Invoke(CurrentDay.ToString());
    }
}
