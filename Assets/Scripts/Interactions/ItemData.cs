using UnityEngine;

[CreateAssetMenu(menuName = "SpartaDungeon/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon; // 향후 UI에 이미지 넣고 싶을 때 사용

    public ItemEffectType effectType;
    public float effectValue;   // 회복량, 속도배수 등
    public float duration;      // 지속시간 (0이면 즉시 효과)
}
