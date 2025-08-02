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
    [Header("Vibing")]
    [SerializeField] private float _petSpeed;
    public PlayerManager PlayerManager => _petsManager.PlayerManager;
    private PetsManager _petsManager;
    private bool _firstSkillActive;
    public Rigidbody Rigidbody;
    private bool _movementSupressed = false;
    public void Initialize(PetsManager petsManager)
    {
        _petsManager = petsManager;
        State = PetState.Vibing;
        Rigidbody = GetComponent<Rigidbody>();
        _petSkill1?.InitializeSkill(this);
        _petSkill2?.InitializeSkill(this);
    }

    public void UpdatePet(float deltaTime)
    {
        _petSkill1?.UpdateUI(deltaTime);
        _petSkill2?.UpdateUI(deltaTime);
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
        if(State == PetState.Dead) return;
        if (State == PetState.DuringSkill) return;

        if (useFirstSkill) _petSkill1?.UsePetSkill();
        else _petSkill2?.UsePetSkill();
        _firstSkillActive = useFirstSkill;
        DisplaySkillUI(false);
    }

    public void DisplaySkillUI(bool On)
    {
        _petSkill1?.DisplayUI(On);
        _petSkill2?.DisplayUI(On);
    }

    public void FixedUpdatePet(float deltaTime)
    {
        if(State == PetState.DuringSkill)
        {
            if (_firstSkillActive) _petSkill1?.FixedUpdateSkill(deltaTime);
            else _petSkill2?.FixedUpdateSkill(deltaTime);
            return;
        }
    }

    public void SuppressMovement(bool value)
    {
        _movementSupressed = value;
    }
}

public enum PetState { Vibing, DuringSkill, Dead }
