using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetSkill : MonoBehaviour
{
    [SerializeField] protected PlayerManager _playerManager;
    [SerializeField] protected PetManager _userPet;
    public virtual void InitializeSkill(PetManager userPet)
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _userPet = userPet;
    }
    public virtual void UsePetSkill() { }
    public virtual void UpdateSkill(float deltaTime) { }
    public virtual void FixedUpdateSkill(float deltaTime) { }
    public virtual void UpdateUI(float deltaTime) { }
    public virtual void DisplayUI(bool On) { }
}
