using UnityEngine;

/// <summary>
/// 조사 가능한 오브젝트에 부착되는 정보 컴포넌트
/// </summary>
public class InspectableObject : MonoBehaviour
{
    public string objectName = "???";
    [TextArea] public string description = "설명이 없습니다.";
}
