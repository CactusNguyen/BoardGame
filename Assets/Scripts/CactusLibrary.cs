using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class CactusLibrary
{
    public static void Shuffle<T>(this IList<T> list)  
    {  
        var n = list.Count;  
        while (n > 1) {  
            n--;  
            var k = Random.Range(0, n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }

    public static IEnumerator Then(this IEnumerator previous, Action then)
    {
        yield return previous;
        then?.Invoke();
    }
}
