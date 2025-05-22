using UnityEngine;
using UnityEngine.InputSystem;


// 플레이어의 이동 및 점프, 그리고 카메라 방향에 따른 회전을 처리하는 컨트롤러입니다.

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBarUI healthUI; // 인스펙터에서 연결
    public bool invincible = false;
    [Header("Stamina or UI")]
    public PlayerStamina stamina;              // 스태미나 컴포넌트 참조
    public InteractableInfoUI infoUI;          // 설명 표시 UI (텍스트 메시지용)

    private PlayerControls inputActions;       // Input System에서 자동 생성된 입력 클래스
    private Rigidbody rb;                      // Rigidbody 컴포넌트 참조

    [Header("Movement Settings")]
    public float moveSpeed = 5f;               // 이동 속도
    public float jumpForce = 7f;               // 점프 힘

    [Header("Sprint Settings")]
    public float sprintSpeed = 9f;            // 대시 시 이동 속도
    public float staminaDrainRate = 10f;      // 초당 스태미나 소모량
    private bool isSprinting = false;         // 현재 대시 중 여부

    private Vector2 moveInput;                 // Input System으로부터 받은 이동 입력 (Vector2)

    [Header("Camera")]
    public Transform cameraTransform;          // 카메라 Transform (Cinemachine FreeLook Camera)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Rigidbody 가져오기
        inputActions = new PlayerControls();   // Input Actions 클래스 초기화
    }
    private void Start()
    {
        // 게임 시작 시 체력을 최대치로 초기화
        currentHealth = maxHealth;

        // UI에 초기 체력 상태 반영
        healthUI.SetHealth(currentHealth, maxHealth);
    }
    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(10f); // H 키 누르면 체력 10 깎임
        }
        // 스페이스바 입력 시
        // 점프 입력 처리 → 스태미나 포함 조건 체크
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[PlayerController] 스페이스바 입력 감지됨");

            if (IsGrounded())
            {
                if (stamina.ConsumeStamina(15f))
                {
                    Jump(); // ← 여기서만 점프 실행
                }
                else
                {
                    infoUI?.ShowInfo("스태미나 부족", "점프할 수 없습니다.", 1.5f);
                }
            }
        }
        // Shift 누르면 대시 시작
        if (Keyboard.current.leftShiftKey.isPressed)
            isSprinting = true;
        else
            isSprinting = false;
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();          // 입력 활성화

        // 이동 입력 설정
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();         // 입력 비활성화
    }
    private void FixedUpdate()
    {
        // 카메라 방향 기준 방향 계산
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // 입력 방향 계산
        Vector3 moveDir = (camRight * moveInput.x + camForward * moveInput.y).normalized;

        float currentSpeed = moveSpeed;

        // ▶ 스프린트 조건일 때 속도 증가 + 스태미나 소모
        if (isSprinting && stamina.currentStamina > 0f)
        {
            float drain = staminaDrainRate * Time.fixedDeltaTime;
            stamina.ConsumeStamina(drain);
            currentSpeed = sprintSpeed;
        }
        else if (isSprinting && stamina.currentStamina <= 0f)
        {
            // 스태미나 부족 시 스프린트 강제 종료
            infoUI?.ShowInfo("스태미나 부족", "달릴 수 없습니다", 1.5f);
            isSprinting = false;
        }

        // 이동 적용
        Vector3 finalVelocity = moveDir * currentSpeed;
        rb.velocity = new Vector3(finalVelocity.x, rb.velocity.y, finalVelocity.z);

        // 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        // 바닥에 닿았는지 Raycast로 체크
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    public void TakeDamage(float damage)
    {
        // 데미지 주는 방식
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
    void Jump()
    {
        // 플레이어 점프 실행 함수
        Debug.Log("[PlayerController] Jump() 실행됨");
        // 기존 y속도 제거 후 점프력 적용
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
}
