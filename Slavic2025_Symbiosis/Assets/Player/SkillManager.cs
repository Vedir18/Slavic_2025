using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public uint SelectedPet { get; private set; }
    private PlayerManager _playerManager;
    private PetsManager _petsManager;

    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
        _petsManager = FindObjectOfType<PetsManager>();
        SelectedPet = 0;
    }

    public void UpdateSkills()
    {
        uint SkillInput = _playerManager.InputManager.GetSkillInput();

        if(SelectedPet == 0)
        {
            SelectedPet = SkillInput;
            _petsManager.DisplaySkillsUI(SelectedPet);
            return;
        }
        
        if(SkillInput ==1 || SkillInput == 2)
        {
            UseSkill(SelectedPet, SkillInput);
            SelectedPet = 0;
            return;
        }

        if(SkillInput == 3)
        {
            SelectedPet = 0;
            _petsManager.DisplaySkillsUI(SelectedPet);
            return;
        }
    }

    private void UseSkill(uint petID, uint skillID)
    {
        _playerManager.PlayerUIManager.HightlightSkill(petID, skillID);
        _petsManager.UseSkill(petID, skillID);
    }
}
