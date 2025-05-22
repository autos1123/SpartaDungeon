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
            if (target != null)
            {
                ui.ShowInfo(target.objectName, target.description);
                return;
            }
        }

        // �ƹ��͵� ������ ����
        ui.HideInfo();
    }
}
