using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public static InputHandler instance;

    private Controls controls;

    public Vector2 move;
    public Vector2 look;

    public bool jumpDown = false;
    public bool fireDown = false;
    public bool aimDown = false;
    public bool damageDown = false;
    public bool pausePressed = false;
    public bool advSens = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Locomotion.Move.performed += controls => move = controls.ReadValue<Vector2>();
        controls.Locomotion.Look.performed += controls => look = controls.ReadValue<Vector2>();

        controls.Locomotion.Jump.performed += controls => jumpDown = true;
        controls.Locomotion.Jump.canceled += controls => jumpDown = false;

        controls.Locomotion.Fire.performed += controls => fireDown = true;
        controls.Locomotion.Fire.canceled += controls => fireDown = false;

        controls.Locomotion.Aim.performed += controls => aimDown = true;
        controls.Locomotion.Aim.canceled += controls => aimDown = false;

        controls.Locomotion.Damage.performed += controls =>
        {
            damageDown = true;

        };

        controls.Locomotion.Damage.canceled += controls => {
            damageDown = false;

        };

        controls.Locomotion.Pause.performed += controls =>
        {
            pausePressed = true;
        };

        controls.Locomotion.Pause.canceled += controls =>
        {
            pausePressed = false;
        };
    }
}
