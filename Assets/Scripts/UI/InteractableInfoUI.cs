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
            Debug.LogWarning("infoText 연결 안 됨!");
            return;
        }

        infoText.text = $"{name}\n{desc}";
        Debug.Log($"[ShowInfo] {infoText.text} (duration: {duration})");

        // 기존 코루틴이 실행 중이면 중단
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        // duration이 0보다 크면 자동 숨김 예약
        if (duration > 0f)
        {
            hideCoroutine = StartCoroutine(AutoHide(duration));
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
