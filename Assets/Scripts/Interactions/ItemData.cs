using UnityEngine;

[CreateAssetMenu(menuName = "SpartaDungeon/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;

    [Header("Α¶»η ½Γ Ό³Έν")]
    [TextArea] public string inspectDescription;

    [Header("ΘΉµζ ½Γ Ό³Έν")]
    [TextArea] public string pickupMessage;

    public ItemEffectType effectType;
    public float effectValue;
    public float duration;
}
