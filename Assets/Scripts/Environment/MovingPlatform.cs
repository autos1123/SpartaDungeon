using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 일정 구간을 왕복하는 움직이는 플랫폼 스크립트입니다.
/// 플레이어가 위에 올라타면 함께 움직입니다.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // 시작 지점
    public Transform pointB; // 도착 지점
    public float speed = 2f; // 이동 속도

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
        // 이전 위치 저장
        previousPosition = transform.position;

        // 이동
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        transform.position = nextPosition;

        // 플레이어들 함께 이동
        Vector3 movement = transform.position - previousPosition;
        foreach (Transform player in playersOnPlatform)
        {
            if (player != null)
            {
                player.position += movement;
            }
        }

        // 목표 지점에 도달하면 방향 전환
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