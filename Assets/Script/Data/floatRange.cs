using UnityEngine;

[System.Serializable]
public struct FloatRange {
    public float start;
    public float end;

    public float GetRandom() {
        return Random.Range(start, end);
    }
}