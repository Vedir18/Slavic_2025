using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsManager : MonoBehaviour
{
    public PlayerManager PlayerManager;
    public PetManager Pet1;
    public PetManager Pet2;
    public PetManager Pet3;

    private void Start()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
        Pet1.Initialize(this);
        Pet2.Initialize(this);
        Pet3.Initialize(this);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        Pet1.UpdatePet(deltaTime);
        Pet2.UpdatePet(deltaTime);
        Pet3.UpdatePet(deltaTime);

    }

    private void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        Pet1.FixedUpdatePet(deltaTime);
        Pet2.FixedUpdatePet(deltaTime);
        Pet3.FixedUpdatePet(deltaTime);
    }

    public void DisplaySkillsUI(uint petID)
    {
        Pet1.DisplaySkillUI(petID == 1);
        Pet2.DisplaySkillUI(petID == 2);
        Pet3.DisplaySkillUI(petID == 3);
    }

    public void UseSkill(uint petID, uint skillID)
    {
        switch(petID)
        {
            case 1:
                Pet1.UseSkill(skillID == 1);
                break;
            case 2:
                Pet2.UseSkill(skillID == 1);
                break;
            case 3:
                Pet3.UseSkill(skillID == 1);
                break;
        }
    }
}
