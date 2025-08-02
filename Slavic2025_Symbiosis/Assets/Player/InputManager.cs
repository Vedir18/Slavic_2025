using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask raycastablePlaneMask;
    public Vector2Int MovementInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    private uint SkillInput;
    private bool attack;

    public void UpdateInput()
    {
        MovementInput = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        MouseWorldPosition = GetMouseWorldPosition();
        if (SkillInput == 0) SkillInput = SetSkillInput();
        attack |= Input.GetKeyDown(KeyCode.Mouse0);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, raycastablePlaneMask))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }

    private uint SetSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return 3;
        return 0;
    }

    public uint GetSkillInput()
    {
        uint cache = SkillInput;
        SkillInput = 0;
        return cache;
    }

    public bool GetAttackDown()
    {
        if (attack)
        {
            attack = false;
            return true;
        }
        else return false;
    }
}