using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    AuraDamage,
    ShootingDamage,
    AreaDamage
}

[CreateAssetMenu(fileName = "PlantObject", menuName = "PlantObject", order = 0)]
public class PlantObject : ScriptableObject
{
    public PlantType plantType;
    public float range;
    public float damage;
    public float rechargeTime;
    public float cost;
    public Sprite sprite;
    public float bulletSpeed;
    public float bombRange;
}
