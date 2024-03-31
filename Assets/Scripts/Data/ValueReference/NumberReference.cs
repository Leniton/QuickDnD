using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberReference : IValueReference<int>
{
    private int value;
    public int Value { get => GetValue(); set => this.value = value; }

    public NumberReference(int _value) => Value = _value;

    public int GetValue() => value;
}
