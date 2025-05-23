using UnityEngine;

public class ObjectScanner : MonoBehaviour
{
    public float scanRange = 5f;
    public LayerMask scanMask;
    public Camera playerCamera;
    public InteractableInfoUI ui;

    private IInteractable currentTarget;

    void Update()
    {
        Vector3 origin = playerCamera.transform.position;
        Vector3 forward = playerCamera.transform.forward;

        Vector3[] directions =
        {
        forward,
        Quaternion.Euler(0, -5, 0) * forward,
        Quaternion.Euler(0, 5, 0) * forward,
        Quaternion.Euler(-5, 0, 0) * forward,
        Quaternion.Euler(5, 0, 0) * forward
    };

        IInteractable foundTarget = null;

        foreach (var dir in directions)
        {
            Ray ray = new Ray(origin, dir);
            Debug.DrawRay(ray.origin, ray.direction * scanRange, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hit, scanRange, scanMask))
            {
                if (hit.collider.TryGetComponent(out InspectableObject inspectable))
                {
                    if (inspectable.TryGetComponent(out ItemPickup pickup) && pickup.itemData != null)
                    {
                        var item = pickup.itemData;
                        ui.ShowInfo(item.itemName, item.inspectDescription);
                    }
                    else
                    {
                        ui.ShowInfo(inspectable.GetName(), inspectable.GetInspecDescription());
                    }
                }

                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    foundTarget = interactable;
                    break;
                }
            }
        }

        if (foundTarget == null)
            ui.HideInfo();

        currentTarget = foundTarget;

        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.Interact(gameObject);
        }
    }

}
