using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sic : PetSkill
{
    [SerializeField] private CoordinateAttack ca;
    [Header("UI")]
    [SerializeField] private LineRenderer enemyLine;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform chargeIndiator;
    [Header("Stats")]
    [SerializeField] private float enemyFindRange;
    [SerializeField] private float sicBonusDuration;
    [SerializeField] private uint sicBonusDamage;
    [Header("Charge")]
    [SerializeField] private float chargeExtendSpeed;
    [SerializeField] private float maxChargeDuration;
    [Header("Attack")]
    [SerializeField] private GameObject hitBox;
    [SerializeField] private float attackVelocity;
    [SerializeField] private uint sicDamage;
    [SerializeField] private float maxAttackDuration;
    [SerializeField] private float attackMissedCooldown;
    [SerializeField] private float attackHitCooldown;

    private enum SicState { Off, Charging, Attacking, Cooldown }
    private SicState state;
    private HealthComponent lastHit;
    private float lastHitTimer;
    private bool displayOn;
    private Enemy target;
    private Vector3 chargeDirection;
    private float chargeTimer;
    private float attackTimer;
    private float cooldownTimer;

    public override void UsePetSkill()
    {
        ca.StopSkill();
        _userPet.State = PetState.Special;
        state = SicState.Charging;
        target = FindTarget();
        chargeTimer = 0;
        attackTimer = 0;
        cooldownTimer = 0;
    }

    public override void DisplayUI(bool On)
    {
        displayOn = On;
        enemyLine.gameObject.SetActive(On);
    }

    public override void UpdateUI(float deltaTime)
    {
        if (displayOn)
        {
            chargeIndiator.gameObject.SetActive(false);
            Enemy enemy = FindTarget();
            if (enemy != null)
            {
                enemyLine.gameObject.SetActive(true);
                enemyLine.SetPosition(1, enemy.transform.position - transform.position);
            }
            else
            {
                enemyLine.gameObject.SetActive(false);
            }
        }
        else
        {
            switch (state)
            {
                case SicState.Off:
                case SicState.Cooldown:
                    chargeIndiator.gameObject.SetActive(false);
                    return;
                case SicState.Charging:
                    Debug.DrawLine(transform.position, transform.position + chargeDirection, Color.red);
                    chargeIndiator.gameObject.SetActive(true);
                    chargeIndiator.localScale = new Vector3(1, 1, chargeDirection.magnitude);
                    chargeIndiator.rotation = Quaternion.LookRotation(chargeDirection.normalized, Vector3.up);
                    break;
                case SicState.Attacking:
                    chargeIndiator.gameObject.SetActive(true);
                    chargeIndiator.localScale = new Vector3(1, 1, chargeDirection.magnitude - attackTimer * attackVelocity);
                    break;

            }
        }
    }

    public override void UpdateSkill(float deltaTime)
    {
        if (lastHit != null)
        {
            lastHitTimer -= deltaTime;
            if (lastHitTimer <= 0) lastHit = null;
        }

        switch (state)
        {
            case SicState.Off:
                return;
            case SicState.Charging:
                UpdateCharging(deltaTime);
                break;
            case SicState.Attacking:
                UpdateAttacking(deltaTime);
                break;
            case SicState.Cooldown:
                UpdateCooldown(deltaTime);
                break;
        }
    }

    private void UpdateCharging(float deltaTime)
    {
        if (target == null)
        {
            _userPet.UseSkill(true);
            state = SicState.Off;
            return;
        }
        chargeTimer += deltaTime;
        Vector3 tempTargetPos = Vector3.MoveTowards(transform.position, target.transform.position, chargeTimer * chargeExtendSpeed);
        Debug.DrawLine(transform.position, tempTargetPos, Color.blue);
        chargeDirection = tempTargetPos - transform.position;
        chargeDirection = new Vector3(chargeDirection.x, 0, chargeDirection.z);
        if (tempTargetPos == target.transform.position || chargeTimer >= maxChargeDuration)
        {
            state = SicState.Attacking;
            attackTimer = 0;
            _userPet.Rigidbody.useGravity = false;
            _userPet.Rigidbody.velocity = chargeDirection.normalized * attackVelocity;
            _userPet.SuppressMovement(true);
            hitBox.SetActive(true);
        }
    }

    private void UpdateAttacking(float deltaTime)
    {
        attackTimer += deltaTime;
        if (attackTimer >= maxAttackDuration)
        {
            EndAttack(false);
            return;
        }
        if (attackTimer * attackVelocity > chargeDirection.magnitude)
        {
            EndAttack(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != SicState.Attacking) return;
        EndAttack(false);
    }

    public void EndAttack(bool hitSomethihng)
    {
        state = SicState.Cooldown;
        cooldownTimer = hitSomethihng ? attackHitCooldown : attackMissedCooldown;
        _userPet.SuppressMovement(false);
        _userPet.Rigidbody.useGravity = true;
        _userPet.Rigidbody.velocity = Vector3.zero;
        hitBox.SetActive(false);
    }

    private void UpdateCooldown(float deltaTime)
    {
        cooldownTimer -= deltaTime;
        if (cooldownTimer <= 0)
        {
            target = FindTarget();
            if (target == null)
            {
                state = SicState.Off;
                _userPet.UseSkill(true);
                return;
            }

            state = SicState.Charging;
            chargeTimer = 0;
        }
    }

    private Enemy FindTarget()
    {
        Vector3 castPosition = _playerManager.InputManager.MouseWorldPosition;
        Collider[] hits = Physics.OverlapSphere(castPosition, enemyFindRange, enemyLayer);
        if (hits.Length > 0)
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Enemy>(out var e))
                {
                    enemies.Add(e);
                }
            }
            if (enemies.Count > 0)
            {
                Enemy res = enemies[0];
                float dist = Vector3.Distance(transform.position, res.transform.position);
                foreach (var enemy in enemies)
                {
                    float enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
                    if (enemyDist < dist)
                    {
                        dist = enemyDist;
                        res = enemy;
                    }
                }
                return res;
            }
        }
        return null;
    }

    public void TrySicDamage(HealthComponent hp)
    {
        if (lastHit == hp)
        {
            hp.Damage(sicBonusDamage);
            lastHit = null;
        }
    }

    public void MarkEnemy(HealthComponent hp)
    {
        lastHitTimer = sicBonusDuration;
        lastHit = hp;
    }

    public void StopSkill()
    {
        state = SicState.Off;
    }
}
