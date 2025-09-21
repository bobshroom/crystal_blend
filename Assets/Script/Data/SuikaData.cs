using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SuikaData", menuName = "Scriptable Objects/SuikaData")]
public class SuikaData : ScriptableObject
{
    public int level;
    public float size;
    public Color color;
    public int score;
    public Sprite sprite;
    public string name;
    public float sizeOffset = 1f;
}
