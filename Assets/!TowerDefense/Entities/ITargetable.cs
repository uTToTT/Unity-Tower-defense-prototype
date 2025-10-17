using System;
using UnityEngine;

public interface ITargetable 
{
    event Action<ITargetable> OnDeath;
    Transform Transform { get; }
    bool IsAlive {  get; }
    void TakeDamage(float damageAmount);
}
