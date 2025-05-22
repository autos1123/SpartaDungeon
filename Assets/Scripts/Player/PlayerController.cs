using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 이동 및 점프, 그리고 카메라 방향에 따른 회전을 처리하는 컨트롤러입니다.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBarUI healthUI; // 인스펙터에서 연결

    private PlayerControls inputActions;       // Input System에서 자동 생성된 입력 클래스
    private Rigidbody rb;                      // Rigidbody 컴포넌트 참조

    [Header("Movement Settings")]
    public float moveSpeed = 5f;               // 이동 속도
    public float jumpForce = 7f;               // 점프 힘

    private Vector2 moveInput;                 // Input System으로부터 받은 이동 입력 (Vector2)
    private bool isJumpPressed;                // 점프 입력 여부

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
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();          // 입력 활성화

        // 이동 입력 설정
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // 점프 입력 설정
        inputActions.Player.Jump.performed += _ => isJumpPressed = true;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();         // 입력 비활성화
    }

    private void FixedUpdate()
    {
        // 카메라 방향 기준으로 입력 방향 변환
        Vector3 camForward = cameraTransform.forward; // 카메라의 전방 벡터
        Vector3 camRight = cameraTransform.right;     // 카메라의 오른쪽 벡터

        camForward.y = 0f;        // 수평 방향만 사용 (상하 무시)
        camRight.y = 0f;
        camForward.Normalize();   // 방향 정규화
        camRight.Normalize();

        // 최종 이동 벡터 계산 (카메라 기준 방향에 따라 이동)
        Vector3 move = (camRight * moveInput.x + camForward * moveInput.y).normalized * moveSpeed;

        // 현재 수직 속도는 유지하고 XZ 방향 속도만 변경
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // 이동 방향이 있을 경우 회전 (카메라 기준 방향으로 자연스럽게 회전)
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up); // 바라볼 방향
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime); // 부드럽게 회전
        }

        // 점프 처리
        if (isJumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 점프 힘 적용
        }

        isJumpPressed = false; // 점프는 1회 입력만 처리
    }

    /// <summary>
    /// 바닥에 닿았는지 Raycast로 체크
    /// </summary>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    /// <summary>
    /// 데미지 주는 방식
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
}
