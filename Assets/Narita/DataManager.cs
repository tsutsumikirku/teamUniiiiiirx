using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public MentalData MentalData { get; private set; }
    public MoneyData MoneyData { get; private set; }
    [SerializeField] int _initialMental = 100;
    [SerializeField] int _maxMental = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MentalData = new MentalData();
        MentalData.Initialize(_initialMental, _maxMental);

        MoneyData = new MoneyData();
    }
}
