using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] float _walkSpeed;
    private Rigidbody _rigidbody;
    private PlayerManager _playerManager;
    private Vector2Int _movementInput => _playerManager.InputManager.MovementInput;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerManager = GetComponent<PlayerManager>();
    }

    public void UpdateMovement(float deltaTime)
    {
        Vector2 horizontalVel = new Vector2(_movementInput.x, _movementInput.y).normalized * _walkSpeed;
        _rigidbody.velocity = new Vector3(horizontalVel.x, _rigidbody.velocity.y, horizontalVel.y);
    }
}
