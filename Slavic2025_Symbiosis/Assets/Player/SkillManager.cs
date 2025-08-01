using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public uint SelectedSkill { get; private set; }
    private PlayerManager _playerManager;


    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
        SelectedSkill = 0;
    }

    public void UpdateSkills()
    {
        uint SkillInput = _playerManager.InputManager.GetSkillInput();

        if(SelectedSkill == 0)
        {
            SelectedSkill = SkillInput;
            return;
        }
        
        if(SkillInput ==1 || SkillInput == 2)
        {
            UseSkill(SelectedSkill, SkillInput);
            SelectedSkill = 0;
            return;
        }

        if(SkillInput == 3)
        {
            SelectedSkill = 0;
            return;
        }
    }

    private void UseSkill(uint petID, uint skillID)
    {
        _playerManager.PlayerUIManager.HightlightSkill(petID, skillID);
    }
}
