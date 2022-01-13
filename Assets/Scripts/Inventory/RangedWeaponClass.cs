using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Ranged Weapon", menuName = "Item/RangedWeapon")]
public class RangedWeaponClass : ItemClass
{
    [Header("RangedWeapon")]
    public eRangedWeaponType RangedWeaponType;
    public enum eRangedWeaponType
    {
        pistol,
        shotgun,
        rifle
    }
    public override ItemClass GetItem() { return this; }
    public override MeleeWeaponClass GetMeleeWeapon() { return null; }
    public override RangedWeaponClass GetRangedWeapon() { return this; }
}
