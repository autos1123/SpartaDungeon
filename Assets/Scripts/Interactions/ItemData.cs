using UnityEngine;

[CreateAssetMenu(menuName = "SpartaDungeon/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;

    [Header("���� �� ����")]
    [TextArea] public string inspectDescription;

    [Header("ȹ�� �� ����")]
    [TextArea] public string pickupMessage;

    public ItemEffectType effectType;
    public float effectValue;
    public float duration;
}
