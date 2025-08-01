using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPosition : MonoBehaviour
{
    private int _obstructors;
    [SerializeField] private LayerMask _groundLayer;
    private bool obstructed => _obstructors > 0;
    private bool grounded = true;
    public int PetID;
    public bool Available => grounded && !obstructed && PetID == 0;

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1f, _groundLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        _obstructors++;   
    }

    private void OnTriggerExit(Collider other)
    {
        _obstructors--;
    }

    private void OnDrawGizmos()
    {
        if (!obstructed && grounded) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .55f);
    }
}
