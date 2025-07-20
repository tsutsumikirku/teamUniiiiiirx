using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private int _maxDayCount = 7;
    private int _currentDay;

    public bool HasReachedMaxDays => _currentDay >= _maxDayCount;

    private void Awake()
    {
        ServiceLocater.Set(this);
    }

    public void AdvanceOneDay()
    {
        _currentDay++;
    }
}
