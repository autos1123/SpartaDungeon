using TMPro;
using UnityEngine;

/// <summary>
/// �ؽ�Ʈ UI�� ���� ��� ������ ǥ���ϴ� ��ũ��Ʈ
/// </summary>
public class InteractableInfoUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    /// <summary>
    /// �ؽ�Ʈ ǥ��
    /// </summary>
    public void ShowInfo(string objectName, string description)
    {
        infoText.text = $"<b>{objectName}</b>\n{description}";
    }

    /// <summary>
    /// �ؽ�Ʈ ����
    /// </summary>
    public void HideInfo()
    {
        infoText.text = "";
    }
}
