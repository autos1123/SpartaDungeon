using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 이동, 점프, 스프린트, 벽타기 및 매달리기 기능을 제어하는 메인 컨트롤러입니다.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;
    public HealthBarUI healthUI;
    public bool invincible = false;

    [Header("Stamina or UI")]
    public PlayerStamina stamina;
    public InteractableInfoUI infoUI;

    private PlayerControls inputActions;
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Sprint Settings")]
    public float sprintSpeed = 9f;
    public float staminaDrainRate = 10f;
    private bool isSprinting = false;

    [Header("Wall Climb Settings")]
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;
    public float climbSpeed = 2f;
    private bool isClimbing = false;
    private bool isTouchingWall = false;

    private Vector2 moveInput;

    [Header("Camera")]
    public Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerControls();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(10f);
        }

        // 점프 입력
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (IsGrounded())
            {
                if (stamina.ConsumeStamina(15f))
                    Jump();
                else
                    infoUI?.ShowInfo("스태미나 부족", "점프할 수 없습니다.", 1.5f);
            }
        }

        // 스프린트 입력 처리
        isSprinting = Keyboard.current.leftShiftKey.isPressed;

        // Ray를 전면, 상단, 하단, 좌우 5방향으로 발사
        isTouchingWall =
            Physics.Raycast(transform.position, transform.forward, wallCheckDistance, wallLayer) ||
            Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, wallCheckDistance, wallLayer) ||
            Physics.Raycast(transform.position + Vector3.down * 0.5f, transform.forward, wallCheckDistance, wallLayer) ||
            Physics.Raycast(transform.position + transform.right * 0.3f, transform.forward, wallCheckDistance, wallLayer) ||
            Physics.Raycast(transform.position - transform.right * 0.3f, transform.forward, wallCheckDistance, wallLayer);

        if (isTouchingWall && !IsGrounded())
        {
            isClimbing = true;
            rb.useGravity = false;
        }
        else
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camRight * moveInput.x + camForward * moveInput.y).normalized;
        float currentSpeed = moveSpeed;

        if (isSprinting && stamina.currentStamina > 0f)
        {
            float drain = staminaDrainRate * Time.fixedDeltaTime;
            stamina.ConsumeStamina(drain);
            currentSpeed = sprintSpeed;
        }
        else if (isSprinting && stamina.currentStamina <= 0f)
        {
            infoUI?.ShowInfo("스태미나 부족", "달릴 수 없습니다", 1.5f);
            isSprinting = false;
        }

        if (isClimbing)
        {
            if (moveInput != Vector2.zero)
            {
                rb.velocity = new Vector3(rb.velocity.x, moveInput.y * climbSpeed, rb.velocity.z);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
            return;
        }

        Vector3 finalVelocity = moveDir * currentSpeed;
        rb.velocity = new Vector3(finalVelocity.x, rb.velocity.y, finalVelocity.z);

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }

    void Jump()
    {
        Debug.Log("[PlayerController] Jump() 실행됨");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
    public void SpeeUp(float boostAmount, float duration)
    {
        StartCoroutine(SpeedUpRoutine(boostAmount, duration));
    }
    private IEnumerator SpeedUpRoutine(float amount, float duration)
    {
        moveSpeed += amount;
        yield return new WaitForSeconds(duration);
        moveSpeed -= amount;
    }
}
