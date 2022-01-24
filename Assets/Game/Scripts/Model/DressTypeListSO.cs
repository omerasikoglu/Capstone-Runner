using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/List/DressList")]
public class DressTypeListSO : ScriptableObject
{
    public List<DressSO> list;
}
