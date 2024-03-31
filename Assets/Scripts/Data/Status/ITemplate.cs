using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITemplate
{
    public Dictionary<string, IValueReference<int>> Stats { get; set; }

    public void SetUp();
}
