using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public MentalData MentalData { get; private set; }
    public MoneyData MoneyData { get; private set; }
    public DayCounter DayData { get; private set; }

    [SerializeField] private int _maxDayCount = 7;

    [SerializeField] private int _initialMental = 100;
    [SerializeField] private int _maxMental = 100;

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
    }
}
