using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ���� ������ �պ��ϴ� �����̴� �÷��� ��ũ��Ʈ�Դϴ�.
/// �÷��̾ ���� �ö�Ÿ�� �Բ� �����Դϴ�.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // ���� ����
    public Transform pointB; // ���� ����
    public float speed = 2f; // �̵� �ӵ�

    private Vector3 target;
    private Vector3 previousPosition;
    private List<Transform> playersOnPlatform = new List<Transform>();

    private void Start()
    {
        transform.position = pointA.position;
        target = pointB.position;
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // ���� ��ġ ����
        previousPosition = transform.position;

        // �̵�
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        transform.position = nextPosition;

        // �÷��̾�� �Բ� �̵�
        Vector3 movement = transform.position - previousPosition;
        foreach (Transform player in playersOnPlatform)
        {
            if (player != null)
            {
                player.position += movement;
            }
        }

        // ��ǥ ������ �����ϸ� ���� ��ȯ
        if (Vector3.Distance(transform.position, pointA.position) <= 0.01f)
        {
            target = pointB.position;
        }
        else if (Vector3.Distance(transform.position, pointB.position) <= 0.01f)
        {
            target = pointA.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playersOnPlatform.Contains(other.transform))
            {
                playersOnPlatform.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnPlatform.Remove(other.transform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playersOnPlatform.Contains(collision.transform))
            {
                playersOnPlatform.Add(collision.transform);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersOnPlatform.Remove(collision.transform);
        }
    }
}