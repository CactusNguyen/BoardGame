using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/Card Conditions", fileName = "CardCondition")]
public class CardConditions : ScriptableObject
{
    public bool Free()
    {
        return true;
    }
}

[Serializable]
public class Condition : SerializableCallback<bool> { }
