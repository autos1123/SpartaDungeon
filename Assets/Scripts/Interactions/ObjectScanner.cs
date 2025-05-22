using UnityEngine;

public class ObjectScanner : MonoBehaviour
{
    public float scanRange = 5f;  // ���� �Ÿ�
    public LayerMask scanMask;   // ���� ��� ���̾�
    public Camera playerCamera;
    public InteractableInfoUI ui;

    void Update()
    {
        // ī�޶� �߽ɿ��� �������� Raycast
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

        // �ƹ��͵� ������ ����
        ui.HideInfo();
    }
}
