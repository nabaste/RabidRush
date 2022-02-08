using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum kindOfDamage
{
    physical,
    aoe,
    chemical
}
public interface IDamageable
{
    void GetDamage(float damage, kindOfDamage kind, float pace = 0, float duration = 0);
}
