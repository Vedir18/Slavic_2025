using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public int PetID;
    public PetState State;
    [Header("Skills")]
    [SerializeField] private PetSkill _petSkill1;
    [SerializeField] private PetSkill _petSkill2;
    [Header("Following")]
    [SerializeField] private float _petSpeed;
    [SerializeField] private float _minSwitchTime, _maxSwitchTime;

    private PetsManager _petsManager;
    private bool _firstSkillActive;
    private float _followTimer;
    private Rigidbody _rigidbody;
    private PetPosition _currentPosition;

    public void Initialize(PetsManager petsManager)
    {
        _petsManager = petsManager;
        State = PetState.Following;
        _rigidbody = GetComponent<Rigidbody>();
        ResetFollow();
    }

    public void UpdatePet(float deltaTime)
    {
        if (State == PetState.Dead) return;
        if (State == PetState.DuringSkill)
        {
            if (_firstSkillActive) _petSkill1?.UpdateSkill(deltaTime);
            else _petSkill2?.UpdateSkill(deltaTime);
            return;
        }
    }

    public void UseSkill(bool useFirstSkill)
    {
        if (useFirstSkill) _petSkill1?.UsePetSkill();
        else _petSkill2?.UsePetSkill();
    }

    public void FixedUpdatePet(float deltaTime)
    {
        if (State == PetState.Following)
        {
            _followTimer -= deltaTime;
            if (_followTimer > 0)
            {
                Vector3 followDirection = _currentPosition.transform.position - _rigidbody.position;
                float t = Mathf.InverseLerp(0, _petSpeed, followDirection.magnitude);
                float a = Mathf.Lerp(0, 2, t);
                followDirection.Normalize();
                _rigidbody.velocity = followDirection * _petSpeed * a;
            }
            else ResetFollow();
            return;
        }
        if(State == PetState.DuringSkill)
        {
            if (_firstSkillActive) _petSkill1.FixedUpdateSkill(deltaTime);
            else _petSkill2.FixedUpdateSkill(deltaTime);
            return;
        }
    }

    private void ResetFollow()
    {
        _followTimer = Random.Range(_minSwitchTime, _maxSwitchTime);
        PetPosition newPosition = _petsManager.GetPetPosition(PetID);
        if (newPosition != null) _currentPosition = newPosition;
        else _currentPosition.PetID = PetID;
    }
}

public enum PetState { Following, DuringSkill, Dead }
