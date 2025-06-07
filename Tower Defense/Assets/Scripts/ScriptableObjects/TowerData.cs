using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/TowerData", fileName = "NewTowerData")]
public class TowerData : ScriptableObject
{
    [field: SerializeField] public string TowerName { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Tower Prefab { get; private set; }
}
