using UnityEngine;

/// <summary>
/// �� ������Ʈ�� ItemData ScriptableObject�� ������ ������ �����մϴ�.
/// </summary>
public class InspectableObject : MonoBehaviour
{
    public ItemData data;

    public string GetName() => data?.itemName ?? "???";
    public string GetDescription() => data?.description ?? "������ �����ϴ�.";
}