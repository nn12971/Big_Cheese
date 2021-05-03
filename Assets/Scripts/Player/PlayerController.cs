using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    private CharacterController controller;

    private Animator anim;

    #region Gravity Variables

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private bool grounded;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private float jumpHeight = 3f;

    #endregion

    void Start()
    {
        input = InputHandler.instance;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    
    void FixedUpdate()
    {
        HandleMovement(Time.deltaTime);
        HandleGravity(Time.deltaTime);
        HandleJump();
    }

    private void HandleMovement(float delta)
    {
        Vector3 movement = (transform.right * input.move.x) + (transform.forward * input.move.y);

        anim.SetFloat("horizontal", movement.x);
        anim.SetFloat("vertical", Mathf.Abs(movement.z));

        controller.Move(movement * 5 * delta);
    }

    private void HandleGravity(float delta)
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * delta;
        controller.Move(velocity * delta);
    }

    private void HandleJump()
    {
        if(input.jumpDown && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

}
