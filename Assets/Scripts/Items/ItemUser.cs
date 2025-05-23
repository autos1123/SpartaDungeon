using UnityEngine;

public class ItemUser : MonoBehaviour
{
    public PlayerController player;

    public void UseItem(ItemData item)
    {
        try
        {
            if (item == null)
                throw new System.NullReferenceException("������ �����Ͱ� null�Դϴ�.");

            switch (item.effectType)
            {
                case ItemEffectType.Heal:
                    player.Heal(item.effectValue);
                    break;

                case ItemEffectType.SpeedUp:
                    player.SpeeUp(item.effectValue, item.duration);
                    break;

                case ItemEffectType.Invincibility:
                    Debug.Log("[ItemUser] ���� �������� ���� �������� �ʾҽ��ϴ�.");
                    break;

                default:
                    throw new System.NotImplementedException($"[{item.effectType}] Ÿ�Կ� ���� ó���� �����Ǿ� ���� �ʽ��ϴ�.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[ItemUser] ������ ��� �� ���� �߻�: {ex.Message}\n{ex.StackTrace}");
        }
    }
}
