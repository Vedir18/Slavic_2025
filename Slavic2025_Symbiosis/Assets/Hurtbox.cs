using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    public bool hurtsEnemy;
    [SerializeField] private uint damage;
    [SerializeField] private UnityEvent<HealthComponent> OnHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HealthComponent>(out HealthComponent hp))
        {
            if (hurtsEnemy)
            {
                if (hp.TryGetComponent<Enemy>(out var e)) DealDamage(hp);
            }
            else
            {
                if (hp.TryGetComponent<PlayerManager>(out var Pm))
                {
                    if (!Pm.Dodging) DealDamage(hp);
                }
                if (hp.TryGetComponent<PetManager>(out var pm)) DealDamage(hp);
            }
        }
    }

    private void DealDamage(HealthComponent hp)
    {
        hp.Damage(damage);
        OnHit?.Invoke(hp);
    }
}
