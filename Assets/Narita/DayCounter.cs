using UnityEngine;

public class DayCounter
{
    public int CurrentDay { get; private set; }

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
    }
}
