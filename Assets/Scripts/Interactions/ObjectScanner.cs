using UnityEngine;

public class ObjectScanner : MonoBehaviour
{
    public float scanRange = 5f;
    public LayerMask scanMask;
    public Camera playerCamera;
    public InteractableInfoUI ui;

    private InspectableObject currentTarget;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * scanRange, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, scanRange, scanMask))
        {
            var target = hit.collider.GetComponent<InspectableObject>();
            if (target != null)
            {
                //  �������̸� itemData�� ���� ���� ���
                if (target.TryGetComponent(out ItemPickup pickup) && pickup.itemData != null)
                {
                    var item = pickup.itemData;
                    ui.ShowInfo(item.itemName, item.inspectDescription);
                }
                else
                {
                    // �Ϲ� InspectableObject��
                    ui.ShowInfo(target.GetName(), target.GetInspecDescription());
                }

                return;
            }
        }

        // Ray�� �ƹ��͵� �� �¾��� ���� Hide (1���� �����)
        if (currentTarget != null)
        {
            currentTarget = null;
            ui.HideInfo();
        }
    }
}
