using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PetSkill
{
    [SerializeField] private float cooldown;
    [SerializeField] private LineRenderer line1;
    [SerializeField] private LineRenderer line2;

    private bool _displayOn;

    public override void DisplayUI(bool On)
    {
        line1.gameObject.SetActive(On);
        line2.gameObject.SetActive(On);
        _displayOn = On;
    }

    public override void UpdateUI(float deltaTime)
    {
        if (_displayOn)
        {
            Vector3 healVector1 = _userPet.PetsManager.Pet1.Rigidbody.position - transform.position;
            Vector3 healVector2 = _userPet.PetsManager.Pet2.Rigidbody.position - transform.position;

            line1.SetPosition(1, healVector1);
            line2.SetPosition(1, healVector2);
        }
    }

    public override void UsePetSkill()
    {
        _userPet.State = PetState.Cooldown;
        _userPet.Cooldown = cooldown;
        _userPet.PetsManager.Pet1.HP.Heal(1);
        _userPet.PetsManager.Pet2.HP.Heal(1);
    }
}
