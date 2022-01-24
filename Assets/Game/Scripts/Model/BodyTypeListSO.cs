using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List/BodyList")]
public class BodyTypeListSO : ScriptableObject
{
    public List<BodySO> list;
}
