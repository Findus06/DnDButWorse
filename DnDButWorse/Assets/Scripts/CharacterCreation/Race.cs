using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Race : ScriptableObject
{
    public string Name;
    public List<Ability> abilityBonuses;
}
