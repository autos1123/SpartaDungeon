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
                throw new System.NullReferenceException("infoText가 할당되지 않았습니다.");

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
            Debug.LogError($"[InteractableInfoUI] 설명 출력 중 오류 발생: {ex.Message}");
        }
    }

    public void HideInfo()
    {
        Debug.Log("[HideInfo] 텍스트 숨김");
        infoText.text = "";
    }

    private IEnumerator AutoHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideInfo();
        hideCoroutine = null; // 종료 후 코루틴 참조 제거
    }
}
