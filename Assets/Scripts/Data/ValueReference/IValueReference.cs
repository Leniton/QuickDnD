using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IValueReference<T>
{
    public T Value { get; set; }
    public T GetValue();
}
