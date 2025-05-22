using TMPro;
using UnityEngine;
using System.Collections;

public class InteractableInfoUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private Coroutine hideCoroutine;

    public void ShowInfo(string name, string desc, float duration = 0f)
    {
        infoText.text = $"{name}\n{desc}";

        if (duration > 0f)
        {
            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);

            hideCoroutine = StartCoroutine(AutoHide(duration));
        }
    }

    public void HideInfo()
    {
        infoText.text = "";
    }

    private IEnumerator AutoHide(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideInfo();
    }
}
