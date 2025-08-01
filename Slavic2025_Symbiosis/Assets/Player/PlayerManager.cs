using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputManager InputManager {  get; private set; }
    public HealthComponent HealthComponent { get; private set; }
    public MovementManager MovementManager { get; private set; }
    public AttackManager AttackManager { get; private set; }
    public SkillManager SkillManager { get; private set; }
    public PlayerUIManager PlayerUIManager { get; private set; }
    public PlayerVisualsManager PlayerVisualsManager { get; private set; }
    public CameraManager CameraManager { get; private set; }

    private void Start()
    {
        InputManager = GetComponent<InputManager>();
        HealthComponent = GetComponent<HealthComponent>();
        MovementManager = GetComponent<MovementManager>();
        AttackManager = GetComponent<AttackManager>();
        SkillManager = GetComponent<SkillManager>();
        PlayerUIManager = GetComponent<PlayerUIManager>();
        PlayerVisualsManager = GetComponent<PlayerVisualsManager>();
        CameraManager = GetComponent<CameraManager>();

        CameraManager.Initialize();
    }

    private void Update()
    {
        InputManager.UpdateInput();
    }

    private void LateUpdate()
    {
        CameraManager.UpdateCamera(Time.deltaTime);
    }
}
