using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    void Death();

    float MaxHealth {  get; set; }
    float CurrentHealth { get; set; }
}
