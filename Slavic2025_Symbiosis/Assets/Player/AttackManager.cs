using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    public AttackState State;
    [SerializeField] private float cooldown;
    [SerializeField] private float attackDuration;
    public GameObject hitBox;
    [SerializeField] private CoordinateAttack p2s1;
    [SerializeField] private Sic p2s2;

    private float attackTimer;
    private float cooldownTimer;
    private PlayerManager _playerManager;
    private PetsManager _petsManager;
    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void UpdateAttack(float deltaTime)
    {
        bool attack = _playerManager.InputManager.GetAttackDown();
        switch(State)
        {
            case AttackState.Off:
                if (attack) StartAttack();
                break;
            case AttackState.Active:
                ProcessAttack(deltaTime); 
                break;
            case AttackState.Cooldown:
                ProcessCooldown(deltaTime);
                break;
        }
    }

    private void StartAttack()
    {
        hitBox.SetActive(true);
        attackTimer = 0;
        State = AttackState.Active;
        TryCoordinatedAttack();
    }

    private void ProcessAttack(float deltaTime)
    {
        attackTimer+= deltaTime;
        if (attackTimer > attackDuration) EndAttack();
    }

    private void EndAttack()
    {
        hitBox.SetActive(false);
        cooldownTimer = 0;
        State = AttackState.Cooldown;
    }

    private void ProcessCooldown    (float deltaTime)
    {
        cooldownTimer += deltaTime;
        if (cooldownTimer > cooldown) State = AttackState.Off;
    }

    public void TryCoordinatedAttack()
    {
        p2s1.TryCoordinatedAttack();
        //Debug.Log(p2s1.transform.position);
        //Debug.DrawLine(p2s1.transform.position, p2s1.transform.position + Vector3.up * 2, Color.red, 0.4f);
    }

    public void TrySicDamage(HealthComponent hp)
    {
        p2s2.TrySicDamage(hp);
    }
}

public enum AttackState { Off, Active, Cooldown}
