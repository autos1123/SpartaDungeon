using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어가 아이템 효과를 실행하는 스크립트
/// </summary>
public class ItemUser : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    public void UseItem(ItemData item)
    {
        switch (item.effectType)
        {
            case ItemEffectType.Heal:
                player.Heal(item.effectValue);
                break;
            case ItemEffectType.SpeedUp:
                StartCoroutine(SpeedUp(item.effectValue, item.duration));
                break;
        }
    }

    private IEnumerator SpeedUp(float multiplier, float duration)
    {
        player.moveSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        player.moveSpeed /= multiplier;
    }
}
