using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    private InputHandler input;
    private PlayerController cont;

    [SerializeField]
    private Transform playerParent;

    [SerializeField]
    private float hSens = 100f;

    [SerializeField]
    private float vSens = 100f;

    [SerializeField]
    private float sens = 100f;


    private float xRot = 0f;

    public Slider sensitivity;
    public Slider vsensitivity;
    public Slider hsensitivity;

    public Text SensTxt;
    public Text vSensTxt;
    public Text hSensTxt;

    void Start()
    {
        input = InputHandler.instance;
        Cursor.lockState = CursorLockMode.Locked;

        hSens = sensitivity.value;
        vSens = sensitivity.value;
        sens = sensitivity.value;
    }


    void Update()
    {
        HandleLook(Time.deltaTime);
    }

    private void HandleLook(float delta)
    {
        float mouseX;
        float mouseY;


        if (hsensitivity.value == 500 && vsensitivity.value == 500)
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


    public void UpdateSensitivity()
    {
        hSens = hsensitivity.value;
        vSens = vsensitivity.value;
        sens = sensitivity.value;
        SensTxt.text = "" + sensitivity.value;
        vSensTxt.text = "" + vsensitivity.value;
        hSensTxt.text = "" + hsensitivity.value;

    }
}
