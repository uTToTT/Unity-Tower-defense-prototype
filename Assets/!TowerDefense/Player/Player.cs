using System;
using UnityEngine;

public class Player : MonoBehaviour, IInitializable
{
    public event Action<float> TakedDamage;
    public event Action<float> HPChanged;

    [SerializeField] private float _maxHp = 20;

    private float _currHp;

    public bool IsInitialized { get; }

    public bool Init()
    {
        _currHp = _maxHp;
        
        return true;
    }

    public void TakeDamage(float damageAmount)
    {
        _currHp = Mathf.Max(0, _currHp - damageAmount);

        HPChanged?.Invoke(_currHp);
        TakedDamage?.Invoke(damageAmount);

        if (_currHp <= 0)
        {
            Debug.Log("Player died!");
        }
    }
}
