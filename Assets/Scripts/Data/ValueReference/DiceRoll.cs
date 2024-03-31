using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : IValueReference<int>
{
    public int Value { get; set; }

    public int lastRoll { get; private set; }

    public DiceRoll(int maxValue)
    {
        Value = maxValue;
    }

    public int GetValue()
    {
        lastRoll = Random.Range(1, Mathf.Max(Value + 1, 1));
        return lastRoll;
    }
}
