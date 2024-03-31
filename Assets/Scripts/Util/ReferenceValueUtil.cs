using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReferenceValueUtil
{
    public static IValueReference<int> Highest(List<IValueReference<int>> values)
    {
        if (values == null || values.Count == 0) return null;
        IValueReference<int> returnValue = values[0];
        int currentValue = returnValue.GetValue();

        for (int i = 1; i < values.Count; i++)
        {
            int newValue = values[i].GetValue();
            if(newValue > currentValue)
            {
                currentValue = newValue;
                returnValue = values[i];
            }
        }

        return returnValue;
    }
}
