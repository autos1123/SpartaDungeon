using UnityEngine;

/// <summary>
/// ������ ������Ʈ�� ����
/// - �÷��̾ �浹�ϸ� ������ ȿ�� ���� + ���� ǥ�� + ������Ʈ ����
/// </summary>
[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // ������ ScriptableObject
    public InteractableInfoUI infoUI; // UI�� ���� ǥ�ÿ�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemUser user = other.GetComponent<ItemUser>();
            if (user != null && itemData != null)
            {
                user.UseItem(itemData); // ������ ȿ�� �ߵ�

                if (infoUI != null)
                {
                    // ����â�� �̸� + ���� ǥ�� (2�ʰ�)
                    infoUI.ShowInfo(itemData.itemName, itemData.description, 2f);
                }

                Destroy(gameObject); // ������ ������Ʈ ����
            }
        }
    }
}
