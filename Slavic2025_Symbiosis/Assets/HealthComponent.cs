using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public uint MaxHP {  get; private set; }
    [SerializeField] public uint CurrentHP { get; private set; }
    public UnityEvent<HealthComponent> OnDamaged;
    public UnityEvent<HealthComponent> OnHealed;
    public UnityEvent<HealthComponent> OnDeath;


    public void Initialize()
    {
        CurrentHP = MaxHP;
    }

    public void Damage(uint damage)
    {
        CurrentHP = (uint)Mathf.Max(0, CurrentHP-damage);
        OnDamaged?.Invoke(this);
        if(CurrentHP == 0)
        {
            OnDeath?.Invoke(this);
        }
    }

    public void Heal(uint healAmount)
    {
        CurrentHP = (uint)Mathf.Min(MaxHP, CurrentHP+healAmount);
        OnHealed?.Invoke(this);
    }
}
