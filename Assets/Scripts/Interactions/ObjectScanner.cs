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

        if (Physics.Raycast(ray, out RaycastHit hit, scanRange, scanMask))
        {
            var target = hit.collider.GetComponent<InspectableObject>();

            if (target != null && target.data != null)
            {
                if (currentTarget != target)
                {
                    currentTarget = target;
                    ui.ShowInfo(target.GetName(), target.GetDescription());
                }
                return;
            }
        }

        // Ray가 아무것도 안 맞았을 때만 Hide (1번만 실행됨)
        if (currentTarget != null)
        {
            currentTarget = null;
            ui.HideInfo();
        }
    }
}
