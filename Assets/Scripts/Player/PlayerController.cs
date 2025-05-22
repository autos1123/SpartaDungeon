using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 이동과 점프를 처리하는 컨트롤러 스크립트입니다.
/// Input System을 사용하여 입력을 받고, Rigidbody를 이용해 물리 기반 이동을 구현합니다.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls inputActions; // InputActions 클래스 인스턴스
    private Rigidbody rb; // Rigidbody 컴포넌트 참조

    [Header("Movement Settings")]
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 7f; // 점프 힘

    private Vector2 moveInput; // 2D 입력값 (WASD 등)
    private bool isJumpPressed; // 점프 입력 여부

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
        inputActions = new PlayerControls(); // Input Actions 인스턴스 생성
    }

    private void OnEnable()
    {
        // 입력 시스템 활성화
        inputActions.Player.Enable();

        // 이동 입력 처리
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // 점프 입력 처리
        inputActions.Player.Jump.performed += _ => isJumpPressed = true;
    }

    private void OnDisable()
    {
        // 입력 시스템 비활성화
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // 2D 입력을 3D 이동 방향으로 변환
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;

        // Y 속도는 유지하고 XZ 방향만 변경
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // 점프 처리
        if (isJumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        isJumpPressed = false; // 점프는 1회성 입력이므로 초기화
    }

    /// <summary>
    /// 바닥에 닿아 있는지 확인하는 함수
    /// </summary>
    /// <returns>Raycast를 통해 Ground 여부 반환</returns>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
