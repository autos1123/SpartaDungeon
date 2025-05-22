using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾��� �̵��� ������ ó���ϴ� ��Ʈ�ѷ� ��ũ��Ʈ�Դϴ�.
/// Input System�� ����Ͽ� �Է��� �ް�, Rigidbody�� �̿��� ���� ��� �̵��� �����մϴ�.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerControls inputActions; // InputActions Ŭ���� �ν��Ͻ�
    private Rigidbody rb; // Rigidbody ������Ʈ ����

    [Header("Movement Settings")]
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 7f; // ���� ��

    private Vector2 moveInput; // 2D �Է°� (WASD ��)
    private bool isJumpPressed; // ���� �Է� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
        inputActions = new PlayerControls(); // Input Actions �ν��Ͻ� ����
    }

    private void OnEnable()
    {
        // �Է� �ý��� Ȱ��ȭ
        inputActions.Player.Enable();

        // �̵� �Է� ó��
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // ���� �Է� ó��
        inputActions.Player.Jump.performed += _ => isJumpPressed = true;
    }

    private void OnDisable()
    {
        // �Է� �ý��� ��Ȱ��ȭ
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // 2D �Է��� 3D �̵� �������� ��ȯ
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;

        // Y �ӵ��� �����ϰ� XZ ���⸸ ����
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // ���� ó��
        if (isJumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        isJumpPressed = false; // ������ 1ȸ�� �Է��̹Ƿ� �ʱ�ȭ
    }

    /// <summary>
    /// �ٴڿ� ��� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <returns>Raycast�� ���� Ground ���� ��ȯ</returns>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
