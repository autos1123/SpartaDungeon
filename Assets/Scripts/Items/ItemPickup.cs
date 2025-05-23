using UnityEngine;

/// <summary>
/// ������ ������Ʈ�� ����. �÷��̾ EŰ�� ��ȣ�ۿ��ϸ� ������ ȹ�� ó���˴ϴ�.
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
