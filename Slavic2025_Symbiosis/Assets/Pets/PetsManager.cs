using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetsManager : MonoBehaviour
{
    [SerializeField] private List<PetPosition> _positions;
    [SerializeField] private PetManager _pet1;
    [SerializeField] private PetManager _pet2;
    [SerializeField] private PetManager _pet3;

    private void Start()
    {
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

    public PetPosition GetPetPosition(int petID)
    {
        List<PetPosition> availablePositions = new List<PetPosition>();
        foreach (var position in _positions)
        {
            if (position.PetID == petID) position.PetID = 0;
            if (position.Available) availablePositions.Add(position);
        }
        if (availablePositions.Count == 0) return null;

        int randomID = Random.Range((int)0, availablePositions.Count);
        PetPosition chosenPosition = availablePositions[randomID];
        chosenPosition.PetID = petID;
        return chosenPosition;
    }
}
