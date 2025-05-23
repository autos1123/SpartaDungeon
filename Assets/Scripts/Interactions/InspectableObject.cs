using UnityEngine;

/// <summary>
/// 이 오브젝트는 ItemData ScriptableObject를 참조해 정보를 전달합니다.
/// </summary>
public class InspectableObject : MonoBehaviour
{
    public ItemData data;

    public string GetName() => data?.itemName ?? "???";
    public string GetInspecDescription() => data?.inspectDescription ?? "설명이 없습니다.";
    public string GetPickupMessage() => data?.pickupMessage ?? "획득 설명이 없습니다.";
}