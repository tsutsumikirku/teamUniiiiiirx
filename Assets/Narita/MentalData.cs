using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalData : MonoBehaviour
{
    [SerializeField] private int _initialMental = 100;
    [SerializeField] private int _maxMental = 100;
    public int CurrentMental { get; private set; }
    private void Awake()
    {
        ServiceLocater.Set(this);
        CurrentMental = _initialMental;
    }

    /// <summary>
    /// メンタルを変更する
    /// </summary>
    /// <param name="value">正か負かで加減算</param>
    public void ChangeMental(int value)
    {
        CurrentMental = Mathf.Min(CurrentMental + value, _maxMental);
    }
}
