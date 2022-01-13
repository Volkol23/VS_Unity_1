using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string ItemName;
    public Sprite ItemIcon;
    public bool IsStackable = true;

    public abstract ItemClass GetItem();
    public abstract MeleeWeaponClass GetMeleeWeapon();
    public abstract RangedWeaponClass GetRangedWeapon();
}
