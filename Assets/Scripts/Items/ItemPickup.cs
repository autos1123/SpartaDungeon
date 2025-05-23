using UnityEngine;

/// <summary>
/// 아이템 오브젝트에 부착. 플레이어가 E키로 상호작용하면 아이템 획득 처리됩니다.
/// </summary>
[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public InteractableInfoUI infoUI;

    public void Interact(GameObject interactor)
    {
        if (!interactor.CompareTag("Player")) return;

        ItemUser user = interactor.GetComponent<ItemUser>();
        if (user != null && itemData != null)
        {
            user.UseItem(itemData);
            if (infoUI != null)
            {
                infoUI.ShowInfo(itemData.itemName, itemData.pickupMessage, 2f);
            }
            Destroy(gameObject);
        }
    }
}
