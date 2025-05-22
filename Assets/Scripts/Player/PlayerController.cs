using UnityEngine;
using UnityEngine.InputSystem;


// �÷��̾��� �̵� �� ����, �׸��� ī�޶� ���⿡ ���� ȸ���� ó���ϴ� ��Ʈ�ѷ��Դϴ�.

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBarUI healthUI; // �ν����Ϳ��� ����
    public bool invincible = false;
    [Header("Stamina or UI")]
    public PlayerStamina stamina;              // ���¹̳� ������Ʈ ����
    public InteractableInfoUI infoUI;          // ���� ǥ�� UI (�ؽ�Ʈ �޽�����)

    private PlayerControls inputActions;       // Input System���� �ڵ� ������ �Է� Ŭ����
    private Rigidbody rb;                      // Rigidbody ������Ʈ ����

    [Header("Movement Settings")]
    public float moveSpeed = 5f;               // �̵� �ӵ�
    public float jumpForce = 7f;               // ���� ��

    [Header("Sprint Settings")]
    public float sprintSpeed = 9f;            // ��� �� �̵� �ӵ�
    public float staminaDrainRate = 10f;      // �ʴ� ���¹̳� �Ҹ�
    private bool isSprinting = false;         // ���� ��� �� ����

    private Vector2 moveInput;                 // Input System���κ��� ���� �̵� �Է� (Vector2)

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
        // �����̽��� �Է� ��
        // ���� �Է� ó�� �� ���¹̳� ���� ���� üũ
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[PlayerController] �����̽��� �Է� ������");

            if (IsGrounded())
            {
                if (stamina.ConsumeStamina(15f))
                {
                    Jump(); // �� ���⼭�� ���� ����
                }
                else
                {
                    infoUI?.ShowInfo("���¹̳� ����", "������ �� �����ϴ�.", 1.5f);
                }
            }
        }
        // Shift ������ ��� ����
        if (Keyboard.current.leftShiftKey.isPressed)
            isSprinting = true;
        else
            isSprinting = false;
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();          // �Է� Ȱ��ȭ

        // �̵� �Է� ����
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();         // �Է� ��Ȱ��ȭ
    }
    private void FixedUpdate()
    {
        // ī�޶� ���� ���� ���� ���
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // �Է� ���� ���
        Vector3 moveDir = (camRight * moveInput.x + camForward * moveInput.y).normalized;

        float currentSpeed = moveSpeed;

        // �� ������Ʈ ������ �� �ӵ� ���� + ���¹̳� �Ҹ�
        if (isSprinting && stamina.currentStamina > 0f)
        {
            float drain = staminaDrainRate * Time.fixedDeltaTime;
            stamina.ConsumeStamina(drain);
            currentSpeed = sprintSpeed;
        }
        else if (isSprinting && stamina.currentStamina <= 0f)
        {
            // ���¹̳� ���� �� ������Ʈ ���� ����
            infoUI?.ShowInfo("���¹̳� ����", "�޸� �� �����ϴ�", 1.5f);
            isSprinting = false;
        }

        // �̵� ����
        Vector3 finalVelocity = moveDir * currentSpeed;
        rb.velocity = new Vector3(finalVelocity.x, rb.velocity.y, finalVelocity.z);

        // ȸ��
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        // �ٴڿ� ��Ҵ��� Raycast�� üũ
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
    public void TakeDamage(float damage)
    {
        // ������ �ִ� ���
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthUI.SetHealth(currentHealth, maxHealth);
    }
    void Jump()
    {
        // �÷��̾� ���� ���� �Լ�
        Debug.Log("[PlayerController] Jump() �����");
        // ���� y�ӵ� ���� �� ������ ����
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
