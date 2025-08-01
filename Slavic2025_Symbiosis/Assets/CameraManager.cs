using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _target;
    [SerializeField] private float _minFollowSpeed, _maxFollowSpeed;
    [SerializeField] private float _maxFollowSpeedDistance;

    private Vector3 _followOffset;
    public void Initialize()
    {
        _followOffset = _cameraTransform.position - _target.position;
    }

    public void UpdateCamera(float deltaTime)
    {
        float targetDistance = Vector3.Distance(_target.position + _followOffset, _cameraTransform.position);
        float speedScale = Mathf.InverseLerp(0, _maxFollowSpeedDistance, targetDistance);
        float followSpeed = Mathf.Lerp(_minFollowSpeed, _maxFollowSpeed, speedScale);
        _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, _target.position + _followOffset, followSpeed * deltaTime);
    }
}
