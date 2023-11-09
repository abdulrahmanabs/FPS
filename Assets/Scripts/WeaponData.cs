using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "New Weapon")]
public class WeaponData : ScriptableObject
{
    public float maxAmmo;
    public float currentAmmo;
    public float damage;
    public float ShootingRate;
}
