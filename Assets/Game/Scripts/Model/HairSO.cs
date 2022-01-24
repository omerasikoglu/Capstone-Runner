using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Model/Hair")]
public class HairSO : ScriptableObject
{
    public Color color; //saç rengi
    public string colorHex; //saç rengi
    public string colorName; // "red", "green", "blue"

    [ShowAssetPreview] public Sprite sprite;   // UI ekraný için 2d resmi
    [ShowAssetPreview] public Sprite taskSprite;   // Task List için 2d resmi

    public string GetRecipeText()
    {
        return $" <color=#{colorHex}>{colorName}</color> Hair";
    }

}
