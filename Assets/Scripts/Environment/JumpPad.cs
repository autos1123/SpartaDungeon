using UnityEngine;

/// <summary>
/// �÷��̾ �����뿡 ������ ���� Ƣ������� �ϴ� ��ũ��Ʈ.
/// ���� �ð� ��Ÿ���� �־� ���� ��� ����.
/// </summary>
[RequireComponent(typeof(Collider))]
public class JumpPad : MonoBehaviour
{
    [Tooltip("������ ���� ��")]
    public float jumpForce = 15f;

    [Tooltip("�� �� ���� �� �ٽ� �۵� ������ ��Ÿ�� (��)")]
    public float cooldownTime = 3f;

    // ���������� �۵��� �ð� ����
    private float lastActivatedTime = -999f;

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹�ߴ��� Ȯ��
        if (!collision.gameObject.CompareTag("Player"))
            return;

        // ��Ÿ���� ���� �� �������� ����
        if (Time.time - lastActivatedTime < cooldownTime)
            return;

        // �÷��̾��� Rigidbody ��������
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �������� ���� ���������� ���� (Impulse)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // ������ �۵� �ð� ���
            lastActivatedTime = Time.time;
        }
    }
}
