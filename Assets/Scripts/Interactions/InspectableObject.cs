using UnityEngine;

/// <summary>
/// �� ������Ʈ�� ItemData ScriptableObject�� ������ ������ �����մϴ�.
/// </summary>
public class InspectableObject : MonoBehaviour
{
    public ItemData data;

    public string GetName() => data?.itemName ?? "???";
    public string GetInspecDescription() => data?.inspectDescription ?? "������ �����ϴ�.";
    public string GetPickupMessage() => data?.pickupMessage ?? "ȹ�� ������ �����ϴ�.";
}