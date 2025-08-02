using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : PetSkill
{
    [Header("Stats")]
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _travelSpeed;

    [Header("Display")]
    [SerializeField] private LineRenderer _throwPath;
    [SerializeField] private Transform _landSpot;
    private bool _displayOn = false;
    private Vector3 throwVector;
    private float throwTimer;
    public override void DisplayUI(bool On)
    {
        _throwPath.gameObject.SetActive(On);
        _landSpot.gameObject.SetActive(On);
        _displayOn = On;
    }

    public override void UpdateUI(float deltaTime)
    {
        transform.position = _playerManager.transform.position;
        UpdateThrowVector();
        if (_displayOn)
        {
            MoveLandingSpot();
            DrawThrowPath();
        }
    }

    public override void UsePetSkill()
    {
        _userPet.State = PetState.DuringSkill;
        _userPet.SuppressMovement(true);
        _userPet.Rigidbody.position = _playerManager.transform.position;
        _userPet.Rigidbody.velocity = throwVector.normalized * _travelSpeed;
        _userPet.Rigidbody.useGravity = false;
        throwTimer = 0;
    }

    public override void FixedUpdateSkill(float deltaTime)
    {
        throwTimer += deltaTime;
        if (throwTimer > throwVector.magnitude / _travelSpeed) EndThrow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EndThrow();
    }

    private void EndThrow()
    {
        _userPet.State = PetState.Vibing;
        _userPet.SuppressMovement(false);
        _userPet.Rigidbody.velocity = Vector3.zero;
        _userPet.Rigidbody.useGravity = true;
    }

    private void MoveLandingSpot()
    {
        
        _landSpot.position = _playerManager.transform.position + throwVector + Vector3.up * .1f;
    }

    private void DrawThrowPath()
    {
        _throwPath.SetPosition(0, Vector3.zero);
        _throwPath.SetPosition(1, throwVector / 4 + Vector3.up * .5f);
        _throwPath.SetPosition(2, throwVector / 2 + Vector3.up * 1f);
        _throwPath.SetPosition(3, throwVector / 4 * 3 + Vector3.up * .5f);
        _throwPath.SetPosition(4, throwVector);
    }

    private void UpdateThrowVector()
    {
        throwVector = _playerManager.InputManager.MouseWorldPosition - _playerManager.transform.position;
        throwVector = new Vector3(throwVector.x, 0, throwVector.z);
        float magnitude = Mathf.Clamp(throwVector.magnitude, _minDistance, _maxDistance);
        throwVector = throwVector.normalized * magnitude;
    }
}
