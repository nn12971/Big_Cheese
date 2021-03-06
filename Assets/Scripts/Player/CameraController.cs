using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputHandler input;

    [SerializeField]
    private Transform playerParent;

    [SerializeField]
    private float hSens = 100f;

    [SerializeField]
    private float vSens = 100f;

    [SerializeField]
    private float sens = 100f;

    private float xRot = 0f;


    void Start()
    {
        input = InputHandler.instance;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        HandleLook(Time.deltaTime);

    }

    private void HandleLook(float delta)
    {
        float mouseX;
        float mouseY;

        if (hSens == vSens)
        {
            mouseX = input.look.x * sens * delta;
            mouseY = input.look.y * sens * delta;
        }
        else
        {
            mouseX = input.look.x * hSens * delta;
            mouseY = input.look.y * vSens * delta;
        }
        //hendle tilting
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        //handle turning
        playerParent.Rotate(Vector3.up, mouseX);
    }
}
