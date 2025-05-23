using UnityEngine;

/// <summary>
/// 플레이어가 점프대에 닿으면 위로 튀어오르게 하는 스크립트.
/// 일정 시간 쿨타임이 있어 연속 사용 방지.
/// </summary>
[RequireComponent(typeof(Collider))]
public class JumpPad : MonoBehaviour
{
    [Tooltip("적용할 점프 힘")]
    public float jumpForce = 15f;

    [Tooltip("한 번 점프 후 다시 작동 가능한 쿨타임 (초)")]
    public float cooldownTime = 3f;

    // 마지막으로 작동한 시간 저장
    private float lastActivatedTime = -999f;

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌했는지 확인
        if (!collision.gameObject.CompareTag("Player"))
            return;

        // 쿨타임이 아직 안 지났으면 무시
        if (Time.time - lastActivatedTime < cooldownTime)
            return;

        // 플레이어의 Rigidbody 가져오기
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 위쪽으로 힘을 순간적으로 가함 (Impulse)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 마지막 작동 시간 기록
            lastActivatedTime = Time.time;
        }
    }
}
