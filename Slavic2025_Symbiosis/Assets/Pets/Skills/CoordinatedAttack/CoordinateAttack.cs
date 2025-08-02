using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class CoordinateAttack : PetSkill
{
    [SerializeField] private GameObject hurtbox;
    [SerializeField] private float attackVelocity;
    [SerializeField] private float attackDuration;
    [SerializeField] private Sic sicAttack;
    [Header("UI")]
    [SerializeField] private LineRenderer path;
    [SerializeField] private Transform indicator;

    private bool displayOn;
    private enum CAState { Off, Waiting, Attacking }
    private CAState state;
    private float attackTimer;

    public override void UsePetSkill()
    {
        sicAttack.StopSkill();
        _userPet.State = PetState.Special;
        state = CAState.Waiting;
        _userPet.Rigidbody.useGravity = false;
    }
    public override void DisplayUI(bool On)
    {
        displayOn = On;
        path.gameObject.SetActive(On);
        indicator.gameObject.SetActive(On);
    }

    public override void UpdateUI(float deltaTime)
    {
        if (displayOn)
        {
            path.SetPosition(1, indicator.position - transform.position);
        }
    }

    public void TryCoordinatedAttack()
    {
        if (state == CAState.Off) return;
        if (state == CAState.Attacking) return;
        Vector3 dashDir = _playerManager.AttackManager.hitBox.transform.position - transform.position;
        state = CAState.Attacking;
        _userPet.SuppressMovement(true);
        dashDir = new Vector3(dashDir.x, 0, dashDir.z);
        _userPet.Rigidbody.velocity = dashDir.normalized * attackVelocity;
        attackTimer = 0;
    }

    public override void UpdateSkill(float deltaTime)
    {
        if (state == CAState.Off) return;
        if (state == CAState.Attacking)
        {
            attackTimer += deltaTime;
            if (attackTimer >= attackDuration) EndAttack();
            return;
        }
        _userPet.Rigidbody.MovePosition(indicator.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != CAState.Attacking) return;
        EndAttack();
    }

    public void EndAttack()
    {
        state = CAState.Waiting;
        _userPet.SuppressMovement(false);
        _userPet.Rigidbody.velocity = Vector3.zero;
        hurtbox.SetActive(false);
    }

    public void StopSkill()
    {
        state = CAState.Off;
        _userPet.Rigidbody.useGravity = true;
    }
}
