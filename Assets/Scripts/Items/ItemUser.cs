using UnityEngine;

public class ItemUser : MonoBehaviour
{
    public PlayerController player;

    public void UseItem(ItemData item)
    {
        try
        {
            if (item == null)
                throw new System.NullReferenceException("아이템 데이터가 null입니다.");

            switch (item.effectType)
            {
                case ItemEffectType.Heal:
                    player.Heal(item.effectValue);
                    break;

                case ItemEffectType.SpeedUp:
                    player.SpeeUp(item.effectValue, item.duration);
                    break;

                case ItemEffectType.Invincibility:
                    Debug.Log("[ItemUser] 무적 아이템은 아직 구현되지 않았습니다.");
                    break;

                default:
                    throw new System.NotImplementedException($"[{item.effectType}] 타입에 대한 처리가 구현되어 있지 않습니다.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[ItemUser] 아이템 사용 중 오류 발생: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
