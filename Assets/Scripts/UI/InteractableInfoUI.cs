using TMPro;
using UnityEngine;
using System.Collections;

public class InteractableInfoUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private Coroutine hideCoroutine;

    public void ShowInfo(string name, string desc, float duration = 0f)
    {
        if (infoText == null)
        {
            Debug.LogWarning("infoText ���� �� ��!");
            return;
        }

        infoText.text = $"{name}\n{desc}";
        Debug.Log($"[ShowInfo] {infoText.text} (duration: {duration})");

        // ���� �ڷ�ƾ�� ���� ���̸� �ߴ�
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        // duration�� 0���� ũ�� �ڵ� ���� ����
        if (duration > 0f)
        {
            hideCoroutine = StartCoroutine(AutoHide(duration));
        }
    }

    public void HideInfo()
    {
        Debug.Log("[HideInfo] �ؽ�Ʈ ����");
        infoText.text = "";
    }

    private IEnumerator AutoHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideInfo();
        hideCoroutine = null; // ���� �� �ڷ�ƾ ���� ����
    }
}
