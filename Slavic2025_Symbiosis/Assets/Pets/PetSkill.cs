using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetSkill : MonoBehaviour
{
    public virtual void UsePetSkill() { }
    public virtual void UpdateSkill(float deltaTime) { }
    public virtual void FixedUpdateSkill(float deltaTime) { }
}
