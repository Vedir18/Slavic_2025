using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask raycastablePlaneMask;
    public Vector2Int MovementInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    public bool Skill1 { get; private set; }
    public bool Skill2 { get; private set; }
    public bool Skill3 { get; private set; }

    public void UpdateInput()
    {
        MovementInput = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        Skill1 = Input.GetKeyDown(KeyCode.Alpha1);
        Skill2 = Input.GetKeyDown(KeyCode.Alpha2);
        Skill3 = Input.GetKeyDown(KeyCode.Alpha3);
        MouseWorldPosition = GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, raycastablePlaneMask))
        {
            return hit.point;
        }else return Vector3.zero;
    }
}