using UnityEngine;

/// <summary>
/// 아이템 오브젝트에 부착
/// - 플레이어가 충돌하면 아이템 효과 실행 + 설명 표시 + 오브젝트 제거
/// </summary>
[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // 연결할 ScriptableObject
    public InteractableInfoUI infoUI; // UI에 설명 표시용

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemUser user = other.GetComponent<ItemUser>();
            if (user != null && itemData != null)
            {
                user.UseItem(itemData); // 아이템 효과 발동

                if (infoUI != null)
                {
                    // 설명창에 이름 + 설명 표시 (2초간)
                    infoUI.ShowInfo(itemData.itemName, itemData.description, 2f);
                }

                Destroy(gameObject); // 아이템 오브젝트 제거
            }
        }
    }
}
