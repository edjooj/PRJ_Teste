using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 6f;
    public float runSpeed = 12f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private float verticalVelocity;

    private Vector2 movementInput;
    public bool isJumping;
    [SerializeField] private bool isRunning;
    public bool isPicking;

    public bool canMove = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (isJumping || !canMove) { return; }
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 gravityMovement = new Vector3(0, verticalVelocity, 0);

        float currentSpeed = isRunning ? runSpeed : speed; // Usar a velocidade de corrida se estiver correndo

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move((moveDir.normalized * currentSpeed + gravityMovement) * Time.deltaTime);
        }
        else
        {
            controller.Move(gravityMovement * Time.deltaTime);
        }

        // Atualizar os par�metros do Animator
        bool isMoving = direction.magnitude >= 0.1f;
        animator.SetBool("isWalking", !isRunning && !isJumping && !isPicking && direction.magnitude >= 0.1f);
        animator.SetBool("isRunning", isRunning && !isJumping && !isPicking && direction.magnitude >= 0.1f);

        if (isJumping || isPicking)
        {
            isRunning = false;
        }
    }

    public void Jump()
    {
        if (controller.isGrounded && isJumping || !canMove)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.ResetTrigger("Jumping");
            isJumping = false;
        }

        if (!controller.isGrounded && verticalVelocity < 0)
        {
            isJumping = false;
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && controller.isGrounded || !canMove)
        {
            isJumping = true;
            animator.SetTrigger("Jumping");
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!isJumping && !isPicking)
        {
            isRunning = context.ReadValueAsButton();
        }
        else
        {
            isRunning = false;
        }
    }

    public void Pick()
    {
        if (controller.isGrounded || !canMove)
            isPicking = true;
        animator.SetTrigger("Pick");
    }
}