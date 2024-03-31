using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Operations
{
    private static Dictionary<string, ITaggedOperation> knownOperations = new();
    private static ITaggedOperation defaultOperation;

    public static ITaggedOperation Get(string operationKey)
    {
        if(!knownOperations.ContainsKey(operationKey)) return defaultOperation;
        return knownOperations[operationKey];
    }

    public static void AddOperation(ITaggedOperation operation, bool setAsDefault = false)
    {
        knownOperations[operation.key] = operation;
    }
}
