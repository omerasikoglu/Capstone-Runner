using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List/HairList")]
public class HairTypeListSO : ScriptableObject
{
    public List<HairSO> list;
}
