using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PetSkill
{
    [SerializeField] private float dashVelocity;
    [SerializeField] private LineRenderer path;
    private Vector3 dashVector;
    private float dashTimer;
    private bool _displayOn;
    public override void DisplayUI(bool On)
    {
        path.gameObject.SetActive(On);
        _displayOn = On;
    }

    public override void UpdateUI(float deltaTime)
    {
        transform.position = _playerManager.transform.position;
        if (_displayOn)
        {
            dashVector = _userPet.Rigidbody.position - _playerManager.transform.position;
            dashVector = new Vector3(dashVector.x, 0, dashVector.z);

            path.SetPosition(0, Vector3.zero);
            path.SetPosition(1, dashVector);
        }
    }

    public override void UsePetSkill()
    {
        _userPet.State = PetState.DuringSkill;
        _userPet.SuppressMovement(true);
        _userPet.Rigidbody.velocity = Vector3.zero;
        _userPet.Rigidbody.useGravity = false;
        _playerManager.MovementManager.SuppressMovement(true);
        _playerManager.MovementManager.SetVelocity(dashVector.normalized * dashVelocity);
        _playerManager.Dodging = true;
        dashTimer = 0;
    }

    public override void UpdateSkill(float deltaTime)
    {
        dashTimer += deltaTime;
        if (dashTimer >= dashVector.magnitude / dashVelocity)
        {
            _userPet.State = PetState.Vibing;
            _userPet.SuppressMovement(false);
            _userPet.Rigidbody.useGravity = true;
            _playerManager.MovementManager.SuppressMovement(false);
            _playerManager.MovementManager.SetVelocity(Vector3.zero);
            _playerManager.Dodging = false;
        }
    }
}
