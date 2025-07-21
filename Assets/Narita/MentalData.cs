using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MentalData
{
    private int _maxMental = 100;
    public int CurrentMental { get; private set; }

    public event Action<float> MindHP;
    public MentalData(int initialMental, int max)
    {
        CurrentMental = initialMental;
        _maxMental = max;
    }

    public void Initialize(int initialMental, int max)
    {
        CurrentMental = initialMental;
        _maxMental = max;
        MindHP = null;
    }

    public void Next()
    {
        MindHP = null;
    }

    /// <summary>
    /// メンタルを変更する
    /// </summary>
    /// <param name="value">正か負かで加減算</param>
    public void ChangeMental(int value)
    {
        CurrentMental = Mathf.Min(CurrentMental + value, _maxMental);
        MindHP?.Invoke((float)(CurrentMental) / (float)(_maxMental));
    }
}
