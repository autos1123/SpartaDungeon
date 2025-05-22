using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾��� �̵� �� ����, �׸��� ī�޶� ���⿡ ���� ȸ���� ó���ϴ� ��Ʈ�ѷ��Դϴ�.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBarUI healthUI; // �ν����Ϳ��� ����

    private PlayerControls inputActions;       // Input System���� �ڵ� ������ �Է� Ŭ����
    private Rigidbody rb;                      // Rigidbody ������Ʈ ����

    [Header("Movement Settings")]
    public float moveSpeed = 5f;               // �̵� �ӵ�
    public float jumpForce = 7f;               // ���� ��

    private Vector2 moveInput;                 // Input System���κ��� ���� �̵� �Է� (Vector2)
    private bool isJumpPressed;                // ���� �Է� ����

    [Header("Camera")]
    public Transform cameraTransform;          // ī�޶� Transform (Cinemachine FreeLook Camera)

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Rigidbody ��������
        inputActions = new PlayerControls();   // Input Actions Ŭ���� �ʱ�ȭ
    }
    private void Start()
    {
        // ���� ���� �� ü���� �ִ�ġ�� �ʱ�ȭ
        currentHealth = maxHealth;

        // UI�� �ʱ� ü�� ���� �ݿ�
        healthUI.SetHealth(currentHealth, maxHealth);
    }
    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(10f); // H Ű ������ ü�� 10 ����
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();          // �Է� Ȱ��ȭ

        // �̵� �Է� ����
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // ���� �Է� ����
        inputActions.Player.Jump.performed += _ => isJumpPressed = true;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();         // �Է� ��Ȱ��ȭ
    }

    private void FixedUpdate()
    {
        // ī�޶� ���� �������� �Է� ���� ��ȯ
        Vector3 camForward = cameraTransform.forward; // ī�޶��� ���� ����
        Vector3 camRight = cameraTransform.right;     // ī�޶��� ������ ����

        camForward.y = 0f;        // ���� ���⸸ ��� (���� ����)
        camRight.y = 0f;
        camForward.Normalize();   // ���� ����ȭ
        camRight.Normalize();

        // ���� �̵� ���� ��� (ī�޶� ���� ���⿡ ���� �̵�)
        Vector3 move = (camRight * moveInput.x + camForward * moveInput.y).normalized * moveSpeed;

        // ���� ���� �ӵ��� �����ϰ� XZ ���� �ӵ��� ����
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // �̵� ������ ���� ��� ȸ�� (ī�޶� ���� �������� �ڿ������� ȸ��)
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up); // �ٶ� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime); // �ε巴�� ȸ��
        }

        // ���� ó��
        if (isJumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ���� �� ����
        }

        isJumpPressed = false; // ������ 1ȸ �Է¸� ó��
    }

    /// <summary>
    /// �ٴڿ� ��Ҵ��� Raycast�� üũ
    /// </summary>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    /// <summary>
    /// ������ �ִ� ���
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
}
