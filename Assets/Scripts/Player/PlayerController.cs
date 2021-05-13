using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private InputHandler input;
    private CharacterController controller;
    public Stats stats;

    public int maxHealth = 100;
    public int Currenthealth;

    public bool takingDamage = false;

    public bool isHurting = false;
    public float hurtTimer = 0;

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
        Currenthealth = maxHealth;
        stats.SetMaxHealth(maxHealth);
        stats.PauseMenuUI.SetActive(false);
    }


    void Update()
    {
        if (input.pausePressed) {

            if (!stats.isPaused)
            {
                stats.Pause();
            } else
            {
                stats.Resume();
            }

            input.pausePressed = false;
        }
        else
        { 

            if (Currenthealth >= 0)
            {
                HandleMovement(Time.deltaTime);
                HandleGravity(Time.deltaTime);
                HandleJump();
            }

            if (input.damageDown && !takingDamage)
            {
                takingDamage = true;
                TakeDamage(21);
            
            } else
            {
                takingDamage = false;
            }

            if(isHurting && hurtTimer <= 3)
            {
                hurtTimer += Time.deltaTime;
            } else
            {
                isHurting = false;
                hurtTimer = 0;
            }
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("hurt") && !isHurting)
        {
            isHurting = true;
            TakeDamage(21);
        }
    }


    void TakeDamage(int damage)
    {
        Currenthealth -= damage;

        stats.SetHealth(Currenthealth);
    }

    private void HandleMovement(float delta)
    {
        Vector3 movement = (transform.right * input.move.x) + (transform.forward * input.move.y);

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
