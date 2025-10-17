using System;
using UnityEngine;

public sealed class HPModule
{
    public event Action OnDie;
    public event Action<float> OnCurrHpChanged;

    public float MaxHP { get; private set; }
    public float CurrHP { get; private set; }

    public bool IsAlive() => CurrHP > 0; 
    public void SetMaxHP(float maxHP)
    {
        if (maxHP <= 0)
            throw new ArgumentException("Max hp must be greater than 0");

        MaxHP = maxHP;
        CurrHP = maxHP;
        OnCurrHpChanged?.Invoke(CurrHP);
    }

    public void TakeDamage(float damageAmount)
    {
        var oldHp = CurrHP;
        CurrHP = Mathf.Max(0, CurrHP - damageAmount);

        if (oldHp != CurrHP) OnCurrHpChanged?.Invoke(CurrHP);
        if (CurrHP <= 0) OnDie?.Invoke();
    }
}
