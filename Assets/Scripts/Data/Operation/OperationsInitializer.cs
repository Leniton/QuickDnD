using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationsInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Initialize()
    {
        Operations.AddOperation(new SumOperation(), true);
        Operations.AddOperation(new MinusOperation());
        Operations.AddOperation(new MultOperation());
    }
}
