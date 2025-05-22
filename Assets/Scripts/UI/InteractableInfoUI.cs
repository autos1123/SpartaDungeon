using TMPro;
using UnityEngine;

/// <summary>
/// 텍스트 UI에 조사 대상 정보를 표시하는 스크립트
/// </summary>
public class InteractableInfoUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    /// <summary>
    /// 텍스트 표시
    /// </summary>
    public void ShowInfo(string objectName, string description)
    {
        infoText.text = $"<b>{objectName}</b>\n{description}";
    }

    /// <summary>
    /// 텍스트 숨김
    /// </summary>
    public void HideInfo()
    {
        infoText.text = "";
    }
}
