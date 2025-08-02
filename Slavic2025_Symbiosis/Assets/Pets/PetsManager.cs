using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsManager : MonoBehaviour
{
    public PlayerManager PlayerManager;
    [SerializeField] private PetManager _pet1;
    [SerializeField] private PetManager _pet2;
    [SerializeField] private PetManager _pet3;

    private void Start()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
        _pet1.Initialize(this);
        _pet2.Initialize(this);
        _pet3.Initialize(this);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        _pet1.UpdatePet(deltaTime);
        _pet2.UpdatePet(deltaTime);
        _pet3.UpdatePet(deltaTime);

    }

    private void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        _pet1.FixedUpdatePet(deltaTime);
        _pet2.FixedUpdatePet(deltaTime);
        _pet3.FixedUpdatePet(deltaTime);
    }

    public void DisplaySkillsUI(uint petID)
    {
        _pet1.DisplaySkillUI(petID == 1);
        _pet2.DisplaySkillUI(petID == 2);
        _pet3.DisplaySkillUI(petID == 3);
    }

    public void UseSkill(uint petID, uint skillID)
    {
        switch(petID)
        {
            case 1:
                _pet1.UseSkill(skillID == 1);
                break;
            case 2:
                _pet2.UseSkill(skillID == 1);
                break;
            case 3:
                _pet3.UseSkill(skillID == 1);
                break;
        }
    }
}
