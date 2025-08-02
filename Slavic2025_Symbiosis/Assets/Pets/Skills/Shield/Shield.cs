using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PetSkill
{
    [SerializeField] private float cooldown;
    [SerializeField] private float shieldDuration;
    [SerializeField] private LineRenderer path;
    [SerializeField] private GameObject indicator;

    private float shieldTimer;
    private bool _displayOn;

    public override void DisplayUI(bool On)
    {
        path.gameObject.SetActive(On);
        indicator.SetActive(On);
        _displayOn = On;
    }

    public override void UpdateUI(float deltaTime)
    {
        transform.position = _playerManager.transform.position;
        if (_displayOn)
        {
            Vector3 dashVector = _userPet.Rigidbody.position - _playerManager.transform.position;
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
        _userPet.Rigidbody.position = _playerManager.transform.position;
        _userPet.transform.localScale = Vector3.one * 2;
        _playerManager.Dodging = true;
        shieldTimer = 0;
    }

    public override void UpdateSkill(float deltaTime)
    {
        shieldTimer += deltaTime;
        _userPet.Rigidbody.position = _playerManager.transform.position;
        if (shieldTimer >= shieldDuration)
        {
            _userPet.transform.localScale = Vector3.one;
            _userPet.State = PetState.Cooldown;
            _userPet.Cooldown = cooldown;
            _userPet.SuppressMovement(false);
            _userPet.Rigidbody.useGravity = true;
            _userPet.Rigidbody.position = _playerManager.transform.position - Vector3.forward;
            _playerManager.Dodging = false;
        }
    }
}
