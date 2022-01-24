using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Model/Dress")]
public class DressSO : ScriptableObject
{
    public Color color;
    public string colorHex; 
    public string colorName;

    [ShowAssetPreview] public Sprite sprite;   // UI ekraný için 2d resmi
    [ShowAssetPreview] public Sprite taskSprite;   // Task List için 2d resmi

    public string GetRecipeText()
    {
        return $" <color=#{colorHex}>{colorName}</color> Dress";
    }

}
