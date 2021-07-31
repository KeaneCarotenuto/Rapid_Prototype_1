using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PropList", menuName = "PropList", order = 0)]
public class PropList : ScriptableObject
{
    public List<GameObject> Prefabs;

}
