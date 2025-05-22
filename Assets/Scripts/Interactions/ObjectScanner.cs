using UnityEngine;

public class ObjectScanner : MonoBehaviour
{
    public float scanRange = 5f;  // 조사 거리
    public LayerMask scanMask;   // 조사 대상 레이어
    public Camera playerCamera;
    public InteractableInfoUI ui;

    void Update()
    {
        // 카메라 중심에서 전방으로 Raycast
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, scanRange, scanMask))
        {
            var target = hit.collider.GetComponent<InspectableObject>();
            if (target != null && target.data != null)
            {
                ui.ShowInfo(target.GetName(), target.GetDescription());
                return;
            }
        }

        // 아무것도 없으면 숨김
        ui.HideInfo();
    }
}
