using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{
    [SerializeField] private Transform _modelTransform;
    private PlayerManager _playerManager;
    private Vector3 mouseWorldPosition => _playerManager.InputManager.MouseWorldPosition;

    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void UpdateVisuals(float deltaTime)
    {
        Vector3 newModelForward = mouseWorldPosition + Vector3.up * _modelTransform.position.y - _modelTransform.position;
        newModelForward.Normalize();
        _modelTransform.rotation = Quaternion.LookRotation(newModelForward, Vector3.up);
    }
}
