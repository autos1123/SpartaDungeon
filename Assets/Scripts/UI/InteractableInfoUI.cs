using TMPro;
using UnityEngine;
using System.Collections;

public class InteractableInfoUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private Coroutine hideCoroutine;

    public void ShowInfo(string name, string desc, float duration = 0f)
    {
        try
        {
            if (infoText == null)
                throw new System.NullReferenceException("infoText�� �Ҵ���� �ʾҽ��ϴ�.");

            infoText.text = $"{name}\n{desc}";

            if (duration > 0f)
            {
                if (hideCoroutine != null)
                    StopCoroutine(hideCoroutine);

                hideCoroutine = StartCoroutine(AutoHide(duration));
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[InteractableInfoUI] ���� ��� �� ���� �߻�: {ex.Message}");
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
