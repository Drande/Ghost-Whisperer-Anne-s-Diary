using UnityEngine;

public static class ArrayExtensions {
    public static TElement Next<TElement>(this TElement[] arr) {
        return arr[Random.Range(0, arr.Length)];
    }

    public static int NextIndex(this object[] arr) {
        return Random.Range(0, arr.Length);
    }
}