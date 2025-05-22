using UnityEngine;

[CreateAssetMenu(menuName = "SpartaDungeon/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public Sprite icon; // ���� UI�� �̹��� �ְ� ���� �� ���
}
