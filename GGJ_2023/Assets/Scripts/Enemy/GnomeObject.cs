using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GnomeObject", menuName = "GnomeObject", order = 0)]
public class GnomeObject : ScriptableObject
{
    public float damage;
    public float endurance;
    public float speed;
    public Sprite normalSprite;
    public Sprite damagedSprite;
}
