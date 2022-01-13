using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Melee Weapon", menuName = "Item/MeleeWeapon")]
public class MeleeWeaponClass : ItemClass
{
    [Header("MeleeWeapon")]
    public eMeleeWeaponType MeleeWeaponType;
    
    public enum eMeleeWeaponType
    {
        sword,
        axe,
        lance
    }
    public override ItemClass GetItem() { return this; }
    public override MeleeWeaponClass GetMeleeWeapon() { return this;}
    public override RangedWeaponClass GetRangedWeapon() { return null; }
}
